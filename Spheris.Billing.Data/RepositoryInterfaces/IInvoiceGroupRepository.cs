using Spheris.Billing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceGroupRepository : ICrudRepository<InvoiceGroup>
    {
        ObservableCollection<InvoiceGroup> FetchGroups(BillingSpecialist specialist, DeliveryMethod deliveryMethod, string contractFilter, string descFilter);
        ObservableCollection<InvoiceGroup> FetchNullContracts();
        InvoiceGroup Get(InvoiceGroup item);

    }
}
