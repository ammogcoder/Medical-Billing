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
    public class OracleContractVolumeEvtRepository : ContractVolumeEvtRepositoryBase
    {
        public override void Add(ContractVolumeEvt r)
        {
            string sql = String.Format("insert into {0}.CONTRACT_VOLUME_EVT "
                                      + "("
            + " CONTRACT_ID"
            + ", CONTRACT_VOLUME_EVT_ID "
            + ", ENDS_AFTER"
            + ", STARTS_ON "
            + ", TALLY_STAT "
            + ", VOLUME_EVT_TYPE_ID "
            + " ) "
            + "values "
            + " ( "
            + "  :CONTRACT_ID"
            + ", :CONTRACT_VOLUME_EVT_ID"
            + ", :ENDS_AFTER"
            + ", :STARTS_ON"
            + ", :TALLY_STAT"
            + ", :VOLUME_EVT_TYPE_ID "
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", CheckNull(r.CONTRACT_VOLUME_EVT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ENDS_AFTER", CheckNull(r.ENDS_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STARTS_ON", CheckNull(r.STARTS_ON), OracleType.DateTime, ParameterDirection.Input));
                if(r.TALLY_STAT == null)
                    parameters.Add(OracleHelper.CreateParameter(":TALLY_STAT", DBNull.Value, OracleType.Char, ParameterDirection.Input));
                else
                    parameters.Add(OracleHelper.CreateParameter(":TALLY_STAT", (r.TALLY_STAT == true)?'Y':'N', OracleType.Char, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":VOLUME_EVT_TYPE_ID", CheckNull(r.VOLUME_EVT_TYPE_ID), OracleType.Number, ParameterDirection.Input));
                
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

        public override void Remove(ContractVolumeEvt package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.CONTRACT_VOLUME_EVT WHERE "
                + " CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", package.CONTRACT_VOLUME_EVT_ID, OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(ContractVolumeEvt r)
        {

            try
            {
                string sql = String.Format("update {0}.CONTRACT_VOLUME_EVT set "
                + "  CONTRACT_ID = :CONTRACT_ID "
                + " ,CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID "
                + " ,ENDS_AFTER = :ENDS_AFTER "
                + " ,STARTS_ON = :STARTS_ON "
                + " ,TALLY_STAT = :TALLY_STAT "
                + " ,VOLUME_EVT_TYPE_ID = :VOLUME_EVT_TYPE_ID "
                + " where "
                + " CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID "
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", CheckNull(r.CONTRACT_VOLUME_EVT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ENDS_AFTER", CheckNull(r.ENDS_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":STARTS_ON", CheckNull(r.STARTS_ON), OracleType.DateTime, ParameterDirection.Input));
                if (r.TALLY_STAT == null)
                    parameters.Add(OracleHelper.CreateParameter(":TALLY_STAT", DBNull.Value, OracleType.Char, ParameterDirection.Input));
                else
                    parameters.Add(OracleHelper.CreateParameter(":TALLY_STAT", (r.TALLY_STAT == true) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));

                parameters.Add(OracleHelper.CreateParameter(":VOLUME_EVT_TYPE_ID", CheckNull(r.VOLUME_EVT_TYPE_ID), OracleType.Number, ParameterDirection.Input));

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


 

        protected override void RowConverter(ContractVolumeEvt r, DataRow record)
        {
            try
            {

                r.CONTRACT_ID = CastDbValueRow(record, "CONTRACT_ID", true, "decimal");
                r.CONTRACT_VOLUME_EVT_ID = CastDbValueRow(record, "CONTRACT_VOLUME_EVT_ID", true, "decimal");
                r.ENDS_AFTER = CastDbValueRow(record, "ENDS_AFTER", true, "DateTime");
                r.STARTS_ON = CastDbValueRow(record, "STARTS_ON", true, "DateTime");
                bool? tstat = CastDbValueRow(record, "TALLY_STAT", true, "Bool");
                if (tstat == false || tstat == null)
                    r.TALLY_STAT = false;
                else
                    r.TALLY_STAT = true;
                //r.TALLY_STAT = CastDbValueRow(record, "TALLY_STAT", true, "Bool");
                r.VOLUME_EVT_TYPE_ID = CastDbValueRow(record, "VOLUME_EVT_TYPE_ID", true, "decimal");

                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ContractVolumeEvt Get(ContractVolumeEvt entity)
        {
            throw new NotImplementedException();
        }



        public override ObservableCollection<ContractVolumeEvt> FetchVolumeEvts(decimal contractId)
        {
            ObservableCollection<ContractVolumeEvt> contractVolumeEvts;
            try
            {
                string sql = String.Format("Select * from {0}.CONTRACT_VOLUME_EVT where CONTRACT_ID={1}", SchemaName, contractId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                contractVolumeEvts = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contractVolumeEvts;
        }
    }
}
