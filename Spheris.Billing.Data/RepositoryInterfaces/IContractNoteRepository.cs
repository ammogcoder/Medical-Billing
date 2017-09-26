using Spheris.Billing.Core.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IContractNoteRepository : ICrudRepository<ContractNote> 
    {
        ObservableCollection<ContractNote> GetByContractID(int contractID);
    }
}
