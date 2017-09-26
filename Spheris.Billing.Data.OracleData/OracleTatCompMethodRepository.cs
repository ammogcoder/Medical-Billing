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
    public class OracleTatCompMethodRepository : TatCompMethodRepositoryBase
    {
        public override void Add(TatCompMethod r)
        {
            string sql = String.Format("insert into {0}.TAT_COMP_METHOD"
            + "("
            + "  TAT_COMP_METHOD"
            + ", DESCR"
            + ", SHORT_DESCR"
            + ", LONG_DESCR"
            + ", SHOW_TAT_COLUMN_ON_INVOICE"
            + " ) "
            + "values "
            + " ( "
            + ", :TAT_COMP_METHOD"
            + ", :DESCR"
            + ", :SHORT_DESCR"
            + ", :LONG_DESCR"
            + ", :SHOW_TAT_COLUMN_ON_INVOICE"
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":NEWID",  OracleType.Number, ParameterDirection.InputOutput));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SHORT_DESCR", CheckNull(r.SHORT_DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":LONG_DESCR", CheckNull(r.LONG_DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SHOW_TAT_COLUMN_ON_INVOICE", (r.SHOW_TAT_COLUMN_ON_INVOICE)?'Y':'N', OracleType.Char, ParameterDirection.Input));
                
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

        public override void Remove(TatCompMethod package)
        {
            try
            {
                string sql = String.Format("DELETE FROM {0}.TAT_COMP_METHOD WHERE TAT_COMP_METHOD = :TAT_COMP_METHOD", SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TAT_COMP_METHOD", package.TAT_COMP_METHOD , OracleType.VarChar, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
                //throw ex;
            }
        }

        public override void Update(TatCompMethod package)
        {
            try
            {
                
                string sql = String.Format("update {0}.TAT_COMP_METHOD set "
                                          + "  DESCR = :DESCR "
                                          + "  ,SHORT_DESCR = :SHORT_DESCR"
                                          + "  ,LONG_DESCR = :LONG_DESCR "
                                          + "  ,SHOW_TAT_COLUMN_ON_INVOICE = :SHOW_TAT_COLUMN_ON_INVOICE "
                                          + "where TAT_COMP_METHOD = :TAT_COMP_METHOD"
                                          , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DESCR", package.DESCR, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SHORT_DESCR", package.SHORT_DESCR, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":LONG_DESCR", package.LONG_DESCR, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SHOW_TAT_COLUMN_ON_INVOICE",(package.SHOW_TAT_COLUMN_ON_INVOICE)?'Y':'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_COMP_METHOD", package.TAT_COMP_METHOD, OracleType.VarChar, ParameterDirection.Input));
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
            }
            catch (OracleException ex)
            {
                throw ex;
                //throw ex;
            }
        }

        protected override void RowConverter(TatCompMethod r, DataRow record)
        {
            try
            {
                r.SHOW_TAT_COLUMN_ON_INVOICE = CastDbValueRow(record, "SHOW_TAT_COLUMN_ON_INVOICE",true,"Bool");
                r.SHORT_DESCR = CastDbValueRow(record, "SHORT_DESCR");
                r.LONG_DESCR = CastDbValueRow(record, "LONG_DESCR");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.TAT_COMP_METHOD = CastDbValueRow(record, "TAT_COMP_METHOD");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<TatCompMethod> GetTATCompMethods()
        {
#if !ORACLE
return null;
#endif
            ObservableCollection<TatCompMethod> TatCompMethods = null;
            try
            {
                string sql = String.Format("Select * from {0}.TAT_COMP_METHOD", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                TatCompMethods = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TatCompMethods;
        }

        public override TatCompMethod Get(TatCompMethod entity)
        {
            throw new NotImplementedException();
        }

    }
}

