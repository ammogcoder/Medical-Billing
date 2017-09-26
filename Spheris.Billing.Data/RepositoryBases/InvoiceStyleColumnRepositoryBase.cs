using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceStyleColumnRepositoryBase:RepositoryBase<InvoiceStyleColumn>, IInvoiceStyleColumnRepository
    {
        public abstract List<InvoiceStyleColumn> GetByInvoiceStyle(string invoiceStyle);
        public abstract List<InvoiceStyleColumn> GetByInvoiceID(int invoiceID);
        public abstract InvoiceStyleColumn Get(InvoiceStyleColumn item);
        public abstract void Add(InvoiceStyleColumn item);
        public abstract void Update(InvoiceStyleColumn item);
        public abstract void Remove(InvoiceStyleColumn item);
    }
}
