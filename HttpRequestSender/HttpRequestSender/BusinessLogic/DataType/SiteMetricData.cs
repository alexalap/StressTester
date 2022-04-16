using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpRequestSender.BusinessLogic.DataType
{
    class SiteMetricData
    {
        private static int ID = 0;
        
        private int id = ID++;
        private string address;
        private DateTime start;
        private int responseCount;
        private double duration = 0;
        private bool closed = false;

        private Dictionary<string, int> responses = new Dictionary<string, int>();

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
        }

        public float ResponseTimeRate()
        {
            return responses["OK"] / ((float)Duration / 1000);
        }

        public void Close()
        {
            if (!closed)
            {
                closed = true;
                Duration = (DateTime.Now - start).TotalMilliseconds;
            }
        }
    }
}
