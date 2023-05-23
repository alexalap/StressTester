using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic.DataType
{
    internal class SaveStepData
    {
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
