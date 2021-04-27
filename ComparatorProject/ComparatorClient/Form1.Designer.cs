
namespace ComparatorClient
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
            this.portBox = new System.Windows.Forms.TextBox();
            this.adressBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.adresLabel = new System.Windows.Forms.Label();
            this.connectBttn = new System.Windows.Forms.Button();
            this.disconnectBttn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(68, 41);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(136, 22);
            this.portBox.TabIndex = 7;
            this.portBox.Text = "2222";
            // 
            // adressBox
            // 
            this.adressBox.Location = new System.Drawing.Point(68, 12);
            this.adressBox.Name = "adressBox";
            this.adressBox.Size = new System.Drawing.Size(136, 22);
            this.adressBox.TabIndex = 6;
            this.adressBox.Text = "127.0.0.1";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 41);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(38, 17);
            this.portLabel.TabIndex = 5;
            this.portLabel.Text = "Port:";
            // 
            // adresLabel
            // 
            this.adresLabel.AutoSize = true;
            this.adresLabel.Location = new System.Drawing.Point(12, 12);
            this.adresLabel.Name = "adresLabel";
            this.adresLabel.Size = new System.Drawing.Size(49, 17);
            this.adresLabel.TabIndex = 4;
            this.adresLabel.Text = "Adres:";
            // 
            // connectBttn
            // 
            this.connectBttn.Location = new System.Drawing.Point(15, 75);
            this.connectBttn.Name = "connectBttn";
            this.connectBttn.Size = new System.Drawing.Size(75, 23);
            this.connectBttn.TabIndex = 8;
            this.connectBttn.Text = "Połącz";
            this.connectBttn.UseVisualStyleBackColor = true;
            this.connectBttn.Click += new System.EventHandler(this.connectBttn_Click);
            // 
            // disconnectBttn
            // 
            this.disconnectBttn.Enabled = false;
            this.disconnectBttn.Location = new System.Drawing.Point(107, 75);
            this.disconnectBttn.Name = "disconnectBttn";
            this.disconnectBttn.Size = new System.Drawing.Size(75, 23);
            this.disconnectBttn.TabIndex = 9;
            this.disconnectBttn.Text = "Rozlącz";
            this.disconnectBttn.UseVisualStyleBackColor = true;
            this.disconnectBttn.Click += new System.EventHandler(this.disconnectBttn_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 121);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(418, 381);
            this.logBox.TabIndex = 10;
            this.logBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 514);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.disconnectBttn);
            this.Controls.Add(this.connectBttn);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.adressBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.adresLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox adressBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label adresLabel;
        private System.Windows.Forms.Button connectBttn;
        private System.Windows.Forms.Button disconnectBttn;
        private System.Windows.Forms.RichTextBox logBox;
    }
}

