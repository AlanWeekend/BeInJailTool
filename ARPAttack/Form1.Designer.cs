namespace ARPAttack
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.combDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGatewayMAC = new System.Windows.Forms.TextBox();
            this.txtGatewayIP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLocalMAC = new System.Windows.Forms.TextBox();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtRecipientMAC = new System.Windows.Forms.TextBox();
            this.txtRecipientIP = new System.Windows.Forms.TextBox();
            this.txtSenderMAC = new System.Windows.Forms.TextBox();
            this.txtSenderIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxCMD = new System.Windows.Forms.TextBox();
            this.richTextShell = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxCKnifePasswd = new System.Windows.Forms.TextBox();
            this.textCKnifeURL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // combDevice
            // 
            this.combDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDevice.FormattingEnabled = true;
            this.combDevice.Location = new System.Drawing.Point(54, 9);
            this.combDevice.Name = "combDevice";
            this.combDevice.Size = new System.Drawing.Size(729, 20);
            this.combDevice.TabIndex = 0;
            this.combDevice.SelectedIndexChanged += new System.EventHandler(this.combDevice_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设备";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGatewayMAC);
            this.groupBox2.Controls.Add(this.txtGatewayIP);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtLocalMAC);
            this.groupBox2.Controls.Add(this.txtLocalIP);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(8, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 81);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本地设备信息";
            // 
            // txtGatewayMAC
            // 
            this.txtGatewayMAC.Location = new System.Drawing.Point(291, 52);
            this.txtGatewayMAC.Name = "txtGatewayMAC";
            this.txtGatewayMAC.ReadOnly = true;
            this.txtGatewayMAC.Size = new System.Drawing.Size(138, 21);
            this.txtGatewayMAC.TabIndex = 7;
            // 
            // txtGatewayIP
            // 
            this.txtGatewayIP.Location = new System.Drawing.Point(291, 25);
            this.txtGatewayIP.Name = "txtGatewayIP";
            this.txtGatewayIP.ReadOnly = true;
            this.txtGatewayIP.Size = new System.Drawing.Size(138, 21);
            this.txtGatewayIP.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(220, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "网关MAC";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "网关IP";
            // 
            // txtLocalMAC
            // 
            this.txtLocalMAC.Location = new System.Drawing.Point(65, 52);
            this.txtLocalMAC.Name = "txtLocalMAC";
            this.txtLocalMAC.ReadOnly = true;
            this.txtLocalMAC.Size = new System.Drawing.Size(138, 21);
            this.txtLocalMAC.TabIndex = 3;
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(65, 25);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.ReadOnly = true;
            this.txtLocalIP.Size = new System.Drawing.Size(138, 21);
            this.txtLocalIP.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "本机MAC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "本机IP";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(803, 467);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTextBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(795, 441);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "中华人民共和国网络安全法";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(789, 435);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.combDevice);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(795, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "牢底坐穿ARP";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMask);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.txtRecipientMAC);
            this.groupBox1.Controls.Add(this.txtRecipientIP);
            this.groupBox1.Controls.Add(this.txtSenderMAC);
            this.groupBox1.Controls.Add(this.txtSenderIP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 303);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ARP";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(333, 259);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 10;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(117, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "send 0 time";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 254);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtRecipientMAC
            // 
            this.txtRecipientMAC.Location = new System.Drawing.Point(131, 196);
            this.txtRecipientMAC.Name = "txtRecipientMAC";
            this.txtRecipientMAC.ReadOnly = true;
            this.txtRecipientMAC.Size = new System.Drawing.Size(164, 21);
            this.txtRecipientMAC.TabIndex = 7;
            // 
            // txtRecipientIP
            // 
            this.txtRecipientIP.Location = new System.Drawing.Point(131, 145);
            this.txtRecipientIP.Name = "txtRecipientIP";
            this.txtRecipientIP.Size = new System.Drawing.Size(164, 21);
            this.txtRecipientIP.TabIndex = 6;
            // 
            // txtSenderMAC
            // 
            this.txtSenderMAC.Location = new System.Drawing.Point(131, 94);
            this.txtSenderMAC.Name = "txtSenderMAC";
            this.txtSenderMAC.ReadOnly = true;
            this.txtSenderMAC.Size = new System.Drawing.Size(164, 21);
            this.txtSenderMAC.TabIndex = 5;
            // 
            // txtSenderIP
            // 
            this.txtSenderIP.Location = new System.Drawing.Point(131, 36);
            this.txtSenderIP.Name = "txtSenderIP";
            this.txtSenderIP.ReadOnly = true;
            this.txtSenderIP.Size = new System.Drawing.Size(164, 21);
            this.txtSenderIP.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "目标MAC（自动检测）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "目标IP（手动填写）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "源MAC（随机生成）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "源IP（网关IP）";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(795, 441);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "牢底坐穿TCP";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 29);
            this.label11.TabIndex = 0;
            this.label11.Text = "TODO";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxCMD);
            this.tabPage3.Controls.Add(this.richTextShell);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.textBoxCKnifePasswd);
            this.tabPage3.Controls.Add(this.textCKnifeURL);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(795, 441);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "牢底坐穿刀";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxCMD
            // 
            this.textBoxCMD.Location = new System.Drawing.Point(3, 415);
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Size = new System.Drawing.Size(777, 21);
            this.textBoxCMD.TabIndex = 6;
            this.textBoxCMD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCMD_KeyUp);
            // 
            // richTextShell
            // 
            this.richTextShell.Location = new System.Drawing.Point(3, 34);
            this.richTextShell.Name = "richTextShell";
            this.richTextShell.ReadOnly = true;
            this.richTextShell.Size = new System.Drawing.Size(777, 375);
            this.richTextShell.TabIndex = 5;
            this.richTextShell.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(705, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxCKnifePasswd
            // 
            this.textBoxCKnifePasswd.Location = new System.Drawing.Point(592, 6);
            this.textBoxCKnifePasswd.Name = "textBoxCKnifePasswd";
            this.textBoxCKnifePasswd.Size = new System.Drawing.Size(107, 21);
            this.textBoxCKnifePasswd.TabIndex = 3;
            this.textBoxCKnifePasswd.Text = "abcd";
            // 
            // textCKnifeURL
            // 
            this.textCKnifeURL.Location = new System.Drawing.Point(3, 6);
            this.textCKnifeURL.Name = "textCKnifeURL";
            this.textCKnifeURL.Size = new System.Drawing.Size(582, 21);
            this.textCKnifeURL.TabIndex = 2;
            this.textCKnifeURL.Text = "http://127.0.0.1/test.php";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 29);
            this.label12.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(333, 51);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(415, 196);
            this.listBox1.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(331, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "子网掩码";
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(390, 23);
            this.txtMask.Name = "txtMask";
            this.txtMask.ReadOnly = true;
            this.txtMask.Size = new System.Drawing.Size(179, 21);
            this.txtMask.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 490);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "牢底坐穿工具箱 by H2O";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLocalIP;
        private System.Windows.Forms.TextBox txtLocalMAC;
        private System.Windows.Forms.TextBox txtGatewayMAC;
        private System.Windows.Forms.TextBox txtGatewayIP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtRecipientMAC;
        private System.Windows.Forms.TextBox txtRecipientIP;
        private System.Windows.Forms.TextBox txtSenderMAC;
        private System.Windows.Forms.TextBox txtSenderIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxCKnifePasswd;
        private System.Windows.Forms.TextBox textCKnifeURL;
        private System.Windows.Forms.RichTextBox richTextShell;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxCMD;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMask;
    }
}

