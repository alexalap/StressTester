using HttpRequestSender.BusinessLogic;
using HttpRequestSender.ErrorHandling;
using HttpRequestSender.Forms;
using HttpRequestSender.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HttpRequestSender.Forms
{
    public partial class StressTester_Form : Form
    {
        private SessionMetrics session;
        private SiteRequester siteRequester;
        private string address;
        private int tickCount = 0;

        public StressTester_Form()
        {
            InitializeComponent();
            //Logger.logGridView = logGrid_DGV;
            UpdateButtons(false);
        }

        private void start_BTN_Click(object sender, EventArgs e)
        {
            UpdateButtons(true);
            if (TextBoxValidator.Validate<int>(reqPerSec_TB.Text, out int value))
            {
                address = URL_TB.Text;
                status_L.Text = "";
                session = new SessionMetrics();
                siteRequester = new SiteRequester(address, session, 10);
                siteRequester.GetResponseParallelPeriodic(value);
                siteRequester.Tick += PeriodicStatisticsUpdate;
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

        private void PeriodicStatisticsUpdate()
        {
            MethodInvoker action = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                DataPointCollection pointList = manual_CH.Series["Response rate"].Points;
                pointList.AddXY(tickCount++, responseRate);
                if (pointList.Count > 36)
                {
                    pointList.RemoveAt(0);
                }
                manual_CH.ResetAutoValues();
            };
            manual_CH.Invoke(action);
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
