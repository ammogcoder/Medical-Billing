using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class TatCompMethodRepositoryBase : RepositoryBase<TatCompMethod>, ITatCompMethodRepository
    {
        public abstract ObservableCollection<TatCompMethod> GetTATCompMethods();

        public abstract TatCompMethod Get(TatCompMethod entity);
        public abstract void Add(TatCompMethod entity);
        public abstract void Update(TatCompMethod entity);
        public abstract void Remove(TatCompMethod entity);

    }
}
