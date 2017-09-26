using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceGrpStatusRepositoryBase : RepositoryBase<InvoiceGrpStatus>, IInvoiceGrpStatusRepository
    {
        public abstract ObservableCollection<InvoiceGrpStatus> GetInvoiceGrpStatuss();
        public abstract InvoiceGrpStatus Get(InvoiceGrpStatus entity);
        public abstract void Add(InvoiceGrpStatus entity);
        public abstract void Update(InvoiceGrpStatus entity);
        public abstract void Remove(InvoiceGrpStatus entity);
    }
}

