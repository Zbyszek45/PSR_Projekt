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
                    waitForData();
                }
            }
            catch (Exception ex)
            {
                logBox.AppendText(ex.ToString() + "\n");
            }
        }

        private void waitForData()
        {
            try
            {
                logBox.AppendText("Waiting for data: \n");
                NetworkStream stream = new NetworkStream(s);
                FilesHeader fh = (FilesHeader)bf.Deserialize(stream);
                logBox.AppendText("Wzorzec" + fh.patternLength.ToString()+"\n");
                foreach (var name in fh.fileNames)
                {
                    logBox.AppendText(name+"\n");
                    //var output = File.Create(@"files\" + name);
                    using (var output = File.Create(@"files\" + name))
                    {
                        Console.WriteLine("Client connected. Starting to receive the file");
                        // read the file in chunks of 1KB
                        var buffer = new byte[16];
                        int bytesRead;
                        Console.WriteLine("start loop for");
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            Console.WriteLine("Still going..{0}", bytesRead);
                            output.Write(buffer, 0, bytesRead);
                            if (bytesRead < buffer.Length) break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                logBox.AppendText(ex.ToString() + "\n");
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
    }
}
