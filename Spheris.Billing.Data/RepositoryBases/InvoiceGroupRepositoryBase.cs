using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceGroupRepositoryBase : RepositoryBase<InvoiceGroup>, IInvoiceGroupRepository
    {
        public abstract ObservableCollection<InvoiceGroup> FetchGroups(BillingSpecialist specialist, DeliveryMethod deliveryMethod, string contractFilter, string descFilter);
        public abstract ObservableCollection<InvoiceGroup> FetchNullContracts();


        public abstract InvoiceGroup Get(InvoiceGroup entity);
        public abstract void Add(InvoiceGroup entity);
        public abstract void Update(InvoiceGroup entity);
        public abstract void Remove(InvoiceGroup entity);
    }
}
