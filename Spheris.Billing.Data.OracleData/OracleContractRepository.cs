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
    public class OracleContractRepository : ContractRepositoryBase 
    {
        public override Contract GetByContractID(int contractID)
        {
            Contract contract = null;
            try
            {

                string sql = string.Format("select * from {0}.CONTRACT where CONTRACT_ID=:CID", SchemaName);

                OracleParameter[] p = { OracleHelper.CreateParameter(":cid", contractID, OracleType.Number, ParameterDirection.Input) };
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                if (dt.Rows.Count > 0)
                {
                    contract = new Contract();
                    RowConverter(contract, dt.Rows[0]);
                    return contract;
                }
                return null;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return contract;
        }


        private string Where(string where, string clause)
        {
            if (clause == null)
                return where;
            if (string.IsNullOrEmpty(where))
                return " WHERE " + clause;
            return where + " AND " + clause;
        }

        public override ObservableCollection<Contract> FetchContracts(string contractFilter, string descFilter)
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<Contract> contracts = null;
            try
            {
                string where = string.Empty;

                string descWhere = String.Format(" UPPER (DESCR) LIKE '%{0}%'", descFilter.ToUpper());
                descWhere = descWhere.Insert(0, "(");
                descWhere += ")";
                where = Where(where, descWhere);

                string sql = string.Format("select * from {0}.CONTRACT\r\n", SchemaName);
                if (!string.IsNullOrEmpty(where))
                    sql += where;
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                contracts = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return contracts;
            //return null;
        }

            
            
        public override Contract Get(Contract entity)
        { return null; }


        public override void Add(Contract entity)
        {
            string sql = String.Format("insert into {0}.contract "
            + "  ( "
                /* 0 */+ " CONTRACT_ID "
                /* 1 */+ " ,CHANGED_BY "
                /* 2 */+ " ,CHANGED_ON "
                /* 3 */+ " ,DESCR "
                /* 4 */+ " ,DISPLAY_STD_RATE_ON_INVOICE "
                /* 5 */+ " ,PAYMENT_GRACE_PERIOD "
                /* 6 */+ " ,PAYMENT_TERMS "
            + " ) "
            + " VALUES             "
            + " ( "
                /* 0 */+ " {0}.CONTRACT_ID.NEXTVAL "
                /* 1 */+ " ,:CHANGED_BY   "
                /* 2 */+ " ,:CHANGED_ON    "
                /* 3 */+ " ,:DESCR    "
                /* 4 */+ " ,:DISPLAY_STD_RATE_ON_INVOICE	 "
                /* 5 */+ " ,:PAYMENT_GRACE_PERIOD	  "
                /* 6 */+ " ,:PAYMENT_TERMS	         "
            + ")  returning CONTRACT_ID into :CONTRACT_ID", SchemaName);
            List<OracleParameter> parameters = new List<OracleParameter>();
            /* 0 */parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", OracleType.Number, ParameterDirection.InputOutput));
            /* 1 */parameters.Add(OracleHelper.CreateParameter(":CHANGED_BY", CheckNull(entity.CHANGED_BY), OracleType.VarChar, ParameterDirection.Input));
            /* 2 */parameters.Add(OracleHelper.CreateParameter(":CHANGED_ON", CheckNull(entity.CHANGED_ON), OracleType.DateTime, ParameterDirection.Input));
            /* 3 */parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(entity.DESCR), OracleType.VarChar, ParameterDirection.Input));
            /* 4 */parameters.Add(OracleHelper.CreateParameter(":DISPLAY_STD_RATE_ON_INVOICE", (entity.DISPLAY_STD_RATE_ON_INVOICE == true) ? 'Y':'N', OracleType.Char, ParameterDirection.Input));
            /* 5 */parameters.Add(OracleHelper.CreateParameter(":PAYMENT_GRACE_PERIOD", CheckNull(entity.PAYMENT_GRACE_PERIOD), OracleType.Number, ParameterDirection.Input));
            /* 6 */parameters.Add(OracleHelper.CreateParameter(":PAYMENT_TERMS", CheckNull(entity.PAYMENT_TERMS), OracleType.VarChar, ParameterDirection.Input));

            OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            entity.CONTRACT_ID = int.Parse(parameters[0].Value.ToString());
        }

        public override void Update(Contract entity)
        { 
            string sql = string.Format("UPDATE {0}.CONTRACT "
            + "SET CHANGED_BY = :CHANGED_BY "
            + " ,CHANGED_ON = :CHANGED_ON "
            + " ,DESCR = :DESCR"
            + " ,DISPLAY_STD_RATE_ON_INVOICE = :DISPLAY_STD_RATE_ON_INVOICE "
            + " ,PAYMENT_GRACE_PERIOD = :PAYMENT_GRACE_PERIOD "
            + " ,PAYMENT_TERMS = :PAYMENT_TERMS "
            + "WHERE CONTRACT_ID = :CONTRACT_ID", SchemaName);
            List<OracleParameter> parameters = new List<OracleParameter>();
            parameters.Add(OracleHelper.CreateParameter(":CHANGED_BY", CheckNull(entity.CHANGED_BY), OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":CHANGED_ON", CheckNull(entity.CHANGED_ON), OracleType.DateTime, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(entity.DESCR), OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":DISPLAY_STD_RATE_ON_INVOICE", (entity.DISPLAY_STD_RATE_ON_INVOICE == true) ? 'Y':'N', OracleType.Char, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":PAYMENT_GRACE_PERIOD", CheckNull(entity.PAYMENT_GRACE_PERIOD), OracleType.Number, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":PAYMENT_TERMS", CheckNull(entity.PAYMENT_TERMS), OracleType.VarChar, ParameterDirection.Input));
            parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", entity.CONTRACT_ID, OracleType.Number, ParameterDirection.Input));
            OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
        }


        public override void Remove(Contract entity)
        {
            string sql = String.Format("delete {0}.CONTRACT where CONTRACT_ID = :CONTRACT_ID", SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":CONTRACT_ID", entity.CONTRACT_ID, OracleType.Number, ParameterDirection.Input) };
            OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, p);
        }

        protected override void RowConverter(Contract item, DataRow row)
        {

            item.CHANGED_BY =  CastDbValueRow(row, "CHANGED_BY");
            item.CHANGED_ON = CastDbValueRow(row, "CHANGED_ON",true,"DateTime");
            item.CONTRACT_ID = CastDbValueRow(row, "CONTRACT_ID",true,"decimal");
            item.DESCR = CastDbValueRow(row, "DESCR");
            item.DISPLAY_STD_RATE_ON_INVOICE = (row["DISPLAY_STD_RATE_ON_INVOICE"].ToString() == "Y") ? true : false;
            item.PAYMENT_GRACE_PERIOD = CastDbValueRow(row, "PAYMENT_GRACE_PERIOD",true,"decimal");
            item.PAYMENT_TERMS = CastDbValueRow(row, "PAYMENT_TERMS");
        }

    }
}
