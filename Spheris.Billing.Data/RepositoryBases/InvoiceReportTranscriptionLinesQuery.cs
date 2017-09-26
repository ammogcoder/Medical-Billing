using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Spheris.Billing.Data.RepositoryBases
{
    public class InvoiceReportTranscriptionLineItem
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public int ReportCount { get; set; }
        public double Units { get; set; }
        public double Subtotal { get; set; }
        public double TatCredit { get; set; }
        public double Total { get; set; }
        public string GroupingColumn { get; set; }
    }

    public abstract class InvoiceReportTranscriptionLinesQuery:RepositoryBase<InvoiceReportTranscriptionLineItem>, IInvoiceReportTranscriptionLinesQuery
    {
        public abstract List<InvoiceReportTranscriptionLineItem> Get(int invoiceID);
    }
}
