using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    public static class DataHelper
    {
        public static List<KeyValuePair<TKey, TValue>> BuildKeyValuePairListFromDataReader<TKey, TValue>(IDataReader dr, string keyColumn, string valueColumn)
        {
            List<KeyValuePair<TKey, TValue>> pairs = new List<KeyValuePair<TKey, TValue>>();
            while (dr.Read())
            {
                TKey key = (TKey)Convert.ChangeType(dr[keyColumn].ToString(), typeof(TKey));
                TValue val = (TValue)Convert.ChangeType(dr[valueColumn].ToString(), typeof(TValue));
                pairs.Add(new KeyValuePair<TKey,TValue>(key, val));
            }
            return pairs;
        }

        public static Dictionary<string, string> ConvertRowToDictionary(DataRow row, DataColumnCollection columns)
        {
            // Populate dictionary.
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (DataColumn col in columns)
            {
                parameters.Add(col.ColumnName, row[col].ToString());
            }
            return parameters;
        }
    }
}
