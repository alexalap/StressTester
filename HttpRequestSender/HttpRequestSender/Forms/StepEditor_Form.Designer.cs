
namespace HttpRequestSender.Forms
{
    partial class StepEditor_Form
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
            this.stepOK_BTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.duration_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stepReqPerSec_TB = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.stepReqPerSec_TB);
            this.panel1.Controls.Add(this.stepOK_BTN);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.duration_TB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 223);
            this.panel1.TabIndex = 0;
            // 
            // stepOK_BTN
            // 
            this.stepOK_BTN.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.stepOK_BTN.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold);
            this.stepOK_BTN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.stepOK_BTN.Location = new System.Drawing.Point(253, 167);
            this.stepOK_BTN.Margin = new System.Windows.Forms.Padding(4);
            this.stepOK_BTN.Name = "stepOK_BTN";
            this.stepOK_BTN.Size = new System.Drawing.Size(138, 39);
            this.stepOK_BTN.TabIndex = 19;
            this.stepOK_BTN.Text = "OK";
            this.stepOK_BTN.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(243, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 31);
            this.label1.TabIndex = 16;
            this.label1.Text = "Step Editor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Duration:";
            // 
            // duration_TB
            // 
            this.duration_TB.Location = new System.Drawing.Point(239, 85);
            this.duration_TB.Margin = new System.Windows.Forms.Padding(4);
            this.duration_TB.Name = "duration_TB";
            this.duration_TB.Size = new System.Drawing.Size(242, 22);
            this.duration_TB.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "Kérés / másodperc:";
            // 
            // stepReqPerSec_TB
            // 
            this.stepReqPerSec_TB.Location = new System.Drawing.Point(327, 126);
            this.stepReqPerSec_TB.Margin = new System.Windows.Forms.Padding(4);
            this.stepReqPerSec_TB.Name = "stepReqPerSec_TB";
            this.stepReqPerSec_TB.Size = new System.Drawing.Size(132, 22);
            this.stepReqPerSec_TB.TabIndex = 20;
            // 
            // StepEditor_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 223);
            this.Controls.Add(this.panel1);
            this.Name = "StepEditor_Form";
            this.Text = "Step Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button stepOK_BTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox duration_TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stepReqPerSec_TB;
    }
}