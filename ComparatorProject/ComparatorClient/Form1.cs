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
        Socket s;
        private BinaryFormatter bf = new BinaryFormatter();
        private ClientRespone cr = new ClientRespone();

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
                    // get header
                    Console.WriteLine("Waiting for data: ");
                    NetworkStream stream = new NetworkStream(s);
                    // deserialize and receive FileHeader
                    FilesHeader fh = (FilesHeader)bf.Deserialize(stream);
                    pattern = fh.patternLength;
                    //Console.WriteLine(fh);

                    // check if should stop by pattern == -1
                    if (fh.patternLength == -1)
                    {
                        Console.WriteLine("Should end");
                        clean();
                        break;
                    }

                    // in loop get the files to compare
                    foreach (FilePair fp in fh.pairs)
                    {
                        Console.WriteLine(fp.f1);
                        using (var output = File.Create(@"files\" + fp.f1))
                        {
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
                        Console.WriteLine(fp.f2);
                        using (var output = File.Create(@"files\" + fp.f2))
                        {
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

                    // start comparison
                    foreach (FilePair fp in fh.pairs)
                    {
                        compareTwoFiles(fp.f1, fp.f2);
                    }

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
            System.IO.DirectoryInfo di = new DirectoryInfo("files");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void compareTwoFiles(string f1, string f2)
        {
            FilesCompResult fcr = new FilesCompResult();
            fcr.f1Name = f1;
            fcr.f2Name = f2;
            fcr.results = new List<FileSingleResult>();
            string file1 = System.IO.File.ReadAllText(@"files\" + f1);
            string file2 = System.IO.File.ReadAllText(@"files\" + f2);
            
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
                        //Console.WriteLine("Znaleziono: {0}", i);
                        //Console.WriteLine(file2.Substring(i, count));
                        fcr.add(new FilePos(j, j+count), new FilePos(i, i+count));
                        i += count;
                        break;
                    }
                }
            }

            // tmp show result
            foreach (FileSingleResult fsr in fcr.results)
            {
                Console.WriteLine("#### RESULT ####");
                Console.WriteLine("In file: {0}", f1);
                foreach (FilePos fp in fsr.f1)
                {
                    Console.WriteLine("Indeks i,j: "+fp.i+","+fp.j+" - "+file1.Substring((int)fp.i, (int)(fp.j-fp.i)));
                }
                Console.WriteLine("In file: {0}", f2);
                foreach (FilePos fp in fsr.f2)
                {
                    Console.WriteLine("Indeks i,j: " + fp.i + "," + fp.j + " - " + file2.Substring((int)fp.i, (int)(fp.j - fp.i)));
                }
            }

            cr.res.Add(fcr);

        }
    }
}
