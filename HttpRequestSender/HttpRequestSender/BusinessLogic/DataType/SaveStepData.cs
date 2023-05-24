using System;

namespace HttpRequestSender.BusinessLogic.DataType
{
    internal class SaveStepData
    {
        /// <summary>
        /// Duration of the relative measurement step.
        /// </summary>
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// The number of requests of a scheduled metric step.
        /// </summary>
        public int Requests { get; set; }

        public SaveStepData()
        {
        }

        public SaveStepData(RelativeScheduleStep step)
        {
            Duration = step.Duration;
            Requests = step.Requests;
        }
    }
}
