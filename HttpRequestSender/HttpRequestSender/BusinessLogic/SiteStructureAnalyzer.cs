using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HttpRequestSender.BusinessLogic
{
    internal class SiteStructureAnalyzer
    {
        private HttpClient client = new HttpClient();
        private string address;

        public SiteStructureAnalyzer(string address)
        {
            this.address = address.TrimEnd('/');
        }

        /// <summary>
        /// Analyzes a site's HTML document. If the analyzing is set to be recursive, it also analyzes the side addresses of the root address.
        /// </summary>
        /// <param name="recursive"> Checks if the analyzation should be recursive. </param>
        /// <returns> Returns the result list of the addresses and the number of times are linked. </returns>
        public async Task<Dictionary<string, int>> Analyze(bool recursive)
        {
            List<string> checkList = new List<string>();
            Dictionary<string, int> endResult = new Dictionary<string, int>();
            await AnalyzeSite(checkList, address, address, recursive, endResult);
            return endResult;
        }

        /// <summary>
        /// Analyzes a site's HTML document. If the analyzing is set to be recursive, it also analyzes the side addresses of the root address.
        /// </summary>
        /// <param name="checkList"> List of already checked addresses to avoid rechecking the same addresses twice. </param>
        /// <param name="rootAddress"> Main website's address.</param>
        /// <param name="address"> Website's address. </param>
        /// <param name="recursive"> Checks if the analyzation should be recursive. </param>
        /// <param name="endResult"> Result list of the addresses and the number of times are linked. </param>
        private async Task AnalyzeSite(List<string> checkList, string rootAddress, string address, bool recursive, Dictionary<string, int> endResult)
        {
            checkList.Add(address);
            string content = await GetSource(address);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            SourceAnalyzer analyzer = new SourceAnalyzer(rootAddress, htmlDocument);
            Dictionary<string, int> result = analyzer.Analyze();
            MergeList(endResult, result);

            if (recursive)
            {
                foreach (string add in result.Keys)
                {
                    if (!checkList.Contains(add))
                    {
                        await AnalyzeSite(checkList, rootAddress, add, recursive, endResult);
                    }

                }
            }
        }

        /// <summary>
        /// Merges two lists.
        /// </summary>
        /// <param name="result"> Result list. </param>
        /// <param name="partResult"> Part result list. </param>
        private static void MergeList(Dictionary<string, int> result, Dictionary<string, int> partResult)
        {
            foreach (string key in partResult.Keys)
            {
                if (result.ContainsKey(key))
                {
                    result[key] += partResult[key];
                }
                else
                {
                    result.Add(key, partResult[key]);
                }
            }
        }

        /// <summary>
        /// Gets the source of the site. (HTML and JavaScript, but only the HTML will be processed.)
        /// </summary>
        /// <param name="address"> Website's address. </param>
        /// <returns> Content of the site. </returns>
        private async Task<string> GetSource(string address)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(address);
                string content = response.Content.ReadAsStringAsync().Result;

                Logger.Log(LogPriority.INFO, "Source acquired.\n");
                return content;
            }
            catch (Exception e)
            {
                Logger.Log(LogPriority.ERROR, "A problem occured while asking for response.\n" + e.Message);
                return string.Empty;
            }
        }
    }
}
