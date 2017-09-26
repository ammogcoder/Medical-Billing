using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public class InvoiceGroupReportsInfoQueryResult
    {
        public int InvoiceGroupID;
        public int ReportTypeID;
        public int TabOrder;
        public string ReportTypeShortName;
        public string FileType;
        public bool IsDataDump;
    }

    public abstract class InvoiceGroupReportsInfoQuery:RepositoryBase<InvoiceGroupReportsInfoQueryResult>, IInvoiceGroupReportsInfoQuery
    {
        public abstract List<InvoiceGroupReportsInfoQueryResult> Get(int invoiceGroupID);
        public abstract List<InvoiceGroupReportsInfoQueryResult> GetByInvoiceID(int invoiceID);
    }
}
