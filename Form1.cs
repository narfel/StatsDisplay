using LibreHardwareMonitor.Hardware;
using Microsoft.Win32.TaskScheduler;
using System.Globalization;
using System.IO.Ports;
using System.Text;

namespace StatsDisplay
{
    public partial class Form1 : Form
    {
        //display variables
        static float cpuUsage;
        static float gpuTemp;
        static float cpuTempAmd;
        static float memUsage;
        static float gpuMemUsage;
        static float gpuUsage;

        //flags
        static bool isPortOpen = false;
        static bool running = true;
        static bool DebugTabView;
        static bool TempTabView;
        static bool StartupRun;

        //serial variables
        static readonly object[] BaudRateSet = { 9600, 19200, 38400, 57600, 74880, 115200 };
        static int BaudRate;
        static string? SelectedPort;
        static string? CPU_temp;
        static SerialPort port = new();
        static string[] AvailablePortNames = GetPortName();

        Thread? serialThread = null;

        //librehardware
        static readonly LibreHardwareMonitor.Hardware.Computer Computer = new()
        {
            IsGpuEnabled = true,
            IsCpuEnabled = true,
            IsMemoryEnabled = true,
        };

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Method called to stop the thread displaying serial data.
        /// </summary>
        public static void Stop()
        {
            running = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeUI();

            PopulateDataListView();

            InitializeSerial();

            StartUpdateTimer();

            GetSettings();

            GetTabs();

            ToolStripStatusLabelPortText.Text = SelectedPort;
            UpdateBRStatusLabel();

            Computer.Open();
            GetSystemInfo(this);
            PopulateTreeView(this);

            Console.Write("Available ports: { ");
            foreach (string portName in AvailablePortNames)
            {
                Console.Write(portName + ", ");
            }
            Console.WriteLine("}");

            if (SelectedPort == "")
            {
                port.PortName = "COM1";
            }
            else
            {
                port.PortName = SelectedPort!;
            }

            if (AvailablePortNames.Contains(port.PortName))
            {
                Console.WriteLine("Configured Port is available");
            }
            else
            {
                Console.WriteLine("Configured Port is not available");
            }

            if (StartupRun)
            {
                running = true;
                RunningStatusLabel.Text = "Running";
                RunningStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
                serialThread = new Thread(() => Run(ResetStartButton, OutputLabel));
                serialThread.Start();
                OutputLabel.Text = "Sending data to display...";
                StartButton.Text = "Turn display off";
            }
        }

        /// <summary>
        ///  Set visibility of tabs based on settings
        /// </summary>
        private void GetTabs()
        {
            if (TempTabView)
            {
                if (!tabControl1.TabPages.Contains(TempTab))
                {
                    tabControl1.TabPages.Add(TempTab);
                }
            }
            else
            {
                tabControl1.TabPages.Remove(TempTab);
            }

            if (DebugTabView)
            {
                if (!tabControl1.TabPages.Contains(DebugTab))
                {
                    tabControl1.TabPages.Add(DebugTab);
                }
            }
            else
            {
                tabControl1.TabPages.Remove(DebugTab);
            }

            int aboutTabIndex = tabControl1.TabPages.Count - 1;
            if (tabControl1.TabPages[aboutTabIndex].Name == "About")
            {
                tabControl1.SelectedIndex = aboutTabIndex;
            }
        }

        /// <summary>
        /// Load settings and set values to preference
        /// </summary>
        private void GetSettings()
        {
            // initial debug output
            Console.SetOut(new ControlWriter(OutputBox));
            Console.WriteLine("Settings loaded: " + StatsSettings.Default.Port + ", " + Convert.ToString(StatsSettings.Default.Baud_rate) + ", Debug=" + StatsSettings.Default.Debug);
            PortComboBox.Text = StatsSettings.Default.Port;
            BaudrateComboBox.Text = Convert.ToString(StatsSettings.Default.Baud_rate);
            AutorunCheckBox.Checked = StatsSettings.Default.Startup_run;
            WinStartCheckBox.Checked = StatsSettings.Default.Win_startup;
            StartMinimizedCheckBox.Checked = StatsSettings.Default.Startup_minimized;

            StartupRun = StatsSettings.Default.Startup_run;
            SelectedPort = StatsSettings.Default.Port;
            BaudRate = StatsSettings.Default.Baud_rate;
            DebugTabView = StatsSettings.Default.Debug;
            TempTabView = StatsSettings.Default.TempSelect;
            CPU_temp = StatsSettings.Default.CPU_Temp;
        }

        /// <summary>
        /// Handle the timer that updates the thread getting system data
        /// </summary>
        private void StartUpdateTimer()
        {
            // update the variables every second
            System.Windows.Forms.Timer update_values_timer = new()
            {
                Interval = 1000
            };
            update_values_timer.Tick += (sender, e) =>
            {
                GetSystemInfo(this);
            };
            update_values_timer.Start();
        }

        /// <summary>
        /// Initialize the UI
        /// </summary>
        private void InitializeUI()
        {
            //initialize tray icon
            TrayIcon.BalloonTipTitle = "Stats Display";
            TrayIcon.BalloonTipText = "OK";
            TrayIcon.Text = "Stats Display";
            TrayIcon.ContextMenuStrip = contextMenuStrip1;
            TrayIcon.Visible = true;

            //populate menu items in context menu
            contextMenuStrip1.Items.Add("Open", null, MenuOpen_Click);
            contextMenuStrip1.Items.Add("Exit", null, MenuExit_Click);
            TrayIcon.ContextMenuStrip = contextMenuStrip1;
        }

        /// <summary>
        /// Setup the serial connection
        /// </summary>
        private void InitializeSerial()
        {
            // initialize serial ports and baudrate
            RunningStatusLabel.Text = "Idle";
            AvailablePortNames = GetPortName();
            PortComboBox.Items.AddRange(AvailablePortNames);
            BaudrateComboBox.Items.AddRange(BaudRateSet);
        }

        /// <summary>
        /// Populate the table showing the system stats that will be sent to the display
        /// </summary>
        private void PopulateDataListView()
        {
            // populate DataListView
            DataListView.Items.Add(new ListViewItem(new string[] { "CPU", cpuTempAmd.ToString(CultureInfo.InvariantCulture) + "º" }));
            DataListView.Items.Add(new ListViewItem(new string[] { "CPU usage", cpuUsage.ToString(CultureInfo.InvariantCulture) + "%" }));
            DataListView.Items.Add(new ListViewItem(new string[] { "Memory", memUsage.ToString(CultureInfo.InvariantCulture) + "%" }));
            DataListView.Items.Add(new ListViewItem(new string[] { "GPU", gpuTemp.ToString(CultureInfo.InvariantCulture) + "º" }));
            DataListView.Items.Add(new ListViewItem(new string[] { "GPU memory", gpuMemUsage.ToString(CultureInfo.InvariantCulture) + "%" }));
            DataListView.Items.Add(new ListViewItem(new string[] { "GPU usage", gpuUsage.ToString(CultureInfo.InvariantCulture) + "%" }));
            DataListView.Columns[0].Width = -2;
            DataListView.Columns[1].Width = -2;
        }

        /// <summary>
        /// Main run method controlling the serial output
        /// </summary>
        /// <param name="callback">Method that will be called on complete</param>
        /// <param name="OutputLabel">Label control that displays errors/status</param>
        static public void Run(System.Action callback, System.Windows.Forms.Label OutputLabel)
        {
            // main thread sending data to serial
            while (running)
            {
                if (port is not null)
                    try
                    {
                        port?.Open();
                    }
                    catch (Exception ex)
                    {
                        if (!port.IsOpen)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        else
                        {
                        }
                    }

                port ??= new SerialPort("COM1", BaudRate);
                if (AvailablePortNames.Contains(port.PortName))
                {
                    if (!isPortOpen)
                    {
                        port.Close();
                        port.PortName = SelectedPort ?? "No Port";
                        port.BaudRate = BaudRate;
                        Console.WriteLine("port.BaudRate (port open): " + port.BaudRate);
                        port.Open();
                        isPortOpen = true;
                    }

                    if (port != null && isPortOpen)
                    {
                        try
                        {
                            SendToSerial();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            port.Close();
                            port.PortName = SelectedPort ?? "No Port";
                            port.BaudRate = BaudRate;
                            Console.WriteLine("port.BaudRate (!null && open): " + port.BaudRate);
                            port.Open();
                            isPortOpen = false;
                        }
                    }
                }
                else
                {
                    OutputLabel.Text = port.PortName + " is not available. Please select a different COM port.";
                    Stop();
                }
                Thread.Sleep(1000);
            }
            callback();
        }

        /// <summary>
        /// Get the system data from librehardware dll
        /// </summary>
        /// <param name="form">Instance of the Windows Form</param>
        static void GetSystemInfo(Form1 form)
        {
            // gather system data from librehardware monitor
            foreach (var hardware in Computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("CPU Total"))
                        {
                            cpuUsage = (float)Math.Round((decimal)sensor.Value.GetValueOrDefault());
                        }
                        else if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains(CPU_temp!))
                        {
                            cpuTempAmd = (float)Math.Round((decimal)sensor.Value.GetValueOrDefault());
                        }
                    }
                }

                if (hardware.HardwareType == HardwareType.Memory)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("Memory"))
                        {
                            memUsage = (float)Math.Round((decimal)sensor.Value.GetValueOrDefault());
                        }
                    }
                }

                if (hardware.HardwareType == HardwareType.GpuAmd || hardware.HardwareType == HardwareType.GpuNvidia)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("GPU Core"))
                        {
                            gpuTemp = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Core"))
                        {
                            gpuUsage = sensor.Value.GetValueOrDefault();
                        }
                        else if (sensor.SensorType == SensorType.Load && sensor.Name.Contains("GPU Memory Controller"))
                        {
                            gpuMemUsage = sensor.Value.GetValueOrDefault();
                        }
                    }
                }
            }
            form.DataListView.Items[0].SubItems[1].Text = cpuTempAmd.ToString(CultureInfo.InvariantCulture) + "º";
            form.DataListView.Items[1].SubItems[1].Text = cpuUsage.ToString(CultureInfo.InvariantCulture) + "%";
            form.DataListView.Items[2].SubItems[1].Text = memUsage.ToString(CultureInfo.InvariantCulture) + "%";
            form.DataListView.Items[3].SubItems[1].Text = gpuTemp.ToString(CultureInfo.InvariantCulture) + "º";
            form.DataListView.Items[4].SubItems[1].Text = gpuMemUsage.ToString(CultureInfo.InvariantCulture) + "%";
            form.DataListView.Items[5].SubItems[1].Text = gpuUsage.ToString(CultureInfo.InvariantCulture) + "%";
        }

        /// <summary>
        /// Update the baud rate status label
        /// </summary>
        private void UpdateBRStatusLabel()
        {
            if (BaudRate == -1)
            {
                ToolStripStatusLabelBRText.Text = "";
            }
            else
            {
                ToolStripStatusLabelBRText.Text = Convert.ToString((double)BaudRate);
            }
        }

        /// <summary>
        /// Prepare data to be sent over serial and send
        /// </summary>
        static void SendToSerial()
        {
            // actual serial sending is done here
            String cpuTempText, cpuUsageText, gpuTempText, gpuUsageText, memUsageText, gpuMemUsageText;
            int CpuTempAMD = (int)Math.Round(cpuTempAmd);
            memUsageText = 1000 + memUsage + "";
            gpuMemUsageText = 1000 + gpuMemUsage + "";
            cpuTempText = 1000 + CpuTempAMD + "";
            cpuUsageText = 1000 + cpuUsage + "";
            gpuTempText = 1000 + gpuTemp + "";
            gpuUsageText = 1000 + gpuUsage + "";
            string data = cpuTempText + cpuUsageText + gpuTempText + gpuUsageText + memUsageText + gpuMemUsageText;

            // check if port is available
            if (port != null && port.IsOpen)
            {
                port.Write(data);
            }

            // format data for console output
            int consoleCpuTemp = int.Parse(data.Substring(1, 3));
            int consoleCpuUsage = int.Parse(data.Substring(5, 3));
            int consoleGpuTemp = int.Parse(data.Substring(9, 3));
            int consoleGpuUsage = int.Parse(data.Substring(13, 3));
            int consoleMemUsage = int.Parse(data.Substring(17, 3));
            int consoleGpuMemUsage = int.Parse(data.Substring(21, 3));
            Console.Write("CPU temp: {0}, mem: {1}, load: {2}, ", consoleCpuTemp, consoleMemUsage, consoleCpuUsage);
            Console.WriteLine("GPU temp: {0}, mem: {1}, load: {2}", consoleGpuTemp, consoleGpuMemUsage, consoleGpuUsage);
        }

        /// <summary>
        /// Populate the CPU temperature treeview to make it selectable for different cpu architectures
        /// </summary>
        /// <param name="form">Instance of the Windows Form</param>
        static void PopulateTreeView(Form1 form)
        {
            // fill a treeview with hardware temp data as discovered by librehardware monitor
            form.TreeView.Nodes.Clear();
            foreach (var hardware in Computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    hardware.Update();
                    var hardwareNode = form.TreeView.Nodes.Add(hardware.HardwareType.ToString());
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            var sensorNode = hardwareNode.Nodes.Add(sensor.Name);
                            sensorNode.Tag = sensor;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Display the value of the selected item as a tooltip
        /// </summary>
        /// <param name="sender">The treeview object that triggered the event</param>
        /// <param name="e">Event data containing the selected tree node</param>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is not null)
            {
                if (e.Node.Tag is ISensor sensor)
                {
                    TreeNode clickedNode = e.Node;
                    System.Windows.Forms.ToolTip toolTip = new()
                    {
                        AutoPopDelay = 5000,
                        InitialDelay = 100,
                        ReshowDelay = 500,
                        ShowAlways = true,
                        ToolTipTitle = "Value:"
                    };

                    toolTip.SetToolTip(e.Node.TreeView, sensor.Value.GetValueOrDefault().ToString());
                    if (CPU_temp != clickedNode.Text)
                    {
                        CPU_temp = clickedNode.Text;
                        StatsSettings.Default.CPU_Temp = CPU_temp;
                    }
                    else
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Get the available com ports on the system
        /// </summary>
        /// <returns></returns>
        static string[] GetPortName()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports;
        }

        /// <summary>
        /// Toggle sending data over serial via thread execution toggle
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (BaudrateComboBox.Text != "")
            {
                StatsSettings.Default.Baud_rate = Convert.ToInt32(BaudrateComboBox.Text);
            }
            if (PortComboBox.Text != "")
            {
                StatsSettings.Default.Port = PortComboBox.Text;
            }

            StatsSettings.Default.Save();
            SelectedPort = StatsSettings.Default.Port;
            BaudRate = StatsSettings.Default.Baud_rate;
            ToolStripStatusLabelBRText.Text = Convert.ToString((double)BaudRate);
            ToolStripStatusLabelPortText.Text = SelectedPort;

            lock (port)
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
                port.PortName = SelectedPort;
                if (BaudRate == -1)
                {
                    port.BaudRate = 215200;
                }
                else
                {
                    port.BaudRate = BaudRate;

                }
            }

            if (serialThread == null || !serialThread.IsAlive)
            {
                running = true;
                RunningStatusLabel.Text = "Running";
                RunningStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
                serialThread = new Thread(() => Run(ResetStartButton, OutputLabel));
                serialThread.Start();
                OutputLabel.Text = "Sending data to display...";
                StartButton.Text = "Turn display off";
            }
            else
            {
                running = false;
                RunningStatusLabel.Text = "Closing";
                OutputLabel.Text = "Closing serial connection...";
                serialThread.Join(3000);
                RunningStatusLabel.Text = "Idle";
                OutputLabel.Text = "";
                RunningStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
                StartButton.Text = "Turn display on";
            }
        }

        /// <summary>
        /// Reset the start button and inform the thread
        /// </summary>
        private void ResetStartButton()
        {
            if (InvokeRequired)
            {
                Invoke(new System.Action(ResetStartButton));
            }
            else
            {
                RunningStatusLabel.Text = "Idle";
                RunningStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
                StartButton.Text = "Turn display on";
            }
        }

        /// <summary>
        /// Refresh available ports in config
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PortComboBox.Items.Clear();
            PortComboBox.Items.AddRange(GetPortName());
        }

        /// <summary>
        /// Save all settings and stop the thread
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            Stop();

            if (BaudrateComboBox.Text != "")
            {
                StatsSettings.Default.Baud_rate = Convert.ToInt32(BaudrateComboBox.Text);
            }
            else
            {
                StatsSettings.Default.Baud_rate = 115200; // default
            }

            if (PortComboBox.Text != "")
            {
                StatsSettings.Default.Port = PortComboBox.Text;
            }
            else
            {
                StatsSettings.Default.Port = "COMX";
            }
            StatsSettings.Default.Win_startup = WinStartCheckBox.Checked;
            StatsSettings.Default.Startup_minimized = StartMinimizedCheckBox.Checked;
            StatsSettings.Default.Startup_run = AutorunCheckBox.Checked;
            Console.WriteLine("Saving settings: " + StatsSettings.Default.Baud_rate + ", " + StatsSettings.Default.Port + ", Autostart: " + StatsSettings.Default.Startup_run + ", ");
            Console.WriteLine("Win startup: " + StatsSettings.Default.Win_startup + ", Start minimized: " + StatsSettings.Default.Startup_minimized);

            StatsSettings.Default.Save();
            BaudRate = StatsSettings.Default.Baud_rate;
            SelectedPort = StatsSettings.Default.Port;
            UpdateBRStatusLabel();
            ToolStripStatusLabelPortText.Text = SelectedPort;
        }

        /// <summary>
        /// Clear config fields and set default values
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            Stop(); //stop the thread
            BaudrateComboBox.Text = "";
            PortComboBox.Text = "";
            ToolStripStatusLabelBRText.Text = "              ";
            ToolStripStatusLabelPortText.Text = "      ";
            StatsSettings.Default.Baud_rate = 115200; // that's actually hardcoded in the arduino file
            StatsSettings.Default.Port = "";
            StatsSettings.Default.Save();
        }

        /// <summary>
        /// Show output tab with debug info
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void DebugButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Contains(DebugTab))
            {
                tabControl1.TabPages.Remove(DebugTab);
                StatsSettings.Default.Debug = false;
            }
            else
            {
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, DebugTab);
                //tabControl1.TabPages.Add(DebugTab.Text, string.Format("{0}", tabControl1.TabPages.Count - 1));
                StatsSettings.Default.Debug = true;
            }
            StatsSettings.Default.Save();
        }

        /// <summary>
        /// Helper class displaying debug info in a textbox
        /// </summary>
        public class ControlWriter : TextWriter
        {

            private readonly System.Windows.Forms.TextBox control;

            public ControlWriter(System.Windows.Forms.TextBox control)
            {
                this.control = control;

                // context menu to clear debug info
                ContextMenuStrip contextMenu = new();
                ToolStripMenuItem clearMenuItem = new("Clear");
                clearMenuItem.Click += (sender, e) => control.Clear();
                contextMenu.Items.Add(clearMenuItem);

                control.ContextMenuStrip = contextMenu;
            }

            public override void Write(char value)
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new Action<char>(Write), value);
                }
                else
                {
                    control.AppendText(value.ToString());
                }
            }

            public override Encoding Encoding
            {
                get { return Encoding.Default; }
            }
        }

        /// <summary>
        /// Stop the thread and exit app
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        void MenuExit_Click(object? sender, EventArgs e)
        {
            try
            {
                // Attempt to join the thread with a timeout of 3s
                serialThread?.Join(3000);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MenuExit_Click: Exception - " + ex.Message);
            }
        }

        void MenuOpen_Click(object? sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// Double click on taskicon opens app window
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            this.Show();
        }

        /// <summary>
        /// Hide into tray instead of exiting app when closing window
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // minimize to tray when app is closed
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    break;
                case CloseReason.WindowsShutDown:
                    break;
                case CloseReason.TaskManagerClosing:
                    break;

            }
        }

        /// <summary>
        /// Controls the Windows startup behaviour
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void WinStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WinStartCheckBox.Checked)
            {
                TaskService ts = new();

                // create a new task
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Run StatsDisplay at startup";
                td.Principal.RunLevel = TaskRunLevel.Highest; // run as admin 
                td.Principal.LogonType = TaskLogonType.InteractiveToken; // run only when the user is logged on

                // create a 30s trigger
                LogonTrigger trigger = new()
                {
                    Delay = TimeSpan.FromSeconds(3)
                };
                td.Triggers.Add(trigger);

                //get app location
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
                string programPath = System.IO.Path.Combine(appPath, "StatsDisplay.exe");

                // create action when the trigger fires
                td.Actions.Add(new ExecAction(programPath, null, null));

                // register task in the root folder
                ts.RootFolder.RegisterTaskDefinition("StatsDisplayRunner", td);

                StatsSettings.Default.Win_startup = WinStartCheckBox.Checked;
                StatsSettings.Default.Save();
            }
            else
            {
                // get the service on the local machine
                using TaskService ts = new();

                // Remove task
                ts.RootFolder.DeleteTask("StatsDisplayRunner");
                StatsSettings.Default.Win_startup = WinStartCheckBox.Checked;
                StatsSettings.Default.Save();
            }
        }

        /// <summary>
        /// Hide taskbar on start unless settings are different
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (StatsSettings.Default.Startup_minimized == true)
            {
                // Hide form from taskbar
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Show output tab with debug info
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Instance of EventArgs containing event data</param>
        private void TempSelectButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Contains(TempTab))
            {
                tabControl1.TabPages.Remove(TempTab);
                StatsSettings.Default.TempSelect = false;
            }
            else
            {
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, TempTab);
                StatsSettings.Default.TempSelect = true;
            }
            StatsSettings.Default.Save();
        }
    }
}