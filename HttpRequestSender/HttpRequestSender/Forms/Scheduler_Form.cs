using HttpRequestSender.BusinessLogic;
using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HttpRequestSender.Forms
{
    public partial class Scheduler_Form : Form
    {
        private Schedule schedule;

        public Schedule Schedule
        {
            get
            {
                if (schedule == null)
                {
                    schedule = new Schedule();
                }
                return schedule;
            }
            set
            {
                schedule = value;
            }
        }

        public Scheduler_Form()
        {
            InitializeComponent();
        }

        private void Scheduler_Form_Load(object sender, System.EventArgs e)
        {
            RefreshGrid();
            ResetFields();
        }

        private void RefreshGrid()
        {
            planGrid.Rows.Clear();
            List<ScheduleStep> schedule = Schedule.GetSchedule();
            for (int i = 0; i < schedule.Count; i++)
            {
                planGrid.Rows.Add(i, schedule[i].StartTime, schedule[i].EndTime, schedule[i].Requests);
            }
        }

        private void planGrid_SelectionChanged(object sender, System.EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                var row = planGrid.SelectedRows[0];
                requests_NUB.Value = int.Parse(row.Cells[3].Value.ToString());
                startTimer_TP.Value = DateTime.Parse(row.Cells[1].Value.ToString());
                endTimer_TP.Value = DateTime.Parse(row.Cells[2].Value.ToString());
            }
        }

        private void add_BTN_Click(object sender, EventArgs e)
        {
            Schedule.AddStep(startTimer_TP.Value, endTimer_TP.Value, (int)requests_NUB.Value);
            RefreshGrid();
        }

        private void edit_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.EditStep(planGrid.SelectedRows[0].Index, startTimer_TP.Value, endTimer_TP.Value, (int)requests_NUB.Value);
                ResetFields();
            }
            RefreshGrid();
        }

        private void remove_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.RemoveStep(planGrid.SelectedRows[0].Index);
            }
            RefreshGrid();
        }

        private void up_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.MoveStep(planGrid.SelectedRows[0].Index, true);
            }
            RefreshGrid();
        }

        private void down_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.MoveStep(planGrid.SelectedRows[0].Index, false);
            }
            RefreshGrid();
        }

        private void planEditorOK_BTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ResetFields()
        {
            startTimer_TP.Value = DateTime.Now.NextMinute();
            endTimer_TP.Value = DateTime.Now.NextMinute().Add(new TimeSpan(0, 0, 6, 0));
            requests_NUB.Value = 10;
        }
    }
}
