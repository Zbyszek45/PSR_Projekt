using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using CompStructures;

namespace ComparatorServer
{

    class Client
    {
        public Thread thread;
        public TcpClient tcpClient;
    }

    class CompConnection
    {
        private Encoding utf_8 = Encoding.UTF8;
        private TcpListener tcpLsn;
        Socket s;
        Thread waitForClientsThread;
        List<Client> clients = new List<Client>();
        private bool shouldWait = true;

        public static RichTextBox log;
        public static ListView fileList;

        private BinaryFormatter bf = new BinaryFormatter();

        public CompConnection(RichTextBox l, ListView lw)
        {
            log = l;
            fileList = lw;
        }

        public void startServer(string adress, int port)
        {
            try
            {
                shouldWait = true;
                tcpLsn = new TcpListener(IPAddress.Parse(adress), port);
                tcpLsn.Start();
                waitForClientsThread = new Thread(new ThreadStart(WaitForClients));
                waitForClientsThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.ToString());
            }
        }

        public void stopServer()
        {
            try
            {
                shouldWait = false;
                waitForClientsThread.Interrupt();
                tcpLsn.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e.ToString());
            }

        }

        private void WaitForClients()
        {
            while (shouldWait)
            {
                try
                {
                    Client client = new Client();
                    Console.WriteLine("Waiting for client..");
                    client.tcpClient = tcpLsn.AcceptTcpClientAbortable();
                    lock (clients)
                    {
                        client.thread = new Thread(new ParameterizedThreadStart(doCalculationsClientThread));
                        clients.Add(client);
                        log.AppendText("Dodano nowego klienta\n");
                        log.ScrollToCaret();
                    }
                }
                catch (ThreadInterruptedException)
                {
                    log.AppendText("Wątek oczekujący został przerwany przez Interupt\n");
                    log.ScrollToCaret();
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.ToString());
                }
            }
        }

        public void doCalculationsClientThread(object i)
        {
            int index = (int)i;
            TcpClient client = ((Client)clients[index]).tcpClient;
            if (client.Connected && client != null)
            {
                try
                {
                    FilesHeader newFH = new FilesHeader();
                    newFH.patternLength = 4;
                    newFH.fileNames = new List<string>();
                    foreach (ListViewItem x in fileList.Items)
                    {
                        newFH.fileNames.Add(x.Text);
                    }

                    bf.Serialize(client.GetStream(), newFH);

                    Socket socket = client.Client;
 
                    foreach (ListViewItem x in fileList.Items)
                    {
                        string path = x.SubItems[1].Text;
                        Console.WriteLine(path);
                        socket.SendFile(path);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void startComparation()
        {
            for (int i=0; i < clients.Count(); i++)
            {
                clients[i].thread.Start(i);
            }
        }
    }
}
