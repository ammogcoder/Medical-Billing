using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IBatchJobRepository
    {
            ObservableCollection<BatchJob> GetByFilter(DateTime? from, DateTime? through, string jobTypeFilter);

        int Add(BatchJob item);
        bool Update(BatchJob item);
        BatchJob FindById(int id);
        List<BatchJob> GetAll();
        void Remove(int id);
        List<BatchJob> ConvertDataReaderToList(IDataReader dr);
        BatchJob GetLatestCompleted(string batchJobType);
        List<BatchJob> GetLatestCompleted();
    }
}
