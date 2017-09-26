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
    public class OracleAddOnChgTypeRepository : AddOnChgTypeRepositoryBase
    {
        public override void Add(AddOnChgType r)
        {
            string sql = String.Format("insert into {0}.ADD_ON_CHG_TYPE "
                                      + "("
            + " ADD_ON_CHG_TYPE_ID"
            + " ,DESCR"
            + " ) "
            + "values "
            + " ( "
            + "  {0}.ADD_ON_CHG_TYPE_ID.NextVal"
            + "  ,:DESCR"
            + " ) "
            + " returning ADD_ON_CHG_TYPE_ID into :ADD_ON_CHG_TYPE_ID "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID",  OracleType.Number, ParameterDirection.Output));
                

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.ADD_ON_CHG_TYPE_ID = int.Parse(parameters[parameters.Count - 1].Value.ToString());
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

        public override void Remove(AddOnChgType package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.ADD_ON_CHG_TYPE WHERE "
                + " ADD_ON_CHG_TYPE_ID = :ADD_ON_CHG_TYPE_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", package.ADD_ON_CHG_TYPE_ID, OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(AddOnChgType r)
        {

            try
            {
                string sql = String.Format("update {0}.ADD_ON_CHG_TYPE set "
                + "  DESCR = :DESCR "
                + " where "
                + " ADD_ON_CHG_TYPE_ID = :ADD_ON_CHG_TYPE_ID"
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", CheckNull(r.ADD_ON_CHG_TYPE_ID), OracleType.Number, ParameterDirection.Input));
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


 

        protected override void RowConverter(AddOnChgType r, DataRow record)
        {
            try
            {

                r.ADD_ON_CHG_TYPE_ID = CastDbValueRow(record, "ADD_ON_CHG_TYPE_ID", true, "decimal");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<AddOnChgType> GetAddOnChgTypes()
        {
            ObservableCollection<AddOnChgType> AddOnChgTypes;
#if !ORACLE
            return null;
#endif
            try
            {
                string sql = String.Format("Select * from {0}.ADD_ON_CHG_TYPE ORDER BY DESCR", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                AddOnChgTypes = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AddOnChgTypes;
        }


        public override AddOnChgType Get(AddOnChgType entity)
        {
            throw new NotImplementedException();
        }

    }
}
