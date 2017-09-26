using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ContractRateRepositoryBase : RepositoryBase<ContractRate>, IContractRateRepository
    {
        public abstract System.Collections.ObjectModel.ObservableCollection<ContractRate> GetRates(int contractId);
        public abstract ContractRate Get(ContractRate entity);
        public abstract void Add(ContractRate entity);
        public abstract void Update(ContractRate entity);
        public abstract void Remove(ContractRate entity);
    }
}
