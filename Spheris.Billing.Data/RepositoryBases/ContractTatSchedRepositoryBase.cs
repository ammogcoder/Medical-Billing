using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.RepositoryInterfaces;


public abstract class ContractTatSchedRepositoryBase : RepositoryBase<ContractTatSched>, IContractTatSchedRepository
{
    public abstract ObservableCollection<ContractTatSched> FetchTatScheds(decimal contractId);
    
    public abstract ContractTatSched Get(ContractTatSched entity);
    public abstract void Add(ContractTatSched entity);
    public abstract void Update(ContractTatSched entity);
    public abstract void Remove(ContractTatSched entity);
}