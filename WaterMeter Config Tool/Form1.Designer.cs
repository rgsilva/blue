namespace WaterMeter_Config_Tool
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
            this.components = new System.ComponentModel.Container();
            this.cbSerialPort = new System.Windows.Forms.ComboBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.txLog = new System.Windows.Forms.TextBox();
            this.btRefreshPorts = new System.Windows.Forms.Button();
            this.lbPorts = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txFirmwareVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbWifiName = new System.Windows.Forms.ComboBox();
            this.txWifiPassword = new System.Windows.Forms.TextBox();
            this.btWifiTest = new System.Windows.Forms.Button();
            this.btSaveConfig = new System.Windows.Forms.Button();
            this.btWifiScan = new System.Windows.Forms.Button();
            this.tmSerialRead = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btDebug4 = new System.Windows.Forms.Button();
            this.btDebug3 = new System.Windows.Forms.Button();
            this.btDebug2 = new System.Windows.Forms.Button();
            this.btDebug1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSerialPort
            // 
            this.cbSerialPort.Location = new System.Drawing.Point(12, 25);
            this.cbSerialPort.Name = "cbSerialPort";
            this.cbSerialPort.Size = new System.Drawing.Size(173, 21);
            this.cbSerialPort.TabIndex = 0;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(12, 52);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(234, 23);
            this.btConnect.TabIndex = 1;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // txLog
            // 
            this.txLog.AcceptsReturn = true;
            this.txLog.AcceptsTab = true;
            this.txLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txLog.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txLog.Location = new System.Drawing.Point(252, 12);
            this.txLog.Multiline = true;
            this.txLog.Name = "txLog";
            this.txLog.ReadOnly = true;
            this.txLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txLog.Size = new System.Drawing.Size(327, 398);
            this.txLog.TabIndex = 2;
            this.txLog.WordWrap = false;
            // 
            // btRefreshPorts
            // 
            this.btRefreshPorts.Location = new System.Drawing.Point(191, 25);
            this.btRefreshPorts.Name = "btRefreshPorts";
            this.btRefreshPorts.Size = new System.Drawing.Size(55, 23);
            this.btRefreshPorts.TabIndex = 3;
            this.btRefreshPorts.Text = "Refresh";
            this.btRefreshPorts.UseVisualStyleBackColor = true;
            this.btRefreshPorts.Click += new System.EventHandler(this.btRefreshPorts_Click);
            // 
            // lbPorts
            // 
            this.lbPorts.AutoSize = true;
            this.lbPorts.Location = new System.Drawing.Point(12, 9);
            this.lbPorts.Name = "lbPorts";
            this.lbPorts.Size = new System.Drawing.Size(57, 13);
            this.lbPorts.TabIndex = 4;
            this.lbPorts.Text = "Serial port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Firmware version";
            // 
            // txFirmwareVersion
            // 
            this.txFirmwareVersion.Location = new System.Drawing.Point(12, 110);
            this.txFirmwareVersion.Name = "txFirmwareVersion";
            this.txFirmwareVersion.ReadOnly = true;
            this.txFirmwareVersion.Size = new System.Drawing.Size(234, 20);
            this.txFirmwareVersion.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "WiFi configuration";
            // 
            // cbWifiName
            // 
            this.cbWifiName.FormattingEnabled = true;
            this.cbWifiName.Location = new System.Drawing.Point(12, 149);
            this.cbWifiName.Name = "cbWifiName";
            this.cbWifiName.Size = new System.Drawing.Size(234, 21);
            this.cbWifiName.TabIndex = 8;
            // 
            // txWifiPassword
            // 
            this.txWifiPassword.Location = new System.Drawing.Point(12, 176);
            this.txWifiPassword.Name = "txWifiPassword";
            this.txWifiPassword.Size = new System.Drawing.Size(234, 20);
            this.txWifiPassword.TabIndex = 9;
            // 
            // btWifiTest
            // 
            this.btWifiTest.Location = new System.Drawing.Point(137, 202);
            this.btWifiTest.Name = "btWifiTest";
            this.btWifiTest.Size = new System.Drawing.Size(109, 23);
            this.btWifiTest.TabIndex = 10;
            this.btWifiTest.Text = "Test";
            this.btWifiTest.UseVisualStyleBackColor = true;
            this.btWifiTest.Click += new System.EventHandler(this.btWifiTest_Click);
            // 
            // btSaveConfig
            // 
            this.btSaveConfig.Location = new System.Drawing.Point(12, 231);
            this.btSaveConfig.Name = "btSaveConfig";
            this.btSaveConfig.Size = new System.Drawing.Size(234, 23);
            this.btSaveConfig.TabIndex = 11;
            this.btSaveConfig.Text = "Save config";
            this.btSaveConfig.UseVisualStyleBackColor = true;
            this.btSaveConfig.Click += new System.EventHandler(this.btSaveConfig_Click);
            // 
            // btWifiScan
            // 
            this.btWifiScan.Location = new System.Drawing.Point(12, 202);
            this.btWifiScan.Name = "btWifiScan";
            this.btWifiScan.Size = new System.Drawing.Size(109, 23);
            this.btWifiScan.TabIndex = 12;
            this.btWifiScan.Text = "Scan";
            this.btWifiScan.UseVisualStyleBackColor = true;
            this.btWifiScan.Click += new System.EventHandler(this.btWifiScan_Click);
            // 
            // tmSerialRead
            // 
            this.tmSerialRead.Enabled = true;
            this.tmSerialRead.Tick += new System.EventHandler(this.tmSerialRead_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btDebug4);
            this.groupBox1.Controls.Add(this.btDebug3);
            this.groupBox1.Controls.Add(this.btDebug2);
            this.groupBox1.Controls.Add(this.btDebug1);
            this.groupBox1.Location = new System.Drawing.Point(12, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 135);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug tools";
            // 
            // btDebug4
            // 
            this.btDebug4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDebug4.Location = new System.Drawing.Point(6, 106);
            this.btDebug4.Name = "btDebug4";
            this.btDebug4.Size = new System.Drawing.Size(222, 23);
            this.btDebug4.TabIndex = 20;
            this.btDebug4.Text = "Send test data";
            this.btDebug4.UseVisualStyleBackColor = true;
            this.btDebug4.Click += new System.EventHandler(this.btDebug4_Click);
            // 
            // btDebug3
            // 
            this.btDebug3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDebug3.Location = new System.Drawing.Point(6, 77);
            this.btDebug3.Name = "btDebug3";
            this.btDebug3.Size = new System.Drawing.Size(222, 23);
            this.btDebug3.TabIndex = 19;
            this.btDebug3.Text = "Clear EEPROM";
            this.btDebug3.UseVisualStyleBackColor = true;
            this.btDebug3.Click += new System.EventHandler(this.btDebug3_Click);
            // 
            // btDebug2
            // 
            this.btDebug2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDebug2.Location = new System.Drawing.Point(6, 48);
            this.btDebug2.Name = "btDebug2";
            this.btDebug2.Size = new System.Drawing.Size(222, 23);
            this.btDebug2.TabIndex = 18;
            this.btDebug2.Text = "Get WiFi status";
            this.btDebug2.UseVisualStyleBackColor = true;
            this.btDebug2.Click += new System.EventHandler(this.btDebug2_Click);
            // 
            // btDebug1
            // 
            this.btDebug1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDebug1.Location = new System.Drawing.Point(6, 19);
            this.btDebug1.Name = "btDebug1";
            this.btDebug1.Size = new System.Drawing.Size(222, 23);
            this.btDebug1.TabIndex = 17;
            this.btDebug1.Text = "Enable serial debug";
            this.btDebug1.UseVisualStyleBackColor = true;
            this.btDebug1.Click += new System.EventHandler(this.btDebug1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 422);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btWifiScan);
            this.Controls.Add(this.btSaveConfig);
            this.Controls.Add(this.btWifiTest);
            this.Controls.Add(this.txWifiPassword);
            this.Controls.Add(this.cbWifiName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txFirmwareVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPorts);
            this.Controls.Add(this.btRefreshPorts);
            this.Controls.Add(this.txLog);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.cbSerialPort);
            this.Name = "Form1";
            this.Text = "WaterMeter Config Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSerialPort;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox txLog;
        private System.Windows.Forms.Button btRefreshPorts;
        private System.Windows.Forms.Label lbPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txFirmwareVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbWifiName;
        private System.Windows.Forms.TextBox txWifiPassword;
        private System.Windows.Forms.Button btWifiTest;
        private System.Windows.Forms.Button btSaveConfig;
        private System.Windows.Forms.Button btWifiScan;
        private System.Windows.Forms.Timer tmSerialRead;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btDebug4;
        private System.Windows.Forms.Button btDebug3;
        private System.Windows.Forms.Button btDebug2;
        private System.Windows.Forms.Button btDebug1;
    }
}

