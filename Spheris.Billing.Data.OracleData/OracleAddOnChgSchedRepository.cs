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
    public class OracleAddOnChgSchedRepository : AddOnChgSchedRepositoryBase
    {
        public override void Add(AddOnChgSched r)
        {
            string sql = String.Format("insert into {0}.ADD_ON_CHG_SCHED "
                                      + "("
            + " ADD_ON_CHG_SCHED_ID"
            + ", ADD_ON_CHG_TYPE_ID "
            + ", ADDED_BY "
            + ", AMT_EACH "
            + ", COMMENTS_FOR_BILLPRINT "
            + ", STARTS_ON "
            + ", ENDS_AFTER "
            + ", INVOICE_GRP_ID"
            + ", NOTES "
            + ", QTY "
            + ", QTY_RULE "
            + ", FREQ "
            + " ) "
            + "values "
            + " ( "
            + "  {0}.ADD_ON_CHG_SCHED_ID.nextval"
            + ", :ADD_ON_CHG_TYPE_ID"
            + ", :ADDED_BY"
            + ", :AMT_EACH"
            + ", :COMMENTS_FOR_BILLPRINT"
            + ", :STARTS_ON "
            + ", :ENDS_AFTER"
            + ", :INVOICE_GRP_ID"
            + ", :NOTES "
            + ", :QTY "
            + ", :QTY_RULE "
            + ", :FREQ "
            + " ) "
            + "returning ADD_ON_CHG_SCHED_ID into :ADD_ON_CHG_SCHED_ID"
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

             parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_SCHED_ID", OracleType.Number, ParameterDirection.InputOutput));

             parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", CheckNull(r.ADD_ON_CHG_TYPE_ID), OracleType.Number, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":ADDED_BY", CheckNull(r.ADDED_BY), OracleType.VarChar, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":AMT_EACH", CheckNull(r.AMT_EACH), OracleType.Number, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":COMMENTS_FOR_BILLPRINT", CheckNull(r.COMMENTS_FOR_BILLPRINT), OracleType.VarChar, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":STARTS_ON", CheckNull(r.STARTS_ON), OracleType.DateTime, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":ENDS_AFTER", CheckNull(r.ENDS_AFTER), OracleType.DateTime, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":FREQ", CheckNull(r.FREQ), OracleType.VarChar, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":INVOICE_GRP_ID", CheckNull(r.INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":NOTES", CheckNull(r.NOTES), OracleType.VarChar, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":QTY", CheckNull(r.QTY), OracleType.Number, ParameterDirection.Input));
             parameters.Add(OracleHelper.CreateParameter(":QTY_RULE", CheckNull(r.QTY_RULE), OracleType.VarChar, ParameterDirection.Input));



                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.ADD_ON_CHG_SCHED_ID = int.Parse(parameters[0].Value.ToString());
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

        public override void Remove(AddOnChgSched package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.ADD_ON_CHG_SCHED WHERE "
                + " ADD_ON_CHG_SCHED_ID = :ADD_ON_CHG_SCHED_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_SCHED_ID", package.ADD_ON_CHG_SCHED_ID, OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(AddOnChgSched r)
        {

            try
            {
                string sql = String.Format("update {0}.ADD_ON_CHG_SCHED set "
                + "  ADD_ON_CHG_TYPE_ID = :ADD_ON_CHG_TYPE_ID "
                + " ,ADDED_BY = :ADDED_BY "
                + " ,AMT_EACH = :AMT_EACH "
                + " ,COMMENTS_FOR_BILLPRINT = :COMMENTS_FOR_BILLPRINT "
                + " ,ENDS_AFTER = :ENDS_AFTER "
                + " ,FREQ = :FREQ "
                + " ,INVOICE_GRP_ID = :INVOICE_GRP_ID "
                + " ,NOTES = :NOTES "
                + " ,QTY = :QTY "
                + " ,QTY_RULE = :QTY_RULE "
                + " ,STARTS_ON = :STARTS_ON "
                + " where "
                + " ADD_ON_CHG_SCHED_ID = :ADD_ON_CHG_SCHED_ID "
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_SCHED_ID", CheckNull(r.ADD_ON_CHG_SCHED_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", CheckNull(r.ADD_ON_CHG_TYPE_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADDED_BY", CheckNull(r.ADDED_BY), OracleType.VarChar , ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":AMT_EACH", CheckNull(r.AMT_EACH), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":COMMENTS_FOR_BILLPRINT", CheckNull(r.COMMENTS_FOR_BILLPRINT), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ENDS_AFTER", CheckNull(r.ENDS_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":FREQ", CheckNull(r.FREQ), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":INVOICE_GRP_ID", CheckNull(r.INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":NOTES", CheckNull(r.NOTES), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":QTY", CheckNull(r.QTY), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":QTY_RULE", CheckNull(r.QTY_RULE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STARTS_ON", CheckNull(r.STARTS_ON), OracleType.DateTime, ParameterDirection.Input));

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


 

        protected override void RowConverter(AddOnChgSched r, DataRow record)
        {
            try
            {

                r.ADD_ON_CHG_SCHED_ID = CastDbValueRow(record, "ADD_ON_CHG_SCHED_ID", true, "decimal");
                r.ADD_ON_CHG_TYPE_ID = CastDbValueRow(record, "ADD_ON_CHG_TYPE_ID", true, "decimal");
                r.ADDED_BY = CastDbValueRow(record, "ADDED_BY");
                r.AMT_EACH = CastDbValueRow(record, "AMT_EACH", true, "decimal");
                r.COMMENTS_FOR_BILLPRINT = CastDbValueRow(record, "COMMENTS_FOR_BILLPRINT" );
                r.ENDS_AFTER = CastDbValueRow(record, "ENDS_AFTER", true, "DateTime");
                r.FREQ = CastDbValueRow(record, "FREQ" );
                r.INVOICE_GRP_ID = CastDbValueRow(record, "INVOICE_GRP_ID", true, "decimal");
                r.NOTES = CastDbValueRow(record, "NOTES" );
                r.QTY = CastDbValueRow(record, "QTY", true, "decimal");
                r.QTY_RULE = CastDbValueRow(record, "QTY_RULE");
                r.STARTS_ON = CastDbValueRow(record, "STARTS_ON", true, "DateTime");

                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<AddOnChgSched> GetAddOnChgScheds(decimal invoiceGrpId)
        {
            ObservableCollection<AddOnChgSched> addOnChgScheds;
            try
            {
                string sql = String.Format("Select * from {0}.ADD_ON_CHG_SCHED where INVOICE_GRP_ID={1}", SchemaName, invoiceGrpId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                addOnChgScheds = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return addOnChgScheds;
        }


        public override AddOnChgSched Get(AddOnChgSched entity)
        {
            throw new NotImplementedException();
        }

    }
}
