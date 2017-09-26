using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class TatSchedRepositoryBase : RepositoryBase<TatSched>, ITatSchedRepository
    {
        public abstract ObservableCollection<TatSched> GetTATSched();

        public abstract TatSched Get(TatSched entity);
        public abstract void Add(TatSched entity);
        public abstract void Update(TatSched entity);
        public abstract void Remove(TatSched entity);

    }
}
