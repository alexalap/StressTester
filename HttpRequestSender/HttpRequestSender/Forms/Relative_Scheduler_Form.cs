using HttpRequestSender.BusinessLogic;
using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HttpRequestSender.Forms
{
    public partial class Relative_Scheduler_Form : Form
    {
        private RelativeSchedule schedule;

        public RelativeSchedule Schedule
        {
            get
            {
                if (schedule == null)
                {
                    schedule = new RelativeSchedule();
                }
                return schedule;
            }
            set
            {
                schedule = value;
            }
        }

        public Relative_Scheduler_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the scheduler form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scheduler_Form_Load(object sender, System.EventArgs e)
        {
            RefreshGrid();
            ResetFields();
        }

        /// <summary>
        /// Refreshes the scheduler grid by clearing the rows and refilling them with a schedule.
        /// The selection will always be on the last row.
        /// </summary>
        private void RefreshGrid()
        {
            planGrid.Rows.Clear();
            List<RelativeScheduleStep> schedule = Schedule.GetSchedule();
            for (int i = 0; i < schedule.Count; i++)
            {
                planGrid.Rows.Add(i, schedule[i].Duration, schedule[i].Requests);
            }
            planGrid.ClearSelection();
            if(planGrid.Rows.Count > 0)
            {
                planGrid.CurrentCell = planGrid.Rows[planGrid.Rows.Count - 1].Cells[0];
            }
        }

        /// <summary>
        /// Changes the values of Scheduler fields based on the selection change on the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planGrid_SelectionChanged(object sender, System.EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                var row = planGrid.SelectedRows[0];
                requests_NUB.Value = int.Parse(row.Cells[2].Value.ToString());
                duration_TP.Value = DateTime.Parse(row.Cells[1].Value.ToString());
            }
        }

        /// <summary>
        /// Adds a new scheduled step and refreshes the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_BTN_Click(object sender, EventArgs e)
        {
            DateTime dt = duration_TP.Value;
            TimeSpan st = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            Schedule.AddStep(st, (int)requests_NUB.Value);
            RefreshGrid();
        }

        /// <summary>
        /// Edits a scheduled step and refreshes the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edit_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                DateTime dt = duration_TP.Value;
                TimeSpan st = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                Schedule.EditStep(planGrid.SelectedRows[0].Index, st, (int)requests_NUB.Value);
                ResetFields();
            }
            RefreshGrid();
        }

        /// <summary>
        /// Removed a scheduled step and refreshes the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.RemoveStep(planGrid.SelectedRows[0].Index);
            }
            RefreshGrid();
        }

        /// <summary>
        /// Moves up a row in the Scheduler grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void up_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.MoveStep(planGrid.SelectedRows[0].Index, true);
            }
            RefreshGrid();
        }

        /// <summary>
        /// Moves down a row in the Scheduler grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void down_BTN_Click(object sender, EventArgs e)
        {
            if (planGrid.SelectedRows.Count > 0)
            {
                Schedule.MoveStep(planGrid.SelectedRows[0].Index, false);
            }
            RefreshGrid();
        }

        /// <summary>
        /// Closes the schedule editor window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planEditorOK_BTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Resets the start and finish times in the Scheduler fields.
        /// </summary>
        private void ResetFields()
        {
            duration_TP.Value = new DateTime(2000, 1, 1, 0, 5, 0);
            requests_NUB.Value = 10;
        }
    }
}
