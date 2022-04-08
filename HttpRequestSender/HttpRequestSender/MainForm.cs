using HttpRequestSender.BusinessLogic;
using HttpRequestSender.Utilities;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace StressTester
{
    public partial class StressTester_Form : Form
    {
        public StressTester_Form()
        {
            InitializeComponent();
            Logger.logGridView = logGrid_DGV;
            UpdateButtons(false);
        }

        private async void start_BTN_Click(object sender, EventArgs e)
        {
            UpdateButtons(true);
            if (ValidateValue(reqPerSec_TB.Text))
            {
                status_L.Text = "";
                SessionMetrics session = new SessionMetrics();
                SiteRequester siteRequester = new SiteRequester(URL_TB.Text, session);
                await siteRequester.GetResponseParallelBatched(1000, 100, 100);
            }
            else
            {
                status_L.Text = "Invalid value in \"Request per second\" field.";
            }
        }

        private bool ValidateValue(string text)
        {
            return int.TryParse(text, out int result);
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
    }
}
