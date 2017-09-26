using System;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IFreqRepository : ICrudRepository<Freq>
    {
        ObservableCollection<Freq> GetFreqs();
    }
}
