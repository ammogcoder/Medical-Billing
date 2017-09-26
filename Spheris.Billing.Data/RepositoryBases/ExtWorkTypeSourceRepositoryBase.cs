using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ExtWorkTypeSourceRepositoryBase : RepositoryBase<ExtWorkTypeSource>, IExtWorkTypeSourceRepository
    {
        public abstract ObservableCollection<ExtWorkTypeSource> GetWorkTypeSources();
        public abstract ExtWorkTypeSource Get(ExtWorkTypeSource entity);

        public abstract void Add(ExtWorkTypeSource entity);
        public abstract void Remove(ExtWorkTypeSource entity);

        public abstract void Update(ExtWorkTypeSource entity);
    }
}
