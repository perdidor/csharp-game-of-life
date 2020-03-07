namespace Game_Of_Life
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.RunButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.movelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bornlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.deadlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.alivelabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PauseButton = new System.Windows.Forms.Button();
            this.RunOneStepButton = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.seedpercent = new System.Windows.Forms.Label();
            this.FillingPercentileTracker = new System.Windows.Forms.TrackBar();
            this.cpcb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.zoomlabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sdprob = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ttl = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newborn = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmax = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nmin = new System.Windows.Forms.NumericUpDown();
            this.ResetButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FillingPercentileTracker)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sdprob)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ttl)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newborn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmin)).BeginInit();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Enabled = false;
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RunButton.Location = new System.Drawing.Point(722, 229);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(115, 23);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Run Life";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.movelabel,
            this.toolStripStatusLabel3,
            this.bornlabel,
            this.toolStripStatusLabel1,
            this.deadlabel,
            this.toolStripStatusLabel2,
            this.alivelabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // movelabel
            // 
            this.movelabel.Name = "movelabel";
            this.movelabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel3.Text = " | ";
            // 
            // bornlabel
            // 
            this.bornlabel.ForeColor = System.Drawing.Color.Blue;
            this.bornlabel.Name = "bornlabel";
            this.bornlabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel1.Text = " | ";
            // 
            // deadlabel
            // 
            this.deadlabel.ForeColor = System.Drawing.Color.Red;
            this.deadlabel.Name = "deadlabel";
            this.deadlabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(17, 17);
            this.toolStripStatusLabel2.Text = " | ";
            // 
            // alivelabel
            // 
            this.alivelabel.ForeColor = System.Drawing.Color.Green;
            this.alivelabel.Name = "alivelabel";
            this.alivelabel.Size = new System.Drawing.Size(0, 17);
            // 
            // PauseButton
            // 
            this.PauseButton.Enabled = false;
            this.PauseButton.Location = new System.Drawing.Point(925, 229);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(69, 23);
            this.PauseButton.TabIndex = 4;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // RunOneStepButton
            // 
            this.RunOneStepButton.Enabled = false;
            this.RunOneStepButton.Location = new System.Drawing.Point(844, 229);
            this.RunOneStepButton.Name = "RunOneStepButton";
            this.RunOneStepButton.Size = new System.Drawing.Size(69, 23);
            this.RunOneStepButton.TabIndex = 7;
            this.RunOneStepButton.Text = "Move >|";
            this.RunOneStepButton.UseVisualStyleBackColor = true;
            this.RunOneStepButton.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // chart
            // 
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Area3DStyle.WallWidth = 1;
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            this.chart.Location = new System.Drawing.Point(0, 25);
            this.chart.Name = "chart";
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Color = System.Drawing.Color.LimeGreen;
            series7.Name = "Alive";
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series8.Color = System.Drawing.Color.Blue;
            series8.Name = "Born";
            series9.BorderWidth = 2;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series9.Color = System.Drawing.Color.Red;
            series9.Name = "Dead";
            this.chart.Series.Add(series7);
            this.chart.Series.Add(series8);
            this.chart.Series.Add(series9);
            this.chart.Size = new System.Drawing.Size(1084, 148);
            this.chart.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.seedpercent);
            this.groupBox1.Controls.Add(this.FillingPercentileTracker);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 70);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Initial seed filling %";
            // 
            // seedpercent
            // 
            this.seedpercent.AutoSize = true;
            this.seedpercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seedpercent.Location = new System.Drawing.Point(181, 30);
            this.seedpercent.Name = "seedpercent";
            this.seedpercent.Size = new System.Drawing.Size(14, 13);
            this.seedpercent.TabIndex = 10;
            this.seedpercent.Text = "0";
            // 
            // FillingPercentileTracker
            // 
            this.FillingPercentileTracker.Location = new System.Drawing.Point(12, 19);
            this.FillingPercentileTracker.Maximum = 100;
            this.FillingPercentileTracker.Name = "FillingPercentileTracker";
            this.FillingPercentileTracker.Size = new System.Drawing.Size(163, 45);
            this.FillingPercentileTracker.TabIndex = 7;
            this.FillingPercentileTracker.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // cpcb
            // 
            this.cpcb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cpcb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cpcb.FormattingEnabled = true;
            this.cpcb.Items.AddRange(new object[] {
            "100",
            "200",
            "300",
            "500",
            "1000"});
            this.cpcb.Location = new System.Drawing.Point(722, 201);
            this.cpcb.Name = "cpcb";
            this.cpcb.Size = new System.Drawing.Size(115, 21);
            this.cpcb.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(722, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Chart points:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(898, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Zoom factor:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1084, 565);
            this.panel1.TabIndex = 16;
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(1084, 565);
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // zoomlabel
            // 
            this.zoomlabel.AutoSize = true;
            this.zoomlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zoomlabel.Location = new System.Drawing.Point(898, 204);
            this.zoomlabel.Name = "zoomlabel";
            this.zoomlabel.Size = new System.Drawing.Size(0, 13);
            this.zoomlabel.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sdprob);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(211, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 64);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sudden Death %";
            // 
            // sdprob
            // 
            this.sdprob.Location = new System.Drawing.Point(6, 24);
            this.sdprob.Name = "sdprob";
            this.sdprob.ReadOnly = true;
            this.sdprob.Size = new System.Drawing.Size(95, 20);
            this.sdprob.TabIndex = 0;
            this.sdprob.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.sdprob.ValueChanged += new System.EventHandler(this.sdprob_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ttl);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(336, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(82, 64);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Max TTL";
            // 
            // ttl
            // 
            this.ttl.Location = new System.Drawing.Point(6, 24);
            this.ttl.Name = "ttl";
            this.ttl.ReadOnly = true;
            this.ttl.Size = new System.Drawing.Size(64, 20);
            this.ttl.TabIndex = 0;
            this.ttl.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ttl.ValueChanged += new System.EventHandler(this.ttl_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.newborn);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.nmax);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.nmin);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(430, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 64);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Neighbors";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Born New";
            // 
            // newborn
            // 
            this.newborn.Location = new System.Drawing.Point(240, 24);
            this.newborn.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.newborn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.newborn.Name = "newborn";
            this.newborn.ReadOnly = true;
            this.newborn.Size = new System.Drawing.Size(42, 20);
            this.newborn.TabIndex = 4;
            this.newborn.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.newborn.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Max";
            // 
            // nmax
            // 
            this.nmax.Location = new System.Drawing.Point(122, 24);
            this.nmax.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nmax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmax.Name = "nmax";
            this.nmax.ReadOnly = true;
            this.nmax.Size = new System.Drawing.Size(42, 20);
            this.nmax.TabIndex = 2;
            this.nmax.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nmax.ValueChanged += new System.EventHandler(this.nmax_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Min";
            // 
            // nmin
            // 
            this.nmin.Location = new System.Drawing.Point(39, 24);
            this.nmin.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nmin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmin.Name = "nmin";
            this.nmin.ReadOnly = true;
            this.nmin.Size = new System.Drawing.Size(42, 20);
            this.nmin.TabIndex = 0;
            this.nmin.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmin.ValueChanged += new System.EventHandler(this.nmin_ValueChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Enabled = false;
            this.ResetButton.Location = new System.Drawing.Point(1003, 229);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(69, 23);
            this.ResetButton.TabIndex = 21;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1084, 824);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.zoomlabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cpcb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.RunOneStepButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RunButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Of Life";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FillingPercentileTracker)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sdprob)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ttl)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newborn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel bornlabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel deadlabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel alivelabel;
        private System.Windows.Forms.ToolStripStatusLabel movelabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button RunOneStepButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label seedpercent;
        private System.Windows.Forms.TrackBar FillingPercentileTracker;
        private System.Windows.Forms.ComboBox cpcb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label zoomlabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown sdprob;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown ttl;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown newborn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ResetButton;
    }
}

