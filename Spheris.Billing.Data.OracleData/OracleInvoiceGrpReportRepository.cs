
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using System.Linq;


namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceGrpReportRepository : InvoiceGrpReportRepositoryBase 
    {
        public override ObservableCollection<InvoiceGrpReport> GetById(int grpId)
        {
            ObservableCollection<InvoiceGrpReport> InvoiceGrpReports = new ObservableCollection<InvoiceGrpReport>();
            try
            {
                string sql = string.Format("select * from sphrsbilling.invoice_grp_report where invoice_grp_id={0}",grpId);
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
                InvoiceGrpReports = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return InvoiceGrpReports;
        }


        public override ObservableCollection<InvoiceGrpReport> GetByIdXLSOnly( int grpId)
        {
            ObservableCollection<InvoiceGrpReport> InvoiceGrpReports = new ObservableCollection<InvoiceGrpReport>();
            try
            {
                string sql = string.Format("select * from sphrsbilling.invoice_grp_report where invoice_grp_id={0} and file_type='XLS3'",grpId);
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
                InvoiceGrpReports = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return InvoiceGrpReports;
        }


        public override InvoiceGrpReport GetByIds(int reportId, int groupId)
        {
            string sql = String.Format("select * from SPHRSBILLING.invoice_grp_report where INVOICE_GRP_ID={0} and REPORT_TYPE_ID={1}",reportId,groupId);

            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

            if (dt.Rows.Count > 0)
            {
                InvoiceGrpReport igr = new InvoiceGrpReport();
                RowConverter(igr, dt.Rows[0]);
                return igr;
            }
            return null;
        }


        public override void Add(InvoiceGrpReport entity)
        {
            {
                // INVOICE_GRP_ID  NUMBER
                // REPORT_TYPE_ID  INTEGER
                // TAB_ORDER       INTEGER
                // FILE_TYPE       CHAR 4
                string sql = String.Format("insert into {0}.invoice_grp_report "
                                          + "  (INVOICE_GRP_ID"
                                          + "  ,REPORT_TYPE_ID"
                                          + "  ,TAB_ORDER"
                                          + "  ,FILE_TYPE) "
                                          + "values "
                                          + "  (:INVOICE_GRP_ID"
                                          + "  ,:REPORT_TYPE_ID"
                                          + "  ,:TAB_ORDER"
                                          + "  ,:FILE_TYPE)"
                                          , SchemaName);
                try
                {
                    List<OracleParameter> parameters = new List<OracleParameter>();
                    parameters.Add(OracleHelper.CreateParameter(":INVOICE_GRP_ID", entity.INVOICE_GRP_ID, OracleType.Number, ParameterDirection.Input));
                    parameters.Add(OracleHelper.CreateParameter(":REPORT_TYPE_ID", entity.REPORT_TYPE_ID, OracleType.Int32, ParameterDirection.Input));
                    parameters.Add(OracleHelper.CreateParameter(":TAB_ORDER", entity.TAB_ORDER, OracleType.Int32, ParameterDirection.Input));
                    parameters.Add(OracleHelper.CreateParameter(":FILE_TYPE",entity.FILE_TYPE, OracleType.NVarChar, ParameterDirection.Input));

                    OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, 
                                                              sql, 
                                                              parameters.ToArray<OracleParameter>());
                }
                catch (OracleException ex)
                {
                    throw ex;
                }
            }            
        }

        public override void Update(InvoiceGrpReport entity)
        {
            try
            {
                string sql = String.Format("update {0}.invoice_grp_report set "
                                          + "  TAB_ORDER = :TAB_ORDER "
                                          + "  FILE_TYPE = :FILE_TYPE "
                                          + "where REPORT_TYPE_ID = :REPORT_TYPE_ID and INVOICE_GRP_ID = :INVOICE_GRP_ID"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TAB_ORDER", entity.TAB_ORDER, OracleType.Int32, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FILE_TYPE", entity.FILE_TYPE, OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }


        public override void Remove(InvoiceGrpReport entity)
        {
            try
            {
                string sql = String.Format("DELETE FROM {0}.invoice_grp_report WHERE REPORT_TYPE_ID = :REPORT_TYPE_ID and INVOICE_GRP_ID = :INVOICE_GRP_ID", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":REPORT_TYPE_ID", entity.REPORT_TYPE_ID, OracleType.Int32, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INVOICE_GRP_ID", entity.INVOICE_GRP_ID, OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }


        protected override void RowConverter(InvoiceGrpReport item, DataRow row)
        {
            item.FILE_TYPE = row["FILE_TYPE"].ToString();
            item.INVOICE_GRP_ID =  int.Parse(row["INVOICE_GRP_ID"].ToString());
            item.REPORT_TYPE_ID = int.Parse(row["REPORT_TYPE_ID"].ToString());
            item.TAB_ORDER = int.Parse(row["TAB_ORDER"].ToString());
        }


        public override InvoiceGrpReport Get(InvoiceGrpReport entity)
        {
            throw new NotImplementedException();
        }
    }
}
