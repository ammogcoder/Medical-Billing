using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceDetailReportTypeQuery
    {
        DataTable Get(int reportTypeID, int invoiceID);
    }
}
