using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceGroupReportsInfoQuery:InvoiceGroupReportsInfoQuery
    {
        public override List<InvoiceGroupReportsInfoQueryResult> Get(int invoiceGroupID)
        {
            string sql = String.Format(  "select  igr.invoice_grp_id              ,igr.report_type_id             ,igr.tab_order \r\n"
                                       + "        ,rt.short_name                  ,igr.file_type                  ,rt.data_dump_only \r\n"
                                       + "from    {0}.invoice_grp_report igr \r\n"
                                       + "join    {0}.report_type rt \r\n"
                                       + "  on    rt.report_type_id = igr.report_type_id \r\n"
                                       + "where   igr.invoice_grp_id = :invoiceGroupID \r\n"
                                       + "order by igr.tab_order"
                                      , base.SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":invoiceGroupID", invoiceGroupID, OracleType.Number, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return base.ConvertDataTableToList(dt);
        }

        public override List<InvoiceGroupReportsInfoQueryResult> GetByInvoiceID(int invoiceID)
        {
            string sql = String.Format("select  igr.invoice_grp_id              ,igr.report_type_id             ,igr.tab_order \r\n"
                                       + "        ,rt.short_name                  ,igr.file_type                  ,rt.data_dump_only \r\n"
                                       + "from    {0}.invoice_grp_report igr \r\n"
                                       + "join    {0}.report_type rt \r\n"
                                       + "  on    rt.report_type_id = igr.report_type_id \r\n"
                                       + "where   igr.invoice_grp_id in (select  invoice_grp_id \r\n"
                                       + "                               from    {0}.invoice \r\n"
                                       + "                               where   invoice_id = :invoiceID) \r\n"
                                       + "order by igr.tab_order"
                                      , base.SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":invoiceID", invoiceID, OracleType.Number, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return base.ConvertDataTableToList(dt);
        }

        protected override void RowConverter(InvoiceGroupReportsInfoQueryResult item, DataRow row)
        {
            item.InvoiceGroupID = int.Parse(row["invoice_grp_id"].ToString());
            item.ReportTypeID = int.Parse(row["report_type_id"].ToString());
            item.TabOrder = int.Parse(row["tab_order"].ToString());
            item.ReportTypeShortName = row["short_name"].ToString();
            item.FileType = row["file_type"].ToString();
            item.IsDataDump = (row["data_dump_only"].ToString() == "Y");
        }
    }
}
