
namespace HttpRequestSender.Forms
{
    partial class PlanEditor_Form
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
            this.planEditorOK_BTN = new System.Windows.Forms.Button();
            this.move_BTN = new System.Windows.Forms.Button();
            this.remove_BTN = new System.Windows.Forms.Button();
            this.edit_BTN = new System.Windows.Forms.Button();
            this.add_BTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.plannedActPlan_LB = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plannedActPlan_LB);
            this.panel1.Controls.Add(this.planEditorOK_BTN);
            this.panel1.Controls.Add(this.move_BTN);
            this.panel1.Controls.Add(this.remove_BTN);
            this.panel1.Controls.Add(this.edit_BTN);
            this.panel1.Controls.Add(this.add_BTN);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 393);
            this.panel1.TabIndex = 0;
            // 
            // planEditorOK_BTN
            // 
            this.planEditorOK_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.planEditorOK_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.planEditorOK_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.planEditorOK_BTN.Location = new System.Drawing.Point(254, 338);
            this.planEditorOK_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.planEditorOK_BTN.Name = "planEditorOK_BTN";
            this.planEditorOK_BTN.Size = new System.Drawing.Size(138, 39);
            this.planEditorOK_BTN.TabIndex = 14;
            this.planEditorOK_BTN.Text = "OK";
            this.planEditorOK_BTN.UseVisualStyleBackColor = false;
            // 
            // move_BTN
            // 
            this.move_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.move_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.move_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.move_BTN.Location = new System.Drawing.Point(478, 68);
            this.move_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.move_BTN.Name = "move_BTN";
            this.move_BTN.Size = new System.Drawing.Size(138, 39);
            this.move_BTN.TabIndex = 12;
            this.move_BTN.Text = "Move";
            this.move_BTN.UseVisualStyleBackColor = false;
            // 
            // remove_BTN
            // 
            this.remove_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.remove_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.remove_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.remove_BTN.Location = new System.Drawing.Point(329, 68);
            this.remove_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.remove_BTN.Name = "remove_BTN";
            this.remove_BTN.Size = new System.Drawing.Size(138, 39);
            this.remove_BTN.TabIndex = 11;
            this.remove_BTN.Text = "Remove";
            this.remove_BTN.UseVisualStyleBackColor = false;
            // 
            // edit_BTN
            // 
            this.edit_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.edit_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.edit_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.edit_BTN.Location = new System.Drawing.Point(180, 68);
            this.edit_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.edit_BTN.Name = "edit_BTN";
            this.edit_BTN.Size = new System.Drawing.Size(138, 39);
            this.edit_BTN.TabIndex = 10;
            this.edit_BTN.Text = "Edit";
            this.edit_BTN.UseVisualStyleBackColor = false;
            // 
            // add_BTN
            // 
            this.add_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.add_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.add_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.add_BTN.Location = new System.Drawing.Point(31, 68);
            this.add_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.add_BTN.Name = "add_BTN";
            this.add_BTN.Size = new System.Drawing.Size(138, 39);
            this.add_BTN.TabIndex = 9;
            this.add_BTN.Text = "Add";
            this.add_BTN.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 31);
            this.label4.TabIndex = 1;
            this.label4.Text = "Plan Editor";
            // 
            // plannedActPlan_LB
            // 
            this.plannedActPlan_LB.FormattingEnabled = true;
            this.plannedActPlan_LB.ItemHeight = 16;
            this.plannedActPlan_LB.Location = new System.Drawing.Point(31, 124);
            this.plannedActPlan_LB.Name = "plannedActPlan_LB";
            this.plannedActPlan_LB.Size = new System.Drawing.Size(583, 196);
            this.plannedActPlan_LB.TabIndex = 15;
            // 
            // PlanEditor_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 393);
            this.Controls.Add(this.panel1);
            this.Name = "PlanEditor_Form";
            this.Text = "Plan Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button move_BTN;
        private System.Windows.Forms.Button remove_BTN;
        private System.Windows.Forms.Button edit_BTN;
        private System.Windows.Forms.Button add_BTN;
        private System.Windows.Forms.Button planEditorOK_BTN;
        private System.Windows.Forms.ListBox plannedActPlan_LB;
    }
}