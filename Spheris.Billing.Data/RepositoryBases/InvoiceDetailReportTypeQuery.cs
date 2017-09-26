using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceDetailReportTypeQuery:DatabaseObject<DataTable>, IInvoiceDetailReportTypeQuery
    {
        public abstract DataTable Get(int reportTypeID, int invoiceID);
    }
}
