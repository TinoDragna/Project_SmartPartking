namespace SmartParking
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnShutDown = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.btnOpenExitGate = new System.Windows.Forms.Button();
            this.btnOpenEntryGate = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnShutDown);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(76, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 171);
            this.panel1.TabIndex = 0;
            // 
            // btnShutDown
            // 
            this.btnShutDown.Location = new System.Drawing.Point(335, 129);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(105, 37);
            this.btnShutDown.TabIndex = 7;
            this.btnShutDown.Text = "Shutdown";
            this.btnShutDown.UseVisualStyleBackColor = true;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(173, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(89, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(20, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Area B:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(89, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(20, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Area A:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Smart Parking";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 52);
            this.button1.TabIndex = 1;
            this.button1.Tag = "A1";
            this.button1.Text = "A1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(830, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 52);
            this.button2.TabIndex = 2;
            this.button2.Tag = "A2";
            this.button2.Text = "A2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 477);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 52);
            this.button3.TabIndex = 3;
            this.button3.Tag = "B1";
            this.button3.Text = "B1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(357, 477);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 52);
            this.button4.TabIndex = 4;
            this.button4.Tag = "B2";
            this.button4.Text = "B2";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(608, 477);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 52);
            this.button5.TabIndex = 5;
            this.button5.Tag = "B3";
            this.button5.Text = "B3";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(830, 477);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 53);
            this.button6.TabIndex = 6;
            this.button6.Tag = "B4";
            this.button6.Text = "B4";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(357, 98);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(144, 26);
            this.txtPort.TabIndex = 17;
            this.txtPort.Text = "1025";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(357, 40);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(207, 26);
            this.txtIpAddress.TabIndex = 16;
            this.txtIpAddress.Text = "172.16.10.171";
            // 
            // btnOpenExitGate
            // 
            this.btnOpenExitGate.Location = new System.Drawing.Point(847, 104);
            this.btnOpenExitGate.Name = "btnOpenExitGate";
            this.btnOpenExitGate.Size = new System.Drawing.Size(148, 58);
            this.btnOpenExitGate.TabIndex = 15;
            this.btnOpenExitGate.Text = "Open Exit";
            this.btnOpenExitGate.UseVisualStyleBackColor = true;
            this.btnOpenExitGate.UseWaitCursor = true;
            this.btnOpenExitGate.Click += new System.EventHandler(this.btnOpenExitGate_Click);
            // 
            // btnOpenEntryGate
            // 
            this.btnOpenEntryGate.Location = new System.Drawing.Point(615, 104);
            this.btnOpenEntryGate.Name = "btnOpenEntryGate";
            this.btnOpenEntryGate.Size = new System.Drawing.Size(179, 58);
            this.btnOpenEntryGate.TabIndex = 14;
            this.btnOpenEntryGate.Text = "Open Entry";
            this.btnOpenEntryGate.UseVisualStyleBackColor = true;
            this.btnOpenEntryGate.Click += new System.EventHandler(this.btnOpenEntryGate_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(751, 39);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(109, 35);
            this.btnDisconnect.TabIndex = 13;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(615, 36);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 35);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(353, 155);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 20);
            this.lbStatus.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Conection Status";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(190, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "TCP Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(190, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Device IP add";
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(608, 244);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(333, 26);
            this.tbStatus.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(611, 209);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "UDP message:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 597);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.btnOpenExitGate);
            this.Controls.Add(this.btnOpenEntryGate);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Smart Parking System";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShutDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button btnOpenExitGate;
        private System.Windows.Forms.Button btnOpenEntryGate;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Label label11;
    }
}

