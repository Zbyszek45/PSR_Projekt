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

        public CompConnection(RichTextBox l)
        {
            log = l;
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
                string str = "Ala ma kota";
                Byte[] byteData = System.Text.Encoding.UTF8.GetBytes(str.ToCharArray());
                client.GetStream().Write(byteData, 0, byteData.Length);
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
