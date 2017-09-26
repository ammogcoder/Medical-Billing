using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    /// <summary>
    /// A class used to manipulate connection strings.
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// A dictionary of the segments of the connection string.
        /// </summary>
        public Dictionary<string, string> Segments;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionString()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">The connection string to initialize with.</param>
        public ConnectionString(string connectionString)
        {
            ParseConnectionString(connectionString);
        }

        /// <summary>
        /// The connection string value.  This is a string representation of the sum of the segments.
        /// </summary>
        public string Value
        {
            get
            {
                if (Segments == null)
                {
                    return "";
                }

                string cs = "";
                foreach(string key in Segments.Keys)
                {
                    cs += String.Format("{0}={1};", key, Segments[key]);
                }
                return cs;
            }
            set { ParseConnectionString(value); }
        }

        /// <summary>
        /// Parses the segments out of a given connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to parse.</param>
        private void ParseConnectionString(string connectionString)
        {
            const int KEY = 0;
            const int VALUE = 1;

            string[] segments;

            // Check the connectionString parameter.
            if (String.IsNullOrEmpty(connectionString))
            {
                Segments = null;
                return;
            }

            Segments = new Dictionary<string,string>();
            segments = connectionString.Split(';');
            foreach (string segment in segments)
            {
                string[] subsegment = segment.Split('=');
                if (subsegment.Length > 1 && !String.IsNullOrEmpty(subsegment[KEY]) && !String.IsNullOrEmpty(subsegment[VALUE]))
                {
                    string value = subsegment[VALUE];
                    // If there are more than two subsegments, then if the subsegments > 2 are blank then they are equal signs indicating encryption end.
                    for (int ndx = 2; ndx < subsegment.Length; ndx++)
                    {
                        if (String.IsNullOrEmpty(subsegment[ndx]))
                        {
                            value += "=";
                        }
                        else
                        {
                            value += subsegment[ndx];
                        }
                    }
                    Segments.Add(subsegment[KEY], value);
                }
            }
        }

        public void SetSegmentValue(string key, string value)
        {
            if (Segments.Keys.Contains(key))
            {
                Segments[key] = value;
            }
            else
            {
                 Segments.Add(key, value);
           }
        }
    }
}
