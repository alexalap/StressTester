using System.IO;

namespace HttpRequestSender.Utilities
{
    static class IOHandler
    {
        /// <summary>
        /// Writes a given content to a given file path.
        /// </summary>
        /// <param name="path"> Given file path (location, file name). </param>
        /// <param name="content"> Given content that needs to be saved. </param>
        public static void WriteToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
