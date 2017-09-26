using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceReportTranscriptionLinesQuery: InvoiceReportTranscriptionLinesQuery
    {
        public override List<InvoiceReportTranscriptionLineItem> Get(int invoiceID)
        {
            // Get invoice columns.
            List<InvoiceStyleColumn> columns;
            using (OracleInvoiceStyleColumnRepository rep = new OracleInvoiceStyleColumnRepository())
            {
                columns = rep.GetByInvoiceID(invoiceID);
            }

            InvoiceStyleColumn defCol = new InvoiceStyleColumn();

            // Build SQL.
            string groupingSql = "";
            if (GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.GroupingIndex == 1)) != "''")
            {
                groupingSql = String.Format("group by {0}, {1}, {2}"
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.GroupingIndex == 1))
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.GroupingIndex == 2))
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.GroupingIndex == 3)));
            }
            string sortSql = "";
            if (GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.SortIndex == 1)) != "''")
            {
                sortSql = String.Format("order by {0}, {1}, {2}"
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.SortIndex == 1))
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.SortIndex == 2))
                                           , GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.SortIndex == 3)));
            }
            string sql = String.Format("SELECT   {3} as column1 \r\n"
                                     + "        ,{4} as column2 \r\n"
                                     + "        ,{5} as column3 \r\n"
                                     + "        ,COUNT(*) AS JOB_COUNT \r\n"
                                     + "        ,SUM(WU.TAT_REDUCTION_CHARGED) AS TAT_REDUCTION_CHARGED \r\n"
                                     + "        ,SUM(WU.QTY_BILLABLE) AS QTY_BILLABLE \r\n"
                                     + "        ,sum(wu.raw_cost) as subtotal \r\n"
                                     + "        ,sum(wu.raw_cost - wu.tat_reduction_charged) as total \r\n"
                                     + "        ,{2} AS GROUPING_COLUMN \r\n"
                                     + "FROM    {0}.WORK_UNIT WU \r\n"
                                     + "JOIN    {0}.EXT_CLIENT EC \r\n"
                                     + "  ON    EC.EXT_SYS = WU.EXT_SYS \r\n"
                                     + " AND    EC.EXT_CLIENT_KEY = WU.EXT_CLIENT_KEY \r\n"
                                     + "JOIN    {0}.EXT_WORK_TYPE EWT \r\n"
                                     + "  ON    EWT.EXT_SYS = WU.EXT_SYS \r\n"
                                     + " AND    EWT.EXT_CLIENT_KEY = WU.EXT_CLIENT_KEY \r\n"
                                     + " AND    EWT.EXT_WORK_TYPE = WU.EXT_WORK_TYPE \r\n"
                                     + "WHERE   WU.INVOICE_ID = {1} \r\n"
                                     + "{6} \r\n"
                                     + "{7}"
                                     ,base.SchemaName
                                     ,invoiceID
                                     ,GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.GroupingIndex == 1 && c.Index == 0))
                                     ,GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.Index == 1))
                                     ,GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.Index == 2))
                                     ,GetColumnProperty(columns, (a => a.SqlColumn.ToString()), (c => c.Index == 3))
                                     ,groupingSql
                                     ,sortSql);

            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            //dt.TableName = "InvoiceReportTranscriptionLines";
            //dt.WriteXml("InvoiceReportTranscriptionLines.xml");
            //DataTable dt = new DataTable();
            //dt.ReadXml(@"C:\Documents and Settings\ilee\My Documents\PoC Projects\Dynamic Subreporting\Dynamic Subreporting\InvoiceReportTranscriptionLines.xml");
            return base.ConvertDataTableToList(dt);
        }

        private string GetColumnProperty(List<InvoiceStyleColumn> columns, Func<InvoiceStyleColumn, string> property, Func<InvoiceStyleColumn, bool> filter )
        {
            foreach (InvoiceStyleColumn col in columns)
            {
                if(filter(col))
                {
                    return property(col);
                }
            }
            return "''";
        }

        protected override void RowConverter(InvoiceReportTranscriptionLineItem item, DataRow row)
        {
            item.Column1 = row["column1"].ToString();
            item.Column2 = row["column2"].ToString();
            item.Column3 = row["column3"].ToString();
            item.ReportCount = int.Parse(row["job_count"].ToString());
            item.Units = String.IsNullOrEmpty(row["qty_billable"].ToString()) ? 0 : double.Parse(row["qty_billable"].ToString());
            item.TatCredit = String.IsNullOrEmpty(row["tat_reduction_charged"].ToString()) ? 0 : double.Parse(row["tat_reduction_charged"].ToString());
            item.GroupingColumn = row["grouping_column"].ToString();
            item.Subtotal = String.IsNullOrEmpty(row["subtotal"].ToString()) ? 0 : double.Parse(row["subtotal"].ToString());
            item.Total = String.IsNullOrEmpty(row["total"].ToString()) ? 0 : double.Parse(row["total"].ToString());
        }
    }
}
