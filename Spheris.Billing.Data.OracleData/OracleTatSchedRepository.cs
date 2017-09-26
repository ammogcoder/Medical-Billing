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
    public class OracleTatSchedRepository : TatSchedRepositoryBase
    {
        public override void Add(TatSched r)
        {
            string sql = String.Format("insert into {0}.TAT_SCHED"
            + "("
            + "  TAT_SCHED_ID"
            + ", DESCR"
            + ", TAT_COMP_METHOD"
            + " ) "
            + "values "
            + " ( "
            + ", {0}.TAT_SCHED_ID.nextval "
            + ", :DESCR"
            + ", :TAT_COMP_METHOD"
            + " ) "
            + " returning TAT_SCHED_ID into :NEWID "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":NEWID",  OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_COMP_METHOD", CheckNull(r.TAT_COMP_METHOD), OracleType.VarChar, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.TAT_SCHED_ID = int.Parse(parameters[0].Value.ToString());
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

        public override void Remove(TatSched package)
        {

            try
            {
                string sql = String.Format("DELETE FROM {0}.TAT_SCHED WHERE TAT_SCHED_ID = :TAT_SCHED_ID", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", package.TAT_SCHED_ID , OracleType.Int32, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
               throw ex;
            }

        }

        public override void Update(TatSched package)
        {
            try
            {
                
                string sql = String.Format("update {0}.TAT_SCHED set "
                                          + "  DESCR = :DESCR "
                                          + "  ,TAT_COMP_METHOD = :TAT_COMP_METHOD "
                                          + "where TAT_SCHED_ID = :TAT_SCHED_ID"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DESCR", package.DESCR, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_COMP_METHOD",package.TAT_COMP_METHOD, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_COMP_METHOD",package.TAT_SCHED_ID, OracleType.Number, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }

        }



 
        protected override void RowConverter(TatSched r, DataRow record)
        {
            try
            {
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.TAT_COMP_METHOD = CastDbValueRow(record, "TAT_COMP_METHOD");
                r.TAT_SCHED_ID = CastDbValueRow(record, "TAT_SCHED_ID",true,"decimal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
  

        public override ObservableCollection<TatSched> GetTATSched()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<TatSched> tatScheds;
            try
            {
                string sql = String.Format("Select * from {0}.TAT_SCHED", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                tatScheds = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tatScheds;
        }

        public override TatSched Get(TatSched entity)
        {
            throw new NotImplementedException();
        }
 
    }
}

