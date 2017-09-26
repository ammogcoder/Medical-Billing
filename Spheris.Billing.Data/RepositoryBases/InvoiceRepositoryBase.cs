using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract    class InvoiceRepositoryBase : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public abstract ObservableCollection<Invoice> FetchInvoices(DateTime? from, DateTime? to, decimal? grpId, bool asBatch = false);
        public abstract ObservableCollection<InvoiceCount> FetchCount(DateTime start, DateTime end);
        public abstract int CountInvoices(decimal grpId);


        public abstract Invoice Get(Invoice entity);
        public abstract void Add(Invoice entity);
        public abstract void Remove(Invoice entity);
        public abstract void Update(Invoice entity);
    }
}
