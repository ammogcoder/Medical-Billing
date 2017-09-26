using System;
using System.Text;
using System.Windows.Forms;

namespace Spheris.Common
{
    public static class EventLog
    {
        private static string _source = Application.ProductName;
        private static string _log = "Spheris";

        public static string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public static string Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public static void WriteEntry(System.Diagnostics.EventLogEntryType entryType, string eventMsg, params object[] args)
        {
            string msg = String.Format(eventMsg, args);
            WriteEntry(msg, entryType);
        }

        public static void WriteEntry(string eventMsg, System.Diagnostics.EventLogEntryType entryType)
        {
            // Make sure the source exists.
            if (!System.Diagnostics.EventLog.SourceExists(Source))
                System.Diagnostics.EventLog.CreateEventSource(new System.Diagnostics.EventSourceCreationData(Source, Log));

            // Write to log.
            System.Diagnostics.EventLog.WriteEntry(Source, eventMsg, entryType);
        }
    }
}
