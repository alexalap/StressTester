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

        public ScheduleStep CurrentStep()
        {
            return isStarted ? scheduleSteps.FirstOrDefault() : null;
        }

        public ScheduleStep NextStep()
        {
            return !isStarted && scheduleSteps.Count > 0 ? scheduleSteps[0] : scheduleSteps.Count > 1 ? scheduleSteps[1] : null;
        }

        public List<ScheduleStep> GetSchedule()
        {
            return scheduleSteps;
        }

        public void AddStep(DateTime startTime, DateTime endTime, int req)
        {
            scheduleSteps.Add(new ScheduleStep(startTime, endTime, req));
        }

        public void RemoveStep(int index)
        {
            scheduleSteps.RemoveAt(index);
        }

        public void MoveStep(int index, bool direction)
        {
            if (scheduleSteps.Count > 0 && (direction && index != 0 || !direction && index != scheduleSteps.Count - 1))
            {
                scheduleSteps.Swap(index, direction ? index - 1 : index + 1);
            }
        }

        public void EditStep(int index, DateTime startTime, DateTime endTime, int req)
        {
            scheduleSteps[index] = new ScheduleStep(startTime, endTime, req);
        }

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

        public void Clear()
        {
            scheduleSteps.Clear();
            isStarted = false;
        }
    }
}
