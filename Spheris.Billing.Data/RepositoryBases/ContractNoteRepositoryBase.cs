using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ContractNoteRepositoryBase:RepositoryBase<ContractNote>, IContractNoteRepository
    {
        public abstract ObservableCollection<ContractNote> GetByContractID(int contractID);
        public abstract ContractNote Get(ContractNote ID);
        public abstract void Add(ContractNote entity);
        public abstract void Update(ContractNote entity);
        public abstract void Remove(ContractNote entity);
    }
}
