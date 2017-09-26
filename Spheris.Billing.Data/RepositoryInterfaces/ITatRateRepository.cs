using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface ITatRateRepository : ICrudRepository<TatRate>
    {
        ObservableCollection<TatRate> GetTatRates(decimal tatSchedId);
        ObservableCollection<ContractsUsingTatSched> GetContractsUsing(decimal tatSchedId);
    }
}
