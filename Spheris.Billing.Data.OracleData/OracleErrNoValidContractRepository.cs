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
    public class OracleErrNoValidContractRepository : ErrNoValidContractRepositoryBase
    {

        protected override void RowConverter(ErrNoValidContract r, DataRow record)
        {
            try
            {


                r.Description = CastDbValueRow(record, "DESCR");
                r.Frequency = CastDbValueRow(record, "BILLING_FREQ");
                r.ID = CastDbValueRow(record, "INVOICE_GRP_ID", true, "decimal");
                r.PrimaryPlatform = CastDbValueRow(record, "PRIMARY_PLATFORM");
                r.Status = CastDbValueRow(record, "INVOICE_GRP_STATUS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<ErrNoValidContract> GetContractErrors(DateTime asOf, bool showOnlyActiveIGs)
        {

            ObservableCollection<ErrNoValidContract> errNoValidContracts;
            try
            {
                string sql = String.Format("Select "
                    + " IG.INVOICE_GRP_ID "
                    + " ,IG.DESCR "
                    + " ,IGS.DESCR AS INVOICE_GRP_STATUS "
                    + " ,P.DESCR AS PRIMARY_PLATFORM "
                    + " ,F.DESCR AS BILLING_FREQ"
                    + " FROM    {0}.INVOICE_GRP IG "
                    + " JOIN    {0}.INVOICE_GRP_STATUS IGS "
                    + " ON    IGS.INVOICE_GRP_STATUS = IG.INVOICE_GRP_STATUS "
                    + " JOIN    {0}.PLATFORM P "
                    + " ON    P.PLATFORM = IG.PRIMARY_PLATFORM "
                    + " JOIN    {0}.FREQ F "
                    + " ON    IG.BILLING_FREQ = F.FREQ "
                    + " WHERE   INVOICE_GRP_ID NOT IN "
                    + " (SELECT  INVOICE_GRP_ID "
                    + " FROM    {0}.BASIC_CONTRACT"
                    + " WHERE   :I_AS_OF    >=  RATE_BEGIN_ON "
                    + " AND :I_AS_OF    <= RATE_END_AFTER  "
                    + " AND :I_AS_OF    >=  TAT_SCHED_BEGIN_ON  "
                    + " AND :I_AS_OF    <= TAT_SCHED_END_AFTER) "
                    + " AND IG.INVOICE_GRP_STATUS LIKE NVL(:I_INVOICE_GRP_STATUS, '%') ", SchemaName);

                string status = "ACTV";
                if (!showOnlyActiveIGs)
                    status = ""; 
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":I_AS_OF", CheckNull(asOf), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":I_INVOICE_GRP_STATUS", status, OracleType.Char, ParameterDirection.Input));

                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                errNoValidContracts = ConvertDataTableToObservableCollection(dt);

            }
            catch (OracleException ox)
            {
                throw ox;
            }
            catch (Exception x)
            {
                throw x;
            }

            return errNoValidContracts;
        }
    }
}
