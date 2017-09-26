using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceDetailRepository : InvoiceDetailRepositoryBase
    {
        public override void Add(ReportTypePackage package)
        {
            //  DESCR 		VARCHAR2
            //  REPORT_TYPE_ID	INTEGER
            //  SHORT_NAME	VARCHAR2
            //  SQL		VARCHAR2
            //  ALLOW_DYNAMIC_MODS  CHAR1
            //  DATA_DUMP_ONLY	CHAR1
            //  MUST_ENCRYPT	CHAR1
            string sql = String.Format("insert into {0}.REPORT_TYPE "
                                      + "  (DESCR"
                                      + "  ,REPORT_TYPE_ID"
                                      + "  ,SHORT_NAME"
                                      + "  ,SQL"
                                      + "  ,ALLOW_DYNAMIC_MODS"
                                      + "  ,DATA_DUMP_ONLY"
                                      + "  ,MUST_ENCRYPT) "
                                      + "values "
                                      + "  (:DESCR"
                                      + "  ,{0}.REPORT_TYPE_ID.nextval"
                                      + "  ,:SHORT_NAME"
                                      + "  ,:SQL"
                                      + "  ,:ALLOW_DYNAMIC_MODS"
                                      + "  ,:DATA_DUMP_ONLY"
                                      + "  ,:MUST_ENCRYPT)"
                                      + "  returning REPORT_TYPE_ID into :NEWID"
                                      , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DESCR", package.Description, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SHORT_NAME", package.ShortName, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SQL", package.Sql, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ALLOW_DYNAMIC_MODS", (package.AllowDynamicMod) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DATA_DUMP_ONLY", (package.DataDumpOnly) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":MUST_ENCRYPT", (package.MustEncrypt) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":NEWID", OracleType.Int32, ParameterDirection.InputOutput));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Id = int.Parse(parameters[ parameters.Count - 1 ].Value.ToString());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Delete(ReportTypePackage package)
        {
            try
            {
                string sql = String.Format("DELETE FROM {0}.REPORT_TYPE WHERE REPORT_TYPE_ID = :REPORT_TYPE_ID", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":REPORT_TYPE_ID", package.Id, OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(ReportTypePackage package)
        {
            if (package.Id == 0)
            {
                throw new ArgumentNullException("Item.InvoiceGrpId", "An item InvoiceGrpId was provided for the update.");
            }
            try
            {
                string sql = String.Format("update {0}.REPORT_TYPE set "
                                          + "  DESC = :DESC "
                                          + "  REPORT_TYPE_ID = :REPORT_TYPE_ID "
                                          + "  SHORT_NAME = :SHORT_NAME "
                                          + "  SQL = :SQL "
                                          + "  ,ALLOW_DYNAMIC_MODS = ALLOW_DYNAMIC_MODS "
                                          + "  ,DATA_DUMP_ONLY = DATA_DUMP_ONLY "
                                          + "  ,MUST_ENCRYPT = :MUST_ENCRYPT "
                                          + "where REPORT_TYPE_ID = :REPORT_TYPE_ID"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DESC", package.Description, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":REPORT_TYPE_ID", null, OracleType.Int32, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":SHORT_NAME", package.ShortName, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SQL", package.Sql, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ALLOW_DYNAMIC_MODS", package.AllowDynamicMod, OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DATA_DUMP_ONLY", package.DataDumpOnly, OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":MUST_ENCRYPT", package.MustEncrypt, OracleType.Char, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }


        public override ReportTypeTable MakeReportTable()
        {

            string sql = String.Format("select DESCR," +
                                        "REPORT_TYPE_ID," +
                                        "SHORT_NAME," +
                                        "SQL," +
                                        "ALLOW_DYNAMIC_MODS," +
                                        "DATA_DUMP_ONLY," +
                                        "MUST_ENCRYPT " +
                                        "from SPHRSBILLING.REPORT_TYPE");

            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            ObservableCollection<ReportTypePackage> rpt = ConvertDataTableToObservableCollection(dt);

            ReportTypeTable rtt = new ReportTypeTable();
            rtt.AvailableFields = GetAvailableFields();
            foreach (ReportTypePackage package in rpt)
            {
                package.Table = rtt;
                package.AvailableFields = rtt.AvailableFields;
                rtt.Add(package);
            }
            return rtt;
        }

        public override ObservableCollection<string> GetAvailableFields()
        {
            string sql = "Select cols.column_name as Name " + "FROM " + "ALL_TAB_COLUMNS cols " + "where 1=1 " + "and cols.table_name = 'CSV_DETAILS' " + "and cols.owner = 'SPHRSBILLING' " + "order by Name ";

            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            ObservableCollection<string> fields = new ObservableCollection<string>();
            foreach (DataRow row in dt.Rows)
                fields.Add( row["Name"].ToString() );
            return fields;
        }


        

        protected override void RowConverter(ReportTypePackage item, DataRow row)
        {
            item.Description = row["DESCR"].ToString();
            item.Id = int.Parse( row["REPORT_TYPE_ID"].ToString());
            item.ShortName = row["SHORT_NAME"].ToString();
            item.Sql = row["SQL"].ToString();
            if (row["ALLOW_DYNAMIC_MODS"] as char? == 'N')
                item.AllowDynamicMod = false;
            else
                item.AllowDynamicMod = false;


            //item.AllowDynamicMod = bool.Parse(row["ALLOW_DYNAMIC_MODS"].ToString());
            if (row["DATA_DUMP_ONLY"] as char? == 'N')
                item.DataDumpOnly = false;
            else
                item.DataDumpOnly = true;
            //item.DataDumpOnly = bool.Parse(row["DATA_DUMP_ONLY"].ToString());

            if (row["MUST_ENCRYPT"] as char? == 'Y')
                item.MustEncrypt = true;
        }

        public override ReportTypePackage RowConverter(IDataRecord record)
        {
            ReportTypePackage rtp = new ReportTypePackage();
            // TODO - Write the select statement
            //rtp.AllowDynamicMod = ...
            return rtp;
        }
#if DEAD
        public override void Add(ReportTypePackage item)
        {
            throw new NotImplementedException();
        }

        public override void Update(ReportTypePackage item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ReportTypePackage item)
        {
            throw new NotImplementedException();
        }

        public override ReportTypePackage Get(ReportTypePackage entity)
        {
            throw new NotImplementedException();
        }
#endif

    }
}
