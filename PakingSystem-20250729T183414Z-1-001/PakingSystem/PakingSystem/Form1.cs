using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking
{
    public partial class Form1 : Form
    {
        // TCP Communication Components
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader reader;
        private StreamWriter writer;
        private readonly object tcpLock = new object();

        private int tcpPort;
        private string tcpIp;

        // Parking System State
        private int xeKhuA = 0;
        private int tongXeKhuA = 4;
        private int xeKhuB = 0;
        private int tongXeKhuB = 4;
        private System.Windows.Forms.Timer timer;
        private Dictionary<string, string> parkingTimes = new Dictionary<string, string>();

        private bool isEntryGateOpen = false;
        private bool isExitGateOpen = false;

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
                lock (tcpLock)
                {
                    tcpListener = new TcpListener(IPAddress.Any, tcpPort);
                    tcpListener.Start();
                }

                UpdateStatus($"Listening on port {tcpPort}", Color.Green);

                while (true)
                {
                    try
                    {
                        var client = await tcpListener.AcceptTcpClientAsync();

                        // Handle new connection (disconnect any existing one)
                        lock (tcpLock)
                        {
                            CleanupClient();
                            tcpClient = client;
                            networkStream = tcpClient.GetStream();
                            reader = new StreamReader(networkStream);
                            writer = new StreamWriter(networkStream) { AutoFlush = true };
                        }

                        UpdateStatus($"Client connected from {GetClientEndpoint()}", Color.Green);
                        _ = HandleClientAsync(); // Start client handling in background
                    }
                    catch (ObjectDisposedException)
                    {
                        // Listener was stopped
                        break;
                    }
                    catch (Exception ex)
                    {
                        UpdateStatus($"Connection error: {ex.Message}", Color.Red);
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Server error: {ex.Message}", Color.Red);
            }
            finally
            {
                CleanupAll();
                UpdateStatus("Server stopped", Color.Orange);
            }
        }

        private async Task HandleClientAsync()
        {
            try
            {
                while (true)
                {
                    string data;
                    try
                    {
                        // Add timeout for read operation
                        var readTask = reader.ReadLineAsync();
                        if (await Task.WhenAny(readTask, Task.Delay(5000)) != readTask)
                        {
                            throw new IOException("Read timeout");
                        }
                        data = await readTask;
                    }
                    catch (IOException ex)
                    {
                        // Connection lost
                        UpdateStatus($"Connection lost: {ex.Message}", Color.Red);
                        break;
                    }

                    if (data == null)
                    {
                        // Client gracefully disconnected
                        UpdateStatus("Client disconnected", Color.Orange);
                        break;
                    }

                    // Process received data
                    this.Invoke((MethodInvoker)(() =>
                    {
                        tbTCPMessage.Text = $"Received: {data}";

                        ProcessParkingStatus(data);
                    }));
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Client error: {ex.Message}", Color.Red);
            }
            finally
            {
                CleanupClient();
            }
        }

        private void CleanupClient()
        {
            try
            {
                writer?.Dispose();
                reader?.Dispose();
                networkStream?.Dispose();
                tcpClient?.Close();

                writer = null;
                reader = null;
                networkStream = null;
                tcpClient = null;
            }
            catch { /* Suppress cleanup errors */ }
        }

        private void CleanupAll()
        {
            lock (tcpLock)
            {
                CleanupClient();
                try
                {
                    tcpListener?.Stop();
                }
                catch { /* Suppress stop errors */ }
                tcpListener = null;
            }
        }

        private string GetClientEndpoint()
        {
            try
            {
                lock (tcpLock)
                {
                    return (tcpClient?.Client?.RemoteEndPoint as IPEndPoint)?.ToString() ?? "unknown";
                }
            }
            catch
            {
                return "unknown";
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            if (lbStatus.InvokeRequired)
            {
                lbStatus.Invoke((MethodInvoker)(() =>
                {
                    lbStatus.Text = message;
                    lbStatus.ForeColor = color;
                }));
            }
            else
            {
                lbStatus.Text = message;
                lbStatus.ForeColor = color;
            }
        }

        private void ProcessParkingStatus(string receivedData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receivedData)) return;

                string[] messages = receivedData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string message in messages)
                {
                    //slot
                    string slot = message.Replace("_IN", "").Replace("_OUT", "");
                    bool isIn = message.EndsWith("_IN");
                    bool isOut = message.EndsWith("_OUT");

                    Button btn = FindButtonByTag(slot);

                    if (btn != null)
                    {
                        if (isIn && btn.BackColor == Color.Transparent)
                        {
                            ToggleParking(btn, true);
                        }
                        else if (isOut && btn.BackColor == Color.MediumSeaGreen)
                        {
                            ToggleParking(btn, false);
                        }
                    }


                    //gate
                    // Add gate status handling
                    if (message == "ENTRY_OPEN")
                    {
                        isEntryGateOpen = true;
                        btnEntryGate.BackColor = Color.LightGreen;
                        btnEntryGate.Text = "Close Entry";
                        tbTCPMessage.Text = "OPEN_ENTRY_GATE";
                    }
                    else if (message == "ENTRY_CLOSE")
                    {
                        isEntryGateOpen = false;
                        btnEntryGate.BackColor = SystemColors.Control;
                        btnEntryGate.Text = "Open Entry";
                        tbTCPMessage.Text = "CLOSE_ENTRY_GATE";
                    }
                    else if (message == "EXIT_OPEN")
                    {
                        isExitGateOpen = true;
                        btnExitGate.BackColor = Color.LightGreen;
                        btnExitGate.Text = "Close Exit";
                        tbTCPMessage.Text = "OPEN_EXIT_GATE";
                    }
                    else if (message == "EXIT_CLOSE")
                    {
                        isExitGateOpen = false;
                        btnExitGate.BackColor = SystemColors.Control;
                        btnExitGate.Text = "Open Exit";
                        tbTCPMessage.Text = "CLOSE_EXIT_GATE";
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Processing error: {ex.Message}", Color.Red);
            }
        }

        private void ToggleParking(Button button, bool isParkingIn)
        {
            string slotName = button.Tag.ToString();
            bool isKhuA = slotName.StartsWith("A");
            ref int xeCount = ref (isKhuA ? ref xeKhuA : ref xeKhuB);
            int maxXe = isKhuA ? tongXeKhuA : tongXeKhuB;

            if (isParkingIn)
            {
                if (button.BackColor == Color.Transparent && xeCount < maxXe)
                {
                    xeCount++;
                    button.BackColor = Color.MediumSeaGreen;
                    button.Text = DateTime.Now.ToString("HH:mm");
                    parkingTimes[slotName] = button.Text;
                }
            }
            else
            {
                if (button.BackColor == Color.MediumSeaGreen)
                {
                    xeCount--;
                    button.BackColor = Color.Transparent;
                    button.Text = slotName;
                    parkingTimes.Remove(slotName);
                }
            }

            UpdateParkingStatus();
            SaveParkingData();
        }

        private void UpdateParkingStatus()
        {
            label3.Text = $"{xeKhuA}/{tongXeKhuA}";
            label5.Text = $"{xeKhuB}/{tongXeKhuB}";
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
                UpdateStatus($"Save error: {ex.Message}", Color.Red);
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
                UpdateStatus($"Load error: {ex.Message}", Color.Red);
            }
        }

        private void StartClock()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (sender, e) => label7.Text = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();
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

        private void ParkingButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ToggleParking(button, button.BackColor != Color.MediumSeaGreen);
            }
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
                lbStatus.Text = $"Listening on {tcpIp}:{tcpPort}";
                lbStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot start TCP listener: " + ex.Message);
                lbStatus.Text = "Connection failed";
                lbStatus.ForeColor = Color.Red;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            CleanupAll();
            UpdateStatus("Server stopped", Color.Orange);
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            SaveParkingData();
            CleanupAll();
            this.Close();
        }


        private void btnEntryGate_Click(object sender, EventArgs e)
        {
            if (isEntryGateOpen)
            {
                SendTcpCommand("CLOSE_ENTRY_GATE");
                tbTCPMessage.Text = "CLOSE_ENTRY_GATE";
                isEntryGateOpen = false;
                btnEntryGate.BackColor = SystemColors.Control;
            }
            else
            {
                SendTcpCommand("OPEN_ENTRY_GATE");
                tbTCPMessage.Text = "OPEN_ENTRY_GATE";
                isEntryGateOpen = true;
                btnEntryGate.BackColor = Color.LightGreen;
            }
        }

        private void btnExitGate_Click(object sender, EventArgs e)
        {
            if (isExitGateOpen)
            {
                SendTcpCommand("CLOSE_EXIT_GATE");
                tbTCPMessage.Text = "CLOSE_EXIT_GATE";
                isExitGateOpen = false;
                btnExitGate.BackColor = SystemColors.Control;
            }
            else
            {
                SendTcpCommand("OPEN_EXIT_GATE");
                tbTCPMessage.Text = "OPEN_EXIT_GATE";
                isExitGateOpen = true;
                btnExitGate.BackColor = Color.LightGreen;
            }
        }

        private void SendTcpCommand(string command)
        {
            try
            {
                lock (tcpLock)
                {
                    if (tcpClient == null || !tcpClient.Connected)
                    {
                        UpdateStatus("Cannot send - not connected", Color.Red);
                        return;
                    }

                    writer.WriteLine(command);
                    tbTCPMessage.Text = $"Sent: {command}";
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Send error: {ex.Message}", Color.Red);
                CleanupClient();
            }
        }
    }
}