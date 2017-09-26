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
    public class OracleAdjTypeRepository : AdjTypeRepositoryBase
    {
        public override void Add(AdjType r)
        {
            string sql = String.Format("insert into {0}.ADJ_TYPE "
                                      + "("
            + " ADJ_TYPE"
            + ", APPLY_AFTER_FINAL_COST "
            + ", DESCR "
            + " ) "
            + "values "
            + " ( "
            + "  :ADJ_TYPE"
            + ", :APPLY_AFTER_FINAL_COST"
            + ", :DESCR"
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":ADJ_TYPE", CheckNull(r.ADJ_TYPE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":APPLY_AFTER_FINAL_COST", (r.APPLY_AFTER_FINAL_COST == true)?'Y':'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.Modified = false;
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

        public override void Remove(AdjType package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.ADJ_TYPE WHERE "
                + " ADJ_TYPE = :ADJ_TYPE"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADJ_TYPE", package.ADJ_TYPE, OracleType.VarChar, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(AdjType r)
        {

            try
            {
                string sql = String.Format("update {0}.ADJ_TYPE set "
                + "  ADJ_TYPE = :ADJ_TYPE "
                + " ,APPLY_AFTER_FINAL_COST = :APPLY_AFTER_FINAL_COST "
                + " ,DESCR = :DESCR "
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADJ_TYPE", CheckNull(r.ADJ_TYPE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":APPLY_AFTER_FINAL_COST", (r.APPLY_AFTER_FINAL_COST == true) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.Modified = false;
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


 

        protected override void RowConverter(AdjType r, DataRow record)
        {
            try
            {

                r.ADJ_TYPE = CastDbValueRow(record, "ADJ_TYPE");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.APPLY_AFTER_FINAL_COST = CastDbValueRow(record, "APPLY_AFTER_FINAL_COST", true, "Bool");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<AdjType> GetAdjTypes()
        {
#if !ORACLE
            return  null;
#endif
            ObservableCollection<AdjType> AdjTypes;
            try
            {
                string sql = String.Format("Select * from {0}.ADJ_TYPE ORDER BY DESCR", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                AdjTypes = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AdjTypes;
        }


        public override AdjType Get(AdjType entity)
        {
            throw new NotImplementedException();
        }

    }
}
