using HttpRequestSender.ErrorHandling;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace HttpRequestSender.BusinessLogic.DataType
{
    class SiteMetricData
    {
        private static int ID = 0;
        
        private readonly int id = ID++;
        private readonly string address;
        private DateTime start;
        private DateTime durationLastUpdate;
        private double duration = 0;
        private bool closed = false;
        private bool paused = false;

        private Dictionary<string, int> responses = new Dictionary<string, int>();

        private Dictionary<string, int> responsesLastSec = new Dictionary<string, int>();

        public double Duration
        {
            get
            {
                if(duration == 0)
                {
                    return (DateTime.Now - durationLastUpdate).TotalMilliseconds;
                }
                else
                {
                    return duration;
                }
            }
            private set
            {
                duration = value;
            }
        }

        public int OKResponseCount
        {
            get
            {
                return responses["OK"];
            }
        }

        public int ErrorResponseCount
        {
            get
            {
                int errorCount = 0;
                foreach (string key in responses.Keys)
                {
                    if(key != "OK")
                    {
                        errorCount += responses[key];
                    }
                }
                return errorCount;
            }
        }

        public string Address => address;

        public DateTime DurationLastUpdate {
            get
            {
                return ResetDurationLastUpdate();
            }
            private set => durationLastUpdate = value;
        }

        private DateTime ResetDurationLastUpdate()
        {
            DateTime temp = durationLastUpdate;
            durationLastUpdate = DateTime.Now;
            duration += (durationLastUpdate - temp).TotalMilliseconds;
            return temp;
        }

        public SiteMetricData(string address)
        {
            this.address = address;
            start = DateTime.Now;
            DurationLastUpdate = DateTime.Now;
        }

        public void AddResponse(string statusCode)
        {
            if(closed || paused)
            {
                throw new IncorrectMetricCallException(nameof(AddResponse), address);
            }
            if (responses.ContainsKey(statusCode))
            {
                responses[statusCode]++;
            }
            else
            {
                responses.Add(statusCode, 1);
            }
            if (responsesLastSec.ContainsKey(statusCode))
            {
                responsesLastSec[statusCode]++;
            }
            else
            {
                responsesLastSec.Add(statusCode, 1);
            }
            Duration = (DateTime.Now - DurationLastUpdate).TotalMilliseconds;
        }

        public float ResponseTimeRate()
        {
            return OKResponseCount / ((float)Duration / 1000);
        }

        public float ErrorTimeRate()
        {
            return ErrorResponseCount / ((float)Duration / 1000);
        }

        public float ResponseTimeRateLastSec()
        {
            float res = responsesLastSec["OK"] / ((float)Duration / 1000);
            responsesLastSec.Remove("OK");
            return res;
        }

        public float ErrorTimeRateLastSec()
        {
            int errorCount = 0;
            foreach (string key in responsesLastSec.Keys)
            {
                if (key != "OK")
                {
                    errorCount += responsesLastSec[key];
                    responsesLastSec.Remove(key);
                }
            }
            return errorCount / ((float)Duration / 1000);
        }

        public void Close()
        {
            if (!closed)
            {
                closed = true;
            }
        }

        public void Pause()
        {
            if (closed || paused)
            {
                throw new IncorrectMetricCallException(nameof(Pause), address);
            }
            ResetDurationLastUpdate();
        }

        public void UnPause()
        {
            if (closed || !paused)
            {
                throw new IncorrectMetricCallException(nameof(UnPause), address);
            }
            durationLastUpdate = DateTime.Now;
        }
    }
}
