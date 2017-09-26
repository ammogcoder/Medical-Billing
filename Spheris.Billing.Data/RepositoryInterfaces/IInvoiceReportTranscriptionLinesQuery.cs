using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceReportTranscriptionLinesQuery
    {
        List<InvoiceReportTranscriptionLineItem> Get(int invoiceID);
    }
}
