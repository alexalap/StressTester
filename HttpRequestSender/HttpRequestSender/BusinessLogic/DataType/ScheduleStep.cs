using System;

namespace HttpRequestSender.BusinessLogic.DataType
{
    public class ScheduleStep
    {
        /// <summary>
        /// The start date time of a scheduled metric step.
        /// </summary>
        public DateTime StartTime { get; private set; }
        /// <summary>
        /// The end date time of a scheduled metric step.
        /// </summary>
        public DateTime EndTime { get; private set; }
        /// <summary>
        /// The number of requests of a scheduled metric step.
        /// </summary>
        public int Requests { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="startTime"> The start time of a step. </param>
        /// <param name="endTime"> The end time of the step. </param>
        /// <param name="requests"> The number of requests. </param>
        public ScheduleStep(DateTime startTime, DateTime endTime, int requests)
        {
            StartTime = startTime;
            EndTime = endTime;
            Requests = requests;
        }
    }
}
