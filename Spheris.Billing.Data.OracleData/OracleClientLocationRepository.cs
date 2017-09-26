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
    public class OracleClientLocationRepository : ClientLocationRepositoryBase
    {
        private string Where(string where, string clause)
        {
            if (clause == null)
                return where;
            if (string.IsNullOrEmpty(where))
                return " WHERE " + clause;
            return where + " AND " + clause;
        }

        public override ObservableCollection<ClientLocation> FetchClients(string clientFilter,bool notAssignedToGroup = false)
        {
            ObservableCollection<ClientLocation> clients = null;
            try
            {
                string where = string.Empty;

                string descWhere = string.Empty;
                if (!string.IsNullOrEmpty(clientFilter))
                {
                    descWhere = String.Format(" UPPER (DESCR) LIKE '%{0}%'", clientFilter.ToUpper());
                    descWhere = descWhere.Insert(0, "(");
                    descWhere += ")";
                    where = Where(where, descWhere);
                }

                if (notAssignedToGroup)
                    where = Where(where, " default_invoice_grp_id IS NULL" );
                if (where == null)
                    return null;

                string sql = string.Format("select * from {0}.ext_client\r\n", SchemaName);
                if (!string.IsNullOrEmpty(where))
                    sql += where;
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                clients = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return clients;
            //return null;
        }

        public override IDataReader GetReader(IDbConnection cnn, string platform)
        {
            string sql = String.Format("select  ext_sys              ,ext_client_key                 ,default_invoice_grp_id \r\n"
                                     + "        ,descr               ,cost_code                      ,ext_work_type_source_descr \r\n"
                                     + "        ,override_key_source ,default_tat_threshold \r\n"
                                     + "from    {0}.ext_client \r\n"
                                     + "where   ext_sys = :ext_sys \r\n"
                                     + "order by ext_sys, ext_client_key"
                                     , base.SchemaName);

            if (cnn == null) cnn = new OracleConnection(ConnectionString.Value);
            OpenConnection(cnn);
            using (IDbCommand cmd = new OracleCommand(sql, (OracleConnection)cnn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60 * 60;
                OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", platform, OracleType.VarChar, ParameterDirection.Input);
                return cmd.ExecuteReader();
            }
        }

        public override ObservableCollection<ClientLocation> FetchLocations(int invoiceId,bool exclusive)
        {
            ObservableCollection<ClientLocation> locations = null;
            try
            {
                string sql;

                if(exclusive)
                    sql = string.Format("select  * from {0}.EXT_CLIENT\r\n"
                                         + " WHERE default_invoice_grp_id={1} ORDER BY DESCR",base.SchemaName,invoiceId);
                else
                    sql = string.Format("select  * from {0}.EXT_CLIENT\r\n"
                                         + " WHERE default_invoice_grp_id IS NULL  ORDER BY DESCR", base.SchemaName, invoiceId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                locations = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return locations;
            //return null;
        }


        public override List<ClientLocation> Get(string platform)
        {
            using (IDbConnection cnn = new OracleConnection(ConnectionString.Value))
            {
                IDataReader dr = GetReader(cnn, platform);
                return ConvertToList(dr);
            }
        }

        public object NullableInput(object obj)
        {
            return (obj == null) ? DBNull.Value : obj;
        }

        public override ClientLocation UnassignLocation(ClientLocation loc)
        {
            try
            {
                string sql = String.Format("update {0}.EXT_CLIENT set "
                                          + "  DEFAULT_INVOICE_GRP_ID = :id "
                                          + " where ext_sys = :ext_sys and ext_client_key = :extclientkey"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":id", DBNull.Value , OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ext_sys", loc.ExtSys, OracleType.VarChar, 20, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":extclientkey", loc.ExtClientKey, OracleType.VarChar, 20, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                return loc;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// COST_CODE  VARCHAR
        /// DEFAULT_INVOICE_GRP_ID  NUMBER
        /// DEFAULT_TAT_THRESHOLD   NUMBER
        /// DESCR                   VARCHAR
        /// EXT_WORK_TYPE_SOURCE_DESCR  VARCHAR
        /// OVERRIDE_KEY_SOURCE  VARCHAR
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public override ClientLocation Update(ClientLocation loc)
        {
            try
            {
                string sql = String.Format("update {0}.EXT_CLIENT set "
                                          + "  DEFAULT_INVOICE_GRP_ID = :id "
                                          + "  ,DEFAULT_TAT_THRESHOLD = :default_tat_threshold "
                                          + "  ,COST_CODE = :cost_code "
                                          + "  ,DESCR = :descr "
                                          + "  ,EXT_WORK_TYPE_SOURCE_DESCR = :ext_work_type_source_descr "
                                          + "  ,OVERRIDE_KEY_SOURCE = :override_key_source "
                                          + " where ext_sys = :ext_sys and ext_client_key = :extclientkey"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":id", loc.DefaultInvoiceGrpID, OracleType.Number , ParameterDirection.Input));

                object v;
                if (loc.DefaultTatThreshold == null)
                    v = DBNull.Value;
                else
                    v = loc.DefaultTatThreshold;

                parameters.Add(OracleHelper.CreateParameter(":default_tat_threshold", NullableInput(loc.DefaultTatThreshold), OracleType.Number, ParameterDirection.Input));

                parameters.Add(OracleHelper.CreateParameter(":cost_code", NullableInput(loc.CostCode), OracleType.VarChar, 10, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":descr", NullableInput(loc.Description), OracleType.VarChar, 100,ParameterDirection.Input));

                parameters.Add(OracleHelper.CreateParameter(":ext_work_type_source_descr", NullableInput(loc.ExtWorkTypeSourceDescr), OracleType.VarChar, 100, ParameterDirection.Input));
               
                parameters.Add(OracleHelper.CreateParameter(":override_key_source", NullableInput( loc.OverrideKeySource), OracleType.VarChar,30, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ext_sys", loc.ExtSys, OracleType.VarChar, 20, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":extclientkey", loc.ExtClientKey, OracleType.VarChar, 20,ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                return loc;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Add(ClientLocation client)
        {
            string sql = String.Format("INSERT INTO {0}.EXT_CLIENT \r\n"
                                     + "  (EXT_SYS                  ,EXT_CLIENT_KEY                 ,DEFAULT_INVOICE_GRP_ID \r\n"
                                     + "   ,DESCR                   ,COST_CODE                      ,EXT_WORK_TYPE_SOURCE_DESCR  \r\n"
                                     + "   ,OVERRIDE_KEY_SOURCE     ,DEFAULT_TAT_THRESHOLD) \r\n"
                                     + "VALUES \r\n"
                                     + "(  :EXT_SYS                 ,:EXT_CLIENT_KEY                ,:DEFAULT_INVOICE_GRP_ID \r\n"
                                     + "   ,:DESCR                  ,:COST_CODE                     ,:EXT_WORK_TYPE_SOURCE_DESCR \r\n"
                                     + "   ,:OVERRIDE_KEY_SOURCE    ,:DEFAULT_TAT_THRESHOLD)"
                                     , base.SchemaName);

            using (IDbConnection cnn = new OracleConnection(ConnectionString.Value))
            using (IDbCommand cmd = new OracleCommand(sql, (OracleConnection)cnn))
            {
                cmd.CommandType = CommandType.Text;
                OracleHelper.AddCommandParameter(cmd, ":EXT_SYS", CheckNull(client.ExtSys), OracleType.Char, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":EXT_CLIENT_KEY", CheckNull(client.ExtClientKey), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DEFAULT_INVOICE_GRP_ID", CheckNull(client.DefaultInvoiceGrpID), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DESCR", CheckNull(client.Description), OracleType.VarChar, 100, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":COST_CODE", CheckNull(client.CostCode), OracleType.VarChar, 10, ParameterDirection.Input);
                if (String.IsNullOrEmpty(client.ExtWorkTypeSourceDescr)) 
                    client.ExtWorkTypeSourceDescr = "<DEFAULT>";
                OracleHelper.AddCommandParameter(cmd, ":EXT_WORK_TYPE_SOURCE_DESCR", client.ExtWorkTypeSourceDescr, OracleType.VarChar, 30, ParameterDirection.Input);
                if (String.IsNullOrEmpty(client.OverrideKeySource)) 
                    client.OverrideKeySource = "<DEFAULT>";
                OracleHelper.AddCommandParameter(cmd, ":OVERRIDE_KEY_SOURCE", client.OverrideKeySource, OracleType.VarChar, 30, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":DEFAULT_TAT_THRESHOLD", CheckNull(client.DefaultTatThreshold), OracleType.Number, ParameterDirection.Input);
                cnn.Open();
                return (cmd.ExecuteNonQuery() > 0);
            }
        }

        protected List<ClientLocation> ConvertToList(IDataReader dr)
        {
            List<ClientLocation> items = new List<ClientLocation>();
            string val;
            while (dr.Read())
            {
                ClientLocation ec = new ClientLocation();
                ec.ExtSys = dr["ext_sys"].ToString();
                ec.ExtClientKey = dr["ext_client_key"].ToString();
                val = dr["default_invoice_grp_id"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    ec.DefaultInvoiceGrpID = null;
                }
                else
                {
                    ec.DefaultInvoiceGrpID = long.Parse(val);
                }
                ec.Description = dr["descr"].ToString();
                ec.CostCode = dr["cost_code"].ToString();
                ec.ExtWorkTypeSourceDescr = dr["ext_work_type_source_descr"].ToString();
                ec.OverrideKeySource = dr["override_key_source"].ToString();
                val = dr["default_tat_threshold"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    ec.DefaultTatThreshold = 12;
                }
                else
                {
                    ec.DefaultTatThreshold = int.Parse(val);
                }
                items.Add(ec);
            }
            return items;
        }

        private int rowcount = 0;

        //private int lastEntry = 0;
        //static private int debugENTRIES = 0;
        protected override void RowConverter(ClientLocation item, DataRow row)
        {
            rowcount++;
            int lastEntry = 0;
            try
            {
                //if (lastEntry > 2636)
                //    debugENTRIES = 331;//break;
                item.ExtSys = row["ext_sys"].ToString();
                lastEntry++;
                item.ExtClientKey = row["ext_client_key"].ToString();
                lastEntry++;
                item.DefaultInvoiceGrpID = CastDbValueRow(row, "default_invoice_grp_id",true,"decimal");
                lastEntry++;
                item.Description = CastDbValueRow(row, "descr");
                lastEntry++;
                item.CostCode = CastDbValueRow(row, "cost_code");
                lastEntry++;
                item.ExtWorkTypeSourceDescr = CastDbValueRow(row, "ext_work_type_source_descr");
                lastEntry++;
                item.OverrideKeySource = CastDbValueRow(row, "override_key_source");
                lastEntry++;
                item.DefaultTatThreshold = CastDbValueRow(row, "default_tat_threshold",true,"decimal");
                lastEntry++;
                item.Modified = false;
                
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }

        public override void Remove(ClientLocation client)
        {
            string sql = String.Format("DELETE FROM {0}.EXT_CLIENT WHERE EXT_SYS = :EXT_SYS and EXT_CLIENT_KEY = :EXT_CLIENT_KEY", SchemaName);
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(OracleHelper.CreateParameter(":EXT_SYS", client.ExtSys, OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":EXT_CLIENT_KEY", client.ExtClientKey, OracleType.VarChar, ParameterDirection.Input));
            OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
        }


        public override ClientLocation RowConverter(IDataRecord row)
        {
            ClientLocation ec = new ClientLocation();
            rowcount++;
            int lastgood= 0;
            try
            {
                ec.ExtSys = row["ext_sys"].ToString();
                lastgood++;
                ec.ExtClientKey = row["ext_client_key"].ToString();
                lastgood++;
                ec.DefaultInvoiceGrpID = CastDbValue(row, "default_invoice_grp_id");
                lastgood++;
                ec.Description = CastDbValue(row, "descr");
                lastgood++;
                ec.CostCode = CastDbValue(row, "cost_code");
                lastgood++;
                ec.ExtWorkTypeSourceDescr = CastDbValue(row, "ext_work_type_source_descr");
                lastgood++;
                ec.OverrideKeySource = CastDbValue(row, "override_key_source");
                lastgood++;
                ec.DefaultTatThreshold = CastDbValue(row, "default_tat_threshold");
                lastgood++;
                ec.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ec;
        }
    }
}
