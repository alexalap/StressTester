using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    internal class SourceAnalyzer
    {
        string rootAddress;
        HtmlDocument htmlDocument;

        public SourceAnalyzer(string rootAddress, HtmlDocument htmlDocument)
        {
            this.rootAddress = rootAddress ?? throw new ArgumentNullException(nameof(rootAddress));
            this.htmlDocument = htmlDocument ?? throw new ArgumentNullException(nameof(htmlDocument));
        }

        public Dictionary<string, int> Analyze()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            HtmlNode rootNode = htmlDocument.DocumentNode;
            HtmlNodeCollection nodes = rootNode.SelectNodes("//a") ?? new HtmlNodeCollection(rootNode);
            foreach (var node in nodes)
            {
                string address = node.Attributes["href"]?.Value ?? "";
                if (!string.IsNullOrEmpty(address) && AddressExclusions.UrlExclusions.Where(x => address.Contains(x)).Count() == 0)
                {
                    if ((address[0] == '/' || address[0] == '#') && !rootAddress.EndsWith(address))
                    {
                        address = rootAddress + (address.StartsWith("/") ? address.Remove(0, 1) : address);
                    }
                    if (result.ContainsKey(address))
                    {
                        result[address]++;
                    }
                    else
                    {
                        //if (address.Contains(rootAddress.Replace("https://", "http://").Replace("http://", "").Split('/')[0]) && !address.Contains("#"))
                        if (address.Contains(rootAddress) && address.Split('/').Last().Count(x => x == '#') < 2)
                        {
                            result.Add(address, 1);
                        }
                    }
                }
            }
            return result;
        }
    }
}
