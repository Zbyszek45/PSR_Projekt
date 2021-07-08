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
        private TcpListener tcpLsn;

        Thread waitForClientsThread;
        List<Client> clients = new List<Client>();
        private bool shouldWait = true;
        private TimeManager timeM = new TimeManager();

        private List<FilesCompResult> res;

        public static RichTextBox log;
        public static ListView fileList;

        private BinaryFormatter bf = new BinaryFormatter();

        public CompConnection(RichTextBox l, ListView lw, List<FilesCompResult> res)
        {
            log = l;
            fileList = lw;
            this.res = res;
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

            int pattern;

            if (client.Connected && client != null)
            {
                List<FilePairRef> list;
                while (true)
                {
                    // use lock
                    lock (fileM)
                    {
                        list = fileM.getFilesToCompare();
                        pattern = fileM.get_pattern();
                    }
                    try
                    {
                        // send header, if stop then pattern = -1
                        FilesHeader newFH = new FilesHeader();
                        newFH.pairs = new List<FilePair>();
                        newFH.patternLength = pattern;

                        if (list == null)
                        {
                            Console.WriteLine("NULL Stop client");
                            newFH.patternLength = -1;
                            bf.Serialize(client.GetStream(), newFH);
                            return;
                        }
                        else
                        {
                            foreach (FilePairRef fp in list)
                            {
                                newFH.pairs.Add(new FilePair(fp.f1.name, fp.f2.name));
                            }
                            bf.Serialize(client.GetStream(), newFH);
                        }

                        // in loop send files
                        Socket socket = client.Client;
                        foreach (FilePairRef fp in list)
                        {
                            socket.SendFile(fp.f1.path);

                            // wait for confirmation for one exact file
                            Byte[] confData = new Byte[50];
                            int rcv = client.Client.Receive(confData, confData.Length, 0);

                            socket.SendFile(fp.f2.path);

                            // wait for confirmation for one exact file
                            confData = new Byte[50];
                            rcv = client.Client.Receive(confData, confData.Length, 0);
                        }

                        // wait for result
                        ClientRespone neew = (ClientRespone)bf.Deserialize(client.GetStream());
                        foreach (FilesCompResult fsr in neew.res)
                        {
                            lock (res)
                            {
                                res.Add(fsr);
                            }
                            Console.WriteLine("#### RESULT ####");
                            Console.WriteLine(fsr);
                        }
                        lock (timeM)
                        {
                            timeM.add(index, neew.time);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        internal int get_calc_time()
        {
            return timeM.get_best_time();
        }

        public void startComparation(long ziarn, int p)
        {
            fileM.create_file_list(fileList, ziarn, p);
            timeM.create_list(clients.Count());

            for (int i = 0; i < clients.Count(); i++)
            {
                clients[i].thread.Start(i);
            }

            for (int i = 0; i < clients.Count(); i++)
            {
                clients[i].thread.Join();
            }

            Console.WriteLine("############# ALL FINISHED ################");
        }

        public String get_name_by_uniq(String f)
        {
            return fileM.get_by_uniq(f);
        }

    }
}
