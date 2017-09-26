 
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceRepository : InvoiceRepositoryBase
    {
        private string Where(string where, string clause)
        {
            if (clause == null)
                return where;
            if (string.IsNullOrEmpty(where))
                return " WHERE " + clause;
            return where + " AND " + clause;
        }

        public override ObservableCollection<InvoiceCount> FetchCount(DateTime start, DateTime end)
        {
            string sql = String.Format("select  bill_period_start, BILL_PERIOD_END_BEFORE, count(*) as THECOUNT"
            + " from  {0}.invoice "
            + " where BILL_PERIOD_START >= :BILL_PERIOD_START"
            + " and BILL_PERIOD_END_BEFORE <= :BILL_PERIOD_END_BEFORE"
            + " group by BILL_PERIOD_START, BILL_PERIOD_END_BEFORE"
            + " order by BILL_PERIOD_START DESC", SchemaName);
            ObservableCollection<InvoiceCount> invoiceCount = new ObservableCollection<InvoiceCount>();
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(OracleHelper.CreateParameter(":BILL_PERIOD_START", CheckNull(start), OracleType.DateTime, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":BILL_PERIOD_END_BEFORE", CheckNull(end), OracleType.DateTime, ParameterDirection.Input));
            
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

            foreach (DataRow row in dt.Rows)
            {
                InvoiceCount item = new InvoiceCount
                    {
                        BILL_PERIOD_START = CastDbValueRow(row, "BILL_PERIOD_START", true, "DateTime"),
                        BILL_PERIOD_END_BEFORE = CastDbValueRow(row, "BILL_PERIOD_END_BEFORE", true, "DateTime"),
                        COUNT = CastDbValueRow(row, "THECOUNT", true, "Int32")
                    };
                invoiceCount.Add(item); 
            }
            return invoiceCount;
        }

        public override int CountInvoices(decimal grpId)
        {
            string sql = String.Format("select count(*) as COUNTOF from {0}.INVOICE where invoice_grp_id={1}", SchemaName, grpId);
            OracleParameter[] p = null;
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            if (dt.Rows.Count > 0)
                return CastDbValueRow(dt.Rows[0], "COUNTOF", true, "Int32");
            return 0;
        }

        public override ObservableCollection<Invoice> FetchInvoices(DateTime? from, DateTime? to, decimal? grpId,bool asBatch = false)
        {
            ObservableCollection<Invoice> invoices = null;
            try
            {
                string where = string.Empty;
                List<OracleParameter> parameters = new List<OracleParameter>();

                string descWhere = string.Empty;
                if (from != null)
                {
                    if(asBatch)
                        descWhere = " I.BILL_PERIOD_START = :FROMdate and I.BILL_PERIOD_END_BEFORE = :TOdate ";
                    else
                        descWhere = " I.BILL_PERIOD_START >= :FROMdate and I.BILL_PERIOD_END_BEFORE <= :TOdate ";
                    where = Where(where, descWhere);
                    parameters.Add(OracleHelper.CreateParameter(":FROMdate", CheckNull(from), OracleType.DateTime, ParameterDirection.Input));
                    parameters.Add(OracleHelper.CreateParameter(":TOdate", CheckNull(to), OracleType.DateTime, ParameterDirection.Input));
                }

                if (grpId != null)
                {
                    where = Where(where, " I.INVOICE_GRP_ID = :grpId ");
                    parameters.Add(OracleHelper.CreateParameter(":grpId", CheckNull(grpId), OracleType.Number, ParameterDirection.Input));
                }
                if (where == null)
                    return null;

                string sql = String.Format("Select "
                 + "  I.BATCH_JOB_ID AS BATCH_JOB_ID"
                 + " ,I.BILL_PERIOD_END_BEFORE AS BILL_PERIOD_END_BEFORE"
                 + " ,I.BILL_PERIOD_START AS BILL_PERIOD_START"
                 + " ,I.CHAR_COMP_METHOD AS CHAR_COMP_METHOD"
                 + " ,I.CREATED_ON AS CREATED_ON"
                 + " ,I.ERR_MSG AS ERR_MSG"
                 + " ,I.EXPORTED_TO_GL_ON AS EXPORTED_TO_GL_ON"
                 + " ,I.INVOICE_GRP_ID AS INVOICE_GRP_ID"
                 + " ,I.INVOICE_ID AS INVOICE_ID"
                 + " ,I.INVOICE_STATUS AS INVOICE_STATUS"
                 + " ,I.JOB_COMPLETED_ON AS JOB_COMPLETED_ON"
                 + " ,I.LINE_ITEM_TOTAL AS LINE_ITEM_TOTAL"
                 + " ,I.NET_AMT_BILLED AS NET_AMT_BILLED"
                 + " ,I.PAYMENT_DUE AS PAYMENT_DUE"
                 + " ,I.PAYMENT_RECVD AS PAYMENT_RECVD"
                 + " ,I.PAYMENT_TERMS_START AS PAYMENT_TERMS_START"
                 + " ,I.RELEASED_TO_PORTAL_ON AS RELEASED_TO_PORTAL_ON"
                 + " ,I.REVERSED_BATCH_JOB_ID AS REVERSED_BATCH_JOB_ID"
                 + " ,I.TARGET_INVOICE_NBR AS TARGET_INVOICE_NBR"
                 + " ,I.TAT_REDUCTION AS TAT_REDUCTION"
                 + " ,I.UNITS_BILLED AS UNITS_BILLED"
                 + " ,IG.DESCR AS DESCR"
                 + " FROM {0}.INVOICE I "
                 + " JOIN {0}.INVOICE_GRP IG "
                 + " ON I.INVOICE_GRP_ID = IG.INVOICE_GRP_ID ", SchemaName);

                sql += where;

                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

                invoices = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return invoices;
        }
  

















        protected override void RowConverter(Invoice item, DataRow row)
        {
            item.BATCH_JOB_ID = CastDbValueRow(row,"BATCH_JOB_ID",true,"decimal");
            item.BILL_PERIOD_END_BEFORE = CastDbValueRow(row, "BILL_PERIOD_END_BEFORE", true, "DateTime");
            item.BILL_PERIOD_START = CastDbValueRow(row, "BILL_PERIOD_START", true, "DateTime");
            item.CHAR_COMP_METHOD = CastDbValueRow(row, "CHAR_COMP_METHOD");
            item.CREATED_ON = CastDbValueRow(row, "CREATED_ON", true, "DateTime");
            item.ERR_MSG = CastDbValueRow(row, "ERR_MSG");
            item.EXPORTED_TO_GL_ON = CastDbValueRow(row, "EXPORTED_TO_GL_ON", true, "DateTime");
            item.INVOICE_GRP_ID = CastDbValueRow(row, "INVOICE_GRP_ID", true, "decimal");
            item.INVOICE_ID = CastDbValueRow(row, "INVOICE_ID", true, "decimal");
            item.INVOICE_STATUS = CastDbValueRow(row, "INVOICE_STATUS");
            item.JOB_COMPLETED_ON = CastDbValueRow(row, "JOB_COMPLETED_ON", true, "DateTime");
            item.LINE_ITEM_TOTAL = CastDbValueRow(row, "LINE_ITEM_TOTAL", true, "decimal");
            item.NET_AMT_BILLED = CastDbValueRow(row, "NET_AMT_BILLED", true, "decimal");
            item.PAYMENT_DUE = CastDbValueRow(row, "PAYMENT_DUE", true, "DateTime");
            item.PAYMENT_RECVD = CastDbValueRow(row, "PAYMENT_RECVD", true, "DateTime");
            item.PAYMENT_TERMS_START = CastDbValueRow(row, "PAYMENT_TERMS_START", true, "DateTime");
            item.RELEASED_TO_PORTAL_ON = CastDbValueRow(row, "RELEASED_TO_PORTAL_ON", true, "DateTime");
            item.REVERSED_BATCH_JOB_ID = CastDbValueRow(row, "REVERSED_BATCH_JOB_ID", true, "decimal");
            item.TARGET_INVOICE_NBR = CastDbValueRow(row, "TARGET_INVOICE_NBR");
            item.TAT_REDUCTION = CastDbValueRow(row, "TAT_REDUCTION", true, "decimal");
            item.UNITS_BILLED = CastDbValueRow(row, "UNITS_BILLED", true, "decimal");
            item.DESCR = CastDbValueRow(row, "DESCR" );
            item.Modified = false;
            

        }

        public override void Remove(Invoice client)
        {
            throw new NotImplementedException();
        }
      
        public override Invoice Get(Invoice entity)
        {
            throw new NotImplementedException();
        }


        public override void Add(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}
