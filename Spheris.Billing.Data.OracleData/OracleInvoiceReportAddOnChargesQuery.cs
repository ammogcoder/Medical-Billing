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
    public class OracleInvoiceReportAddOnChargesQuery:InvoiceReportAddOnChargesQuery
    {
        public override List<InvoiceReportAddOnCharge> Get(int invoiceID)
        {
            string sql = String.Format("select  aoc.comments_for_billprint      ,aoc.qty                ,aoc.amt_each \r\n"
                                      + "       ,aoct.descr                     ,aoc.add_on_chg_type_id \r\n"
                                      + "from    {0}.add_on_chg aoc \r\n"
                                      + "join    {0}.add_on_chg_type aoct \r\n"
                                      + "  on    aoct.add_on_chg_type_id = aoc.add_on_chg_type_id \r\n"
                                      + "where   invoice_id = :invoice_id \r\n"
                                      + "  and   aoc.add_on_chg_type_id <> 14"  //TAT reduction this invoice
                                      , base.SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":invoice_id", invoiceID, OracleType.Int32, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            //dt.TableName = "InvoiceReportAddOnCharges";
            //dt.WriteXml("InvoiceReportAddOnCharges.xml");       // TODO: Remove after testing.
            //DataTable dt = new DataTable();
            //dt.ReadXml(@"C:\Documents and Settings\ilee\My Documents\PoC Projects\Dynamic Subreporting\Dynamic Subreporting\InvoiceReportAddOnCharges.xml");
            return ConvertDataTableToList(dt);
        }

        protected override void RowConverter(InvoiceReportAddOnCharge item, DataRow row)
        {
            item.AddOnChargeTypeId = row["add_on_chg_type_id"].ToString();
            item.Comments = row["comments_for_billprint"].ToString();
            item.Description = row["descr"].ToString();
            item.AmountEach = double.Parse(row["amt_each"].ToString());
            item.Quantity = double.Parse(row["qty"].ToString());
        }
    }
}
