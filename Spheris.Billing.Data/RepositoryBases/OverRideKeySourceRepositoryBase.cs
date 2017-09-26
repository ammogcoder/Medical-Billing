using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class OverRideKeySourceRepositoryBase : RepositoryBase<OverRideKeySource>, IOverRideKeySourceRepository
    {
        public abstract System.Collections.ObjectModel.ObservableCollection<OverRideKeySource> GetOverRides();

        public abstract OverRideKeySource Get(OverRideKeySource entity);

        public abstract void Add(OverRideKeySource entity);

        public abstract void Remove(OverRideKeySource entity);

        public abstract void Update(OverRideKeySource entity);
    }
}
