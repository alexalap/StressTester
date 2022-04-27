using HttpRequestSender.BusinessLogic;
using HttpRequestSender.ErrorHandling;
using HttpRequestSender.Forms;
using HttpRequestSender.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HttpRequestSender.Forms
{
    public partial class StressTester_Form : Form
    {
        SessionMetrics session;
        SiteRequester siteRequester;

        public StressTester_Form()
        {
            InitializeComponent();
            //Logger.logGridView = logGrid_DGV;
            UpdateButtons(false);
        }

        private async void start_BTN_Click(object sender, EventArgs e)
        {
            UpdateButtons(true);
            if (TextBoxValidator.Validate<int>(reqPerSec_TB.Text, out int value))
            {
                status_L.Text = "";
                session = new SessionMetrics();
                siteRequester = new SiteRequester(URL_TB.Text, session);
                await siteRequester.GetResponseParallel(int.MaxValue, value);
            }
            else
            {
                status_L.Text = "Invalid value in \"Request per second\" field.";
            }
        }

        private void stop_BTN_Click(object sender, EventArgs e)
        {
            UpdateButtons(false);
        }


        private void UpdateButtons(bool toggle)
        {
            if (toggle)
            {
                start_BTN.BackColor = Color.Gray;
                start_BTN.ForeColor = Color.White;
                start_BTN.Text = "Running...";
                start_BTN.Enabled = false;

                stop_BTN.BackColor = Color.OrangeRed;
                stop_BTN.ForeColor = Color.White;
                stop_BTN.Text = "Stop";
                stop_BTN.Enabled = true;
            }
            else
            {
                start_BTN.BackColor = Color.LimeGreen;
                start_BTN.ForeColor = Color.White;
                start_BTN.Text = "Start";
                start_BTN.Enabled = true;

                stop_BTN.BackColor = Color.Gray;
                stop_BTN.ForeColor = Color.White;
                stop_BTN.Text = "Stopped";
                stop_BTN.Enabled = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            
        }

        private void planEditor_BTN_Click(object sender, EventArgs e)
        {
            PlanEditor_Form planeditor = new PlanEditor_Form();
        }
    }
}
