
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
    public class OracleScopeRuleRepository : ScopeRuleRepositoryBase
    {
        public override void Add(ScopeRule r)
        {
            string sql = String.Format("insert into {0}.SCOPE_RULE "
                                      + "("
            + " SCOPE_RULE"
            + ", DESCR "
            + " ) "
            + "values "
            + " ( "
            + "  :SCOPE_RULE"
            + ", :DESCR"
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":SCOPE_RULE", CheckNull(r.SCOPE_RULE), OracleType.VarChar, ParameterDirection.Input));
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

        public override void Remove(ScopeRule package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.SCOPE_RULE WHERE "
                + " SCOPE_RULE = :SCOPE_RULE"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":SCOPE_RULE", package.SCOPE_RULE, OracleType.VarChar, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(ScopeRule r)
        {

            try
            {
                string sql = String.Format("update {0}.SCOPE_RULE set "
                + "  SCOPE_RULE = :SCOPE_RULE "
                + " ,DESCR = :DESCR "
                + " where "
                + " SCOPE_RULE = :SCOPE_RULE"
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":SCOPE_RULE", CheckNull(r.SCOPE_RULE), OracleType.VarChar, ParameterDirection.Input));
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


 

        protected override void RowConverter(ScopeRule r, DataRow record)
        {
            try
            {
                r.SCOPE_RULE = CastDbValueRow(record, "SCOPE_RULE");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<ScopeRule> GetScopeRules()
        {
#if !ORACLE
            return  null;
#endif
            ObservableCollection<ScopeRule> ScopeRules;
            try
            {
                string sql = String.Format("Select * from {0}.SCOPE_RULE ORDER BY DESCR", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                ScopeRules = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ScopeRules;
        }


        public override ScopeRule Get(ScopeRule entity)
        {
            throw new NotImplementedException();
        }

    }
}
