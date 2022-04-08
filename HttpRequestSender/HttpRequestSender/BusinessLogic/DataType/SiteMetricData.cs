using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddResponse()
        {
            responseCount++;
        }

        public float ResponseTimeRate()
        {
            return responseCount / ((float)Duration / 1000);
        }

        public void Close()
        {
            Duration = (DateTime.Now - start).TotalMilliseconds;
        }
    }
}
