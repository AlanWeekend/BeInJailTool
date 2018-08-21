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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // combDevice
            // 
            this.combDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDevice.FormattingEnabled = true;
            this.combDevice.Location = new System.Drawing.Point(59, 17);
            this.combDevice.Name = "combDevice";
            this.combDevice.Size = new System.Drawing.Size(729, 20);
            this.combDevice.TabIndex = 0;
            this.combDevice.SelectedIndexChanged += new System.EventHandler(this.combDevice_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device";
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(13, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 379);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ARPAttack";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(8, 215);
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
            this.label6.Location = new System.Drawing.Point(105, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "the 0 time";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(8, 259);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtRecipientMAC
            // 
            this.txtRecipientMAC.Location = new System.Drawing.Point(107, 160);
            this.txtRecipientMAC.Name = "txtRecipientMAC";
            this.txtRecipientMAC.Size = new System.Drawing.Size(164, 21);
            this.txtRecipientMAC.TabIndex = 7;
            // 
            // txtRecipientIP
            // 
            this.txtRecipientIP.Location = new System.Drawing.Point(107, 117);
            this.txtRecipientIP.Name = "txtRecipientIP";
            this.txtRecipientIP.Size = new System.Drawing.Size(164, 21);
            this.txtRecipientIP.TabIndex = 6;
            // 
            // txtSenderMAC
            // 
            this.txtSenderMAC.Location = new System.Drawing.Point(107, 76);
            this.txtSenderMAC.Name = "txtSenderMAC";
            this.txtSenderMAC.Size = new System.Drawing.Size(164, 21);
            this.txtSenderMAC.TabIndex = 5;
            // 
            // txtSenderIP
            // 
            this.txtSenderIP.Location = new System.Drawing.Point(107, 36);
            this.txtSenderIP.Name = "txtSenderIP";
            this.txtSenderIP.Size = new System.Drawing.Size(164, 21);
            this.txtSenderIP.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Recipient MAC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Recipient IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sender MAC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sender IP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combDevice);
            this.Name = "Form1";
            this.Text = "ARPAttack by H2O";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRecipientMAC;
        private System.Windows.Forms.TextBox txtRecipientIP;
        private System.Windows.Forms.TextBox txtSenderMAC;
        private System.Windows.Forms.TextBox txtSenderIP;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnScan;
    }
}

