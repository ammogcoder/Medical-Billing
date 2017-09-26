using Spheris.Billing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceStyleColumnRepository:ICrudRepository<InvoiceStyleColumn>
    {
        List<InvoiceStyleColumn> GetByInvoiceStyle(string invoiceStyle);
        List<InvoiceStyleColumn> GetByInvoiceID(int invoiceID);
    }
}
