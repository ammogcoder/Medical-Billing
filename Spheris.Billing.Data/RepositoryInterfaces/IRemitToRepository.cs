using System;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IRemitToRepository : ICrudRepository<RemitTo>
    {
        ObservableCollection<RemitTo> GetRemitTos();
    }
}
