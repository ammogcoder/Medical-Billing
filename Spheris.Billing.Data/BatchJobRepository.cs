using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryBases;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data
{
    /// <summary>
    /// Batch Job DAL base
    /// </summary>
    public abstract class BatchJobRepository : RepositoryBase<BatchJob>, IBatchJobRepository
    {
        /// <summary>
        /// Retrieves all available invoice detail file types.
        /// </summary>
        /// <returns>Array of InvoiceDetailFileTypeItem structures.</returns>
        public abstract List<BatchJob> GetAll();

        public abstract ObservableCollection<BatchJob> GetByFilter(DateTime? from, DateTime? through, string jobTypeFilter);
        /// <summary>
        /// Retrieves a item that matches a given InvoiceGrpId.
        /// </summary>
        /// <param name="id">The unique identifier for an item.</param>
        /// <returns>A single item.</returns>
        public abstract BatchJob FindById(int id);

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The unique identifier the the item to delete.</param>
        public abstract void Remove(int id);

        /// <summary>
        /// Adds a new batch job.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <returns>The InvoiceGrpId of the item that was created.</returns>
        public abstract int Add(BatchJob batch);

        public abstract bool Update(BatchJob batch);

        public abstract BatchJob GetLatestCompleted(string batchJobType);

        public abstract List<BatchJob> GetLatestCompleted();

        public abstract List<BatchJob> ConvertDataReaderToList(IDataReader reader);
    }
}
