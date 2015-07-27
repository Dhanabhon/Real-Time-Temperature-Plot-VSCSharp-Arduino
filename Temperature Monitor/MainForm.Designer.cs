namespace Temperature_Monitor
{
    partial class MainForm
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
            this.gbPortSettings = new System.Windows.Forms.GroupBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.zgcTemperature = new ZedGraph.ZedGraphControl();
            this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
            this.tmrDrawLine = new System.Windows.Forms.Timer(this.components);
            this.gbPortSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPortSettings
            // 
            this.gbPortSettings.Controls.Add(this.cmbPortName);
            this.gbPortSettings.Controls.Add(this.cmbBaudRate);
            this.gbPortSettings.Controls.Add(this.cmbStopBits);
            this.gbPortSettings.Controls.Add(this.cmbParity);
            this.gbPortSettings.Controls.Add(this.cmbDataBits);
            this.gbPortSettings.Controls.Add(this.lblComPort);
            this.gbPortSettings.Controls.Add(this.lblStopBits);
            this.gbPortSettings.Controls.Add(this.lblBaudRate);
            this.gbPortSettings.Controls.Add(this.lblDataBits);
            this.gbPortSettings.Controls.Add(this.lblParity);
            this.gbPortSettings.Location = new System.Drawing.Point(12, 12);
            this.gbPortSettings.Name = "gbPortSettings";
            this.gbPortSettings.Size = new System.Drawing.Size(142, 226);
            this.gbPortSettings.TabIndex = 5;
            this.gbPortSettings.TabStop = false;
            this.gbPortSettings.Text = "COM Serial Port Settings";
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbPortName.Location = new System.Drawing.Point(13, 35);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(116, 21);
            this.cmbPortName.TabIndex = 1;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(13, 75);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(116, 21);
            this.cmbBaudRate.TabIndex = 3;
            this.cmbBaudRate.Validating += new System.ComponentModel.CancelEventHandler(this.cmbBaudRate_Validating);
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbStopBits.Location = new System.Drawing.Point(13, 195);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(116, 21);
            this.cmbStopBits.TabIndex = 9;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParity.Location = new System.Drawing.Point(13, 115);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(116, 21);
            this.cmbParity.TabIndex = 5;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbDataBits.Location = new System.Drawing.Point(13, 155);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(116, 21);
            this.cmbDataBits.TabIndex = 7;
            this.cmbDataBits.Validating += new System.ComponentModel.CancelEventHandler(this.cmbDataBits_Validating);
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(12, 19);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(56, 13);
            this.lblComPort.TabIndex = 0;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(12, 179);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(52, 13);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "Stop Bits:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(12, 59);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(12, 139);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(53, 13);
            this.lblDataBits.TabIndex = 6;
            this.lblDataBits.Text = "Data Bits:";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(12, 99);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(36, 13);
            this.lblParity.TabIndex = 4;
            this.lblParity.Text = "Parity:";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(12, 244);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(142, 32);
            this.btnOpenPort.TabIndex = 7;
            this.btnOpenPort.Text = "&Open Port";
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 282);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(142, 32);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "&Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Location = new System.Drawing.Point(12, 320);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(142, 141);
            this.rtfTerminal.TabIndex = 11;
            this.rtfTerminal.Text = "";
            // 
            // zgcTemperature
            // 
            this.zgcTemperature.Location = new System.Drawing.Point(160, 18);
            this.zgcTemperature.Name = "zgcTemperature";
            this.zgcTemperature.ScrollGrace = 0D;
            this.zgcTemperature.ScrollMaxX = 0D;
            this.zgcTemperature.ScrollMaxY = 0D;
            this.zgcTemperature.ScrollMaxY2 = 0D;
            this.zgcTemperature.ScrollMinX = 0D;
            this.zgcTemperature.ScrollMinY = 0D;
            this.zgcTemperature.ScrollMinY2 = 0D;
            this.zgcTemperature.Size = new System.Drawing.Size(638, 443);
            this.zgcTemperature.TabIndex = 12;
            // 
            // tmrCheckComPorts
            // 
            this.tmrCheckComPorts.Enabled = true;
            this.tmrCheckComPorts.Interval = 500;
            this.tmrCheckComPorts.Tick += new System.EventHandler(this.tmrCheckComPorts_Tick);
            // 
            // tmrDrawLine
            // 
            this.tmrDrawLine.Interval = 1000;
            this.tmrDrawLine.Tick += new System.EventHandler(this.tmrDrawLine_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 473);
            this.Controls.Add(this.zgcTemperature);
            this.Controls.Add(this.rtfTerminal);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.gbPortSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Real-Time Temperature Plot (Arduino)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.gbPortSettings.ResumeLayout(false);
            this.gbPortSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPortSettings;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        private ZedGraph.ZedGraphControl zgcTemperature;
        private System.Windows.Forms.Timer tmrCheckComPorts;
        private System.Windows.Forms.Timer tmrDrawLine;
    }
}

