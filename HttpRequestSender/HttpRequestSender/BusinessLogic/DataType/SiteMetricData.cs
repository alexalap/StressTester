using System;
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
        private double duration = 0;
        private bool closed = false;

        private Dictionary<string, int> responses = new Dictionary<string, int>();

        private Dictionary<string, int> responsesLastSec = new Dictionary<string, int>();

        public double Duration
        {
            get
            {
                if(duration == 0)
                {
                    return (DateTime.Now - start).TotalMilliseconds;
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

        public SiteMetricData(string address)
        {
            this.address = address;
            this.start = DateTime.Now;
        }

        public void AddResponse(string statusCode)
        {
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
            Duration = (DateTime.Now - start).TotalMilliseconds;
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
    }
}
