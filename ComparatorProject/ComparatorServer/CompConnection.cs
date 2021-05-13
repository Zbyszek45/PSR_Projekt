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
        private FileManager fileM = new FileManager();
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
                    //log.AppendText("Wątek oczekujący został przerwany przez Interupt\n");
                    //log.ScrollToCaret();
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
                List<FileComp> list;
                // use lock
                lock (fileM)
                {
                    list = fileM.getFilesToCompare(index);
                }
                try
                {
                    FilesHeader newFH = new FilesHeader();
                    newFH.patternLength = 4;
                    newFH.fileNames = new List<string>();
                    foreach (FileComp x in list)
                    {
                        newFH.fileNames.Add(x.name);
                    }

                    // serialize FileHeader and send to client
                    bf.Serialize(client.GetStream(), newFH);

                    Socket socket = client.Client;
                    // loop that start sending files to client
                    foreach (FileComp x in list)
                    {
                        lock (fileM)
                        {
                            if (fileM.checkIfClientHasFile(index, x))
                            {
                                continue;
                            }
                        }
                        string path = x.path;
                        Console.WriteLine(path);
                        socket.SendFile(path);

                        // wait for confirmation for one exact file
                        Byte[] confData = new Byte[50];
                        int rcv = client.Client.Receive(confData, confData.Length, 0);
                        if (rcv <= 0)
                        {
                            break;
                        }
                    }

                    // wait fo results

                    // temporary way to clean client
                    FilesHeader finishFH = new FilesHeader();
                    finishFH.patternLength = -1;
                    finishFH.fileNames = new List<string>();
                    bf.Serialize(client.GetStream(), finishFH);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }


        public void startComparation(int ziarn)
        {
            fileM.create_file_list(fileList, ziarn, clients.Count());


            for (int i = 0; i < clients.Count(); i++)
            {
                clients[i].thread.Start(i);
            }
        }
    }
}
