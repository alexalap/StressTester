
namespace HttpRequestSender.Forms
{
    partial class Explorer_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.explorerURL_TB = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.explorerActPlan_DGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.explorerOK_BTN = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerActPlan_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.explorerOK_BTN);
            this.panel1.Controls.Add(this.explorerActPlan_DGV);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.explorerURL_TB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 448);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(264, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Explorer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "URL:";
            // 
            // explorerURL_TB
            // 
            this.explorerURL_TB.Location = new System.Drawing.Point(116, 85);
            this.explorerURL_TB.Margin = new System.Windows.Forms.Padding(4);
            this.explorerURL_TB.Name = "explorerURL_TB";
            this.explorerURL_TB.Size = new System.Drawing.Size(467, 22);
            this.explorerURL_TB.TabIndex = 4;
            this.explorerURL_TB.Text = "https://hf.mit.bme.hu/tmp/alexandra/test.php?sleep=100";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.button5.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.button5.Location = new System.Drawing.Point(253, 122);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 39);
            this.button5.TabIndex = 15;
            this.button5.Text = "Gomb";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // explorerActPlan_DGV
            // 
            this.explorerActPlan_DGV.AllowUserToAddRows = false;
            this.explorerActPlan_DGV.AllowUserToDeleteRows = false;
            this.explorerActPlan_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.explorerActPlan_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.explorerActPlan_DGV.Location = new System.Drawing.Point(31, 174);
            this.explorerActPlan_DGV.Margin = new System.Windows.Forms.Padding(4);
            this.explorerActPlan_DGV.Name = "explorerActPlan_DGV";
            this.explorerActPlan_DGV.ReadOnly = true;
            this.explorerActPlan_DGV.RowHeadersVisible = false;
            this.explorerActPlan_DGV.RowHeadersWidth = 51;
            this.explorerActPlan_DGV.Size = new System.Drawing.Size(583, 207);
            this.explorerActPlan_DGV.TabIndex = 16;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Message";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 165;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Duration";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // explorerOK_BTN
            // 
            this.explorerOK_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.explorerOK_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.explorerOK_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.explorerOK_BTN.Location = new System.Drawing.Point(253, 393);
            this.explorerOK_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.explorerOK_BTN.Name = "explorerOK_BTN";
            this.explorerOK_BTN.Size = new System.Drawing.Size(138, 39);
            this.explorerOK_BTN.TabIndex = 17;
            this.explorerOK_BTN.Text = "OK";
            this.explorerOK_BTN.UseVisualStyleBackColor = false;
            // 
            // Explorer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 448);
            this.Controls.Add(this.panel1);
            this.Name = "Explorer_Form";
            this.Text = "Explorer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerActPlan_DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox explorerURL_TB;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button explorerOK_BTN;
        private System.Windows.Forms.DataGridView explorerActPlan_DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}