using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ContractRepositoryBase : RepositoryBase<Contract>, IContractRepository
    {
        public abstract ObservableCollection<Contract> FetchContracts(string contractFilter, string descFilter);
        public abstract Contract Get(Contract entity);
        public abstract void Add(Contract entity);
        public abstract void Update(Contract entity);
        public abstract void Remove(Contract entity);
        public abstract Contract GetByContractID(int contractID);
    }
}
