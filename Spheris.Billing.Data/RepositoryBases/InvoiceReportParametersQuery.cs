using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceReportParametersQuery:RepositoryBase<Dictionary<string,string>>, IInvoiceReportParametersQuery
    {
        public abstract Dictionary<string, string> Get(int invoiceID);
    }
}
