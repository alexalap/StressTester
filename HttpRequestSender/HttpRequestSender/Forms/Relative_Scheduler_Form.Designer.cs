
namespace HttpRequestSender.Forms
{
    partial class Relative_Scheduler_Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.requests_NUB = new System.Windows.Forms.NumericUpDown();
            this.down_BTN = new System.Windows.Forms.Button();
            this.up_BTN = new System.Windows.Forms.Button();
            this.duration_TP = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.planGrid = new System.Windows.Forms.DataGridView();
            this.stepColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requestsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planEditorOK_BTN = new System.Windows.Forms.Button();
            this.remove_BTN = new System.Windows.Forms.Button();
            this.edit_BTN = new System.Windows.Forms.Button();
            this.add_BTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.export_BTN = new System.Windows.Forms.Button();
            this.import_BTN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requests_NUB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.import_BTN);
            this.panel1.Controls.Add(this.export_BTN);
            this.panel1.Controls.Add(this.requests_NUB);
            this.panel1.Controls.Add(this.down_BTN);
            this.panel1.Controls.Add(this.up_BTN);
            this.panel1.Controls.Add(this.duration_TP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.planGrid);
            this.panel1.Controls.Add(this.planEditorOK_BTN);
            this.panel1.Controls.Add(this.remove_BTN);
            this.panel1.Controls.Add(this.edit_BTN);
            this.panel1.Controls.Add(this.add_BTN);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 420);
            this.panel1.TabIndex = 0;
            // 
            // requests_NUB
            // 
            this.requests_NUB.Location = new System.Drawing.Point(79, 61);
            this.requests_NUB.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.requests_NUB.Name = "requests_NUB";
            this.requests_NUB.Size = new System.Drawing.Size(160, 20);
            this.requests_NUB.TabIndex = 22;
            this.requests_NUB.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // down_BTN
            // 
            this.down_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.down_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.down_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.down_BTN.Location = new System.Drawing.Point(430, 124);
            this.down_BTN.Name = "down_BTN";
            this.down_BTN.Size = new System.Drawing.Size(32, 32);
            this.down_BTN.TabIndex = 21;
            this.down_BTN.Text = "↓";
            this.down_BTN.UseVisualStyleBackColor = false;
            this.down_BTN.Click += new System.EventHandler(this.down_BTN_Click);
            // 
            // up_BTN
            // 
            this.up_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.up_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.up_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.up_BTN.Location = new System.Drawing.Point(392, 124);
            this.up_BTN.Name = "up_BTN";
            this.up_BTN.Size = new System.Drawing.Size(32, 32);
            this.up_BTN.TabIndex = 20;
            this.up_BTN.Text = "↑";
            this.up_BTN.UseVisualStyleBackColor = false;
            this.up_BTN.Click += new System.EventHandler(this.up_BTN_Click);
            // 
            // duration_TP
            // 
            this.duration_TP.CustomFormat = "HH:mm:ss";
            this.duration_TP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.duration_TP.Location = new System.Drawing.Point(79, 89);
            this.duration_TP.Name = "duration_TP";
            this.duration_TP.ShowUpDown = true;
            this.duration_TP.Size = new System.Drawing.Size(160, 20);
            this.duration_TP.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Duration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Requests";
            // 
            // planGrid
            // 
            this.planGrid.AllowUserToAddRows = false;
            this.planGrid.AllowUserToDeleteRows = false;
            this.planGrid.AllowUserToResizeColumns = false;
            this.planGrid.AllowUserToResizeRows = false;
            this.planGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.planGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stepColumn,
            this.endTimeColumn,
            this.requestsColumn});
            this.planGrid.Location = new System.Drawing.Point(23, 163);
            this.planGrid.Name = "planGrid";
            this.planGrid.RowHeadersVisible = false;
            this.planGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.planGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.planGrid.Size = new System.Drawing.Size(439, 205);
            this.planGrid.TabIndex = 15;
            this.planGrid.SelectionChanged += new System.EventHandler(this.planGrid_SelectionChanged);
            // 
            // stepColumn
            // 
            this.stepColumn.HeaderText = "Step";
            this.stepColumn.Name = "stepColumn";
            this.stepColumn.ReadOnly = true;
            this.stepColumn.Width = 40;
            // 
            // endTimeColumn
            // 
            this.endTimeColumn.HeaderText = "Duration";
            this.endTimeColumn.Name = "endTimeColumn";
            this.endTimeColumn.ReadOnly = true;
            this.endTimeColumn.Width = 190;
            // 
            // requestsColumn
            // 
            this.requestsColumn.HeaderText = "Requests";
            this.requestsColumn.Name = "requestsColumn";
            this.requestsColumn.ReadOnly = true;
            this.requestsColumn.Width = 185;
            // 
            // planEditorOK_BTN
            // 
            this.planEditorOK_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.planEditorOK_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.planEditorOK_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.planEditorOK_BTN.Location = new System.Drawing.Point(190, 374);
            this.planEditorOK_BTN.Name = "planEditorOK_BTN";
            this.planEditorOK_BTN.Size = new System.Drawing.Size(104, 32);
            this.planEditorOK_BTN.TabIndex = 14;
            this.planEditorOK_BTN.Text = "OK";
            this.planEditorOK_BTN.UseVisualStyleBackColor = false;
            this.planEditorOK_BTN.Click += new System.EventHandler(this.planEditorOK_BTN_Click);
            // 
            // remove_BTN
            // 
            this.remove_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.remove_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.remove_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.remove_BTN.Location = new System.Drawing.Point(247, 124);
            this.remove_BTN.Name = "remove_BTN";
            this.remove_BTN.Size = new System.Drawing.Size(104, 32);
            this.remove_BTN.TabIndex = 11;
            this.remove_BTN.Text = "Remove";
            this.remove_BTN.UseVisualStyleBackColor = false;
            this.remove_BTN.Click += new System.EventHandler(this.remove_BTN_Click);
            // 
            // edit_BTN
            // 
            this.edit_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.edit_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.edit_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.edit_BTN.Location = new System.Drawing.Point(135, 124);
            this.edit_BTN.Name = "edit_BTN";
            this.edit_BTN.Size = new System.Drawing.Size(104, 32);
            this.edit_BTN.TabIndex = 10;
            this.edit_BTN.Text = "Edit";
            this.edit_BTN.UseVisualStyleBackColor = false;
            this.edit_BTN.Click += new System.EventHandler(this.edit_BTN_Click);
            // 
            // add_BTN
            // 
            this.add_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.add_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.add_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.add_BTN.Location = new System.Drawing.Point(23, 124);
            this.add_BTN.Name = "add_BTN";
            this.add_BTN.Size = new System.Drawing.Size(104, 32);
            this.add_BTN.TabIndex = 9;
            this.add_BTN.Text = "Add";
            this.add_BTN.UseVisualStyleBackColor = false;
            this.add_BTN.Click += new System.EventHandler(this.add_BTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(182, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Scheduler";
            // 
            // export_BTN
            // 
            this.export_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.export_BTN.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.export_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.export_BTN.Location = new System.Drawing.Point(358, 86);
            this.export_BTN.Name = "export_BTN";
            this.export_BTN.Size = new System.Drawing.Size(104, 29);
            this.export_BTN.TabIndex = 23;
            this.export_BTN.Text = "Export";
            this.export_BTN.UseVisualStyleBackColor = false;
            this.export_BTN.Click += new System.EventHandler(this.Export_BTN_Click);
            // 
            // import_BTN
            // 
            this.import_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.import_BTN.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Bold);
            this.import_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.import_BTN.Location = new System.Drawing.Point(358, 51);
            this.import_BTN.Name = "import_BTN";
            this.import_BTN.Size = new System.Drawing.Size(104, 29);
            this.import_BTN.TabIndex = 24;
            this.import_BTN.Text = "Import";
            this.import_BTN.UseVisualStyleBackColor = false;
            this.import_BTN.Click += new System.EventHandler(this.Import_BTN_Click);
            // 
            // Relative_Scheduler_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 420);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Relative_Scheduler_Form";
            this.Text = "Scheduler";
            this.Load += new System.EventHandler(this.Scheduler_Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requests_NUB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button remove_BTN;
        private System.Windows.Forms.Button edit_BTN;
        private System.Windows.Forms.Button add_BTN;
        private System.Windows.Forms.Button planEditorOK_BTN;
        private System.Windows.Forms.DataGridView planGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button down_BTN;
        private System.Windows.Forms.Button up_BTN;
        private System.Windows.Forms.NumericUpDown requests_NUB;
        private System.Windows.Forms.DateTimePicker duration_TP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn stepColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestsColumn;
        private System.Windows.Forms.Button import_BTN;
        private System.Windows.Forms.Button export_BTN;
    }
}