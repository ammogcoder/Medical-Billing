using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceGrpOverrideRepositoryBase : RepositoryBase<InvoiceGrpOverride> , IInvoiceGrpOverrideRepository
    {
        public abstract ObservableCollection<InvoiceGrpOverride> FetchOverRides(decimal grpId);

        public abstract InvoiceGrpOverride Get(InvoiceGrpOverride entity);

        public abstract void Add(InvoiceGrpOverride entity);
        public abstract void Remove(InvoiceGrpOverride entity);
        public abstract void Update(InvoiceGrpOverride entity);
    }
}
