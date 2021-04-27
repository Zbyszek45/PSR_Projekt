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

namespace ComparatorClient
{
    public partial class Form1 : Form
    {
        Socket s;
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
            logBox.AppendText("Waiting for data");
            Byte[] reveived = new byte[500];
            NetworkStream stream = new NetworkStream(s);
            stream.Read(reveived, 0, reveived.Length);
            if (reveived.Length > 0)
            {
                logBox.AppendText("data:");
                string tmp = null;
                tmp = System.Text.Encoding.UTF8.GetString(reveived);
                logBox.AppendText(tmp + "\n");
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
