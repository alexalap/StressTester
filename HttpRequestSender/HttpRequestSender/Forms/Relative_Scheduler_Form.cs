﻿using HttpRequestSender.BusinessLogic;
using HttpRequestSender.BusinessLogic.DataType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Loads the relative scheduler form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scheduler_Form_Load(object sender, System.EventArgs e)
        {
            RefreshGrid();
            ResetFields();
        }

        /// <summary>
        /// Refreshes the relative scheduler grid by clearing the rows and refilling them with a schedule.
        /// The selection will always be on the last row.
        /// </summary>
        private void RefreshGrid()
        {
            planGrid.Rows.Clear();
            IReadOnlyList<RelativeScheduleStep> schedule = Schedule.GetSchedule();
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
        /// Changes the values of relative Scheduler fields based on the selection change on the grid.
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
        /// Adds a new relative scheduled step and refreshes the grid.
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
        /// Edits a relative scheduled step and refreshes the grid.
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
        /// Removed a relative scheduled step and refreshes the grid.
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
        /// Moves up a row in the relative Scheduler grid.
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
        /// Moves down a row in the relative Scheduler grid.
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
        /// Closes the relative schedule editor window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planEditorOK_BTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Resets the start and finish times in the relative Scheduler fields.
        /// </summary>
        private void ResetFields()
        {
            duration_TP.Value = new DateTime(2000, 1, 1, 0, 5, 0);
            requests_NUB.Value = 10;
        }

        private void Import_BTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select schedule file";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFileDialog.Filter = "Schedule files | *.schedule";
            openFileDialog.DefaultExt = "schedule";
            if (openFileDialog.ShowDialog() == DialogResult.OK && File.Exists(openFileDialog.FileName))
            {
                List<SaveStepData> data = JsonConvert.DeserializeObject<List<SaveStepData>>(File.ReadAllText(openFileDialog.FileName));
                Schedule.Clear();
                foreach (SaveStepData step in data)
                {
                    Schedule.AddStep(step.Duration, step.Requests);
                }
                RefreshGrid();
                ResetFields();
            }
        }

        private void Export_BTN_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save schedule file";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog.Filter = "Schedule files | *.schedule";
            saveFileDialog.DefaultExt = "schedule";
            if (saveFileDialog.ShowDialog() == DialogResult.OK && !File.Exists(saveFileDialog.FileName))
            {
                List<SaveStepData> data = new List<SaveStepData>();
                foreach (RelativeScheduleStep step in Schedule.GetSchedule())
                {
                    data.Add(new SaveStepData(step));
                }

                string serialized = JsonConvert.SerializeObject(data);

                File.WriteAllText(saveFileDialog.FileName, serialized);
            }
        }
    }
}
