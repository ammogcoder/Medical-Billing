using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    public struct InvoiceDetailFileTypeItem
    {
        /// <summary>
        /// The unique InvoiceGrpId of the file type.
        /// </summary>
        public string Id;

        /// <summary>
        /// The description of the file type.
        /// </summary>
        public string Description;

        /// <summary>
        /// Is the file type available for use or has it been retired?
        /// </summary>
        public bool IsAvailable;

        /// <summary>
        /// What file extension should the created file have?
        /// </summary>
        public string FileExtension;

        /// <summary>
        /// The SQL that generates the data used to populate the invoice detail file.
        /// </summary>
        public string Sql;
    }

    public abstract class InvoiceDetailFileTypeDal : DatabaseObject<InvoiceDetailFileTypeItem>
    {
        /// <summary>
        /// Retrieves an array of all the column names that are available to be included in the detail file.
        /// </summary>
        /// <returns>String array of column names.</returns>
        public abstract string[] GetAllSqlColumnNames();
        
        /// <summary>
        /// Retrieves an array of column names that are either in use by the file type or those that are not in use depending on the parameter value.
        /// </summary>
        /// <param name="inUse">True returns the column names that are currently in use by the file type, false returns those that are not currently in use.</param>
        /// <returns>String array of column names.</returns>
        public abstract string[] GetSqlColumnNames(bool inUse);

        /// <summary>
        /// Retrieves all available invoice detail file types.
        /// </summary>
        /// <returns>Array of InvoiceDetailFileTypeItem structures.</returns>
        public abstract List<InvoiceDetailFileTypeItem> GetAll();

        /// <summary>
        /// Retrieves a item that matches a given InvoiceGrpId.
        /// </summary>
        /// <param name="id">The unique identifier for an item.</param>
        /// <returns>A single item.</returns>
        public abstract InvoiceDetailFileTypeItem FindById(string id);

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The unique identifier the the item to delete.</param>
        public abstract void Delete(string id);

        /// <summary>
        /// Saves an item.  If it is a new item, it will create it.  If the item already exists, it will update it.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <returns>The InvoiceGrpId of the item that is inserted/updated.</returns>
        public abstract string Save(InvoiceDetailFileTypeItem item);
    }
}