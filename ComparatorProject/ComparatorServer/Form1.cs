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

        private int current_comp = 0;
        private FilesCompResult current_fcr = null;
        
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

            int calc_time = conn.get_calc_time();
            timeBox.Text = calc_time.ToString();
        }


        private void change_fcr()
        {
            if (res1ListView.Items.Count > 0 && res2ListView.Items.Count > 0)
            {
                current_comp = 0;

                string path1 = res1ListView.SelectedItems[0].SubItems[1].Text;
                string path2 = res2ListView.SelectedItems[0].SubItems[1].Text;
                string namef1 = conn.get_name_by_uniq(path1);
                string namef2 = conn.get_name_by_uniq(path2);

                current_fcr = get_file_result(namef1, namef2);
                Console.WriteLine("FCR: " + current_fcr);
                show_comp();
            }
        }

        private void show_comp()
        {
            int addit = 20;
            if (current_fcr != null)
            {
                string path1 = res1ListView.SelectedItems[0].SubItems[1].Text;
                string path2 = res2ListView.SelectedItems[0].SubItems[1].Text;
                string f1 = System.IO.File.ReadAllText(path1);
                string f2 = System.IO.File.ReadAllText(path2);
                FilePos fp1 = current_fcr.results[current_comp].f1;
                FilePos fp2 = current_fcr.results[current_comp].f2;
                
                int start1, start2, end1, end2;
                if (fp1.i > addit)
                {
                    start1 = (int)(fp1.i - addit);
                }
                else
                {
                    start1 = 0;
                }
                if (fp2.i > addit)
                {
                    start2 = (int)(fp2.i - addit);
                }
                else
                {
                    start2 = 0;
                }
                if (fp1.j + addit < f1.Length)
                {
                    end1 = (int)(fp1.j + addit);
                }
                else
                {
                    end1 = f1.Length;
                }
                if (fp2.j + addit < f2.Length)
                {
                    end2 = (int)(fp2.j + addit);
                }
                else
                {
                    end2 = f2.Length;
                }

                Res1Box.Text = f1.Substring(start1, end1-start1);
                Res2Box.Text = f2.Substring(start2, end2-start2);
            }
        }

        private FilesCompResult get_file_result(string namef1, string namef2)
        {
            FilesCompResult ret = null;
            foreach (FilesCompResult fcr in res)
            {
                if (String.Compare(fcr.f1Name, namef1) == 0 &&
                    String.Compare(fcr.f2Name, namef2) == 0)
                {
                    ret = fcr;
                }
            }
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.Compare(res1ListView.SelectedItems[0].SubItems[1].Text, res2ListView.SelectedItems[0].SubItems[1].Text) != 0){
                change_fcr(); 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (current_comp > 0)
            {
                current_comp--;
            }
            show_comp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (current_fcr != null)
            {
                if ((current_comp + 1) < current_fcr.results.Count)
                {
                    current_comp++;
                }
            }
            show_comp();
        }
    }
}
