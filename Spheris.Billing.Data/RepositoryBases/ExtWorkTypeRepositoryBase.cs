using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using System.Data;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ExtWorkTypeRepositoryBase : RepositoryBase<ExtWorkType>, IExtWorkTypeRepository
    {
        public abstract ObservableCollection<ExtWorkType> GetWorkTypes(string extsys, string extClientKey = null);

        public abstract ExtWorkType Get(ExtWorkType entity);
        public abstract void Add(ExtWorkType entity);
        public abstract void Remove(ExtWorkType entity);
        public abstract  void Update(ExtWorkType entity);
    }
}
