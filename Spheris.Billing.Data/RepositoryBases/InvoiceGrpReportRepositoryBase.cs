using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceGrpReportRepositoryBase : RepositoryBase<InvoiceGrpReport>, IInvoiceGrpReportRepository
    {
        public abstract ObservableCollection<InvoiceGrpReport> GetById(int grpId);
        public abstract InvoiceGrpReport GetByIds(int reportId, int groupId);
        public abstract InvoiceGrpReport Get(InvoiceGrpReport entity);
        public abstract void Add(InvoiceGrpReport entity);
        public abstract void Update(InvoiceGrpReport entity);
        public abstract void Remove(InvoiceGrpReport entity);
        public abstract ObservableCollection<InvoiceGrpReport> GetByIdXLSOnly(int grpId);
    }
}
