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
    public class OracleInvoiceStyleColumnRepository: InvoiceStyleColumnRepositoryBase
    {
        public override List<InvoiceStyleColumn> GetByInvoiceID(int invoiceID)
        {
            string sql = String.Format("select  ig.invoice_style \r\n"
                                      + "       ,isc.column_id \r\n"
                                      + "       ,nvl(isco.column_index, isc.column_index) as column_index \r\n"
                                      + "       ,nvl(isco.column_name, isc.column_name) as column_name \r\n"
                                      + "       ,nvl(isco.sql_column_name, isc.sql_column_name) as sql_column_name \r\n"
                                      + "       ,nvl(isco.grouping_index, isc.grouping_index) as grouping_index \r\n"
                                      + "       ,nvl(isco.sort_index, isc.sort_index) as sort_index \r\n"
                                      + "       ,nvl(isco.width, isc.width) as width \r\n"
                                      + "from   {0}.invoice_grp ig \r\n"
                                      + "join   {0}.invoice_style_column isc \r\n"
                                      + "  on   ig.invoice_style = isc.invoice_style \r\n"
                                      + "left join {0}.invoice_grp_is_column_override isco \r\n"
                                      + "  on   isco.invoice_grp_id = ig.invoice_grp_id \r\n"
                                      + " and   isco.column_id = isc.column_id \r\n"
                                      + "where  ig.invoice_grp_id in (select  invoice_grp_id \r\n"
                                      + "                             from    {0}.invoice \r\n"
                                      + "                             where   invoice_id = :invoice_id) \r\n"
                                      , base.SchemaName);

            OracleParameter[] p = { OracleHelper.CreateParameter(":invoice_id", invoiceID, OracleType.Int32, ParameterDirection.Input) };
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return ConvertDataTableToList(dt);
        }

        public override List<InvoiceStyleColumn> GetByInvoiceStyle(string invoiceStyle)
        {
            throw new NotImplementedException();
        }

        public override InvoiceStyleColumn Get(InvoiceStyleColumn item)
        {
            throw new NotImplementedException();
        }

        public override void Add(InvoiceStyleColumn item)
        {
            throw new NotImplementedException();
        }

        public override void Update(InvoiceStyleColumn item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(InvoiceStyleColumn item)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(InvoiceStyleColumn item, DataRow row)
        {
            item.InvoiceStyle = row["invoice_style"].ToString();
            item.ID = int.Parse(row["column_id"].ToString());
            if (!String.IsNullOrEmpty(row["column_index"].ToString()))
            {
                item.Index = int.Parse(row["column_index"].ToString());
            }
            item.Name = row["column_name"].ToString();
            item.SqlColumn = row["sql_column_name"].ToString();
            if(!String.IsNullOrEmpty(row["grouping_index"].ToString()))
            {
                item.GroupingIndex = int.Parse(row["grouping_index"].ToString());
            }
            if (!String.IsNullOrEmpty(row["sort_index"].ToString()))
            {
                item.SortIndex = int.Parse(row["sort_index"].ToString());
            }
            item.Width = row["width"].ToString();
        }
   }
}
