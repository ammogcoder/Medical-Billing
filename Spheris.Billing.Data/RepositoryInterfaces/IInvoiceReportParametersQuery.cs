using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceReportParametersQuery
    {
        Dictionary<string,string> Get(int invoiceID);
    }
}
