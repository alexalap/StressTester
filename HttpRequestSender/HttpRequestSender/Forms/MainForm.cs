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
        private List<SiteRequester> siteRequesters = new List<SiteRequester>();
        private string address;
        private int tickCount = 0;
        private States manualState = States.Inactive;
        private States plannedState = States.Inactive;
        private States explorationState = States.Inactive;
        private Schedule schedule = new Schedule();
        private Dictionary<string, (int, int)> siteStructureData;
        private object lockObject = new object();

        public StressTester_Form()
        {
            InitializeComponent();
            //Logger.logGridView = logGrid_DGV;
            UpdateButtons();
        }

        private void start_BTN_Click(object sender, EventArgs e)
        {
            manual_CH.Series["Response rate"].Points.Clear();
            manualState = States.Active;
            UpdateButtons();
            if (TextBoxValidator.Validate<int>(reqPerSec_TB.Text, out int value))
            {
                address = URL_TB.Text;
                status_L.Text = "";
                session = new SessionMetrics();
                siteRequester = new SiteRequester(address, session, 1);
                siteRequester.StartMeasurement(value, null, RequesterMode.Manual);
                siteRequester.Tick += ManualStatisticsUpdate;
            }
            else
            {
                status_L.Text = "Invalid value in \"Request per second\" field.";
            }
        }

        private void pause_BTN_Click(object sender, EventArgs e)
        {
            if (manualState == States.Paused)
            {
                siteRequester.Resume();
                manualState = States.Active;
            }
            else
            {
                siteRequester.Pause();
                manualState = States.Paused;
            }
            UpdateButtons();
        }

        private void stop_BTN_Click(object sender, EventArgs e)
        {
            manualState = States.Inactive;
            siteRequester?.Stop();
            UpdateButtons();
        }


        private void UpdateButtons()
        {
            switch (manualState)
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

                    manualState = States.Inactive;

                    stop_BTN.BackColor = Color.Gray;
                    stop_BTN.ForeColor = Color.White;
                    stop_BTN.Text = "Stopped";
                    stop_BTN.Enabled = false;

                    if (session != null)
                    {
                        report_BTN.Enabled = true;
                    }
                    break;
            }
        }

        private void ManualStatisticsUpdate()
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                if (responseRate != -1)
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
        }

        private void planEditor_BTN_Click(object sender, EventArgs e)
        {
            Scheduler_Form planEditor = new Scheduler_Form();
            if (schedule.CurrentStep() != null)
            {
                schedule.Clear();
            }
            planEditor.Schedule = schedule;
            planEditor.ShowDialog();
            RefreshGrid();
            UpdatePlannedButtons();
        }

        private void report_BTN_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select folder for report files.";
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
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
            siteStructureData?.Clear();
            UpdateExplorationButtons();
            explorer_BTN.Enabled = false;
            SiteStructureAnalyzer siteStructureAnalyzer = new SiteStructureAnalyzer(explorationURL_TB.Text);
            Dictionary<string, int> result = await siteStructureAnalyzer.Analyze(exploration_CHB.Checked);
            int minValue = result.Min(x => x.Value);
            siteStructureData = result.ToDictionary(x => x.Key, x => (x.Value, (int)Math.Round(x.Value / (double)minValue)));
            foreach (string key in siteStructureData.Keys)
            {
                exloration_Grid.Rows.Add(key, siteStructureData[key].Item1, siteStructureData[key].Item2);
            }
            RefreshTotalRequestLabel();
            UpdateExplorationButtons();
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

        private void plannedStart_BTN_Click(object sender, EventArgs e)
        {
            planned_CH.Series["Response rate"].Points.Clear();
            plannedState = States.Active;
            UpdatePlannedButtons();
            address = plannedURL_TB.Text;
            status_L.Text = "";
            session = new SessionMetrics();
            siteRequester = new SiteRequester(address, session, 1);
            siteRequester.StartMeasurement(0, schedule, RequesterMode.Planned);
            siteRequester.Tick += PlannedStatisticsUpdate;
            siteRequester.OnPlanFinish += OnPlannedMeasurementFinish;
        }

        private void OnPlannedMeasurementFinish()
        {
            PlannedStatisticsUpdate();
            MethodInvoker updateVisuals = delegate
            {
                plannedState = States.Inactive;
                UpdatePlannedButtons();
                RefreshGrid();
            };
            plannedStart_BTN.Invoke(updateVisuals);
        }

        private void plannedStop_BTN_Click(object sender, EventArgs e)
        {
            plannedState = States.Inactive;
            siteRequester?.PlannedStop();
            UpdatePlannedButtons();
            schedule.Clear();
        }

        private void UpdatePlannedButtons()
        {
            switch (plannedState)
            {
                case States.Active:
                    planEditor_BTN.BackColor = Color.Gray;
                    planEditor_BTN.ForeColor = Color.White;
                    planEditor_BTN.Enabled = false;

                    plannedStart_BTN.BackColor = Color.Gray;
                    plannedStart_BTN.ForeColor = Color.White;
                    plannedStart_BTN.Text = "Running...";
                    plannedStart_BTN.Enabled = false;

                    plannedStop_BTN.BackColor = Color.OrangeRed;
                    plannedStop_BTN.ForeColor = Color.White;
                    plannedStop_BTN.Text = "Stop";
                    plannedStop_BTN.Enabled = true;

                    plannedReport_BTN.Enabled = false;
                    break;
                case States.Inactive:
                    ScheduleStep nextStep = schedule.NextStep();
                    if (nextStep != null && nextStep.StartTime > DateTime.Now)
                    {
                        plannedStart_BTN.BackColor = Color.LimeGreen;
                        plannedStart_BTN.ForeColor = Color.White;
                        plannedStart_BTN.Text = "Start";
                        plannedStart_BTN.Enabled = true;
                    }
                    else
                    {
                        plannedStart_BTN.BackColor = Color.Gray;
                        plannedStart_BTN.ForeColor = Color.White;
                        plannedStart_BTN.Text = nextStep == null ? "No plan" : "Invalid plan";
                        plannedStart_BTN.Enabled = false;
                    }

                    planEditor_BTN.BackColor = SystemColors.Window;
                    planEditor_BTN.ForeColor = Color.White;
                    planEditor_BTN.Enabled = true;

                    plannedStop_BTN.BackColor = Color.Gray;
                    plannedStop_BTN.ForeColor = Color.White;
                    plannedStop_BTN.Text = "Stopped";
                    plannedStop_BTN.Enabled = false;

                    if (session != null)
                    {
                        plannedReport_BTN.Enabled = true;
                    }
                    break;
            }
        }

        private void PlannedStatisticsUpdate()
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                if (responseRate != -1)
                {
                    DataPointCollection pointList = planned_CH.Series["Response rate"].Points;
                    pointList.AddXY(tickCount++, responseRate);
                    if (pointList.Count > 36)
                    {
                        pointList.RemoveAt(0);
                    }
                    planned_CH.ResetAutoValues();
                }
            };
                MethodInvoker updateGrid = delegate
                {
                    RefreshGrid();
                };
                planned_CH.Invoke(updateMetricsVisual);
                planGrid.Invoke(updateGrid);
            }
        }

        private void URLStart_BTN_Click(object sender, EventArgs e)
        {
            siteRequesters.Clear();
            exploration_CH.Series.Clear();
            explorationState = States.Active;
            UpdateExplorationButtons();
            status_L.Text = "";
            session = new SessionMetrics();
            foreach (string address in siteStructureData.Keys)
            {
                SiteRequester siteRequester = new SiteRequester(address, session, 1);
                siteRequester.StartMeasurement((int)explorationRequest_NUD.Value * siteStructureData[address].Item2, null, RequesterMode.Manual);
                siteRequester.AddressedTick += ExplorationStatisticsUpdate;
                siteRequesters.Add(siteRequester);
            }
        }

        private void ExplorationStatisticsUpdate(string address)
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                if (responseRate != -1)
                {
                    if (exploration_CH.Series.FindByName(address) == null)
                    {
                        Series series = new Series();
                        series.Name = address;
                        series.ChartType = SeriesChartType.Area;
                        exploration_CH.Series.Add(series);
                    }
                    DataPointCollection pointList = exploration_CH.Series[address].Points;
                    pointList.AddXY(tickCount++, responseRate);
                    if (pointList.Count > 36)
                    {
                        pointList.RemoveAt(0);
                    }
                    planned_CH.ResetAutoValues();
                }
            };
                planned_CH.Invoke(updateMetricsVisual);
            }
        }

        private void URLStop_BTN_Click(object sender, EventArgs e)
        {
            explorationState = States.Inactive;
            foreach (SiteRequester siteRequester in siteRequesters)
            {
                siteRequester?.Stop();
            }
            UpdateExplorationButtons();
        }

        private void UpdateExplorationButtons()
        {
            switch (explorationState)
            {
                case States.Active:
                    explorer_BTN.BackColor = Color.Gray;
                    explorer_BTN.ForeColor = Color.White;
                    explorer_BTN.Enabled = false;

                    URLStart_BTN.BackColor = Color.Gray;
                    URLStart_BTN.ForeColor = Color.White;
                    URLStart_BTN.Text = "Running...";
                    URLStart_BTN.Enabled = false;

                    URLStop_BTN.BackColor = Color.OrangeRed;
                    URLStop_BTN.ForeColor = Color.White;
                    URLStop_BTN.Text = "Stop";
                    URLStop_BTN.Enabled = true;

                    URLReport_BTN.Enabled = false;
                    break;
                case States.Inactive:
                    if (siteStructureData != null && siteStructureData.Count > 0)
                    {
                        URLStart_BTN.BackColor = Color.LimeGreen;
                        URLStart_BTN.ForeColor = Color.White;
                        URLStart_BTN.Text = "Start";
                        URLStart_BTN.Enabled = true;
                    }
                    else
                    {
                        URLStart_BTN.BackColor = Color.Gray;
                        URLStart_BTN.ForeColor = Color.White;
                        URLStart_BTN.Text = "Unexplored";
                        URLStart_BTN.Enabled = false;
                    }

                    explorer_BTN.BackColor = SystemColors.Window;
                    explorer_BTN.ForeColor = Color.Black;
                    explorer_BTN.Enabled = true;

                    URLStop_BTN.BackColor = Color.Gray;
                    URLStop_BTN.ForeColor = Color.White;
                    URLStop_BTN.Text = "Stopped";
                    URLStop_BTN.Enabled = false;

                    if (session != null)
                    {
                        URLReport_BTN.Enabled = true;
                    }
                    break;
            }
        }
    }
}
