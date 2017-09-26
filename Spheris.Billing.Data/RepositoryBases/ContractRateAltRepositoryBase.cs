using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ContractRateAltRepositoryBase : RepositoryBase<ContractRateAlt>, IContractRateAltRepository
    {
        public abstract ObservableCollection<ContractRateAlt> GetAltRates(int contractRateId);
        public abstract ObservableCollection<ContractRateAlt> GetAltRatesFromClients(string extSys, string extClientKey);

        public abstract ContractRateAlt Get(ContractRateAlt entity);
        public abstract void Add(ContractRateAlt entity);
        public abstract void Update(ContractRateAlt entity);
        public abstract void Remove(ContractRateAlt entity);
    }
}
