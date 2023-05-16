using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using System.Windows.Forms;

namespace HttpRequestSender.Utilities
{
    /// <summary>
    /// Logging priority types
    /// </summary>
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
        private delegate void PrintLog();

        private const long logGridViewRefreshInterval = 2000;

        private static object lockObject = new object();

        private static System.Timers.Timer timer;

        public static DataGridView logGridView { get; set; }

        private static List<LogEntry> logEntries = new List<LogEntry>();

        private static List<LogEntry> newLogEntries = new List<LogEntry>();
        //private static string logFile = Directory.GetCurrentDirectory() + "log_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";

        /// <summary>
        /// Logs a log.
        /// </summary>
        /// <param name="logPriority"> The priority of the log. </param>
        /// <param name="message">The message of the log. </param>
        public static void Log(LogPriority logPriority, string message)
        {
            LogEntry entry = new LogEntry(logPriority, DateTime.Now, message);
            lock (lockObject)
            {
                if(timer == null)
                {
                    timer = new System.Timers.Timer();
                    timer.Interval = logGridViewRefreshInterval;
                    timer.Elapsed += TimerTick;
                    timer.AutoReset = true;
                    timer.Enabled = true;
                }

                logEntries.Add(entry);
                newLogEntries.Add(entry);
            }
        }

        private static void LogGridViewRefresh()
        {
            logGridView?.Invoke(new PrintLog(LogToLogsTab));
        }

        private static void LogToLogsTab()
        {
            if (newLogEntries.Count > 0)
            {
                lock (lockObject)
                {
                    foreach (LogEntry entry in newLogEntries)
                    {
                        logGridView.Rows.Add(entry.logPriority.ToString(), entry.timeStamp.ToString("G", CultureInfo.CurrentCulture), entry.message);
                    }
                    newLogEntries.Clear();
                }
                logGridView.FirstDisplayedScrollingRowIndex = logGridView.RowCount - 1;
            }
        }

        private static void TimerTick(Object source, ElapsedEventArgs e)
        {
            LogGridViewRefresh();
        }
    }
}
