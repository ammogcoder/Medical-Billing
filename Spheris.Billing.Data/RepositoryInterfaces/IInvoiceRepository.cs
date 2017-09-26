using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceRepository : ICrudRepository<Invoice>
    {
        ObservableCollection<Invoice> FetchInvoices(DateTime? from, DateTime? to, decimal? grpId, bool asBatch = false);
        ObservableCollection<InvoiceCount> FetchCount(DateTime start, DateTime end);
        int CountInvoices(decimal grpId);

    }
}
