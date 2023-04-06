using System;

namespace HttpRequestSender.BusinessLogic.DataType
{
    public class ScheduleStep
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int Requests { get; private set; }

        public ScheduleStep(DateTime startTime, DateTime endTime, int requests)
        {
            StartTime = startTime;
            EndTime = endTime;
            Requests = requests;
        }
    }
}
