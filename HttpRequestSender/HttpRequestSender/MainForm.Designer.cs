
namespace StressTester
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
            this.label1 = new System.Windows.Forms.Label();
            this.URL_TB = new System.Windows.Forms.TextBox();
            this.reqPerSec_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.start_BTN = new System.Windows.Forms.Button();
            this.stop_BTN = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logGrid_DGV = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_L = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid_DGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(162, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stress  Tester";
            // 
            // URL_TB
            // 
            this.URL_TB.Location = new System.Drawing.Point(87, 69);
            this.URL_TB.Name = "URL_TB";
            this.URL_TB.Size = new System.Drawing.Size(351, 20);
            this.URL_TB.TabIndex = 0;
            this.URL_TB.Text = "https://hf.mit.bme.hu/tmp/alexandra/test.php?sleep=100";
            // 
            // reqPerSec_TB
            // 
            this.reqPerSec_TB.Location = new System.Drawing.Point(245, 102);
            this.reqPerSec_TB.Name = "reqPerSec_TB";
            this.reqPerSec_TB.Size = new System.Drawing.Size(100, 20);
            this.reqPerSec_TB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "URL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Kérés / másodperc:";
            // 
            // start_BTN
            // 
            this.start_BTN.BackColor = System.Drawing.Color.LimeGreen;
            this.start_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_BTN.ForeColor = System.Drawing.Color.White;
            this.start_BTN.Location = new System.Drawing.Point(57, 136);
            this.start_BTN.Name = "start_BTN";
            this.start_BTN.Size = new System.Drawing.Size(170, 50);
            this.start_BTN.TabIndex = 4;
            this.start_BTN.Text = "Start";
            this.start_BTN.UseVisualStyleBackColor = false;
            this.start_BTN.Click += new System.EventHandler(this.start_BTN_Click);
            // 
            // stop_BTN
            // 
            this.stop_BTN.BackColor = System.Drawing.Color.OrangeRed;
            this.stop_BTN.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold);
            this.stop_BTN.ForeColor = System.Drawing.Color.White;
            this.stop_BTN.Location = new System.Drawing.Point(257, 136);
            this.stop_BTN.Name = "stop_BTN";
            this.stop_BTN.Size = new System.Drawing.Size(170, 50);
            this.stop_BTN.TabIndex = 5;
            this.stop_BTN.Text = "Stop";
            this.stop_BTN.UseVisualStyleBackColor = false;
            this.stop_BTN.Click += new System.EventHandler(this.stop_BTN_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Controls.Add(this.logGrid_DGV);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.stop_BTN);
            this.panel2.Controls.Add(this.start_BTN);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.reqPerSec_TB);
            this.panel2.Controls.Add(this.URL_TB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 411);
            this.panel2.TabIndex = 1;
            // 
            // logGrid_DGV
            // 
            this.logGrid_DGV.AllowUserToAddRows = false;
            this.logGrid_DGV.AllowUserToDeleteRows = false;
            this.logGrid_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGrid_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timestamp,
            this.Message,
            this.Duration});
            this.logGrid_DGV.Location = new System.Drawing.Point(23, 213);
            this.logGrid_DGV.Name = "logGrid_DGV";
            this.logGrid_DGV.ReadOnly = true;
            this.logGrid_DGV.RowHeadersVisible = false;
            this.logGrid_DGV.Size = new System.Drawing.Size(437, 168);
            this.logGrid_DGV.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_L});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_L
            // 
            this.status_L.ForeColor = System.Drawing.Color.Red;
            this.status_L.Name = "status_L";
            this.status_L.Size = new System.Drawing.Size(0, 17);
            this.status_L.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // Timestamp
            // 
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            this.Timestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Timestamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Timestamp.Width = 150;
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Message.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Message.Width = 165;
            // 
            // Duration
            // 
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            this.Duration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Duration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StressTester_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.panel2);
            this.Name = "StressTester_Form";
            this.Text = "Stress Tester";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid_DGV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.DataGridView logGrid_DGV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_L;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
    }
}

