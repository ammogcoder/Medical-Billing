using System;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IDeliveryMethods : ICrudRepository<DeliveryMethod>
    {
        ObservableCollection<DeliveryMethod> GetDeliveryMethods();
    }
}
