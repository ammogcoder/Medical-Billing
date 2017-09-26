using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ExtSysRepositoryBase : RepositoryBase<ExtSys>, IExtSysRepository
    {
        public abstract ExtSys Get(ExtSys entity);
        public abstract void Add(ExtSys entity);
        public abstract void Remove(ExtSys entity);
        public abstract void Update(ExtSys entity);

        public abstract ObservableCollection<ExtSys> GetExtSys();
    }
}
