using HttpRequestSender.BusinessLogic;
using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.ErrorHandling;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HttpRequestSender.Forms
{
    public partial class S : Form
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
        private States manualState = States.Inactive;
        private States plannedState = States.Inactive;
        private States explorationState = States.Inactive;
        private Schedule schedule = new Schedule();
        private RelativeSchedule relativeSchedule = new RelativeSchedule();
        private Dictionary<string, (int, int)> siteStructureData;
        private object lockObject = new object();
        private string planStatus = "";
        private Dictionary<string, int> urlTickCount = new Dictionary<string, int>();

        public S()
        {
            InitializeComponent();
            Logger.logGridView = logs_DGV;
            UpdateButtons();
        }

        internal SiteRequester SiteRequester
        {
            get => default;
            set
            {
            }
        }

        internal SessionMetrics SessionMetrics
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Starts the measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                siteRequester.StartMeasurement(value, null, null, RequesterMode.Manual);
                siteRequester.Tick += ManualStatisticsUpdate;
            }
            else
            {
                status_L.Text = "Invalid value in \"Request per second\" field.";
            }
        }

        /// <summary>
        /// Pauses the measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Stops the measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_BTN_Click(object sender, EventArgs e)
        {
            manualState = States.Inactive;
            siteRequester?.Stop();
            UpdateButtons();
        }

        /// <summary>
        /// Updates the buttons according to the measurement states.
        /// </summary>
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

        /// <summary>
        /// Updates the statistics of manual measurement.
        /// </summary>
        private void ManualStatisticsUpdate(int tick)
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
            {
                float responseRate = session.ResponseTimeRateLastSec(address);
                if (responseRate != -1)
                {
                    DataPointCollection pointList = manual_CH.Series["Response rate"].Points;
                    InsertGapIfNeeded(tick, pointList);
                    pointList.AddXY(tick, responseRate);
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
                    if (session.GetOKResponseCount(address) != -1)
                    {
                        averageResTime_L.Text = Math.Round(session.ResponseTimeRate(address)) + " ms";
                    }
                };
                manual_CH.Invoke(updateMetricsVisual);
                numberOfRes_L.Invoke(updateNumberOfRes);
                averageResTime_L.Invoke(updateAverageResTime);
            }
        }

        /// <summary>
        /// Opens the Absolute Scheduler window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planEditor_BTN_Click(object sender, EventArgs e)
        {
            Scheduler_Form planEditor = new Scheduler_Form();
            if (schedule.CurrentStep() != null)
            {
                schedule.Clear();
            }
            planEditor.Schedule = schedule;
            planEditor.ShowDialog();
            RefreshGrid("0");
            UpdatePlannedButtons();
        }

        /// <summary>
        /// Opens the Relative Scheduler window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelativePlanEditor_BTN_Click(object sender, EventArgs e)
        {
            Relative_Scheduler_Form planEditor = new Relative_Scheduler_Form();
            if (relativeSchedule.CurrentStep() != null)
            {
                relativeSchedule.Clear();
            }
            planEditor.Schedule = relativeSchedule;
            planEditor.ShowDialog();
            RefreshRelativeGrid("0");
            UpdateRelativePlannedButtons();
        }

        /// <summary>
        /// Creates a report of the measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Refreshes the absolute scheduler grid by clearing the rows and refilling them with a schedule.
        /// Gives a status to the first scheduled step in the plan grid.
        /// </summary>
        private void RefreshGrid(string status)
        {
            planGrid.Rows.Clear();
            List<ScheduleStep> scheduler = schedule.GetSchedule();
            if (scheduler.Count > 0 && scheduler.First().EndTime < DateTime.Now)
            {
                scheduler.RemoveAt(0);
            }
            for (int i = 0; i < scheduler.Count; i++)
            {
                planGrid.Rows.Add(i == 0 ? status : i.ToString(), scheduler[i].StartTime, scheduler[i].EndTime, scheduler[i].Requests);
            }
        }

        /// <summary>
        /// Refreshes the relative scheduler grid by clearing the rows and refilling them with a schedule.
        /// </summary>
        private void RefreshRelativeGrid(string status)
        {
            relativePlanGrid.Rows.Clear();
            IReadOnlyList<RelativeScheduleStep> scheduler = relativeSchedule.GetSchedule();
            int currentIndex = relativeSchedule.GetCurrentStepIndex();
            for (int i = 0; i < scheduler.Count; i++)
            {
                relativePlanGrid.Rows.Add(i == currentIndex ? status : i.ToString(), scheduler[i].Duration, scheduler[i].Requests);
            }
            Debug.WriteLine(status);
        }

        /// <summary>
        /// Explores a website to look for more addresses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void explorer_BTN_Click(object sender, EventArgs e)
        {
            session = new SessionMetrics();
            exloration_Grid.Rows.Clear();
            siteStructureData?.Clear();
            UpdateExplorationButtons();
            explorer_BTN.Enabled = false;
            SiteStructureAnalyzer siteStructureAnalyzer = new SiteStructureAnalyzer(explorationURL_TB.Text);
            Dictionary<string, int> result = await siteStructureAnalyzer.Analyze(exploration_CHB.Checked);
            if (result.Count == 0)
            {
                UpdateExplorationButtons();
                return;
            }
            int minValue = result.Min(x => x.Value);
            siteStructureData = result.ToDictionary(x => x.Key, x => (x.Value, (int)Math.Round(x.Value / (double)minValue)));
            foreach (string key in siteStructureData.Keys)
            {
                exloration_Grid.Rows.Add(key, siteStructureData[key].Item1, siteStructureData[key].Item2);
            }
            RefreshTotalRequestLabel();
            UpdateExplorationButtons();
        }

        /// <summary>
        /// Refreshes the total number of requests label.
        /// </summary>
        private void RefreshTotalRequestLabel()
        {
            if (siteStructureData != null && siteStructureData.Count > 0)
            {
                int multiplierSum = siteStructureData.Sum(x => x.Value.Item2);
                totalRequest_L.Text = "(" + explorationRequest_NUD.Value * multiplierSum + " in total)";
            }
        }

        /// <summary>
        /// Refreshes the total number of requests label if the NUD value has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explorationRequest_NUD_ValueChanged(object sender, EventArgs e)
        {
            RefreshTotalRequestLabel();
        }

        /// <summary>
        /// Starts an absolute  planned measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plannedStart_BTN_Click(object sender, EventArgs e)
        {
            planned_CH.Series["Response rate"].Points.Clear();
            plannedState = States.Active;
            UpdatePlannedButtons();
            address = plannedURL_TB.Text;
            status_L.Text = "";
            planStatus = "Waiting";
            session = new SessionMetrics();
            siteRequester = new SiteRequester(address, session, 1);
            siteRequester.StartMeasurement(0, schedule, null, RequesterMode.PlannedAbsolute);
            siteRequester.Tick += PlannedStatisticsUpdate;
            siteRequester.PlanTick += PlannedStatusUpdate;
            siteRequester.OnPlanFinish += OnPlannedMeasurementFinish;
            RefreshGrid(planStatus);
        }

        /// <summary>
        /// Starts a relative planned measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelativePlannedStart_BTN_Click(object sender, EventArgs e)
        {
            relativePlanned_CH.Series["Response rate"].Points.Clear();
            plannedState = States.Active;
            UpdateRelativePlannedButtons();
            address = relativePlannedURL_TB.Text;
            status_L.Text = "";
            planStatus = "Running";
            session = new SessionMetrics();
            siteRequester = new SiteRequester(address, session, 1);
            siteRequester.StartMeasurement(0, null, relativeSchedule, RequesterMode.PlannedRelative);
            siteRequester.Tick += RelativePlannedStatisticsUpdate;
            siteRequester.PlanTick += RelativePlannedStatusUpdate;
            siteRequester.OnPlanFinish += (i) => relativeSchedule.Reset();
            siteRequester.OnPlanFinish += OnRelativePlannedMeasurementFinish;
            RefreshRelativeGrid(planStatus);
        }

        /// <summary>
        /// Updates the statistics and UI of the absolute planned measurement.
        /// </summary>
        private void OnPlannedMeasurementFinish(int tick)
        {
            PlannedStatisticsUpdate(tick);
            MethodInvoker updateVisuals = delegate
            {
                plannedState = States.Inactive;
                planStatus = "Stopped";
                UpdatePlannedButtons();
                RefreshGrid("0");
            };
            plannedStart_BTN.Invoke(updateVisuals);
        }

        /// <summary>
        /// Updates the statistics and UI of the relative planned measurement.
        /// </summary>
        private void OnRelativePlannedMeasurementFinish(int tick)
        {
            RelativePlannedStatisticsUpdate(tick);
            MethodInvoker updateVisuals = delegate
            {
                plannedState = States.Inactive;
                planStatus = "Stopped";
                UpdateRelativePlannedButtons();
                RefreshRelativeGrid("0");
            };
            relativePlannedStart_BTN.Invoke(updateVisuals);
        }

        /// <summary>
        /// Stops an absolute planned measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plannedStop_BTN_Click(object sender, EventArgs e)
        {
            plannedState = States.Inactive;
            siteRequester?.PlannedStop();
            planStatus = "Idle";
            UpdatePlannedButtons();
            schedule.Clear();
            RefreshGrid(planStatus);
        }

        /// <summary>
        /// Stops a relative planned measurement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelativePlannedStop_BTN_Click(object sender, EventArgs e)
        {
            plannedState = States.Inactive;
            siteRequester?.RelativePlannedStop();
            planStatus = "Idle";
            UpdateRelativePlannedButtons();
            relativeSchedule.Clear();
            RefreshRelativeGrid(planStatus);
        }

        /// <summary>
        /// Updates the absolute planned buttons according to the planned measurement states.
        /// </summary>
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
                    planEditor_BTN.ForeColor = Color.Black;
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

        /// <summary>
        /// Updates the relative planned buttons according to the planned measurement states.
        /// </summary>
        private void UpdateRelativePlannedButtons()
        {
            switch (plannedState)
            {
                case States.Active:
                    relativePlanEditor_BTN.BackColor = Color.Gray;
                    relativePlanEditor_BTN.ForeColor = Color.White;
                    relativePlanEditor_BTN.Enabled = false;

                    relativePlannedStart_BTN.BackColor = Color.Gray;
                    relativePlannedStart_BTN.ForeColor = Color.White;
                    relativePlannedStart_BTN.Text = "Running...";
                    relativePlannedStart_BTN.Enabled = false;

                    relativePlannedStop_BTN.BackColor = Color.OrangeRed;
                    relativePlannedStop_BTN.ForeColor = Color.White;
                    relativePlannedStop_BTN.Text = "Stop";
                    relativePlannedStop_BTN.Enabled = true;

                    relativePlannedReport_BTN.Enabled = false;
                    break;
                case States.Inactive:
                    RelativeScheduleStep nextStep = relativeSchedule.NextStep();
                    if (nextStep != null)
                    {
                        relativePlannedStart_BTN.BackColor = Color.LimeGreen;
                        relativePlannedStart_BTN.ForeColor = Color.White;
                        relativePlannedStart_BTN.Text = "Start";
                        relativePlannedStart_BTN.Enabled = true;
                    }
                    else
                    {
                        relativePlannedStart_BTN.BackColor = Color.Gray;
                        relativePlannedStart_BTN.ForeColor = Color.White;
                        relativePlannedStart_BTN.Text = nextStep == null ? "No plan" : "Invalid plan";
                        relativePlannedStart_BTN.Enabled = false;
                    }

                    relativePlanEditor_BTN.BackColor = SystemColors.Window;
                    relativePlanEditor_BTN.ForeColor = Color.Black;
                    relativePlanEditor_BTN.Enabled = true;

                    relativePlannedStop_BTN.BackColor = Color.Gray;
                    relativePlannedStop_BTN.ForeColor = Color.White;
                    relativePlannedStop_BTN.Text = "Stopped";
                    relativePlannedStop_BTN.Enabled = false;

                    if (session != null)
                    {
                        relativePlannedReport_BTN.Enabled = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Updates the statistics of the absolute planned measurement.
        /// </summary>
        private void PlannedStatisticsUpdate(int tick)
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
                {
                    float responseRate = session.ResponseTimeRateLastSec(address);
                    if (responseRate != -1)
                    {
                        DataPointCollection pointList = planned_CH.Series["Response rate"].Points;
                        InsertGapIfNeeded(tick, pointList);
                        pointList.AddXY(tick, responseRate);
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

        /// <summary>
        /// Updates the statistics of the relative planned measurement.
        /// </summary>
        private void RelativePlannedStatisticsUpdate(int tick)
        {
            lock (lockObject)
            {
                MethodInvoker updateMetricsVisual = delegate
                {
                    float responseRate = session.ResponseTimeRateLastSec(address);
                    if (responseRate != -1)
                    {
                        DataPointCollection pointList = relativePlanned_CH.Series["Response rate"].Points;
                        InsertGapIfNeeded(tick, pointList);
                        pointList.AddXY(tick, responseRate);
                        if (pointList.Count > 36)
                        {
                            pointList.RemoveAt(0);
                        }
                        relativePlanned_CH.ResetAutoValues();
                    }
                };
                relativePlanned_CH.Invoke(updateMetricsVisual);
            }
        }

        /// <summary>
        /// Updates the status of the absolute schedule.
        /// </summary>
        private void PlannedStatusUpdate(int tick)
        {
            planStatus = siteRequester?.Status ?? "";
            MethodInvoker updateGrid = delegate
            {
                RefreshGrid(planStatus);
            };
            planGrid.Invoke(updateGrid);
        }

        /// <summary>
        /// Updates the status of the relative schedule.
        /// </summary>
        private void RelativePlannedStatusUpdate(int tick)
        {
            planStatus = siteRequester?.Status ?? "";
            MethodInvoker updateGrid = delegate
            {
                RefreshRelativeGrid(planStatus);
            };
            relativePlanGrid.Invoke(updateGrid);
        }

        /// <summary>
        /// Starts the URL exploration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URLStart_BTN_Click(object sender, EventArgs e)
        {
            siteRequesters.Clear();
            exploration_CH.Series.Clear();
            urlTickCount.Clear();
            explorationState = States.Active;
            UpdateExplorationButtons();
            status_L.Text = "";
            foreach (string address in siteStructureData.Keys)
            {
                SiteRequester siteRequester = new SiteRequester(address, session, 1);
                siteRequester.StartMeasurement((int)explorationRequest_NUD.Value * siteStructureData[address].Item2, null, null, RequesterMode.Exploration);
                siteRequester.AddressedTick += ExplorationStatisticsUpdate;
                siteRequesters.Add(siteRequester);
            }
        }

        /// <summary>
        /// Updates the statistics of exploration measurement.
        /// </summary>
        /// <param name="address"></param>
        private void ExplorationStatisticsUpdate(int tick, string address)
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
                        series.ChartType = SeriesChartType.Line;
                        exploration_CH.Series.Add(series);
                    }
                    DataPointCollection pointList = exploration_CH.Series[address].Points;
                    InsertGapIfNeeded(tick, pointList);
                    pointList.AddXY(tick, responseRate);
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

        /// <summary>
        /// Inserts a gap into the planned chart when the requests are on-hold.
        /// </summary>
        /// <param name="tick"></param>
        /// <param name="pointList"></param>
        private static void InsertGapIfNeeded(int tick, DataPointCollection pointList)
        {
            if (pointList != null && pointList.Count > 0)
            {
                double lastX = pointList.Last().XValue;
                if (lastX != tick - 1)
                {
                    pointList.AddXY(lastX + 1, Double.NaN);
                }
            }
        }

        /// <summary>
        /// Stops the URL exploration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URLStop_BTN_Click(object sender, EventArgs e)
        {
            explorationState = States.Inactive;
            foreach (SiteRequester siteRequester in siteRequesters)
            {
                siteRequester?.Stop();
            }
            UpdateExplorationButtons();
        }

        /// <summary>
        /// Updates the exploration buttons according to the exploration measurement states.
        /// </summary>
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

                    if (session != null && !session.IsEmpty())
                    {
                        URLReport_BTN.Enabled = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Clears the selection in the absolute plan grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planGrid_SelectionChanged(object sender, EventArgs e)
        {
            planGrid.ClearSelection();
        }

        /// <summary>
        /// Clears the selection in the relative plan grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelativePlanGrid_SelectionChanged(object sender, EventArgs e)
        {
            relativePlanGrid.ClearSelection();
        }
    }
}
