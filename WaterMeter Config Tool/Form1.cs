using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;

namespace WaterMeter_Config_Tool
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort = null;
        private bool isSerialReading = false;

        #region Water Meter code

        private void serialPortDataReceived(object sender, SerialDataReceivedEventArgs e) {
        }

        private string GetResponse()
        {
            this.isSerialReading = true;

            string reply = String.Empty;
            do
            {
                reply = this.serialPort.ReadLine();
                {
                    this.Log(reply);
                }
            } while (!reply.StartsWith("~"));

            this.isSerialReading = false;

            return reply.Substring(2);
        }

        private void Handshake()
        {
            this.serialPort.Write("?");

            this.txFirmwareVersion.Text = GetResponse();
        }

        private void ReadConfig()
        {
            this.serialPort.Write("R");

            string reply = this.GetResponse().Trim();

            string[] config = reply.Split(';');

            this.cbWifiName.Text = config[0];
            this.txWifiPassword.Text = config[1];
        }

        private void WriteConfig()
        {
            this.serialPort.Write("W");

            string reply = this.GetResponse().Trim();

            if (reply == "OK")
            {
                MessageBox.Show("Success!", "WaterMeter Config Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error!", "WaterMeter Config Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScanNetworks()
        {
            this.serialPort.Write("S");

            string reply = GetResponse().Trim();

            // Parse it.
            if (String.IsNullOrEmpty(reply))
            {
                MessageBox.Show("No networks found.");
            }
            else
            {
                // Removes the final ";" to avoid bugs.
                if (reply.EndsWith(";"))
                {
                    reply = reply.Substring(0, reply.Length - 1);
                }

                this.cbWifiName.Items.Clear();
                this.cbWifiName.Items.AddRange(reply.Split(';'));
            }
        }

        private void TestNetwork(string name, string password)
        {
            this.serialPort.Write(String.Format("T {0};{1}", name, password));

            string reply = GetResponse().Trim();

            if (reply.StartsWith("OK"))
            {
                string ip = reply.Split(' ')[1];

                // Sucess.
                MessageBox.Show(String.Format("Success! ({0})", ip), "WaterMeter Config Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Error.
                MessageBox.Show("Connection failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunDebugCode(int debugCode)
        {
            this.serialPort.Write(debugCode.ToString());

            GetResponse();
        }

        #endregion

        #region Private UI code

        private void Log(string str)
        {
            this.txLog.AppendText(str + "\r\n");
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
            this.cbSerialPort.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.btRefreshPorts_Click(this.btRefreshPorts, EventArgs.Empty);
        }

        private void btRefreshPorts_Click(object sender, EventArgs e)
        {
            this.cbSerialPort.Items.Clear();
            this.cbSerialPort.Items.AddRange(SerialPort.GetPortNames());
        }

        private void btConnect_Click(object sender, EventArgs ea)
        {
            try
            {
                this.serialPort = new SerialPort(this.cbSerialPort.Text, 115200);
                this.serialPort.NewLine = "\n";
                this.serialPort.DataReceived += serialPortDataReceived;

                this.serialPort.Open();
                this.Log("+ Port is open.");

                this.Handshake();

                this.ReadConfig();
            }
            catch (Exception e)
            {
                this.serialPort = null;
                this.Log("- Error: " + e.Message);
            }
        }

        private void btWifiScan_Click(object sender, EventArgs e)
        {
            this.ScanNetworks();
        }

        private void tmSerialRead_Tick(object sender, EventArgs e)
        {
            if (this.serialPort != null && !this.isSerialReading)
            {
                if (this.serialPort.BytesToRead > 0)
                {
                    this.Log(this.serialPort.ReadExisting());
                }
            }
        }

        private void btWifiTest_Click(object sender, EventArgs e)
        {
            this.TestNetwork(this.cbWifiName.Text, this.txWifiPassword.Text);
        }

        private void btDebug1_Click(object sender, EventArgs e)
        {
            this.RunDebugCode(1);
        }

        private void btDebug2_Click(object sender, EventArgs e)
        {
            this.RunDebugCode(2);
        }

        private void btDebug3_Click(object sender, EventArgs e)
        {
            this.RunDebugCode(3);
        }

        private void btDebug4_Click(object sender, EventArgs e)
        {
            this.RunDebugCode(4);
        }

        private void btSaveConfig_Click(object sender, EventArgs e)
        {
            this.WriteConfig();
        }
    }
}
