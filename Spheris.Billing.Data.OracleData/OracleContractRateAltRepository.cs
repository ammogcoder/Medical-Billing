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
    public class OracleContractRateAltRepository : ContractRateAltRepositoryBase
    {
        public override void Add(ContractRateAlt r)
        {
            string sql = String.Format("insert into {0}.CONTRACT_RATE_ALT"
                                     + "("
            + "  CONTRACT_RATE_ID"
            + ", DESCR"
            + ", INHOUSE_SR_RATE"
            + ", INHOUSE_SR_RATE_DELTA "
            + ", INHOUSE_TR_RATE "
            + ", INHOUSE_TR_RATE_DELTA "
            + ", OFFSHORE_RATE "
            + ", OFFSHORE_RATE_DELTA "
            + ", RATE_NBR "
            + ", SR_OFFSHORE_RATE "
            + ", SR_OFFSHORE_RATE_DELTA "
            + ", SR_RATE "
            + ", SR_RATE_DELTA "
            + ", STD_RATE "
            + ", STD_RATE_DELTA "
            + " ) "
            + "values "
            + " ( "
            + "  :CONTRACT_RATE_ID"
            + ", :DESCR"
            + ", :INHOUSE_SR_RATE"
            + ", :INHOUSE_SR_RATE_DELTA "
            + ", :INHOUSE_TR_RATE "
            + ", :INHOUSE_TR_RATE_DELTA "
            + ", :OFFSHORE_RATE "
            + ", :OFFSHORE_RATE_DELTA "
            + ", :RATE_NBR "
            + ", :SR_OFFSHORE_RATE "
            + ", :SR_OFFSHORE_RATE_DELTA "
            + ", :SR_RATE "
            + ", :SR_RATE_DELTA "
            + ", :STD_RATE "
            + ", :STD_RATE_DELTA "
            + " ) "
           // + " returning RATE_NBR into :RATE_NBR "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", CheckNull(r.CONTRACT_RATE_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE", CheckNull(r.INHOUSE_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE_DELTA", CheckNull(r.INHOUSE_SR_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE", CheckNull(r.INHOUSE_TR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE_DELTA", CheckNull(r.INHOUSE_TR_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE", CheckNull(r.OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE_DELTA", CheckNull(r.OFFSHORE_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":RATE_NBR", CheckNull(r.RATE_NBR), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE", CheckNull(r.SR_OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE_DELTA", r.SR_OFFSHORE_RATE_DELTA  , OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE", CheckNull(r.SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE_DELTA", CheckNull(r.SR_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE", CheckNull(r.STD_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE_DELTA", CheckNull(r.STD_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
               // parameters.Add(OracleHelper.CreateParameter(":RATE_NBR", OracleType.Int32, ParameterDirection.InputOutput));
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

        public override void Remove(ContractRateAlt package)
        {

            try
            {
                string sql = String.Format("DELETE FROM {0}.CONTRACT_RATE_ALT WHERE CONTRACT_RATE_ID = :CONTRACT_RATE_ID and RATE_NBR = :RATE_NBR", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", package.CONTRACT_RATE_ID, OracleType.Int32, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":RATE_NBR", package.RATE_NBR, OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
                //throw ex;
            }
        }

        public override void Update(ContractRateAlt package)
        {

            try
            {
                
                string sql = String.Format("update {0}.CONTRACT_RATE_ALT set "
                                          + "  DESCR = :DESCR "
                                          + "  ,INHOUSE_SR_RATE = :INHOUSE_SR_RATE "
                                          + "  ,INHOUSE_SR_RATE_DELTA = :INHOUSE_SR_RATE_DELTA "
                                          + "  ,INHOUSE_TR_RATE = :INHOUSE_TR_RATE "
                                          + "  ,INHOUSE_TR_RATE_DELTA = :INHOUSE_TR_RATE_DELTA "
                                          + "  ,OFFSHORE_RATE = :OFFSHORE_RATE "
                                          + "  ,OFFSHORE_RATE_DELTA = :OFFSHORE_RATE_DELTA "
                                          + "  ,RATE_NBR = :RATE_NBR "
                                          + "  ,SR_OFFSHORE_RATE = :SR_OFFSHORE_RATE "
                                          + "  ,SR_OFFSHORE_RATE_DELTA = :SR_OFFSHORE_RATE_DELTA "
                                          + "  ,SR_RATE = :SR_RATE "
                                          + "  ,SR_RATE_DELTA = :SR_RATE_DELTA "
                                          + "  ,STD_RATE = :STD_RATE "
                                          + "  ,STD_RATE_DELTA = :STD_RATE_DELTA "
                                          + "where CONTRACT_RATE_ID = :CONTRACT_RATE_ID AND RATE_NBR = :RATE_NBR"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DESCR", package.DESCR, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE",CheckNull( package.INHOUSE_SR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_SR_RATE_DELTA", CheckNull( package.INHOUSE_SR_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE", CheckNull( package.INHOUSE_TR_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INHOUSE_TR_RATE_DELTA", CheckNull( package.INHOUSE_TR_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE", CheckNull( package.OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OFFSHORE_RATE_DELTA", CheckNull( package.OFFSHORE_RATE_DELTA), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":RATE_NBR", CheckNull( package.RATE_NBR), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE", CheckNull( package.SR_OFFSHORE_RATE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SR_OFFSHORE_RATE_DELTA", CheckNull( package.SR_OFFSHORE_RATE_DELTA), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE", CheckNull(package.SR_RATE), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":SR_RATE_DELTA", CheckNull(package.SR_RATE_DELTA), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE", CheckNull(package.STD_RATE), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":STD_RATE_DELTA", CheckNull(package.STD_RATE_DELTA), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_RATE_ID", CheckNull(package.CONTRACT_RATE_ID), OracleType.Number, ParameterDirection.InputOutput));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
                //throw ex;
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
            ObservableCollection<ContractRateAlt> rpt = ConvertDataTableToObservableCollection(dt);

            ReportTypeTable rtt = new ReportTypeTable();
            rtt.AvailableFields = GetAvailableFields();
            foreach (ContractRateAlt package in rpt)
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
        protected override void RowConverter(ContractRateAlt item, DataRow row)
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

        protected override void RowConverter(ContractRateAlt r, DataRow record)
        {
            try
            {
                r.CONTRACT_RATE_ID = CastDbValueRow(record, "CONTRACT_RATE_ID",true,"Int32");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.INHOUSE_SR_RATE = CastDbValueRow(record, "INHOUSE_SR_RATE", true, "decimal");
                r.INHOUSE_TR_RATE = CastDbValueRow(record, "INHOUSE_TR_RATE", true, "decimal");
                r.OFFSHORE_RATE = CastDbValueRow(record, "OFFSHORE_RATE", true, "decimal");
                r.SR_OFFSHORE_RATE = CastDbValueRow(record, "SR_OFFSHORE_RATE", true, "decimal");
                r.SR_RATE = CastDbValueRow(record, "SR_RATE", true, "decimal");
                r.CONTRACT_RATE_ID = CastDbValueRow(record, "CONTRACT_RATE_ID", true, "Int32");
                r.STD_RATE = CastDbValueRow(record, "STD_RATE", true, "decimal");

                r.INHOUSE_SR_RATE_DELTA = CastDbValueRow(record,"INHOUSE_SR_RATE_DELTA",true,"decimal");
                r.INHOUSE_TR_RATE_DELTA = CastDbValueRow(record,"INHOUSE_TR_RATE_DELTA",true,"decimal");
                r.OFFSHORE_RATE_DELTA = CastDbValueRow(record,"OFFSHORE_RATE_DELTA",true,"decimal");
                r.RATE_NBR = CastDbValueRow(record,"RATE_NBR",true,"decimal");
                r.SR_OFFSHORE_RATE_DELTA = CastDbValueRow(record,"SR_OFFSHORE_RATE_DELTA",true,"decimal");
                r.SR_RATE_DELTA = CastDbValueRow(record,"SR_RATE_DELTA",true,"decimal");
                r.STD_RATE_DELTA = CastDbValueRow(record, "STD_RATE_DELTA", true, "decimal");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#if ASDFASDF
        protected override void RowConverter(ContractRateAlt r, IDataRecord record)
        {
            //ContractRateAlt r = new ContractRateAlt();
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
        public override void Add(ContractRateAlt item)
        {
            throw new NotImplementedException();
        }

        public override void Update(ContractRateAlt item)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ContractRateAlt item)
        {
            throw new NotImplementedException();
        }

        public override ContractRateAlt Get(ContractRateAlt entity)
        {
            throw new NotImplementedException();
        }
#endif

        public override ObservableCollection<ContractRateAlt> GetAltRatesFromClients(string extSys, string extClientKey)
        {
            string sql = String.Format(
                    "select  cra.* \r\n"
                + "from {0}.invoice_grp ig \r\n"
                + "join {0}.ext_client ec \r\n"
                + "on EC.DEFAULT_INVOICE_GRP_ID = ig.invoice_grp_id \r\n"
                + "join {0}.contract_rate cr \r\n"
                + "on cr.contract_id = ig.contract_id \r\n"
                + "join {0}.contract_rate_alt cra \r\n"
                + "on cra.contract_rate_id = cr.contract_rate_id where \r\n"
                + "ec.ext_sys = '{1}' \r\n"
                + "and ec.ext_client_key = '{2}'", SchemaName, extSys, extClientKey);
            OracleParameter[] p = null;
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return  ConvertDataTableToObservableCollection(dt);
        }


        public override ObservableCollection<ContractRateAlt> GetAltRates(int contractRateId)
        {
            ObservableCollection<ContractRateAlt> ContractRateAlts;
            try
            {
                string sql = String.Format("Select * from {0}.CONTRACT_RATE_ALT where CONTRACT_RATE_ID={1}", SchemaName, contractRateId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                ContractRateAlts = ConvertDataTableToObservableCollection(dt);


            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ContractRateAlts;
        }

        public override ContractRateAlt Get(ContractRateAlt entity)
        {
            throw new NotImplementedException();
        }

        //public override void Remove(ContractRateAlt entity)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
