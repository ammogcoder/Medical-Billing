using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;


namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class RepositoryBase<T>:DatabaseObject<T> where T: new()
    {
        protected abstract void RowConverter(T item, DataRow row);

#if LOADING_YIELD
        IEnumerable<T> LoadStuff(DataTable table)
        {
            DateTime Start = DateTime.Now;
            foreach (DataRow row in table.Rows)
            {
                T item = new T();
                RowConverter(item, row);
                yield return item;
            }
            DateTime End = DateTime.Now;
            TimeSpan CallTime = End - Start;
            Debug.WriteLine("Elapsed + " + CallTime.Milliseconds.ToString());

        }
#endif

        protected virtual ObservableCollection<T> ConvertDataTableToObservableCollection(DataTable table)
        {
#if LOADING_YIELD
            return new ObservableCollection<T>(LoadStuff(table));
#else
#if TIME_IT
            DateTime Start = DateTime.Now;
#endif
            ObservableCollection<T> items = new ObservableCollection<T>();
            foreach (DataRow row in table.Rows)
            {
                T item = new T();
                RowConverter(item, row);
                items.Add(item);
            }
#if TIME_IT
            DateTime End = DateTime.Now;
            TimeSpan CallTime = End - Start;
            Debug.WriteLine("Elapsed + " + CallTime.Milliseconds.ToString());
#endif            
            return items;
#endif
        }

        protected virtual List<T> ConvertDataTableToList(DataTable table)
        {
            List<T> items = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T item = new T();
                RowConverter(item, row);
                items.Add(item);
            }
            return items;
        }

        protected virtual decimal? CastDbValueRowDecimalOrZero(DataRow dr, string colName)
        {
            object value = CastDbValueRow(dr, colName, true, "decimal");
            if (value == null) return 0;
            else
                return value as decimal?;
        }

        protected virtual dynamic CastDbValueRow(DataRow dr, string colName, bool ignoreMissingCol = true, string typeOverride = null)
        {
            object value;

            // Make sure the requested column name is valid and handle as requested.
            try
            {
                value = dr[colName];
            }
            catch (IndexOutOfRangeException ex)
            {
                if (ignoreMissingCol)
                {
                    return null;
                }
                else throw;
            }
            catch
            {
                throw;
            }
            if (value == null) return null;
            string val = value.ToString();
            if (String.IsNullOrEmpty(val)) return null;
            string type = value.GetType().UnderlyingSystemType.Name;
            if (typeOverride != null) type = typeOverride;
            switch (type)
            {
                case "Bool":
                    return (val == "Y") ? true : false;
                case "decimal":
                    return decimal.Parse(val);
                case "Decimal":
                    return float.Parse(val);
                case "Double":
                    return double.Parse(val);
                case "Int32":
                    return int.Parse(val);
                case "Int64":
                    return long.Parse(val);
                case "DateTime":
                    return DateTime.Parse(val);
                case "String":
                    return val;
            }
            return null;
        }

        protected virtual dynamic CastDbValueRow(object value, string colName, bool ignoreMissingCol = true, string typeOverride = null)
        {
            if (value == null) return null;
            string val = value.ToString();
            if (String.IsNullOrEmpty(val)) return null;
            string type = value.GetType().UnderlyingSystemType.Name;
            if (typeOverride != null) type = typeOverride;
            switch (type)
            {
                case "Bool":
                    return (val == "Y") ? true : false;
                case "decimal":
                    return decimal.Parse(val);
                case "Decimal":
                    return float.Parse(val);
                case "Double":
                    return double.Parse(val);
                case "Int32":
                    return int.Parse(val);
                case "Int64":
                    return long.Parse(val);
                case "DateTime":
                    return DateTime.Parse(val);
                case "String":
                    return val;
            }
            return null;
        }

        protected virtual decimal? CastDbValueDecimalOrZero(IDataRecord dr, string colName)
        {
            object value = CastDbValue(dr, colName, true, "decimal");
            if (value == null) return 0;
            else
                return value as decimal?;
        }

        protected virtual dynamic CastDbValue(IDataRecord dr, string colName, bool ignoreMissingCol = true, string typeOverride = null)
        {
            object value;

            // Make sure the requested column name is valid and handle as requested.
            try
            {
                value = dr[colName];
            }
            catch (IndexOutOfRangeException ex)
            {
                if (ignoreMissingCol)
                {
                    return null;
                }
                else throw;
            }
            catch
            {
                throw;
            }
            if (value == null) return null;
            string val = value.ToString();
            if (String.IsNullOrEmpty(val)) return null;
            string type = value.GetType().UnderlyingSystemType.Name;
            if (typeOverride != null) type = typeOverride;
            switch (type)
            {
                case "decimal":
                    return decimal.Parse(val);
                case "Decimal":
                    return float.Parse(val);
                case "Double":
                    return double.Parse(val);
                case "Int32":
                    return int.Parse(val);
                case "Int64":
                    return long.Parse(val);
                case "DateTime":
                    return DateTime.Parse(val);
                case "String":
                    return val;
            }
            return null;
        }

        protected virtual object NullableInput(object obj)
        {
            return (obj == null) ? DBNull.Value : obj;
        }
    }
}
