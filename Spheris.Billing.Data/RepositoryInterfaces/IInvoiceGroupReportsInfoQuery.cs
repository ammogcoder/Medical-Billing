using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceGroupReportsInfoQuery
    {
        List<InvoiceGroupReportsInfoQueryResult> Get(int invoiceGroupID);
        List<InvoiceGroupReportsInfoQueryResult> GetByInvoiceID(int invoiceID);
    }
}
