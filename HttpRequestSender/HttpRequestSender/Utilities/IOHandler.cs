using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.Utilities
{
    static class IOHandler
    {
        public static void WriteToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
