using System;

namespace HttpRequestSender.BusinessLogic.DataType
{
    public class RelativeScheduleStep
    {
        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// The number of requests of a scheduled metric step.
        /// </summary>
        public int Requests { get; private set; }

        public RelativeScheduleStep(TimeSpan duration, int requests)
        {
            Duration = duration;
            Requests = requests;
        }
    }
}
