using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace HttpRequestSender.Utilities
{
    public enum LogPriority
    {
        INFO,
        WARNING,
        ERROR
    }

    class LogEntry
    {
        public readonly LogPriority logPriority;
        public readonly DateTime timeStamp;
        public readonly string message;

        public LogEntry(LogPriority logPriority, DateTime timeStamp, string message)
        {
            this.logPriority = logPriority;
            this.timeStamp = timeStamp;
            this.message = message;
        }
    }

    static class Logger
    {
        private delegate void PrintLog(LogEntry logEntry);

        public static DataGridView logGridView { get; set; }

        private static List<LogEntry> logEntries = new List<LogEntry>();
        //private static string logFile = Directory.GetCurrentDirectory() + "log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";

        public static void Log(LogPriority logPriority, string message)
        {
            LogEntry entry = new LogEntry(logPriority, DateTime.Now, message);

            logEntries.Add(entry); // TODO: NOT threadsafe
            logGridView.Invoke(new PrintLog(LogToLogsTab), entry);
        }

        private static void LogToLogsTab(LogEntry entry)
        {
            logGridView.Rows.Add(entry.logPriority.ToString(), entry.timeStamp.ToString("G", CultureInfo.CurrentCulture), entry.message);
            logGridView.FirstDisplayedScrollingRowIndex = logGridView.RowCount - 1;
        }
    }
}
