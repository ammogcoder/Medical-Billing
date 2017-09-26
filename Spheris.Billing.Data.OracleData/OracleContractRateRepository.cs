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
    public class OracleContractRateRepository : ContractRateRepositoryBase
    {
        public override void Add(ContractRate r)
        {
            string sql = String.Format("insert into {0}.CONTRACT_RATE "
                                      + "("
            + "  BEGIN_ON"
            + ", CHAR_COMP_METHOD"
            + ", CONTRACT_ID"
            + ", CONTRACT_RATE_ID "
            + ", DICTATOR_SR_THRESHOLD "
            + ", DOCUMENT_SR_THRESHOLD "
            + ", END_AFTER "
            + ", FAX_COMP_METHOD "
            + ", FAX_RATE "
            + ", FRONTEND_SR_RATE "
            + ", HOLIDAY_RATE_DELTA "
            + ", INCLUDE_INHOUSE_IN_TAT_CALC "
            + ", INHOUSE_SR_RATE "
            + ", INHOUSE_TR_RATE "
            + ", OFFSHORE_RATE "
            + ", SR_OFFSHORE_RATE "
            + ", SR_RATE "
            + ", STAT_COMP_METHOD "
            + ", STAT_PREMIUM "
            + ", STAT_THRESHOLD "
            + ", STD_RATE "
            + ", SYLCOUNT "
            + ", TECHNOLOGY_CHG "
            + ", EER_SR_THRESHOLD "
            + " ) "
            + "values "
            + " ( "
            + "  :BEGIN_ON"
            + ", :CHAR_COMP_METHOD"
            + ", :CONTRACT_ID"
            + ", {0}.CONTRACT_RATE_ID.nextval "
            + ", :DICTATOR_SR_THRESHOLD "
            + ", :DOCUMENT_SR_THRESHOLD "
            + ", :END_AFTER "
            + ", :FAX_COMP_METHOD "
            + ", :FAX_RATE "
            + ", :FRONTEND_SR_RATE "
            + ", :HOLIDAY_RATE_DELTA "
            + ", :INCLUDE_INHOUSE_IN_TAT_CALC "
            + ", :INHOUSE_SR_RATE "
            + ", :INHOUSE_TR_RATE "
            + ", :OFFSHORE_RATE "
            + ", :SR_OFFSHORE_RATE "
            + ", :SR_RATE "
            + ", :STAT_COMP_METHOD "
            + ", :STAT_PREMIUM "
            + ", :STAT_THRESHOLD "
            + ", :STD_RATE "
            + ", :SYLCOUNT "
            + ", :TECHNOLOGY_CHG "
            + ", :EER_SR_THRESHOLD "
            + " ) "
            + " returning CONTRACT_RATE_ID into :RATEID "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":BEGIN_ON", CheckNull(r.BEGIN_ON), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CHAR_COMP_METHOD", CheckNull(r.CHAR_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DICTATOR_SR_THRESHOLD", CheckNull(r.DICTATOR_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DOCUMENT_SR_THRESHOLD", CheckNull(r.DOCUMENT_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":END_AFTER", CheckNull(r.END_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FAX_COMP_METHOD", CheckNull(r.FAX_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FAX_RATE", CheckNull(r.FAX_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FRONTEND_SR_RATE", CheckNull(r.FRONTEND_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":HOLIDAY_RATE_DELTA", CheckNull(r.HOLIDAY_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INCLUDE_INHOUSE_IN_TAT_CALC", (r.INCLUDE_INHOUSE_IN_TAT_CALC) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE", CheckNull(r.INHOUSE_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE", CheckNull(r.INHOUSE_TR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE", CheckNull(r.OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE", CheckNull(r.SR_OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE", CheckNull(r.SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_COMP_METHOD", CheckNull(r.STAT_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_PREMIUM", CheckNull(r.STAT_PREMIUM), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_THRESHOLD", CheckNull(r.STAT_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE", CheckNull(r.STD_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SYLCOUNT", CheckNull(r.SYLCOUNT), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TECHNOLOGY_CHG", CheckNull(r.TECHNOLOGY_CHG), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":EER_SR_THRESHOLD", CheckNull(r.EER_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));

                parameters.Add(OracleHelper.CreateParameter(":RATEID", OracleType.Int32, ParameterDirection.InputOutput));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.CONTRACT_RATE_ID = int.Parse(parameters[parameters.Count - 1].Value.ToString());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public override void Remove(ContractRate package)
        {
            string sql;
            try
            {
                //string sql = String.Format("DELETE FROM {0}.CONTRACT_RATE WHERE CONTRACT_RATE_ID = :CONTRACT_RATE_ID", SchemaName);
                sql = String.Format("DELETE FROM {0}.CONTRACT_RATE_ALT WHERE CONTRACT_RATE_ID = :CONTRACT_RATE_ID", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", package.CONTRACT_RATE_ID, OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

                sql = String.Format("DELETE FROM {0}.CONTRACT_RATE WHERE CONTRACT_RATE_ID = :CONTRACT_RATE_ID", SchemaName);
                parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", package.CONTRACT_RATE_ID, OracleType.Int32, ParameterDirection.Input));
                outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

            }
            catch (OracleException ex)
            {
                throw ex;
                //throw ex;
            }

        }

        public override void Update(ContractRate r)
        {
            //r.BEGIN_ON =                   :BEGIN_ON =
            //r.CHAR_COMP_METHOD =           :CHAR_COMP_METHOD =
            //r.CONTRACT_ID =                :CONTRACT_ID =
            //r.CONTRACT_RATE_ID =           :CONTRACT_RATE_ID = 
            //r.DICTATOR_SR_THRESHOLD =      :DICTATOR_SR_THRESHOLD = 
            //r.DOCUMENT_SR_THRESHOLD =      :DOCUMENT_SR_THRESHOLD = 
            //r.END_AFTER =                  :END_AFTER = 
            //r.FAX_COMP_METHOD =            :FAX_COMP_METHOD = 
            //r.FAX_RATE =                   :FAX_RATE = 
            //r.FRONTEND_SR_RATE =           :FRONTEND_SR_RATE = 
            //r.HOLIDAY_RATE_DELTA =         :HOLIDAY_RATE_DELTA = 
            //r.INCLUDE_INHOUSE_IN_TAT_CALC =:INCLUDE_INHOUSE_IN_TAT_CALC =  
            //r.INHOUSE_SR_RATE =            :INHOUSE_SR_RATE = 
            //r.INHOUSE_TR_RATE =            :INHOUSE_TR_RATE = 
            //r.OFFSHORE_RATE =              :OFFSHORE_RATE = 
            //r.SR_OFFSHORE_RATE =           :SR_OFFSHORE_RATE = 
            //r.SR_RATE =                    :SR_RATE = 
            //r.STAT_COMP_METHOD =           :STAT_COMP_METHOD = 
            //r.STAT_PREMIUM =               :STAT_PREMIUM = 
            //r.STAT_THRESHOLD =             :STAT_THRESHOLD = 
            //r.STD_RATE =                   :STD_RATE = 
            //r.SYLCOUNT =                   :SYLCOUNT = 
            //r.TECHNOLOGY_CHG =             :TECHNOLOGY_CHG 
            //                               :

            if (r.CONTRACT_RATE_ID == 0)
            {
                throw new ArgumentNullException("Item.InvoiceGrpId", "An item InvoiceGrpId was provided for the update.");
            }
            try
            {
                string sql = String.Format("update {0}.CONTRACT_RATE set "
                                          + " BEGIN_ON = :BEGIN_ON "
                                          + " ,CHAR_COMP_METHOD = :CHAR_COMP_METHOD "
                                          + " ,CONTRACT_ID = :CONTRACT_ID "
                                          + " ,DICTATOR_SR_THRESHOLD = :DICTATOR_SR_THRESHOLD "
                                          + " ,DOCUMENT_SR_THRESHOLD = :DOCUMENT_SR_THRESHOLD "
                                          + " ,END_AFTER = :END_AFTER "
                                          + " ,FAX_COMP_METHOD = :FAX_COMP_METHOD "
                                          + " ,FAX_RATE = :FAX_RATE "
                                          + " ,FRONTEND_SR_RATE = :FRONTEND_SR_RATE "
                                          + " ,HOLIDAY_RATE_DELTA = :HOLIDAY_RATE_DELTA "
                                          + " ,INCLUDE_INHOUSE_IN_TAT_CALC = :INCLUDE_INHOUSE_IN_TAT_CALC"
                                          + " ,INHOUSE_SR_RATE = :INHOUSE_SR_RATE "
                                          + " ,INHOUSE_TR_RATE = :INHOUSE_TR_RATE "
                                          + " ,OFFSHORE_RATE = :OFFSHORE_RATE "
                                          + " ,SR_OFFSHORE_RATE = :SR_OFFSHORE_RATE "
                                          + " ,SR_RATE = :SR_RATE "
                                          + " ,STAT_COMP_METHOD = :STAT_COMP_METHOD "
                                          + " ,STAT_PREMIUM = :STAT_PREMIUM "
                                          + " ,STAT_THRESHOLD = :STAT_THRESHOLD "
                                          + " ,STD_RATE = :STD_RATE "
                                          + " ,SYLCOUNT = :SYLCOUNT "
                                          + " ,TECHNOLOGY_CHG = :TECHNOLOGY_CHG "
                                          + " ,EER_SR_THRESHOLD = :EER_SR_THRESHOLD "
                                          + " where CONTRACT_RATE_ID = :CONTRACT_RATE_ID", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":BEGIN_ON", CheckNull( r.BEGIN_ON), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CHAR_COMP_METHOD", CheckNull(r.CHAR_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DICTATOR_SR_THRESHOLD", CheckNull(r.DICTATOR_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DOCUMENT_SR_THRESHOLD", CheckNull(r.DOCUMENT_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":END_AFTER", CheckNull(r.END_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FAX_COMP_METHOD", CheckNull(r.FAX_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FAX_RATE", CheckNull(r.FAX_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FRONTEND_SR_RATE", CheckNull(r.FRONTEND_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":HOLIDAY_RATE_DELTA", CheckNull(r.HOLIDAY_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INCLUDE_INHOUSE_IN_TAT_CALC", (r.INCLUDE_INHOUSE_IN_TAT_CALC) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE", CheckNull(r.INHOUSE_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE", CheckNull(r.INHOUSE_TR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE", CheckNull(r.OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE", CheckNull(r.SR_OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE", CheckNull(r.SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_COMP_METHOD", CheckNull(r.STAT_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_PREMIUM", CheckNull(r.STAT_PREMIUM), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STAT_THRESHOLD", CheckNull(r.STAT_THRESHOLD), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE", CheckNull(r.STD_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SYLCOUNT", CheckNull(r.SYLCOUNT), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TECHNOLOGY_CHG", CheckNull(r.TECHNOLOGY_CHG), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":EER_SR_THRESHOLD", CheckNull(r.EER_SR_THRESHOLD), OracleType.Number, ParameterDirection.Input));

                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", CheckNull(r.CONTRACT_RATE_ID), OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

#if ASDF
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
            ObservableCollection<ContractRate> rpt = ConvertDataTableToObservableCollection(dt);

            ReportTypeTable rtt = new ReportTypeTable();
            rtt.AvailableFields = GetAvailableFields();
            foreach (ContractRate package in rpt)
            {
                package.Table = rtt;
                package.AvailableFields = rtt.AvailableFields;
                rtt.Add(package);
            }
            return rtt;
        }
#endif

#if ASDF
        public override ObservableCollection<string> GetAvailableFields()
        {
            string sql = "Select cols.column_name as Name " + "FROM " + "ALL_TAB_COLUMNS cols " + "where 1=1 " + "and cols.table_name = 'CSV_DETAILS' " + "and cols.owner = 'SPHRSBILLING' " + "order by Name ";

            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            ObservableCollection<string> fields = new ObservableCollection<string>();
            foreach (DataRow row in dt.Rows)
                fields.Add(row["Name"].ToString());
            return fields;
        }

#endif


#if TODO
        protected override void RowConverter(ContractRate item, DataRow row)
        0{
            item.Description = row["DESCR"].ToString();
            item.Id = int.Parse(row["REPORT_TYPE_ID"].ToString());
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
#endif

        protected override void RowConverter(ContractRate r, DataRow record)
        {
            try
            {
                r.BEGIN_ON = CastDbValueRow(record, "BEGIN_ON", true, "DateTime");
                r.CHAR_COMP_METHOD = CastDbValueRow(record, "CHAR_COMP_METHOD");
                r.CONTRACT_ID = CastDbValueRow(record, "CONTRACT_ID", true, "decimal");
                r.CONTRACT_RATE_ID = CastDbValueRow(record, "CONTRACT_RATE_ID", true, "Int32");
                r.DICTATOR_SR_THRESHOLD = CastDbValueRow(record, "DICTATOR_SR_THRESHOLD",true,"decimal");
                r.DOCUMENT_SR_THRESHOLD = CastDbValueRow(record, "DOCUMENT_SR_THRESHOLD", true, "decimal");
                r.END_AFTER = CastDbValueRow(record, "END_AFTER", true, "DateTime");
                r.FAX_COMP_METHOD = CastDbValueRow(record, "FAX_COMP_METHOD");
                r.FAX_RATE = CastDbValueRow(record, "FAX_RATE", true, "decimal");
                r.FRONTEND_SR_RATE = CastDbValueRow(record, "FRONTEND_SR_RATE", true, "decimal");
                r.HOLIDAY_RATE_DELTA = CastDbValueRow(record, "HOLIDAY_RATE_DELTA", true, "decimal");
                r.INCLUDE_INHOUSE_IN_TAT_CALC = CastDbValueRow(record, "INCLUDE_INHOUSE_IN_TAT_CALC",true,"Bool");
                r.INHOUSE_SR_RATE = CastDbValueRow(record, "INHOUSE_SR_RATE", true, "decimal");
                r.INHOUSE_TR_RATE = CastDbValueRow(record, "INHOUSE_TR_RATE", true, "decimal");
                r.OFFSHORE_RATE = CastDbValueRow(record, "OFFSHORE_RATE",true,"decimal");
                r.SR_OFFSHORE_RATE = CastDbValueRow(record, "SR_OFFSHORE_RATE", true, "decimal");
                r.SR_RATE = CastDbValueRow(record, "SR_RATE", true, "decimal");
                r.STAT_COMP_METHOD = CastDbValueRow(record, "STAT_COMP_METHOD");
                r.STAT_PREMIUM = CastDbValueRow(record, "STAT_PREMIUM", true, "decimal");
                r.STAT_THRESHOLD = CastDbValueRow(record, "STAT_THRESHOLD", true, "decimal");
                r.STD_RATE = CastDbValueRow(record, "STD_RATE", true, "decimal");
                r.SYLCOUNT = CastDbValueRow(record, "SYLCOUNT", true, "decimal");
                r.TECHNOLOGY_CHG = CastDbValueRow(record, "TECHNOLOGY_CHG", true, "decimal");
                r.EER_SR_THRESHOLD = CastDbValueRow(record, "EER_SR_THRESHOLD", true, "decimal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#if ASDFASDF
        protected override void RowConverter(ContractRate r, IDataRecord record)
        {
            //ContractRate r = new ContractRate();
            r.BEGIN_ON = CastDbValue(record,"BEGIN_ON",true,"DateTime");
            r.CHAR_COMP_METHOD = CastDbValue(record,"CHAR_COMP_METHOD");
            r.CONTRACT_ID = CastDbValue(record,"CONTRACT_ID", true, "Int32");
            r.CONTRACT_RATE_ID = CastDbValue(record,"CONTRACT_RATE_ID", true, "Int32");
            r.DICTATOR_SR_THRESHOLD = CastDbValue(record,"DICTATOR_SR_THRESHOLD");
            r.DOCUMENT_SR_THRESHOLD = CastDbValue(record,"DOCUMENT_SR_THRESHOLD");
            r.END_AFTER = CastDbValue(record,"END_AFTER",true,"DateTime");
            r.FAX_COMP_METHOD = CastDbValue(record,"FAX_COMP_METHOD");
            r.FAX_RATE = CastDbValue(record,"FAX_RATE");
            r.FRONTEND_SR_RATE = CastDbValue(record,"FRONTEND_SR_RATE");
            r.HOLIDAY_RATE_DELTA = CastDbValue(record,"HOLIDAY_RATE_DELTA");
            r.INCLUDE_INHOUSE_IN_TAT_CALC = CastDbValue(record,"INCLUDE_INHOUSE_IN_TAT_CALC");
            r.INHOUSE_SR_RATE = CastDbValue(record,"INHOUSE_SR_RATE");
            r.INHOUSE_TR_RATE = CastDbValue(record,"INHOUSE_TR_RATE");
            r.OFFSHORE_RATE = CastDbValue(record,"OFFSHORE_RATE");
            r.SR_OFFSHORE_RATE = CastDbValue(record,"SR_OFFSHORE_RATE");
            r.SR_RATE = CastDbValue(record,"SR_RATE");
            r.STAT_COMP_METHOD = CastDbValue(record,"STAT_COMP_METHOD");
            r.STAT_PREMIUM = CastDbValue(record,"STAT_PREMIUM");
            r.STAT_THRESHOLD = CastDbValue(record,"STAT_THRESHOLD");
            r.STD_RATE = CastDbValue(record,"STD_RATE");
            r.SYLCOUNT = CastDbValue(record,"SYLCOUNT");
            return ;
        }
#endif

#if DEAD
        public override void Add(ContractRate item)
        {
            throw new NotImplementedException();
        }

        public override void Update(ContractRate item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ContractRate item)
        {
            throw new NotImplementedException();
        }

        public override ContractRate Get(ContractRate entity)
        {
            throw new NotImplementedException();
        }
#endif


        public override ObservableCollection<ContractRate> GetRates(int contractId)
        {
            ObservableCollection<ContractRate> contractRates;
            try
            {
                string sql = String.Format("Select * from {0}.CONTRACT_RATE where CONTRACT_ID={1} ORDER BY BEGIN_ON DESC", SchemaName, contractId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                contractRates = ConvertDataTableToObservableCollection(dt);


            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contractRates;
        }

        public override ContractRate Get(ContractRate entity)
        {
            throw new NotImplementedException();
        }

        //public override void Remove(ContractRate entity)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
