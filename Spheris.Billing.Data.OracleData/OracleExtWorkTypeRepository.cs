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
    public class OracleExtWorkTypeRepository :  ExtWorkTypeRepositoryBase
    {
        public override ObservableCollection<ExtWorkType> GetWorkTypes( string extsys,string extClientKey = null)
        {
            string sql = String.Format("select  ext_sys                 ,ext_client_key                 ,ext_work_type \r\n"
                                     + "        ,descr                  ,tat_threshold \r\n"
                                     + "        ,std_work_type          ,invoice_grp_id                 ,stat_tat_threshold \r\n"
                                     + "        ,rate_nbr               ,tally_for_volume_evt           ,platform_wt_id \r\n"
                                     + "from    {0}.ext_work_type \r\n"
                                     + "where   ext_sys = '{1}' \r\n"
                                     + ( (extClientKey == null) ? string.Empty : "and ext_client_key='{2}' ")
                                     + "order by ext_sys                ,ext_client_key                 ,ext_work_type"
                                     , base.SchemaName, extsys, extClientKey);
            List<OracleParameter> parameters = new List<OracleParameter>();
            OracleParameter[] p = null;
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            return ConvertDataTableToObservableCollection(dt);

        }


        public override void Add(ExtWorkType client)
        {
            string sql = String.Format("insert into {0}.ext_work_type \r\n "
                                     + "(   ext_sys                         ,ext_client_key                             ,ext_work_type \r\n"
                                     + "    ,descr                          ,tat_threshold                              ,std_work_type \r\n"
                                     + "    ,invoice_grp_id                 ,stat_tat_threshold                         ,rate_nbr \r\n"
                                     + "    ,tally_for_volume_evt           ,platform_wt_id) \r\n"
                                     + "values \r\n"
                                     + "(   :ext_sys                        ,:ext_client_key                            ,:ext_work_type \r\n"
                                     + "    ,:descr                         ,:tat_threshold                             ,:std_work_type \r\n"
                                     + "    ,:invoice_grp_id                ,:stat_tat_threshold                        ,:rate_nbr \r\n"
                                     + "    ,:tally_for_volume_evt          ,:platform_wt_id)"
                                     , base.SchemaName);

            using (IDbConnection cnn = new OracleConnection(ConnectionString.Value))
            using (IDbCommand cmd = new OracleCommand(sql, (OracleConnection)cnn))
            {
                cmd.CommandType = CommandType.Text;
                OracleHelper.AddCommandParameter(cmd, ":ext_sys", client.EXT_SYS, OracleType.Char, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_client_key", client.EXT_CLIENT_KEY, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_work_type", client.EXT_WORK_TYPE, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":descr", CheckNull(client.DESCR), OracleType.VarChar, 50, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":tat_threshold", CheckNull(client.TAT_THRESHOLD), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":std_work_type", CheckNull(client.STD_WORK_TYPE), OracleType.VarChar, 4, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":invoice_grp_id", CheckNull(client.INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":stat_tat_threshold", CheckNull(client.STAT_TAT_THRESHOLD), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":rate_nbr", CheckNull(client.RATE_NBR), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":tally_for_volume_evt", (client.TALLY_FOR_VOLUME_EVT == true) ? "Y" : "N", OracleType.VarChar, 1, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":platform_wt_id", client.PLATFORM_WT_ID, OracleType.VarChar, 20, ParameterDirection.Input);
                cnn.Open();
                try
                {
                    cmd.ExecuteNonQuery() ;
                }
                catch (OracleException ex)
                {
                    if (ex.ErrorCode == -2146232008 && ex.Code == 1)        // TODO:  Find out why this is needed.
                    {
                    }
                    throw ex;

                }
            }
        }

        public override void Update(ExtWorkType item)
        {
            using (IDbConnection cnn = new OracleConnection(ConnectionString.Value))
            using (IDbCommand cmd = new OracleCommand())
            {
                cmd.CommandText = String.Format(
                                  "update  {0}.ext_work_type set \r\n"
                                + "  INVOICE_GRP_ID = :INVOICE_GRP_ID \r\n"
                                + "  ,RATE_NBR = :RATE_NBR \r\n"
                                + "  ,descr = :descr \r\n"
                                + "  ,tat_threshold = :tat_threshold \r\n"
                                + "  ,stat_tat_threshold = :stat_tat_threshold \r\n"
                                + "  ,platform_wt_id = :platform_wt_id \r\n"
                                + "where ext_sys = :ext_sys \r\n"
                                + "  and ext_client_key = :ext_client_key \r\n"
                                + "  and ext_work_type = :ext_work_type"
                                , SchemaName);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                OracleHelper.AddCommandParameter(cmd, ":INVOICE_GRP_ID", CheckNull(item.INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":RATE_NBR", CheckNull(item.RATE_NBR ), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":descr", item.DESCR, OracleType.VarChar, 50, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":tat_threshold", CheckNull(item.TAT_THRESHOLD), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":stat_tat_threshold", CheckNull(item.STAT_TAT_THRESHOLD), OracleType.Number, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":platform_wt_id", CheckNull(item.PLATFORM_WT_ID), OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_sys", item.EXT_SYS, OracleType.Char, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_client_key", item.EXT_CLIENT_KEY, OracleType.VarChar, 20, ParameterDirection.Input);
                OracleHelper.AddCommandParameter(cmd, ":ext_work_type", item.EXT_WORK_TYPE, OracleType.VarChar, 20, ParameterDirection.Input);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected List<ExtWorkType> ConvertToList(IDataReader dr)
        {
            List<ExtWorkType> items = new List<ExtWorkType>();
            string val;
            while (dr.Read())
            {
                ExtWorkType wt = new ExtWorkType();
                wt.EXT_SYS = dr["ext_sys"].ToString();
                wt.EXT_CLIENT_KEY = dr["ext_client_key"].ToString();
                wt.EXT_WORK_TYPE = dr["ext_work_type"].ToString();
                wt.DESCR = dr["descr"].ToString();
                val = dr["tat_threshold"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    wt.TAT_THRESHOLD = null;
                }
                else
                {
                    wt.TAT_THRESHOLD = decimal.Parse(val);
                }
                val = dr["std_work_type"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    wt.STD_WORK_TYPE = null;
                }
                else
                {
                    wt.STD_WORK_TYPE = val;
                }
                val = dr["invoice_grp_id"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    wt.INVOICE_GRP_ID = null;
                }
                else
                {
                    wt.INVOICE_GRP_ID = decimal.Parse(val);// long.Parse(val);
                }
                val = dr["stat_tat_threshold"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    wt.STAT_TAT_THRESHOLD= null;
                }
                else
                {
                    wt.STAT_TAT_THRESHOLD = decimal.Parse(val);
                }
                val = dr["rate_nbr"].ToString();
                if (String.IsNullOrEmpty(val))
                {
                    wt.RATE_NBR = null;
                }
                else
                {
                    wt.RATE_NBR = int.Parse(val);
                }

                wt.TALLY_FOR_VOLUME_EVT = (dr["tally_for_volume_evt"].ToString() == "Y") ? true : false;

                //val = dr["tally_for_volume_evt"].ToString();
                //if (String.IsNullOrEmpty(val))
                //{
                //    wt.TALLY_FOR_VOLUME_EVT = null;
                //}
                //else
                //{
                //    wt.TALLY_FOR_VOLUME_EVT = val;
                //}
                items.Add(wt);
            }
            return items;
        }

        protected override void RowConverter(ExtWorkType wt , DataRow row)
        {
            wt.EXT_SYS = row["ext_sys"].ToString();
            wt.EXT_CLIENT_KEY = row["ext_client_key"].ToString();
            wt.EXT_WORK_TYPE = row["ext_work_type"].ToString();
            wt.DESCR = row["descr"].ToString();
            wt.TAT_THRESHOLD = CastDbValueRow(row, "tat_threshold", true, "decimal");
            wt.STD_WORK_TYPE = CastDbValueRow(row, "std_work_type");
            wt.INVOICE_GRP_ID = CastDbValueRow(row, "invoice_grp_id",true,"decimal");
            wt.STAT_TAT_THRESHOLD = CastDbValueRow(row, "stat_tat_threshold", true, "decimal");
            wt.RATE_NBR = CastDbValueRow(row, "rate_nbr",true,"decimal");
            wt.TALLY_FOR_VOLUME_EVT = CastDbValueRow(row, "tally_for_volume_evt",true,"Bool");
            wt.PLATFORM_WT_ID = CastDbValueRow(row, "platform_wt_id", true, "String");
        }

        public  ExtWorkType RowConverter(IDataRecord row)
        {
            ExtWorkType wt = new ExtWorkType();
            wt.EXT_SYS = row["ext_sys"].ToString();
            wt.EXT_CLIENT_KEY = row["ext_client_key"].ToString();
            wt.EXT_WORK_TYPE = row["ext_work_type"].ToString();
            wt.DESCR = row["descr"].ToString();
            wt.TAT_THRESHOLD = CastDbValue(row, "tat_threshold", true, "decimal");
            wt.STD_WORK_TYPE = CastDbValue(row, "std_work_type");
            wt.INVOICE_GRP_ID = CastDbValue(row, "invoice_grp_id",true,"decimal");
            wt.STAT_TAT_THRESHOLD = CastDbValue(row, "stat_tat_threshold", true, "decimal");
            wt.RATE_NBR = CastDbValue(row, "rate_nbr");
            wt.TALLY_FOR_VOLUME_EVT = CastDbValue(row, "tally_for_volume_evt",true, "Bool");
            wt.PLATFORM_WT_ID = CastDbValue(row, "platform_wt_id", true, "String");
            return wt;
        }

        public override ExtWorkType Get(ExtWorkType entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ExtWorkType entity)
        {
            string sql = String.Format("DELETE FROM {0}.ext_work_type WHERE EXT_SYS = :EXT_SYS and EXT_CLIENT_KEY = :EXT_CLIENT_KEY and EXT_WORK_TYPE = :EXT_WORK_TYPE", SchemaName);
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(OracleHelper.CreateParameter(":EXT_SYS", entity.EXT_SYS, OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":EXT_CLIENT_KEY", entity.EXT_CLIENT_KEY, OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":EXT_WORK_TYPE", entity.EXT_WORK_TYPE, OracleType.VarChar, ParameterDirection.Input));
            OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());

        }
    }
}
