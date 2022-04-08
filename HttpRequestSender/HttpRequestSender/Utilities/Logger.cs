using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace HttpRequestSender.Utilities
{
    class LogEntry
    {
        public readonly DateTime timeStamp;
        public readonly string message;
        public readonly long duration;

        public LogEntry(DateTime timeStamp, string message, long duration)
        {
            this.timeStamp = timeStamp;
            this.message = message;
            this.duration = duration;
        }
    }

    static class Logger
    {
        private delegate void PrintLog(LogEntry logEntry);

        public static DataGridView logGridView { get; set; }

        private static List<LogEntry> logEntries = new List<LogEntry>();
        private static string logFile = Directory.GetCurrentDirectory() + "log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";

        public static void Log(DateTime timestamp, string message, long duration)
        {
            LogEntry entry = new LogEntry(timestamp, message, duration);

            logEntries.Add(entry);
            //LogToLogsTab(entry);
            logGridView.Invoke(new PrintLog(LogToLogsTab), entry);
        }

        private static void LogToLogsTab(LogEntry entry)
        {
            logGridView.Rows.Add(entry.timeStamp.ToString("G", CultureInfo.CurrentCulture), entry.message, entry.duration + " ms");
            logGridView.FirstDisplayedScrollingRowIndex = logGridView.RowCount - 1;
        }
    }
}
