using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class StatCompMethodRepositoryBase : RepositoryBase<StatCompMethod>, IStatCompMethodRepository
    {
        public abstract ObservableCollection<StatCompMethod> GetStatCompMethods();
        public abstract StatCompMethod Get(StatCompMethod entity);
        public abstract void Add(StatCompMethod entity);
        public abstract void Update(StatCompMethod entity);
        public abstract void Remove(StatCompMethod entity);
    }
}
