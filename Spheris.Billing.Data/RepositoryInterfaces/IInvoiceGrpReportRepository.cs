using Spheris.Billing.Core.Domain;
using System;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceGrpReportRepository : ICrudRepository<InvoiceGrpReport> 
    {
        ObservableCollection<InvoiceGrpReport> GetById(int grpId);
        InvoiceGrpReport GetByIds(int reportId, int groupId);
        ObservableCollection<InvoiceGrpReport> GetByIdXLSOnly(int grpId);
    }
}
