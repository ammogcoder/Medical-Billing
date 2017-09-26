
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceStyleRepositoryBase : RepositoryBase<InvoiceStyle>, IInvoiceStyleRepository
    {
        public abstract ObservableCollection<InvoiceStyle> GetInvoiceStyles();
        public abstract InvoiceStyle Get(InvoiceStyle entity);
        public abstract void Add(InvoiceStyle entity);
        public abstract void Update(InvoiceStyle entity);
        public abstract void Remove(InvoiceStyle entity);
    }
}
