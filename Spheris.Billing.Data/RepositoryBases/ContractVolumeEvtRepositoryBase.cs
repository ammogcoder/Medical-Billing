using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ContractVolumeEvtRepositoryBase : RepositoryBase<ContractVolumeEvt>, IContractVolumeEvtRepository
    {
        public abstract ObservableCollection<ContractVolumeEvt> FetchVolumeEvts(decimal contractId);
        public abstract ContractVolumeEvt Get(ContractVolumeEvt entity);
        public abstract void Add(ContractVolumeEvt entity);
        public abstract void Remove(ContractVolumeEvt entity);
        public abstract void Update(ContractVolumeEvt entity);
    }
}
