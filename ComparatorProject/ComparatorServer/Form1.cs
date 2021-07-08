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
using CompStructures;

namespace ComparatorServer
{
    public partial class Form1 : Form
    {
        CompConnection conn;
        private List<FilesCompResult> res = new List<FilesCompResult>();
        
        public Form1()
        {
            InitializeComponent();
            conn = new CompConnection(logMess, fileListView, res);
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
            conn.startComparation(long.Parse(ziarnBox.Text), int.Parse(wzorzBox.Text));
            
            foreach (ListViewItem lvi in fileListView.Items)
            {
                ListViewItem l1 = (ListViewItem)lvi.Clone();
                ListViewItem l2 = (ListViewItem)lvi.Clone();
                res1ListView.Items.Add(l1);
                res2ListView.Items.Add(l2);
            }

            if (res1ListView.Items.Count > 0)
            {
                res1ListView.Items[0].Selected = true;
                res1ListView.Select();
            }
            if (res2ListView.Items.Count > 0)
            {
                res2ListView.Items[0].Selected = true;
                res2ListView.Select();
            }
        }

        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void res1ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
    }
}
