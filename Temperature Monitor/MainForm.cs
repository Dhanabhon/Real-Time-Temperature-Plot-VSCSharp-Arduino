/* Credit : SerialPort Terminal by http://coad.net */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;
using Temperature_Monitor.Properties;
using ZedGraph;

namespace Temperature_Monitor
{
    #region Public Enumerations
    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };

    #endregion

    public partial class MainForm : Form
    {
        #region Local Variables

        // The main control for communicating through the RS-232 port
        private SerialPort comport = new SerialPort();

        // Various colors for logging info
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };

        private Settings settings = Settings.Default;

        private GraphPane tempPane;
        private float pointWidth = 4f;
        private float lineWidth = 2f;
        private Color pointColor = Color.Red;
        private SymbolType symbolType = SymbolType.Circle;
        private Color pointColorSetPoint = Color.Blue;

        // Starting time in milliseconds
        private int tickStart = 0;
        private int countSecond = 0;
        private double tempData = 0.0;

        private bool plotGraph = false;

        #endregion

        #region Constructor
        public MainForm()
        {
            settings.Reload();

            InitializeComponent();

            // Restore the users settings
            InitializeControlValues();

            InitializeTemperatureGraph();

            // Enable/disable controls based on the current state
            EnableControls();

            this.comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        #endregion Constructor

        #region Methods
        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            cmbParity.Text = settings.Parity.ToString();
            cmbStopBits.Text = settings.StopBits.ToString();
            cmbDataBits.Text = settings.DataBits.ToString();
            cmbParity.Text = settings.Parity.ToString();
            cmbBaudRate.Text = settings.BaudRate.ToString();

            RefreshComPortList();

            //chkClearOnOpen.Checked = settings.ClearOnOpen;
            //chkClearWithDTR.Checked = settings.ClearWithDTR;

            // If it is still avalible, select the last com port used
            if (cmbPortName.Items.Contains(settings.PortName)) cmbPortName.Text = settings.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // Enable/disable controls based on the app's current state.
        private void EnableControls()
        {
            // Enable/disable controls based on whether the port is open or not
            gbPortSettings.Enabled = !comport.IsOpen;
            //txtSendData.Enabled = btnSend.Enabled = comport.IsOpen;
            //chkDTR.Enabled = chkRTS.Enabled = comport.IsOpen;

            if (comport.IsOpen)
            {
                btnOpenPort.Text = "&Close Port";
                ClearGraph();
                InitializeTemperatureGraph();
            }
            else
            {
                btnOpenPort.Text = "&Open Port";

                tmrDrawLine.Stop();
                countSecond = 0;
            }
        }

        /// <summary> Log data to the terminal window. </summary>
        /// <param name="msgtype"> The type of message to be written. </param>
        /// <param name="msg"> The string containing the message to be shown. </param>
        private void Log(LogMsgType msgtype, string msg)
        {
            rtfTerminal.Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
                rtfTerminal.SelectionFont = new Font(rtfTerminal.SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
        }

        private void ClearTerminal()
        {
            rtfTerminal.Clear();
        }

        private void SaveSettings()
        {
            settings.BaudRate = int.Parse(cmbBaudRate.Text);
            settings.DataBits = int.Parse(cmbDataBits.Text);
            settings.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
            settings.PortName = cmbPortName.Text;
            //settings.ClearOnOpen = chkClearOnOpen.Checked;
            //settings.ClearWithDTR = chkClearWithDTR.Checked;

            settings.Save();
        }

        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }

        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        private void InitializeTemperatureGraph()
        {
            tempPane = zgcTemperature.GraphPane;
            tempPane.Chart.Fill = new Fill(Color.AntiqueWhite, Color.Honeydew, -45F);
            tempPane.CurveList.Clear();
            tempPane.Title.Text = "Temperature Sensor";
            tempPane.XAxis.Title.Text = "Time (second)";
            tempPane.YAxis.Title.Text = "Temperature (°C)";
            tempPane.XAxis.MajorTic.IsOutside = false;
            tempPane.XAxis.MinorTic.IsOutside = false;
            tempPane.YAxis.MajorTic.IsOutside = false;
            tempPane.YAxis.MinorTic.IsOutside = false;
            tempPane.XAxis.MajorGrid.IsVisible = true;
            tempPane.YAxis.MajorGrid.IsVisible = true;
            tempPane.XAxis.MajorGrid.Color = Color.LightGray;
            tempPane.YAxis.MajorGrid.Color = Color.LightGray;
            //tempPane.XAxis.Scale.MajorStep = 1d;

            // Just manually control the X axis range so it scrolls continuously
            // instead of discrete step-sized jumps
            tempPane.XAxis.Scale.Min = 0;
            tempPane.XAxis.Scale.Max = 30;
            tempPane.XAxis.Scale.MinorStepAuto = true;
            tempPane.XAxis.Scale.MajorStepAuto = true;

            //tempPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            //tempPane.XAxis.Scale.MinorUnit = DateUnit.Second;
            //tempPane.XAxis.Scale.Format = "HH:mm:ss";
            //tempPane.XAxis.Type = AxisType.DateAsOrdinal;

            // Save 60000 points. At 50 ms sample rate. this is one minute
            // The RollingPointPairList is an efficint storage class the always
            // Keeps a rolling set of point data without needing to shif any data values
            RollingPointPairList list = new RollingPointPairList(60000);

            LineItem curve = tempPane.AddCurve("Temperature", list, pointColor, symbolType);
            curve.Symbol.Fill.Type = FillType.Solid;
            curve.Symbol.Size = pointWidth;
            curve.Line.Width = lineWidth;
            //curve.Line.IsSmooth = true;
            //curve.Line.SmoothTension = 0.3f;

            // Scale the axes
            zgcTemperature.AxisChange();

            // Save the beginning time for reference
            tickStart = Environment.TickCount;
        }

        private void DrawLineTempGraph(int n, double data)
        {
            double amountTime;
            double.TryParse(n.ToString(), out amountTime);

            if (zgcTemperature.GraphPane.CurveList.Count <= 0)
                return;

            LineItem curve = zgcTemperature.GraphPane.CurveList[0] as LineItem;

            if (curve == null)
                return;

            IPointListEdit list = curve.Points as IPointListEdit;

            if (list == null)
                return;

            list.Add(amountTime, data);

            curve.Line.Width = lineWidth;
            curve.Symbol.Fill.Type = FillType.Solid;
            curve.Symbol.Size = pointWidth;

            double time = (Environment.TickCount - tickStart) / 1000.0;

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value an the end of the axis
            Scale scale = zgcTemperature.GraphPane.XAxis.Scale;
            if (time > scale.Max - scale.MajorStep)
            {
                scale.Max = time + scale.MajorStep;
                scale.Min = scale.Max - 30.0;
            }

            int step = 0;

            if (n < 20)
                step = 1;
            else
                step = (int)(n / 20);

            const double offset = 0.05;

            // Draw Text Value
            for (int i = n - 1; i < n; i += step)
            {
                PointPair pt = curve.Points[i];
             
                TextObj text = new TextObj(pt.Y.ToString("f2"), pt.X, pt.Y + offset, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                text.ZOrder = ZOrder.A_InFront;

                text.FontSpec.Size = 10;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                text.FontSpec.Angle = 45;

                text.IsClippedToChartRect = true;

                tempPane.GraphObjList.Add(text);
            }


            //// Draw a box item to highlight a value range
            //BoxObj box = new BoxObj(0, 35, time*2, 10, Color.Empty, Color.FromArgb(150, Color.LightGreen));
            //box.Fill = new Fill(Color.White, Color.FromArgb(200, Color.LightGreen), 45.0F);
            //// Use the BehindGrid zorder to draw the highlight beneath the grid lines
            //box.ZOrder = ZOrder.F_BehindGrid;
            //tempPane.GraphObjList.Add(box); 

            zgcTemperature.AxisChange();
            zgcTemperature.Invalidate();
        }

        private void ClearGraph()
        {
            zgcTemperature.GraphPane.CurveList.Clear();
            zgcTemperature.GraphPane.GraphObjList.Clear();
            zgcTemperature.Refresh();
        }

        #endregion Methods

        #region Events

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com port has been closed, do nothing
            if (!comport.IsOpen) 
                return;

                // Read all the data waiting in the buffer
                string data = comport.ReadLine();

                this.tempData = Double.Parse(data);

                // Display the text to the user in the terminal
                Log(LogMsgType.Incoming, data);
        }

        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            // checks to see if COM ports have been added or removed
            // since it is quite common now with USB-to-Serial adapters
            RefreshComPortList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTerminal();
            countSecond = 0;

            ClearGraph();
            InitializeTemperatureGraph();
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            bool error = false;

            // If the port is open, close it.
            if (comport.IsOpen) 
                comport.Close();
            else
            {
                // Set the port's settings
                comport.BaudRate = int.Parse(cmbBaudRate.Text);
                comport.DataBits = int.Parse(cmbDataBits.Text);
                comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                comport.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                comport.PortName = cmbPortName.Text;

                try
                {
                    // Open the port
                    comport.Open();
                    // Start Graph Timer
                    tmrDrawLine.Start();
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) 
                    MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                
            }

            // Change the state of the form's controls
            EnableControls();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // The form is closing, save the user's preferences
            SaveSettings();
        }

        private void cmbBaudRate_Validating(object sender, CancelEventArgs e)
        {
            int x; e.Cancel = !int.TryParse(cmbBaudRate.Text, out x);
        }

        private void cmbDataBits_Validating(object sender, CancelEventArgs e)
        {
            int x; e.Cancel = !int.TryParse(cmbDataBits.Text, out x);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Log(LogMsgType.Normal, String.Format("Application Started at {0}\n", DateTime.Now));
        }

        private void tmrDrawLine_Tick(object sender, EventArgs e)
        {
            countSecond++;
            DrawLineTempGraph(countSecond, tempData);
        }

        #endregion Events   
        
    } // class
} // namespace
