using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceStatusRepositoryBase : RepositoryBase<InvoiceStatus>, IInvoiceStatusRepository
    {
        public abstract ObservableCollection<InvoiceStatus> GetInvoiceStatuss();
        public abstract InvoiceStatus Get(InvoiceStatus entity);
        public abstract void Add(InvoiceStatus entity);
        public abstract void Update(InvoiceStatus entity);
        public abstract void Remove(InvoiceStatus entity);
    }
}
