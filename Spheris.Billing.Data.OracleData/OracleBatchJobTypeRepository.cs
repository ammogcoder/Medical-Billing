
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
    public class OracleBatchJobTypeRepository : BatchJobTypeRepositoryBase
    {
        public override void Add(BatchJobType r)
        {
            string sql = String.Format("insert into {0}.BATCH_JOB_TYPE "
                                      + "("
            + "  BATCH_JOB_TYPE"
            + ", DESCR"
            + ", PLATFORM"
            + " ) "
            + "values "
            + " ( "
            + "  :BATCH_JOB_TYPE"
            + ", :DESCR"
            + ", :PLATFORM"
            + " ) "
            + " returning BATCH_JOB_TYPE into :BATCH_JOB_TYPE "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":BATCH_JOB_TYPE", CheckNull(r.BATCH_JOB_TYPE), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":PLATFORM", CheckNull(r.PLATFORM), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":BATCH_JOB_TYPE", OracleType.Int32, ParameterDirection.InputOutput));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                //r.BATCH_JOB_TYPE = parameters[parameters.Count - 1].Value.ToString();
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

        public override void Remove(BatchJobType package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.BATCH_JOB_TYPE WHERE BATCH_JOB_TYPE = :BATCH_JOB_TYPE", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":BATCH_JOB_TYPE", package.BATCH_JOB_TYPE, OracleType.VarChar, ParameterDirection.Input));
                OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
            }

        }

        public override void Update(BatchJobType r)
        {

            if (string.IsNullOrEmpty( r.BATCH_JOB_TYPE ) )
            {
                throw new ArgumentNullException("Item.BATCH_JOB_TYPE", "An item BATCH_JOB_TYPE was not provided for the update.");
            }
            try
            {
                string sql = String.Format("update {0}.BATCH_JOB_TYPE set "
                                          + " BATCH_JOB_TYPE = :BATCH_JOB_TYPE "
                                          + " ,DESCR = :DESCR "
                                          + " ,PLATFORM = :PLATFORM "
                                          + " where BATCH_JOB_TYPE = :BATCH_JOB_TYPE", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":BATCH_JOB_TYPE", CheckNull(r.BATCH_JOB_TYPE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":PLATFORM", CheckNull(r.PLATFORM), OracleType.VarChar, ParameterDirection.Input));
                OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
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

        protected override void RowConverter(BatchJobType r, DataRow record)
        {
            try
            {
                r.BATCH_JOB_TYPE = CastDbValueRow(record, "BATCH_JOB_TYPE");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.PLATFORM = CastDbValueRow(record, "PLATFORM");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override BatchJobType Get(BatchJobType entity)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<BatchJobType> GetBatchJobTypes()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<BatchJobType> BatchJobTypes;
            try
            {
                string sql = String.Format("Select * from {0}.BATCH_JOB_TYPE order by DESCR", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                BatchJobTypes = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BatchJobTypes;
        }
    }
}
