using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    class TestSite
    {
        private HttpClient client;
        private string address;

        public TestSite(string address)
        {
            this.address = address;
        }

        public async Task GetResponse()
        {
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            try
            {
                HttpResponseMessage response = await client.GetAsync(address).ConfigureAwait(false);

                stopper.Stop();
                Logger.Log(DateTime.Now, "Response received.", stopper.ElapsedMilliseconds);
            }
            catch(Exception e)
            {
                Logger.Log(DateTime.Now, "ERROR: A problem occured while asking for response.\n" + e.Message, stopper.ElapsedMilliseconds);
            }
        }

        public async Task GetResponseParallel(int numberOfRequests)
        {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfRequests; i++)
            {
                tasks.Add(GetResponse());
            }
            await Task.WhenAll(tasks);
        }
    }
}
