﻿using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpRequestSender.BusinessLogic
{
    public class RelativeSchedule
    {
        private List<RelativeScheduleStep> scheduleSteps = new List<RelativeScheduleStep>();
        private int currentStep = -1;

        private bool IsStarted => currentStep > -1;

        /// <summary>
        /// Gets the current scheduled metric step.
        /// </summary>
        /// <returns>Returns the current scheduled metric step if it is started, otherwise it returns null. </returns>
        public RelativeScheduleStep CurrentStep()
        {
            return IsStarted ? scheduleSteps[currentStep] : null;
        }

        /// <summary>
        /// Gets the next scheduled metric step.
        /// Returns the 1st step if the schedule hasn't started yet or returns the next step or null.
        /// 
        /// Stepping the schedule is done by removing the first element. The next step always will be the second, if it is started.
        /// If it has not been started yet, then it returns the first step. Otherwise it returns null.
        /// </summary>
        /// <returns>Returns the 1st step if the schedule hasn't started yet or returns the next step or null.</returns>
        public RelativeScheduleStep NextStep()
        {
            return scheduleSteps.Count > currentStep + 1 ? scheduleSteps[currentStep + 1] : null;
        }

        /// <summary>
        /// Gets the metric schedule.
        /// </summary>
        /// <returns>Returns a list of the scheduled metric steps.</returns>
        public IReadOnlyList<RelativeScheduleStep> GetSchedule()
        {
            return scheduleSteps;
        }

        /// <summary>
        /// Adds a new step into the list of scheduled steps.
        /// </summary>
        /// <param name="duration">Duration of step. </param>
        /// <param name="req">Number of requests to be sent. </param>
        public void AddStep(TimeSpan duration, int req)
        {
            scheduleSteps.Add(new RelativeScheduleStep(duration, req));
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
        /// <param name="req">Number of requests to be sent.</param>
        public void EditStep(int index, TimeSpan duration, int req)
        {
            scheduleSteps[index] = new RelativeScheduleStep(duration, req);
        }

        /// <summary>
        /// Steps the schedule.
        /// 
        /// If the schedule has not been started, it starts the schedule. Otherwise it removes the first step.
        /// </summary>
        public void Step()
        {
            currentStep += currentStep + 1 == scheduleSteps.Count ? 0 : 1;
        }

        /// <summary>
        /// Clears the list of scheduled steps.
        /// </summary>
        public void Clear()
        {
            Reset();
            scheduleSteps.Clear();
        }

        public void Reset()
        {
            currentStep = -1;
        }

        internal int GetCurrentStepIndex()
        {
            return currentStep;
        }
    }
}
