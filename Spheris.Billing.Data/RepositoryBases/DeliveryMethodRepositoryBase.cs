using System;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class DeliveryMethodRepositoryBase : RepositoryBase<DeliveryMethod>, IDeliveryMethods
    {
        public abstract ObservableCollection<DeliveryMethod> GetDeliveryMethods();

        public abstract DeliveryMethod Get(DeliveryMethod entity);

        public abstract void Add(DeliveryMethod entity);

        public abstract void Remove(DeliveryMethod entity);

        public abstract void Update(DeliveryMethod entity);

    }
}
