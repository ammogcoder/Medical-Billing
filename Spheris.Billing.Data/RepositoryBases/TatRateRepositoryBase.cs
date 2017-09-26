using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class TatRateRepositoryBase : RepositoryBase<TatRate>, ITatRateRepository
    {
        public abstract ObservableCollection<TatRate> GetTatRates(decimal tatSchedId);
        public abstract ObservableCollection<ContractsUsingTatSched> GetContractsUsing(decimal tatSchedId);

        public abstract TatRate Get(TatRate entity);
        public abstract void Add(TatRate entity);
        public abstract void Update(TatRate entity);
        public abstract void Remove(TatRate entity);   
    }
}
