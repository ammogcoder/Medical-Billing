using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class FaxCompMethodRepositoryBase : RepositoryBase<FaxCompMethod>, IFaxCompMethodRepository
    {
        public abstract ObservableCollection<FaxCompMethod> GetFaxCompMethods();

        public abstract FaxCompMethod Get(FaxCompMethod entity);

        public abstract void Add(FaxCompMethod entity);

        public abstract void Remove(FaxCompMethod entity);

        public abstract void Update(FaxCompMethod entity);

    }
}
