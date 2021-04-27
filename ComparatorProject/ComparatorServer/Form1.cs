using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ComparatorServer
{
    public partial class Form1 : Form
    {
        CompConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new CompConnection(logMess);
        }

        private void chooseFileBttn_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog() { Filter="Text files|*.txt", Multiselect = true })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string f in dialog.FileNames)
                    {
                        FileInfo fInfo = new FileInfo(f);
                        ListViewItem lvItem = new ListViewItem(fInfo.Name);
                        lvItem.SubItems.Add(fInfo.FullName);
                        fileListView.Items.Add(lvItem);
                    }
                }
            }
        }

        private void removeFileBttn_Click(object sender, EventArgs e)
        {
            if (fileListView.Items.Count > 0)
            {
                fileListView.Items.Remove(fileListView.SelectedItems[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startServerBttn_Click(object sender, EventArgs e)
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

            if (startServerBttn.Text == "Start")
            {
                conn.startServer(adressBox.Text, int.Parse(portBox.Text));
                startServerBttn.Text = "Stop";
                adressBox.Enabled = false;
                portBox.Enabled = false;
                logMess.AppendText("Serwer wystartowany... \n");
                logMess.ScrollToCaret();
            }
            else
            {
                conn.stopServer();
                startServerBttn.Text = "Start";
                adressBox.Enabled = true;
                portBox.Enabled = true;
                logMess.AppendText("Serwer zatrzymany... \n");
                logMess.ScrollToCaret();
            }
            
        }


        private void logMess_TextChanged(object sender, EventArgs e)
        {

        }

        private void compareBttn_Click(object sender, EventArgs e)
        {
            conn.startComparation();
        }
    }
}
