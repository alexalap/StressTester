using HttpRequestSender.BusinessLogic;
using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private Schedule schedule = new Schedule();
        private Dictionary<string, (int, int)> siteStructureData;

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
                siteRequester.GetResponseParallelPeriodic(value, RequesterMode.Manual);
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

                    report_BTN.Enabled = false;
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

                    if(session != null)
                    {
                        report_BTN.Enabled = true;
                    }
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
            Scheduler_Form planEditor = new Scheduler_Form();
            planEditor.Schedule = schedule;
            planEditor.ShowDialog();
            RefreshGrid();
        }

        private void report_BTN_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select folder for report files.";
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
            {
                session.GenerateReport(folderBrowserDialog.SelectedPath);
            }
        }

        private void RefreshGrid()
        {
            planGrid.Rows.Clear();
            List<ScheduleStep> scheduler = schedule.GetSchedule();
            for (int i = 0; i < scheduler.Count; i++)
            {
                planGrid.Rows.Add(i, scheduler[i].StartTime, scheduler[i].EndTime, scheduler[i].Requests);
            }
        }

        private async void explorer_BTN_Click(object sender, EventArgs e)
        {
            exloration_Grid.Rows.Clear();
            SiteStructureAnalyzer siteStructureAnalyzer = new SiteStructureAnalyzer(explorationURL_TB.Text);
            Dictionary<string, int> result = await siteStructureAnalyzer.Analyze(exploration_CHB.Checked);
            int minValue = result.Min(x => x.Value);
            siteStructureData = result.ToDictionary(x => x.Key, x => (x.Value, (int)Math.Round(x.Value / (double)minValue)));
            foreach (string key in siteStructureData.Keys)
            {
                exloration_Grid.Rows.Add(key, siteStructureData[key].Item1, siteStructureData[key].Item2);
            }
            RefreshTotalRequestLabel();
        }

        private void RefreshTotalRequestLabel()
        {
            if (siteStructureData != null && siteStructureData.Count > 0)
            {
                int multiplierSum = siteStructureData.Sum(x => x.Value.Item2);
                totalRequest_L.Text = "(" + explorationRequest_NUD.Value * multiplierSum + " in total)";
            }
        }

        private void explorationRequest_NUD_ValueChanged(object sender, EventArgs e)
        {
            RefreshTotalRequestLabel();
        }
    }
}
