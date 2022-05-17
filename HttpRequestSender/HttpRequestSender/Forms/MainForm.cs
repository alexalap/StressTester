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
        private enum States
        {
            Active,
            Paused,
            Inactive
        }

        private SessionMetrics session;
        private SiteRequester siteRequester;
        private string address;
        private int tickCount = 0;
        private States state = States.Inactive;

        public StressTester_Form()
        {
            InitializeComponent();
            //Logger.logGridView = logGrid_DGV;
            UpdateButtons();
        }

        private void start_BTN_Click(object sender, EventArgs e)
        {
            manual_CH.Series["Response rate"].Points.Clear();
            state = States.Active;
            UpdateButtons();
            if (TextBoxValidator.Validate<int>(reqPerSec_TB.Text, out int value))
            {
                address = URL_TB.Text;
                status_L.Text = "";
                session = new SessionMetrics();
                siteRequester = new SiteRequester(address, session, 1);
                siteRequester.GetResponseParallelPeriodic(value);
                siteRequester.Tick += PeriodicStatisticsUpdate;
            }
            else
            {
                status_L.Text = "Invalid value in \"Request per second\" field.";
            }
        }

        private void pause_BTN_Click(object sender, EventArgs e)
        {
            if(state == States.Paused)
            {
                siteRequester.Resume();
                state = States.Active;
            }
            else
            {
                siteRequester.Pause();
                state = States.Paused;
            }
            UpdateButtons();
        }

        private void stop_BTN_Click(object sender, EventArgs e)
        {
            state = States.Inactive;
            siteRequester?.Stop();
            UpdateButtons();
        }


        private void UpdateButtons()
        {
            switch (state)
            {
                case States.Active:
                    start_BTN.BackColor = Color.Gray;
                    start_BTN.ForeColor = Color.White;
                    start_BTN.Text = "Running...";
                    start_BTN.Enabled = false;

                    pause_BTN.BackColor = Color.Orange;
                    pause_BTN.ForeColor = Color.White;
                    pause_BTN.Text = "Pause";
                    pause_BTN.Enabled = true;

                    stop_BTN.BackColor = Color.OrangeRed;
                    stop_BTN.ForeColor = Color.White;
                    stop_BTN.Text = "Stop";
                    stop_BTN.Enabled = true;
                    break;
                case States.Paused:
                    pause_BTN.Text = "Resume";
                    break;
                case States.Inactive:
                    start_BTN.BackColor = Color.LimeGreen;
                    start_BTN.ForeColor = Color.White;
                    start_BTN.Text = "Start";
                    start_BTN.Enabled = true;

                    pause_BTN.BackColor = Color.Gray;
                    pause_BTN.ForeColor = Color.White;
                    pause_BTN.Text = "Pause";
                    pause_BTN.Enabled = false;

                    state = States.Inactive;

                    stop_BTN.BackColor = Color.Gray;
                    stop_BTN.ForeColor = Color.White;
                    stop_BTN.Text = "Stopped";
                    stop_BTN.Enabled = false;
                    break;
            }
        }

        private void PeriodicStatisticsUpdate()
        {
            MethodInvoker updateMetricsVisual = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                if(responseRate != -1)
                {
                    DataPointCollection pointList = manual_CH.Series["Response rate"].Points;
                    pointList.AddXY(tickCount++, responseRate);
                    if (pointList.Count > 36)
                    {
                        pointList.RemoveAt(0);
                    }
                    manual_CH.ResetAutoValues();
                }
            };
            MethodInvoker updateNumberOfRes = delegate
            {
                double okResponseCount = session.GetOKResponseCount(address);
                if (okResponseCount != -1)
                {
                    numberOfRes_L.Text = okResponseCount.ToString();
                }
            };

            MethodInvoker updateAverageResTime = delegate
            {
                double duration = session.GetDuration(address);
                double okResponseCount = session.GetOKResponseCount(address);
                if (duration != -1 && okResponseCount != -1)
                {
                    averageResTime_L.Text = duration / okResponseCount + " ms";
                }
            };
            manual_CH.Invoke(updateMetricsVisual);
            numberOfRes_L.Invoke(updateNumberOfRes);
            averageResTime_L.Invoke(updateAverageResTime);
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
