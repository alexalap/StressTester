using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpRequestSender.BusinessLogic
{
    public class Schedule
    {
        private List<ScheduleStep> scheduleSteps = new List<ScheduleStep>();
        private bool isStarted = false;

        /// <summary>
        /// Gets the current scheduled metric step.
        /// </summary>
        /// <returns>Returns the current scheduled metric step if </returns>
        public ScheduleStep CurrentStep()
        {
            return isStarted ? scheduleSteps.FirstOrDefault() : null;
        }

        /// <summary>
        /// Gets the next scheduled metric step.
        /// </summary>
        /// <returns>Returns the 1st step if the schedule hasn't started yet or returns the 2nd step or null.</returns>
        public ScheduleStep NextStep()
        {
            return !isStarted && scheduleSteps.Count > 0 ? scheduleSteps[0] : scheduleSteps.Count > 1 ? scheduleSteps[1] : null;
        }

        /// <summary>
        /// Gets the metric schedule.
        /// </summary>
        /// <returns>Returns a list of the scheduled metric steps.</returns>
        public List<ScheduleStep> GetSchedule()
        {
            return scheduleSteps;
        }

        /// <summary>
        /// Adds a new step into the list of scheduled steps.
        /// </summary>
        /// <param name="startTime">Start time of the scheduled step.</param>
        /// <param name="endTime">End time of the scheduled step.</param>
        /// <param name="req">Number of requests to be sent.</param>
        public void AddStep(DateTime startTime, DateTime endTime, int req)
        {
            scheduleSteps.Add(new ScheduleStep(startTime, endTime, req));
        }

        /// <summary>
        /// Removes a scheduled step from the list of scheduled steps.
        /// </summary>
        /// <param name="index">Index of scheduled step to be removed.</param>
        public void RemoveStep(int index)
        {
            scheduleSteps.RemoveAt(index);
        }

        /// <summary>
        /// Moves a scheduled step to a different place on the list.
        /// </summary>
        /// <param name="index">Index of scheduled step to be moved.</param>
        /// <param name="direction">Movement direction of the scheduled step.</param>
        public void MoveStep(int index, bool direction)
        {
            if (scheduleSteps.Count > 0 && (direction && index != 0 || !direction && index != scheduleSteps.Count - 1))
            {
                scheduleSteps.Swap(index, direction ? index - 1 : index + 1);
            }
        }

        /// <summary>
        /// Edits a scheduled step.
        /// </summary>
        /// <param name="index">Index of scheduled step to be edited.</param>
        /// <param name="startTime">Start time of the scheduled step.</param>
        /// <param name="endTime">End time of the scheduled step.</param>
        /// <param name="req">Number of requests to be sent.</param>
        public void EditStep(int index, DateTime startTime, DateTime endTime, int req)
        {
            scheduleSteps[index] = new ScheduleStep(startTime, endTime, req);
        }

        /// <summary>
        /// Starts the schedule.
        /// </summary>
        public void Step()
        {
            if (scheduleSteps.Count > 0 && isStarted == false)
            {
                isStarted = true;
            }
            else if (scheduleSteps.Count > 0)
            {
                scheduleSteps.RemoveAt(0);
            }
        }

        /// <summary>
        /// Clears the list of scheduled steps.
        /// </summary>
        public void Clear()
        {
            scheduleSteps.Clear();
            isStarted = false;
        }
    }
}
