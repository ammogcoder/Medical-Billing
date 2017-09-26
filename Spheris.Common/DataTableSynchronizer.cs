using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Spheris.Data
{
    #region Custom Exception Classes

    /// <summary>
    /// This exception is thrown when an expected primary key is not found on the specified table.
    /// </summary>
    public class TableKeyNotFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tableName">The name of the table that is missing a primary key.</param>
        public TableKeyNotFoundException(string tableName)
            : base(String.Format("The {0} DataTable does not have a primary key defined.", tableName))
        {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tableName">The name of the table that is missing a primary key.</param>
        /// <param name="innerException">Inner exception.</param>
        public TableKeyNotFoundException(string tableName, Exception innerException)
            : base(String.Format("The {0} DataTable does not have a primary key defined.", tableName), innerException)
        {}
    }

    /// <summary>
    /// This exception is thrown when a specified column is not found in a table.
    /// </summary>
    public class ColumnNotFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnName">The name of the column that is missing.</param>
        public ColumnNotFoundException(string columnName)
            : base(String.Format("The {0} column does not exist.", columnName))
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnName">The name of the column that is missing.</param>
        /// <param name="innerException">Inner exception</param>
        public ColumnNotFoundException(string columnName, Exception innerException)
            : base(String.Format("The {0} column does not exist.", columnName), innerException)
        { }
    }

    #endregion

    /// <summary>
    /// This class is used to synchronize the data between two DataTable objects.
    /// </summary>
    public class DataTableSynchronizer : IDisposable
    {
        #region Private Fields

        private DataTable _source;
        private DataTable _destination;
        private bool _doInserts;
        private bool _doUpdates;
        private int _rowsInserted;
        private int _rowsUpdated;
        private bool _disposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataTableSynchronizer()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allowInserts">If there are rows in the source table that aren't in the destination, should the source row be inserted into the destination table?</param>
        /// <param name="allowUpdates">If differences are found between the source and destination rows, should the destination row be updated to match the source?</param>
        public DataTableSynchronizer(bool allowInserts, bool allowUpdates)
        {
            _doInserts = allowInserts;
            _doUpdates = allowUpdates;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source">The source table.</param>
        /// <param name="destination">The destination table.</param>
        public DataTableSynchronizer(DataTable source, DataTable destination)
        {
            _source = source;
            _destination = destination;
            _doInserts = true;
            _doUpdates = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source">The source table.</param>
        /// <param name="destination">The destination table.</param>
        /// <param name="allowInserts">If there are rows in the source table that aren't in the destination, should the source row be inserted into the destination table?</param>
        /// <param name="allowUpdates">If differences are found between the source and destination rows, should the destination row be updated to match the source?</param>
        public DataTableSynchronizer(DataTable source, DataTable destination, bool allowInserts, bool allowUpdates)
        {
            _source = source;
            _destination = destination;
            _doInserts = allowInserts;
            _doUpdates = allowUpdates;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The source table.
        /// </summary>
        public DataTable Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// The destination table.
        /// </summary>
        public DataTable Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        /// <summary>
        /// If there are rows in the source table that aren't in the destination, should the source row be inserted into the destination table?
        /// </summary>
        public bool AllowInserts
        {
            get { return _doInserts; }
            set { _doInserts = value; }
        }

        /// <summary>
        /// If differences are found between the source and destination rows, should the destination row be updated to match the source?
        /// </summary>
        public bool AllowUpdates
        {
            get { return _doUpdates; }
            set { _doUpdates = value; }
        }

        /// <summary>
        /// Read-only.  The number of rows that were inserted into the destination table.
        /// </summary>
        public int RowsInserted
        {
            get { return _rowsInserted; }
        }

        /// <summary>
        /// Read-only.  The number of rows that were updated in the destination table.
        /// </summary>
        public int RowsUpdated
        {
            get { return _rowsUpdated; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Synchronizes the destination table with the source table.  This is a one-way sync that updates rows in the destination table
        /// to match the rows in the source table.  The destination table must have at least all the columns defined in the source table.
        /// The PrimaryKey property of the source table must be defined prior to calling this function.
        /// </summary>
        public virtual void Synchronize()
        {
            // Make sure that the source PrimaryKey property has been defined.
            if (_source.PrimaryKey.Length == 0)
            { 
                // Throw a custom exception.
                throw new TableKeyNotFoundException("source");
            }

            // Set the sort property of the destination table to match the primary key column(s) of the source table.
            string sortValue = "";
            foreach (DataColumn key in _source.PrimaryKey)
            {
                sortValue += key.ColumnName;
            }
            _destination.DefaultView.Sort = sortValue;

            // Compare rows.
            foreach (DataRow sourceRow in _source.Rows)
            {
                bool rowUpdated = false;

                // Find destination row.
                DataRow destinationRow = FindDestinationRow(sourceRow);

                // If row not found, insert a new row.
                if (destinationRow == null && _doInserts)
                {
                    destinationRow = _destination.NewRow();
                    _rowsInserted++;
                }

                // Update the destination row.
                if(destinationRow != null)
                {
                    // Compare columns.
                    foreach (DataColumn sourceColumn in _source.Columns)
                    {
                        rowUpdated |= CompareColumns(sourceRow, destinationRow, sourceColumn.ColumnName);
                    }

                    if (rowUpdated)
                    {
                        _rowsUpdated++;
                    }
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Compares a column between the source and destination tables.  The columns in each table must have the same name.
        /// If AllowUpdates is true and the columns are different, then the destination column will be updated to equal the value of the source column.
        /// </summary>
        /// <param name="sourceRow">The source row.</param>
        /// <param name="destinationRow">The destination row.</param>
        /// <param name="columnName">The name of the column to compare.</param>
        /// <returns>True if the destination column was updated.  Otherwise, false.</returns>
        protected virtual bool CompareColumns(DataRow sourceRow, DataRow destinationRow, string columnName)
        {
            // If source column not found in the destination row, raise an exception.
            if (destinationRow[columnName] == null)
            {
                throw new ColumnNotFoundException("destination." + columnName);
            }

            if (sourceRow[columnName].ToString() != destinationRow[columnName].ToString())
            {
                // If columns are different, update the destination column with the source column's value.
                if (_doUpdates)
                {
                    destinationRow[columnName] = sourceRow[columnName];
                    return true;
                }
            }

            // If columns are equal, do nothing.
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Finds the first row in the destination table who's key columns match that of the key columns from the source row.
        /// </summary>
        /// <param name="sourceRow">The source row.</param>
        /// <returns>First DataRow that satisfies the match.</returns>
        private DataRow FindDestinationRow(DataRow sourceRow)
        {
            DataColumn[] keyColumns = sourceRow.Table.PrimaryKey;

            // Build an array of the source key values.
            object[] keyValue = new object[keyColumns.Length];
            int ndx = 0;
            foreach (DataColumn column in keyColumns)
            {
                keyValue[ndx] = sourceRow[column.ColumnName];
                ndx++;
            }

            // Find the first row that matches the key.
            ndx = _destination.DefaultView.Find(keyValue);
            if (ndx < 0)
            {
                // No match found.
                return null;
            }
            else
            {
                return _destination.DefaultView[ndx].Row;
            }
        }

        #endregion

        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                    _source.Dispose();
                    _destination.Dispose();
                }
                // free native resources
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
