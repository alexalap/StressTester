
namespace HttpRequestSender.Forms
{
    partial class StressTester_Form
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.URL_TB = new System.Windows.Forms.TextBox();
            this.reqPerSec_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.start_BTN = new System.Windows.Forms.Button();
            this.stop_BTN = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.averageResTime_L = new System.Windows.Forms.Label();
            this.numberOfRes_L = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.manual_CH = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pause_BTN = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_L = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_TC = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plannedActPlan_LB = new System.Windows.Forms.ListBox();
            this.planned_CH = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.planEditor_BTN = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.plannedStop_BTN = new System.Windows.Forms.Button();
            this.plannedStart_BTN = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.actualStep_TB = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.URLReport_BTN = new System.Windows.Forms.Button();
            this.URLStop_BTN = new System.Windows.Forms.Button();
            this.URLStart_BTN = new System.Windows.Forms.Button();
            this.explorer_BTN = new System.Windows.Forms.Button();
            this.statusStrip4 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.URLReqPerSec_TB = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.logs_DGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.report_BTN = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manual_CH)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.main_TC.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planned_CH)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.statusStrip4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logs_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(175, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manual Stress Tester";
            // 
            // URL_TB
            // 
            this.URL_TB.Location = new System.Drawing.Point(116, 85);
            this.URL_TB.Margin = new System.Windows.Forms.Padding(4);
            this.URL_TB.Name = "URL_TB";
            this.URL_TB.Size = new System.Drawing.Size(467, 22);
            this.URL_TB.TabIndex = 0;
            this.URL_TB.Text = "https://hf.mit.bme.hu/tmp/alexandra/test.php?sleep=100";
            // 
            // reqPerSec_TB
            // 
            this.reqPerSec_TB.Location = new System.Drawing.Point(327, 126);
            this.reqPerSec_TB.Margin = new System.Windows.Forms.Padding(4);
            this.reqPerSec_TB.Name = "reqPerSec_TB";
            this.reqPerSec_TB.Size = new System.Drawing.Size(132, 22);
            this.reqPerSec_TB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "URL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Request / second:";
            // 
            // start_BTN
            // 
            this.start_BTN.BackColor = System.Drawing.Color.LimeGreen;
            this.start_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_BTN.ForeColor = System.Drawing.Color.White;
            this.start_BTN.Location = new System.Drawing.Point(25, 167);
            this.start_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.start_BTN.Name = "start_BTN";
            this.start_BTN.Size = new System.Drawing.Size(176, 46);
            this.start_BTN.TabIndex = 4;
            this.start_BTN.Text = "Start";
            this.start_BTN.UseVisualStyleBackColor = false;
            this.start_BTN.Click += new System.EventHandler(this.start_BTN_Click);
            // 
            // stop_BTN
            // 
            this.stop_BTN.BackColor = System.Drawing.Color.OrangeRed;
            this.stop_BTN.Enabled = false;
            this.stop_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.stop_BTN.ForeColor = System.Drawing.Color.White;
            this.stop_BTN.Location = new System.Drawing.Point(432, 167);
            this.stop_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.stop_BTN.Name = "stop_BTN";
            this.stop_BTN.Size = new System.Drawing.Size(176, 46);
            this.stop_BTN.TabIndex = 5;
            this.stop_BTN.Text = "Stop";
            this.stop_BTN.UseVisualStyleBackColor = false;
            this.stop_BTN.Click += new System.EventHandler(this.stop_BTN_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.report_BTN);
            this.panel2.Controls.Add(this.averageResTime_L);
            this.panel2.Controls.Add(this.numberOfRes_L);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.manual_CH);
            this.panel2.Controls.Add(this.pause_BTN);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.stop_BTN);
            this.panel2.Controls.Add(this.start_BTN);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.reqPerSec_TB);
            this.panel2.Controls.Add(this.URL_TB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 748);
            this.panel2.TabIndex = 1;
            // 
            // averageResTime_L
            // 
            this.averageResTime_L.AutoSize = true;
            this.averageResTime_L.Location = new System.Drawing.Point(186, 586);
            this.averageResTime_L.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.averageResTime_L.Name = "averageResTime_L";
            this.averageResTime_L.Size = new System.Drawing.Size(31, 17);
            this.averageResTime_L.TabIndex = 15;
            this.averageResTime_L.Text = "N/A";
            // 
            // numberOfRes_L
            // 
            this.numberOfRes_L.AutoSize = true;
            this.numberOfRes_L.Location = new System.Drawing.Point(186, 559);
            this.numberOfRes_L.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfRes_L.Name = "numberOfRes_L";
            this.numberOfRes_L.Size = new System.Drawing.Size(31, 17);
            this.numberOfRes_L.TabIndex = 14;
            this.numberOfRes_L.Text = "N/A";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 586);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "Average response time:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 559);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Number of responses:";
            // 
            // manual_CH
            // 
            chartArea3.Name = "ChartArea1";
            this.manual_CH.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.manual_CH.Legends.Add(legend3);
            this.manual_CH.Location = new System.Drawing.Point(25, 228);
            this.manual_CH.Name = "manual_CH";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series3.Legend = "Legend1";
            series3.Name = "Response rate";
            this.manual_CH.Series.Add(series3);
            this.manual_CH.Size = new System.Drawing.Size(583, 253);
            this.manual_CH.TabIndex = 9;
            this.manual_CH.Text = "chart2";
            // 
            // pause_BTN
            // 
            this.pause_BTN.BackColor = System.Drawing.Color.Orange;
            this.pause_BTN.Enabled = false;
            this.pause_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.pause_BTN.ForeColor = System.Drawing.Color.White;
            this.pause_BTN.Location = new System.Drawing.Point(228, 167);
            this.pause_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.pause_BTN.Name = "pause_BTN";
            this.pause_BTN.Size = new System.Drawing.Size(176, 46);
            this.pause_BTN.TabIndex = 8;
            this.pause_BTN.Text = "Pause";
            this.pause_BTN.UseVisualStyleBackColor = false;
            this.pause_BTN.Click += new System.EventHandler(this.pause_BTN_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_L});
            this.statusStrip1.Location = new System.Drawing.Point(0, 726);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_L
            // 
            this.status_L.ForeColor = System.Drawing.Color.Red;
            this.status_L.Name = "status_L";
            this.status_L.Size = new System.Drawing.Size(0, 16);
            this.status_L.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // main_TC
            // 
            this.main_TC.Controls.Add(this.tabPage1);
            this.main_TC.Controls.Add(this.tabPage2);
            this.main_TC.Controls.Add(this.tabPage3);
            this.main_TC.Controls.Add(this.tabPage4);
            this.main_TC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_TC.Location = new System.Drawing.Point(0, 0);
            this.main_TC.Name = "main_TC";
            this.main_TC.SelectedIndex = 0;
            this.main_TC.Size = new System.Drawing.Size(645, 783);
            this.main_TC.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 754);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Manual";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 754);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Planned";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plannedActPlan_LB);
            this.panel1.Controls.Add(this.planned_CH);
            this.panel1.Controls.Add(this.planEditor_BTN);
            this.panel1.Controls.Add(this.statusStrip2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.plannedStop_BTN);
            this.panel1.Controls.Add(this.plannedStart_BTN);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.actualStep_TB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 748);
            this.panel1.TabIndex = 3;
            // 
            // plannedActPlan_LB
            // 
            this.plannedActPlan_LB.FormattingEnabled = true;
            this.plannedActPlan_LB.ItemHeight = 16;
            this.plannedActPlan_LB.Location = new System.Drawing.Point(23, 123);
            this.plannedActPlan_LB.Name = "plannedActPlan_LB";
            this.plannedActPlan_LB.Size = new System.Drawing.Size(583, 196);
            this.plannedActPlan_LB.TabIndex = 10;
            // 
            // planned_CH
            // 
            chartArea4.Name = "ChartArea1";
            this.planned_CH.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.planned_CH.Legends.Add(legend4);
            this.planned_CH.Location = new System.Drawing.Point(23, 451);
            this.planned_CH.Name = "planned_CH";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.planned_CH.Series.Add(series4);
            this.planned_CH.Size = new System.Drawing.Size(583, 253);
            this.planned_CH.TabIndex = 9;
            this.planned_CH.Text = "chart1";
            // 
            // planEditor_BTN
            // 
            this.planEditor_BTN.BackColor = System.Drawing.SystemColors.Window;
            this.planEditor_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.planEditor_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.planEditor_BTN.Location = new System.Drawing.Point(196, 64);
            this.planEditor_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.planEditor_BTN.Name = "planEditor_BTN";
            this.planEditor_BTN.Size = new System.Drawing.Size(234, 46);
            this.planEditor_BTN.TabIndex = 8;
            this.planEditor_BTN.Text = "Terv Szerkesztő";
            this.planEditor_BTN.UseVisualStyleBackColor = false;
            this.planEditor_BTN.Click += new System.EventHandler(this.planEditor_BTN_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip2.Location = new System.Drawing.Point(0, 726);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip2.Size = new System.Drawing.Size(631, 22);
            this.statusStrip2.TabIndex = 7;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            this.toolStripStatusLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(160, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "Planned Stress Tester";
            // 
            // plannedStop_BTN
            // 
            this.plannedStop_BTN.BackColor = System.Drawing.Color.OrangeRed;
            this.plannedStop_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.plannedStop_BTN.ForeColor = System.Drawing.Color.White;
            this.plannedStop_BTN.Location = new System.Drawing.Point(341, 344);
            this.plannedStop_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.plannedStop_BTN.Name = "plannedStop_BTN";
            this.plannedStop_BTN.Size = new System.Drawing.Size(188, 57);
            this.plannedStop_BTN.TabIndex = 5;
            this.plannedStop_BTN.Text = "Stop";
            this.plannedStop_BTN.UseVisualStyleBackColor = false;
            // 
            // plannedStart_BTN
            // 
            this.plannedStart_BTN.BackColor = System.Drawing.Color.LimeGreen;
            this.plannedStart_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plannedStart_BTN.ForeColor = System.Drawing.Color.White;
            this.plannedStart_BTN.Location = new System.Drawing.Point(99, 344);
            this.plannedStart_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.plannedStart_BTN.Name = "plannedStart_BTN";
            this.plannedStart_BTN.Size = new System.Drawing.Size(188, 57);
            this.plannedStart_BTN.TabIndex = 4;
            this.plannedStart_BTN.Text = "Start";
            this.plannedStart_BTN.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 417);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Aktuális lépés:";
            // 
            // actualStep_TB
            // 
            this.actualStep_TB.Location = new System.Drawing.Point(245, 414);
            this.actualStep_TB.Margin = new System.Windows.Forms.Padding(4);
            this.actualStep_TB.Name = "actualStep_TB";
            this.actualStep_TB.Size = new System.Drawing.Size(227, 22);
            this.actualStep_TB.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(637, 754);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "URL Exploration";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listBox1);
            this.panel4.Controls.Add(this.URLReport_BTN);
            this.panel4.Controls.Add(this.URLStop_BTN);
            this.panel4.Controls.Add(this.URLStart_BTN);
            this.panel4.Controls.Add(this.explorer_BTN);
            this.panel4.Controls.Add(this.statusStrip4);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.URLReqPerSec_TB);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(631, 748);
            this.panel4.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(23, 126);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(583, 196);
            this.listBox1.TabIndex = 13;
            // 
            // URLReport_BTN
            // 
            this.URLReport_BTN.BackColor = System.Drawing.SystemColors.Window;
            this.URLReport_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.URLReport_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.URLReport_BTN.Location = new System.Drawing.Point(196, 442);
            this.URLReport_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.URLReport_BTN.Name = "URLReport_BTN";
            this.URLReport_BTN.Size = new System.Drawing.Size(234, 46);
            this.URLReport_BTN.TabIndex = 12;
            this.URLReport_BTN.Text = "Report készítés";
            this.URLReport_BTN.UseVisualStyleBackColor = false;
            // 
            // URLStop_BTN
            // 
            this.URLStop_BTN.BackColor = System.Drawing.Color.OrangeRed;
            this.URLStop_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.URLStop_BTN.ForeColor = System.Drawing.Color.White;
            this.URLStop_BTN.Location = new System.Drawing.Point(342, 373);
            this.URLStop_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.URLStop_BTN.Name = "URLStop_BTN";
            this.URLStop_BTN.Size = new System.Drawing.Size(188, 57);
            this.URLStop_BTN.TabIndex = 11;
            this.URLStop_BTN.Text = "Stop";
            this.URLStop_BTN.UseVisualStyleBackColor = false;
            // 
            // URLStart_BTN
            // 
            this.URLStart_BTN.BackColor = System.Drawing.Color.LimeGreen;
            this.URLStart_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLStart_BTN.ForeColor = System.Drawing.Color.White;
            this.URLStart_BTN.Location = new System.Drawing.Point(100, 373);
            this.URLStart_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.URLStart_BTN.Name = "URLStart_BTN";
            this.URLStart_BTN.Size = new System.Drawing.Size(188, 57);
            this.URLStart_BTN.TabIndex = 10;
            this.URLStart_BTN.Text = "Start";
            this.URLStart_BTN.UseVisualStyleBackColor = false;
            // 
            // explorer_BTN
            // 
            this.explorer_BTN.BackColor = System.Drawing.SystemColors.Window;
            this.explorer_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.explorer_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.explorer_BTN.Location = new System.Drawing.Point(227, 64);
            this.explorer_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.explorer_BTN.Name = "explorer_BTN";
            this.explorer_BTN.Size = new System.Drawing.Size(177, 46);
            this.explorer_BTN.TabIndex = 9;
            this.explorer_BTN.Text = "Felderítő";
            this.explorer_BTN.UseVisualStyleBackColor = false;
            // 
            // statusStrip4
            // 
            this.statusStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3});
            this.statusStrip4.Location = new System.Drawing.Point(0, 726);
            this.statusStrip4.Name = "statusStrip4";
            this.statusStrip4.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip4.Size = new System.Drawing.Size(631, 22);
            this.statusStrip4.TabIndex = 7;
            this.statusStrip4.Text = "statusStrip4";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(0, 16);
            this.toolStripStatusLabel3.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(200, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(226, 31);
            this.label9.TabIndex = 0;
            this.label9.Text = "URL Exploration";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(177, 341);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "Kérés / másodperc:";
            // 
            // URLReqPerSec_TB
            // 
            this.URLReqPerSec_TB.Location = new System.Drawing.Point(319, 338);
            this.URLReqPerSec_TB.Margin = new System.Windows.Forms.Padding(4);
            this.URLReqPerSec_TB.Name = "URLReqPerSec_TB";
            this.URLReqPerSec_TB.Size = new System.Drawing.Size(132, 22);
            this.URLReqPerSec_TB.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(637, 754);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Logs";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.statusStrip3);
            this.panel3.Controls.Add(this.logs_DGV);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(631, 748);
            this.panel3.TabIndex = 2;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Orange;
            this.button5.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(228, 64);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(176, 46);
            this.button5.TabIndex = 8;
            this.button5.Text = "Pause";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // statusStrip3
            // 
            this.statusStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip3.Location = new System.Drawing.Point(0, 726);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip3.Size = new System.Drawing.Size(631, 22);
            this.statusStrip3.TabIndex = 7;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 16);
            this.toolStripStatusLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // logs_DGV
            // 
            this.logs_DGV.AllowUserToAddRows = false;
            this.logs_DGV.AllowUserToDeleteRows = false;
            this.logs_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logs_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Priority,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.logs_DGV.Location = new System.Drawing.Point(25, 123);
            this.logs_DGV.Margin = new System.Windows.Forms.Padding(4);
            this.logs_DGV.Name = "logs_DGV";
            this.logs_DGV.ReadOnly = true;
            this.logs_DGV.RowHeadersVisible = false;
            this.logs_DGV.RowHeadersWidth = 51;
            this.logs_DGV.Size = new System.Drawing.Size(583, 302);
            this.logs_DGV.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // Priority
            // 
            this.Priority.HeaderText = "Priority";
            this.Priority.MinimumWidth = 6;
            this.Priority.Name = "Priority";
            this.Priority.ReadOnly = true;
            this.Priority.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Priority.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Priority.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Message";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 165;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Duration";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 125;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(281, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "Logs";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.OrangeRed;
            this.button6.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(432, 64);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(176, 46);
            this.button6.TabIndex = 5;
            this.button6.Text = "Stop";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.LimeGreen;
            this.button7.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(25, 64);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(176, 46);
            this.button7.TabIndex = 4;
            this.button7.Text = "Start";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // report_BTN
            // 
            this.report_BTN.BackColor = System.Drawing.SystemColors.Window;
            this.report_BTN.Enabled = false;
            this.report_BTN.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.report_BTN.ForeColor = System.Drawing.Color.Black;
            this.report_BTN.Location = new System.Drawing.Point(228, 498);
            this.report_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.report_BTN.Name = "report_BTN";
            this.report_BTN.Size = new System.Drawing.Size(176, 46);
            this.report_BTN.TabIndex = 16;
            this.report_BTN.Text = "Generate Report";
            this.report_BTN.UseVisualStyleBackColor = false;
            this.report_BTN.Click += new System.EventHandler(this.report_BTN_Click);
            // 
            // StressTester_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 783);
            this.Controls.Add(this.main_TC);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StressTester_Form";
            this.Text = "Stress Tester";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.manual_CH)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.main_TC.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planned_CH)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.statusStrip4.ResumeLayout(false);
            this.statusStrip4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logs_DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox URL_TB;
        private System.Windows.Forms.TextBox reqPerSec_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button start_BTN;
        private System.Windows.Forms.Button stop_BTN;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_L;
        private System.Windows.Forms.Button pause_BTN;
        private System.Windows.Forms.TabControl main_TC;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button planEditor_BTN;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button plannedStop_BTN;
        private System.Windows.Forms.Button plannedStart_BTN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox actualStep_TB;
        private System.Windows.Forms.DataVisualization.Charting.Chart manual_CH;
        private System.Windows.Forms.DataVisualization.Charting.Chart planned_CH;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip statusStrip4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox URLReqPerSec_TB;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.DataGridView logs_DGV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button URLReport_BTN;
        private System.Windows.Forms.Button URLStop_BTN;
        private System.Windows.Forms.Button URLStart_BTN;
        private System.Windows.Forms.Button explorer_BTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ListBox plannedActPlan_LB;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label averageResTime_L;
        private System.Windows.Forms.Label numberOfRes_L;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button report_BTN;
    }
}

