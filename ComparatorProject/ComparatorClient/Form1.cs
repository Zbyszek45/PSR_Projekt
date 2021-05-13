using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using CompStructures;
using System.IO;

namespace ComparatorClient
{
    public partial class Form1 : Form
    {
        int pattern = 0;
        List<String> files = new List<string>();
        Socket s;
        private BinaryFormatter bf = new BinaryFormatter();
        public Form1()
        {
            InitializeComponent();
        }

        private void connectBttn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(adressBox.Text))
            {
                MessageBox.Show("Adres jest pusty", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(portBox.Text))
            {
                MessageBox.Show("Adres jest pusty", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint epHost = new IPEndPoint(IPAddress.Parse(adressBox.Text), Int32.Parse(portBox.Text));
                s.Connect(epHost);
                if (s.Connected)
                {
                    adressBox.Enabled = false;
                    portBox.Enabled = false;
                    disconnectBttn.Enabled = true;
                    connectBttn.Enabled = false;
                    logBox.AppendText("Połączono\n");
                    Console.WriteLine("Połączono");
                    waitForData();
                }
            }
            catch (Exception ex)
            {
                logBox.AppendText(ex.ToString() + "\n");
                Console.WriteLine(ex.ToString());
            }
        }

        private void waitForData()
        {
            while (true)
            {
                try
                {
                    logBox.AppendText("Waiting for data: \n");
                    Console.WriteLine("Waiting for data: ");
                    NetworkStream stream = new NetworkStream(s);

                    // deserialize and receive FileHeader
                    FilesHeader fh = (FilesHeader)bf.Deserialize(stream);

                    // if pattern will be -1 it mean that client should no longer work
                    if (fh.patternLength == -1)
                    {
                        clean();
                        break;
                    }
                    pattern = fh.patternLength;

                    logBox.AppendText("Wzorzec: " + fh.patternLength.ToString() + "\n");
                    Console.WriteLine("Wzorzec: " + pattern.ToString());

                    // loop that start receive files from server
                    foreach (var name in fh.fileNames)
                    {
                        logBox.AppendText(name + "\n");
                        Console.WriteLine(name);

                        using (var output = File.Create(@"files\" + name))
                        {
                            files.Add(@"files\" + name);
                            Console.WriteLine("Client connected. Starting to receive the file");
                            var buffer = new byte[1024];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                                if (bytesRead < buffer.Length) break;
                            }

                            // sending confirmation for one exact file
                            String conf = "Client odebrał plik";
                            Byte[] confData = Encoding.UTF8.GetBytes(conf.ToCharArray());
                            s.Send(confData, conf.Length, 0);
                        }
                    }

                    // do comparisios
                    compareAllFiles();

                    // send result


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    logBox.AppendText(ex.ToString() + "\n");
                }
            }
        }

        private void disconnectBttn_Click(object sender, EventArgs e)
        {
            try
            {
                s.Disconnect(false);
                s.Close();
                adressBox.Enabled = true;
                portBox.Enabled = true;
                disconnectBttn.Enabled = false;
                connectBttn.Enabled = true;
            }
            catch (Exception ex)
            {
                logBox.AppendText(ex.ToString());
            }
        }

        private void clean()
        {
            files.Clear();

            System.IO.DirectoryInfo di = new DirectoryInfo("files");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void compareAllFiles()
        {
            for (int i = 0; i < files.Count() - 1; i++)
            {
                for (int j = i + 1; j < files.Count(); j++)
                {
                    Console.WriteLine("Comparing {0} with {1}:", files[i], files[j]);
                    compareTwoFiles(System.IO.File.ReadAllText(files[i]),
                        System.IO.File.ReadAllText(files[j]));
                }
            }
        }

        private void compareTwoFiles(string file1, string file2)
        {
            int count = 0;
            for (int i = 0; i < file2.Length; i++)
            {
                for (int j = 0; j < file1.Length; j++)
                {
                    count = 0;
                    while ((j + count) < file1.Length && (i + count) < file2.Length)
                    {

                        if (file2[i + count] == file1[j + count]
                            && (Char.IsLetterOrDigit(file2[i + count]) || Char.IsWhiteSpace(file2[i + count])))
                        {
                            count++;
                        }

                        else
                        {
                            break;
                        }
                    }
                    if (count > pattern)
                    {
                        Console.WriteLine("Znaleziono: {0}", i);
                        Console.WriteLine(file2.Substring(i, count));
                        i += count;
                        break;
                    }
                }
            }
        }
    }
}
