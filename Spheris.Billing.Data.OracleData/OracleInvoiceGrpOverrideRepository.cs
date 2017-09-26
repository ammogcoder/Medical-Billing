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
    public class OracleInvoiceGrpOverrideRepository : InvoiceGrpOverrideRepositoryBase
    {
        public override void Add(InvoiceGrpOverride r)
        {
            string sql = String.Format("insert into {0}.INVOICE_GRP_OVERRIDE "
                                      + "("
            + " DEFAULT_INVOICE_GRP_ID"
            + ", OVERRIDE_KEY "
            + ",  SEND_TO_INVOICE_GRP_ID"
            + " ) "
            + "values "
            + " ( "
            + "  :DEFAULT_INVOICE_GRP_ID "
            + ", :OVERRIDE_KEY"
            + ", :SEND_TO_INVOICE_GRP_ID"
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":DEFAULT_INVOICE_GRP_ID", CheckNull(r.DEFAULT_INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OVERRIDE_KEY", CheckNull(r.OVERRIDE_KEY), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SEND_TO_INVOICE_GRP_ID", CheckNull(r.SEND_TO_INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));

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

        public override void Remove(InvoiceGrpOverride package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.INVOICE_GRP_OVERRIDE WHERE "
                + " OVERRIDE_KEY = :OVERRIDE_KEY AND"
                + " DEFAULT_INVOICE_GRP_ID = :DEFAULT_INVOICE_GRP_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":OVERRIDE_KEY", package.OVERRIDE_KEY, OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DEFAULT_INVOICE_GRP_ID", package.DEFAULT_INVOICE_GRP_ID, OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Special note on this.  The keys are 
        /// CONTRACT_ID
        /// TAT_SCHED_ID
        /// BEGIN_ON
        /// 
        /// The fields TAT_SCHED_ID and BEGIN_ON are editable and represent a condition
        /// where they keys may collide - Current software checks this condition and
        /// displays a warning to avoid it.
        /// </summary>
        /// <param name="r"></param>
        public override void Update(InvoiceGrpOverride r)
        {

            try
            {
                string sql = String.Format("update {0}.INVOICE_GRP_OVERRIDE set "
                + "  DEFAULT_INVOICE_GRP_ID = :DEFAULT_INVOICE_GRP_ID "
                + " ,OVERRIDE_KEY = :OVERRIDE_KEY "
                + " ,SEND_TO_INVOICE_GRP_ID = :SEND_TO_INVOICE_GRP_ID "
                + " where "
                + " OVERRIDE_KEY = :OVERRIDE_KEY AND "
                + " DEFAULT_INVOICE_GRP_ID = :DEFAULT_INVOICE_GRP_ID "
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":DEFAULT_INVOICE_GRP_ID", CheckNull(r.DEFAULT_INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":OVERRIDE_KEY", CheckNull(r.OVERRIDE_KEY), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SEND_TO_INVOICE_GRP_ID", CheckNull(r.SEND_TO_INVOICE_GRP_ID), OracleType.Number, ParameterDirection.Input));

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


 

        protected override void RowConverter(InvoiceGrpOverride r, DataRow record)
        {
            try
            {
                r.DEFAULT_INVOICE_GRP_ID = CastDbValueRow(record, "DEFAULT_INVOICE_GRP_ID", true, "decimal");
                r.SEND_TO_INVOICE_GRP_ID = CastDbValueRow(record, "SEND_TO_INVOICE_GRP_ID", true, "decimal");
                r.OVERRIDE_KEY = CastDbValueRow(record, "OVERRIDE_KEY");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<InvoiceGrpOverride> FetchOverRides(decimal grpId)
        {
            ObservableCollection<InvoiceGrpOverride> InvoiceGrpOverrides;
            try
            {
                string sql = String.Format("Select * from {0}.INVOICE_GRP_OVERRIDE where DEFAULT_INVOICE_GRP_ID={1}", SchemaName,grpId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                InvoiceGrpOverrides = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return InvoiceGrpOverrides;
        }

        public override InvoiceGrpOverride Get(InvoiceGrpOverride entity)
        {
            throw new NotImplementedException();
        }



    }
}
