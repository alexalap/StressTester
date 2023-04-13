using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    public static class AddressExclusions
    {
        public static readonly string[] UrlExclusions = new string[]
        {
            "cdn-cgi",
            "mailto",
        };
    }
}
