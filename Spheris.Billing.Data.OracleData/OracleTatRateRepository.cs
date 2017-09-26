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
    public class OracleTatRateRepository : TatRateRepositoryBase
    {
        public override void Add(TatRate r)
        {
            //r.TIER
            //r.TAT_SCHED_ID
            //r.PENALTY
            //r.ALT_PENALTY
            string sql = String.Format("insert into {0}.TAT_RATE"
            + "("
            + "  TIER"
            + ", TAT_SCHED_ID"
            + ", PENALTY"
            + ", ALT_PENALTY"
            + " ) "
            + "values "
            + " ( "
            + ", :TIER"
            + ", :TAT_SCHED_ID"
            + ", :PENALTY"
            + ", :ALT_PENALTY"
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":TIER", CheckNull(r.TIER), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", CheckNull(r.TAT_SCHED_ID), OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":PENALTY", CheckNull(r.PENALTY), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ALT_PENALTY", CheckNull(r.ALT_PENALTY), OracleType.VarChar, ParameterDirection.Input));

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

        public override void Remove(TatRate package)
        {
            try
            {
                string sql = String.Format("DELETE FROM {0}.TAT_RATE WHERE TAT_SCHED_ID = :TAT_SCHED_ID AND TIER=:TIER", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", package.TAT_SCHED_ID, OracleType.Int32, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TIER", package.TIER , OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
               throw ex;
            }

        }

        public override void Update(TatRate package)
        {
            try
            {
                string sql = String.Format("update {0}.TAT_RATE set "
                                          + "  PENALTY = :PENALTY "
                                          + "  ,ALT_PENALTY = :ALT_PENALTY "
                                          //+ "  ,TIER = :TIER"  - Presume this would be taboo!
                                          + "where TAT_SCHED_ID = :TAT_SCHED_ID AND TIER=:TIER"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":PENALTY", package.PENALTY, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ALT_PENALTY", package.ALT_PENALTY, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", package.TAT_SCHED_ID, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TIER", package.TIER, OracleType.Number, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }

        }



 
        protected override void RowConverter(TatRate r, DataRow record)
        {
            try
            {
                r.ALT_PENALTY = CastDbValueRow(record, "ALT_PENALTY", true, "decimal");
                r.PENALTY = CastDbValueRow(record, "PENALTY", true, "decimal");
                r.TAT_SCHED_ID = CastDbValueRow(record, "TAT_SCHED_ID", true, "decimal");
                r.TIER = CastDbValueRow(record, "TIER", true, "decimal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ObservableCollection<ContractsUsingTatSched> GetContractsUsing(decimal tatSchedId)
        {
            string sql = String.Format("select DISTINCT TC.CONTRACT_ID,C.DESCR from {0}.TODAYS_CONTRACT TC JOIN {0}.CONTRACT C ON C.CONTRACT_ID=TC.CONTRACT_ID WHERE TAT_SCHED_ID={1}", SchemaName,tatSchedId);

            OracleParameter[] p = null;
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

            ObservableCollection<ContractsUsingTatSched> contractList = new ObservableCollection<ContractsUsingTatSched>();
            foreach (DataRow row in dt.Rows)
            {
                ContractsUsingTatSched item = new ContractsUsingTatSched();

                item.CONTRACT_ID = CastDbValueRow(row , "CONTRACT_ID", true, "decimal");
                item.DESCR = CastDbValueRow(row, "DESCR");
                contractList.Add(item);
            }

            return contractList;
        }

        public override ObservableCollection<TatRate> GetTatRates(decimal tatSchedId)
        {
            ObservableCollection<TatRate> TatRates;
            try
            {
                string sql = String.Format("Select * from {0}.TAT_RATE where TAT_SCHED_ID={1}", SchemaName,tatSchedId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                TatRates = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TatRates;
        }

        public override TatRate Get(TatRate entity)
        {
            throw new NotImplementedException();
        }


 
 
    }
}

