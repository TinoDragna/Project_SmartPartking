using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;

namespace SmartParking
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private int tcpPort = 30000;
        private string tcpIp;
        private int xeKhuA = 0;
        private int tongXeKhuA = 2;
        private int xeKhuB = 0;
        private int tongXeKhuB = 4;
        private System.Windows.Forms.Timer timer;
        private Dictionary<string, string> parkingTimes = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            InitializeParkingButtons();
            LoadParkingData();
            UpdateParkingStatus();
            StartClock();
        }

        private async void StartTCPListener()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(tcpIp), tcpPort);
                tcpListener.Start();

                Invoke((MethodInvoker)delegate {
                    lbStatus.Text = $"Listening on {tcpIp}:{tcpPort}";
                    lbStatus.ForeColor = Color.Green;
                });

                while (true)
                {
                    tcpClient = await tcpListener.AcceptTcpClientAsync();
                    networkStream = tcpClient.GetStream();

                    Invoke((MethodInvoker)delegate {
                        tbStatus.Text = "Client connected";
                    });

                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while (tcpClient.Connected)
                    {
                        bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;

                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Invoke((MethodInvoker)delegate {
                            HandleIncomingData(receivedData);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TCP Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleIncomingData(string receivedData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receivedData))
                    return;

                tbStatus.Text = $"Received: {receivedData}";
                ProcessParkingStatus(receivedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing data: " + ex.Message);
            }
        }

        private void ProcessParkingStatus(string receivedData)
        {
            string slot = receivedData.Replace("_IN", "").Replace("_OUT", "");
            bool isIn = receivedData.EndsWith("_IN");
            bool isOut = receivedData.EndsWith("_OUT");

            Button btn = FindButtonByTag(slot);

            if (btn != null)
            {
                if (isIn && btn.BackColor == Color.Transparent)
                {
                    ToggleParking(btn);
                }
                else if (isOut && btn.BackColor == Color.MediumSeaGreen)
                {
                    ToggleParking(btn);
                }
            }
        }

        private Button FindButtonByTag(string tag)
        {
            foreach (Control control in this.Controls)
                if (control is Button button && button.Tag?.ToString() == tag)
                    return button;
            return null;
        }

        private void InitializeParkingButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag != null)
                {
                    button.Click += ParkingButton_Click;
                    button.BackColor = Color.Transparent;
                }
            }
        }

        private void UpdateParkingStatus()
        {
            label3.Text = $"{xeKhuA}/{tongXeKhuA}";
            label5.Text = $"{xeKhuB}/{tongXeKhuB}";
        }

        private void StartClock()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, e) => label7.Text = DateTime.Now.ToString("HH:mm");
            timer.Start();
        }

        private void ParkingButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ToggleParking(button);
            }
        }

        private void ToggleParking(Button button)
        {
            string slotName = button.Tag.ToString();
            bool isKhuA = slotName.StartsWith("A");
            ref int xeCount = ref (isKhuA ? ref xeKhuA : ref xeKhuB);
            int maxXe = isKhuA ? tongXeKhuA : tongXeKhuB;

            if (button.BackColor == Color.Transparent && xeCount < maxXe)
            {
                xeCount++;
                button.BackColor = Color.MediumSeaGreen;
                button.Text = DateTime.Now.ToString("HH:mm");
                parkingTimes[slotName] = button.Text;
            }
            else if (button.BackColor == Color.MediumSeaGreen)
            {
                xeCount--;
                button.BackColor = Color.Transparent;
                button.Text = slotName;
                parkingTimes.Remove(slotName);
            }

            UpdateParkingStatus();
            SaveParkingData();
        }

        private void SaveParkingData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("parking_data.txt"))
                {
                    foreach (var entry in parkingTimes)
                    {
                        writer.WriteLine($"{entry.Key},{entry.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save error: {ex.Message}");
            }
        }

        private void LoadParkingData()
        {
            try
            {
                if (File.Exists("parking_data.txt"))
                {
                    parkingTimes.Clear();
                    foreach (string line in File.ReadAllLines("parking_data.txt"))
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            parkingTimes[parts[0]] = parts[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load error: {ex.Message}");
            }
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            SaveParkingData();
            networkStream?.Close();
            tcpClient?.Close();
            tcpListener?.Stop();
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            tcpIp = txtIpAddress.Text.Trim();
            string port = txtPort.Text.Trim();

            if (!IPAddress.TryParse(tcpIp, out _))
            {
                MessageBox.Show("Invalid IP address.");
                return;
            }

            if (!int.TryParse(port, out tcpPort) || tcpPort <= 0 || tcpPort > 65535)
            {
                MessageBox.Show("Invalid port.");
                return;
            }

            if (tcpListener != null)
            {
                MessageBox.Show("TCP Listener is already running.");
                return;
            }

            try
            {
                Task.Run(() => StartTCPListener());
                MessageBox.Show($"Listening on {tcpIp}:{tcpPort}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start error: " + ex.Message);
                lbStatus.Text = "Connection failed";
                lbStatus.ForeColor = Color.Red;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                networkStream?.Close();
                tcpClient?.Close();
                tcpListener?.Stop();
                tcpListener = null;

                MessageBox.Show("TCP Listener stopped.");
                lbStatus.Text = "Disconnected";
                lbStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnect error: " + ex.Message);
            }
        }

        private void SendTcpCommand(string command)
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected)
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(tcpIp, tcpPort);
                    networkStream = tcpClient.GetStream();
                }

                byte[] sendBytes = Encoding.ASCII.GetBytes(command + "\n");
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                tbStatus.Text = $"Sent: {command}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send error: " + ex.Message);
            }
        }

        private void btnOpenEntryGate_Click(object sender, EventArgs e)
        {
            SendTcpCommand("OPEN_ENTRY_GATE");
        }

        private void btnOpenExitGate_Click(object sender, EventArgs e)
        {
            SendTcpCommand("OPEN_EXIT_GATE");
        }
    }
}