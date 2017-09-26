using Spheris.Billing.Core.Domain;
using Spheris.Billing.Core.Exceptions;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceReportParametersQuery:InvoiceReportParametersQuery
    {
        public override Dictionary<string, string> Get(int invoiceID)
        {
            string sql = String.Format("select   i.created_on as \"InvoiceDate\" "
                                     + "        ,ig.descr as \"Location\" "
                                     + "        ,ig.invoice_grp_id || '-' || i.invoice_id as \"InvoiceNumber\" "
                                     + "        ,pt.descr as \"PaymentTerms\" "
                                     + "        ,ig.attn_line_heading as \"AttnLineHeading\" "
                                     + "        ,ig.attn_line as \"AttnLine\" "
                                     + "        ,rt.line1 as \"RemitToLine1\" "
                                     + "        ,rt.line2 as \"RemitToLine2\" "
                                     + "        ,rt.line3 as \"RemitToLine3\" "
                                     + "        ,rt.line4 as \"RemitToLine4\" "
                                     + "        ,i.bill_period_start as \"InvoiceBeginDate\" "
                                     + "        ,i.bill_period_end_before as \"InvoiceEndDate\" "
                                     + "        ,bs.display_title as \"BillSpecialistTitle\" "
                                     + "        ,bs.email as \"BillSpecialistEmail\" "
                                     + "        ,bs.name as \"BillSpecialistName\" "
                                     + "        ,bs.phone as \"BillSpecialistPhone\" "
                                     + "        ,ig.invoice_style as \"InvoiceStyle\" "
                                     + "        ,ccm.invoice_column_header as \"UnitsTitle\" "
                                     + "        ,ig.bw_invoice_style as \"IsBWInvoice\" "
                                     + "        ,ccm.invoice_column_header as \"UnitsColumn\" "
                                     + "        ,ig.invoice_grp_id as \"InvoiceGroupID\" "
                                     + "        ,ig.descr as \"InvoiceGroupDescription\" "
                                     + "        ,ig.default_bill_file_path as \"DefaultFilePath\" "
                                     + "        ,case when tcm.show_tat_column_on_invoice = 'N' or cts.auto_chg = 'N' then 0 else 1 end \"ShowTatColumn\" "
                                     + "        ,ist.engine_version as \"EngineVersion\" "
                                     + "from    {0}.invoice i "
                                     + "join    {0}.invoice_grp ig "
                                     + "  on    ig.invoice_grp_id = i.invoice_grp_id "
                                     + "join    {0}.invoice_style ist "
                                     + "  on    ist.invoice_style = ig.invoice_style "
                                     + "join    {0}.contract c "
                                     + "  on    c.contract_id = ig.contract_id "
                                     + "join    {0}.payment_terms pt "
                                     + "  on    pt.payment_terms = c.payment_terms "
                                     + "join    {0}.remit_to rt "
                                     + "  on    rt.remit_to_id = ig.remit_to_id "
                                     + "join    {0}.bill_specialist bs "
                                     + "  on    bs.bill_specialist_id = ig.bill_specialist_id "
                                     + "join    {0}.char_comp_method ccm "
                                     + "  on    ccm.char_comp_method = i.char_comp_method "
                                     + "join    {0}.contract_tat_sched cts "
                                     + "  on    cts.contract_id = c.contract_id "
                                     + " and    cts.end_after >= i.bill_period_start "
                                     + " and    cts.begin_on <= i.bill_period_start "
                                     + "join    {0}.tat_sched ts "
                                     + "  on    ts.tat_sched_id = cts.tat_sched_id "
                                     + "join    {0}.tat_comp_method tcm "
                                     + "  on    tcm.tat_comp_method = ts.tat_comp_method "
                                     + "where   i.invoice_id = :invoice_id "
                                     , base.SchemaName);

            OracleParameter[] parameters = {OracleHelper.CreateParameter(":invoice_id", invoiceID, System.Data.OracleClient.OracleType.Number, System.Data.ParameterDirection.Input)};
            DataTable t = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, parameters);
            Debug.Assert(t.Rows.Count == 1, "There should only be one row returned by this query.");
            if (t.Rows.Count == 0)
            {
                throw new InvoiceNotFoundException(invoiceID);
            }
            Dictionary<string, string> rptParams = DataHelper.ConvertRowToDictionary(t.Rows[0], t.Columns);

            // Add invoice column names.
            OracleInvoiceStyleColumnRepository rep = new OracleInvoiceStyleColumnRepository();
            List<InvoiceStyleColumn> cols = rep.GetByInvoiceID(invoiceID);
            foreach (InvoiceStyleColumn col in cols.Where(c => c.Index > 0))
            {
                int ndx = col.Index;
                rptParams.Add(String.Format("Column{0}Name", ndx), col.Name);
            }

            return rptParams;
        }

        protected override void RowConverter(Dictionary<string, string> item, DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
