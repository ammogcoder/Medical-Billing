using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class BatchJobTypeRepositoryBase : RepositoryBase<BatchJobType>, IBatchJobTypeRepository
    {
        public abstract ObservableCollection<BatchJobType> GetBatchJobTypes();
        public abstract BatchJobType Get(BatchJobType entity);
        public abstract void Add(BatchJobType entity);
        public abstract void Update(BatchJobType entity);
        public abstract void Remove(BatchJobType entity);
    }
}
