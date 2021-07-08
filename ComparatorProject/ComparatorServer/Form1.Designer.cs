
namespace ComparatorServer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.adresLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.adressBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.clientNumLabel = new System.Windows.Forms.Label();
            this.clientNum = new System.Windows.Forms.Label();
            this.filesLabel = new System.Windows.Forms.Label();
            this.chooseFileBttn = new System.Windows.Forms.Button();
            this.removeFileBttn = new System.Windows.Forms.Button();
            this.fileListView = new System.Windows.Forms.ListView();
            this.nameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pathCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startServerBttn = new System.Windows.Forms.Button();
            this.ziarnLabel = new System.Windows.Forms.Label();
            this.wzorzLabel = new System.Windows.Forms.Label();
            this.wzorzBox = new System.Windows.Forms.TextBox();
            this.ziarnBox = new System.Windows.Forms.TextBox();
            this.compareBttn = new System.Windows.Forms.Button();
            this.logMess = new System.Windows.Forms.RichTextBox();
            this.res1ListView = new System.Windows.Forms.ListView();
            this.nameColRes1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pathColRes1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.res2ListView = new System.Windows.Forms.ListView();
            this.nameColRes2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pathColRes2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Res1Box = new System.Windows.Forms.RichTextBox();
            this.Res2Box = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // adresLabel
            // 
            this.adresLabel.AutoSize = true;
            this.adresLabel.Location = new System.Drawing.Point(13, 13);
            this.adresLabel.Name = "adresLabel";
            this.adresLabel.Size = new System.Drawing.Size(49, 17);
            this.adresLabel.TabIndex = 0;
            this.adresLabel.Text = "Adres:";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(13, 42);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(38, 17);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port:";
            // 
            // adressBox
            // 
            this.adressBox.Location = new System.Drawing.Point(69, 13);
            this.adressBox.Name = "adressBox";
            this.adressBox.Size = new System.Drawing.Size(136, 22);
            this.adressBox.TabIndex = 2;
            this.adressBox.Text = "127.0.0.1";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(69, 42);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(136, 22);
            this.portBox.TabIndex = 3;
            this.portBox.Text = "2222";
            // 
            // clientNumLabel
            // 
            this.clientNumLabel.AutoSize = true;
            this.clientNumLabel.Location = new System.Drawing.Point(13, 74);
            this.clientNumLabel.Name = "clientNumLabel";
            this.clientNumLabel.Size = new System.Drawing.Size(94, 17);
            this.clientNumLabel.TabIndex = 4;
            this.clientNumLabel.Text = "Ilość klientów:";
            // 
            // clientNum
            // 
            this.clientNum.AutoSize = true;
            this.clientNum.Location = new System.Drawing.Point(113, 74);
            this.clientNum.Name = "clientNum";
            this.clientNum.Size = new System.Drawing.Size(16, 17);
            this.clientNum.TabIndex = 5;
            this.clientNum.Text = "0";
            // 
            // filesLabel
            // 
            this.filesLabel.AutoSize = true;
            this.filesLabel.Location = new System.Drawing.Point(14, 140);
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Size = new System.Drawing.Size(77, 17);
            this.filesLabel.TabIndex = 6;
            this.filesLabel.Text = "Wgraj pliki:";
            // 
            // chooseFileBttn
            // 
            this.chooseFileBttn.Location = new System.Drawing.Point(16, 160);
            this.chooseFileBttn.Name = "chooseFileBttn";
            this.chooseFileBttn.Size = new System.Drawing.Size(75, 23);
            this.chooseFileBttn.TabIndex = 7;
            this.chooseFileBttn.Text = "Choose..";
            this.chooseFileBttn.UseVisualStyleBackColor = true;
            this.chooseFileBttn.Click += new System.EventHandler(this.chooseFileBttn_Click);
            // 
            // removeFileBttn
            // 
            this.removeFileBttn.Location = new System.Drawing.Point(107, 160);
            this.removeFileBttn.Name = "removeFileBttn";
            this.removeFileBttn.Size = new System.Drawing.Size(75, 23);
            this.removeFileBttn.TabIndex = 8;
            this.removeFileBttn.Text = "Remove";
            this.removeFileBttn.UseVisualStyleBackColor = true;
            this.removeFileBttn.Click += new System.EventHandler(this.removeFileBttn_Click);
            // 
            // fileListView
            // 
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameCol,
            this.pathCol});
            this.fileListView.HideSelection = false;
            this.fileListView.Location = new System.Drawing.Point(16, 190);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(383, 135);
            this.fileListView.TabIndex = 9;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            // 
            // nameCol
            // 
            this.nameCol.Text = "Name";
            // 
            // pathCol
            // 
            this.pathCol.Text = "Path";
            this.pathCol.Width = 308;
            // 
            // startServerBttn
            // 
            this.startServerBttn.Location = new System.Drawing.Point(16, 101);
            this.startServerBttn.Name = "startServerBttn";
            this.startServerBttn.Size = new System.Drawing.Size(124, 23);
            this.startServerBttn.TabIndex = 10;
            this.startServerBttn.Text = "Start";
            this.startServerBttn.UseVisualStyleBackColor = true;
            this.startServerBttn.Click += new System.EventHandler(this.startServerBttn_Click);
            // 
            // ziarnLabel
            // 
            this.ziarnLabel.AutoSize = true;
            this.ziarnLabel.Location = new System.Drawing.Point(12, 334);
            this.ziarnLabel.Name = "ziarnLabel";
            this.ziarnLabel.Size = new System.Drawing.Size(113, 17);
            this.ziarnLabel.TabIndex = 11;
            this.ziarnLabel.Text = "Ziarnistość (KB):";
            // 
            // wzorzLabel
            // 
            this.wzorzLabel.AutoSize = true;
            this.wzorzLabel.Location = new System.Drawing.Point(14, 358);
            this.wzorzLabel.Name = "wzorzLabel";
            this.wzorzLabel.Size = new System.Drawing.Size(129, 17);
            this.wzorzLabel.TabIndex = 12;
            this.wzorzLabel.Text = "Minimalny wzorzec:";
            // 
            // wzorzBox
            // 
            this.wzorzBox.Location = new System.Drawing.Point(149, 359);
            this.wzorzBox.Name = "wzorzBox";
            this.wzorzBox.Size = new System.Drawing.Size(100, 22);
            this.wzorzBox.TabIndex = 13;
            this.wzorzBox.Text = "4";
            // 
            // ziarnBox
            // 
            this.ziarnBox.Location = new System.Drawing.Point(149, 331);
            this.ziarnBox.Name = "ziarnBox";
            this.ziarnBox.Size = new System.Drawing.Size(100, 22);
            this.ziarnBox.TabIndex = 14;
            this.ziarnBox.Text = "300";
            // 
            // compareBttn
            // 
            this.compareBttn.Location = new System.Drawing.Point(17, 387);
            this.compareBttn.Name = "compareBttn";
            this.compareBttn.Size = new System.Drawing.Size(232, 28);
            this.compareBttn.TabIndex = 15;
            this.compareBttn.Text = "Rozpocznij porównanie";
            this.compareBttn.UseVisualStyleBackColor = true;
            this.compareBttn.Click += new System.EventHandler(this.compareBttn_Click);
            // 
            // logMess
            // 
            this.logMess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logMess.Location = new System.Drawing.Point(12, 421);
            this.logMess.Name = "logMess";
            this.logMess.ReadOnly = true;
            this.logMess.Size = new System.Drawing.Size(387, 120);
            this.logMess.TabIndex = 16;
            this.logMess.Text = "";
            // 
            // res1ListView
            // 
            this.res1ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColRes1,
            this.pathColRes1});
            this.res1ListView.FullRowSelect = true;
            this.res1ListView.HideSelection = false;
            this.res1ListView.Location = new System.Drawing.Point(440, 42);
            this.res1ListView.MultiSelect = false;
            this.res1ListView.Name = "res1ListView";
            this.res1ListView.Size = new System.Drawing.Size(220, 99);
            this.res1ListView.TabIndex = 17;
            this.res1ListView.UseCompatibleStateImageBehavior = false;
            this.res1ListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColRes1
            // 
            this.nameColRes1.Text = "Name";
            // 
            // pathColRes1
            // 
            this.pathColRes1.Text = "Path";
            this.pathColRes1.Width = 152;
            // 
            // res2ListView
            // 
            this.res2ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColRes2,
            this.pathColRes2});
            this.res2ListView.FullRowSelect = true;
            this.res2ListView.HideSelection = false;
            this.res2ListView.Location = new System.Drawing.Point(747, 42);
            this.res2ListView.Name = "res2ListView";
            this.res2ListView.Size = new System.Drawing.Size(218, 97);
            this.res2ListView.TabIndex = 18;
            this.res2ListView.UseCompatibleStateImageBehavior = false;
            this.res2ListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColRes2
            // 
            this.nameColRes2.Text = "Name";
            // 
            // pathColRes2
            // 
            this.pathColRes2.Text = "Path";
            this.pathColRes2.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(440, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Wybierz pierwszy plik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(744, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Wybierz drugi plik";
            // 
            // Res1Box
            // 
            this.Res1Box.Location = new System.Drawing.Point(440, 160);
            this.Res1Box.Name = "Res1Box";
            this.Res1Box.Size = new System.Drawing.Size(220, 206);
            this.Res1Box.TabIndex = 21;
            this.Res1Box.Text = "";
            // 
            // Res2Box
            // 
            this.Res2Box.Location = new System.Drawing.Point(747, 160);
            this.Res2Box.Name = "Res2Box";
            this.Res2Box.Size = new System.Drawing.Size(220, 206);
            this.Res2Box.TabIndex = 22;
            this.Res2Box.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(666, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "Down";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(666, 160);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 25;
            this.button4.Text = "Up";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(667, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Zatwierdź";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(596, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Czas (s):";
            // 
            // timeBox
            // 
            this.timeBox.Location = new System.Drawing.Point(575, 434);
            this.timeBox.Name = "timeBox";
            this.timeBox.ReadOnly = true;
            this.timeBox.Size = new System.Drawing.Size(100, 22);
            this.timeBox.TabIndex = 29;
            this.timeBox.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.timeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Res2Box);
            this.Controls.Add(this.Res1Box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.res2ListView);
            this.Controls.Add(this.res1ListView);
            this.Controls.Add(this.logMess);
            this.Controls.Add(this.compareBttn);
            this.Controls.Add(this.ziarnBox);
            this.Controls.Add(this.wzorzBox);
            this.Controls.Add(this.wzorzLabel);
            this.Controls.Add(this.ziarnLabel);
            this.Controls.Add(this.startServerBttn);
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.removeFileBttn);
            this.Controls.Add(this.chooseFileBttn);
            this.Controls.Add(this.filesLabel);
            this.Controls.Add(this.clientNum);
            this.Controls.Add(this.clientNumLabel);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.adressBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.adresLabel);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "Form1";
            this.Text = "Komparator Serwer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label adresLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox adressBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label clientNumLabel;
        private System.Windows.Forms.Label clientNum;
        private System.Windows.Forms.Label filesLabel;
        private System.Windows.Forms.Button chooseFileBttn;
        private System.Windows.Forms.Button removeFileBttn;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader nameCol;
        private System.Windows.Forms.ColumnHeader pathCol;
        private System.Windows.Forms.Button startServerBttn;
        private System.Windows.Forms.Label ziarnLabel;
        private System.Windows.Forms.Label wzorzLabel;
        private System.Windows.Forms.TextBox wzorzBox;
        private System.Windows.Forms.TextBox ziarnBox;
        private System.Windows.Forms.Button compareBttn;
        private System.Windows.Forms.RichTextBox logMess;
        private System.Windows.Forms.ListView res1ListView;
        private System.Windows.Forms.ListView res2ListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader nameColRes1;
        private System.Windows.Forms.ColumnHeader pathColRes1;
        private System.Windows.Forms.ColumnHeader nameColRes2;
        private System.Windows.Forms.ColumnHeader pathColRes2;
        private System.Windows.Forms.RichTextBox Res1Box;
        private System.Windows.Forms.RichTextBox Res2Box;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeBox;
    }
}

