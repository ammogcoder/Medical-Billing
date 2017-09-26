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
    public class OracleVolumeEvtRateRepository : VolumeEvtRateRepositoryBase
    {
        public override void Add(VolumeEvtRate r)
        {
            string sql = String.Format("insert into {0}.VOLUME_EVT_RATE "
                                      + "("
            + " CONTRACT_VOLUME_EVT_ID"
            + ", TIER "
            + ", ADJ "
            + ", ADJ_INHOUSE "
            + ", ADJ_OFFSHORE "
            + ", ADJ_SR"
            + ", ADJ_SR_INHOUSE "
            + ", ADJ_SR_OFFSHORE "
            + " ) "
            + "values "
            + " ( "
            + "  :CONTRACT_VOLUME_EVT_ID"
            + ", :TIER"
            + ", :ADJ"
            + ", :ADJ_INHOUSE"
            + ", :ADJ_OFFSHORE"
            + ", :ADJ_SR"
            + ", :ADJ_SR_INHOUSE "
            + ", :ADJ_SR_OFFSHORE "
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", CheckNull(r.CONTRACT_VOLUME_EVT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TIER", CheckNull(r.TIER), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ", CheckNull(r.ADJ), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_INHOUSE", CheckNull(r.ADJ_INHOUSE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_OFFSHORE", CheckNull(r.ADJ_OFFSHORE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR", CheckNull(r.ADJ_SR), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR_INHOUSE", CheckNull(r.ADJ_SR_INHOUSE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR_OFFSHORE", CheckNull(r.ADJ_SR_OFFSHORE), OracleType.Number, ParameterDirection.Input));
                

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

        public override void Remove(VolumeEvtRate package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.VOLUME_EVT_RATE WHERE "
                + " TIER = :TIER"
                + " and CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TIER", package.TIER, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", package.CONTRACT_VOLUME_EVT_ID, OracleType.Number, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                package.Modified = false;

            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public override void Update(VolumeEvtRate r)
        {

            try
            {
                string sql = String.Format("update {0}.VOLUME_EVT_RATE set "
                + "  ADJ = :ADJ "
                + " ,ADJ_INHOUSE = :ADJ_INHOUSE "
                + " ,ADJ_OFFSHORE = :ADJ_OFFSHORE "
                + " ,ADJ_SR = :ADJ_SR "
                + " ,ADJ_SR_INHOUSE = :ADJ_SR_INHOUSE "
                + " ,ADJ_SR_OFFSHORE = :ADJ_SR_OFFSHORE "
                + " ,CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID "
                + " ,TIER = :TIER "
                + " where "
                + " CONTRACT_VOLUME_EVT_ID = :CONTRACT_VOLUME_EVT_ID AND"
                + " TIER = :TIER"
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":ADJ", CheckNull(r.ADJ), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_INHOUSE", CheckNull(r.ADJ_INHOUSE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_OFFSHORE", CheckNull(r.ADJ_OFFSHORE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR", CheckNull(r.ADJ_SR), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR_INHOUSE", CheckNull(r.ADJ_SR_INHOUSE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_SR_OFFSHORE", CheckNull(r.ADJ_SR_OFFSHORE), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_VOLUME_EVT_ID", CheckNull(r.CONTRACT_VOLUME_EVT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TIER", CheckNull(r.TIER), OracleType.Number, ParameterDirection.Input));

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


 

        protected override void RowConverter(VolumeEvtRate r, DataRow record)
        {
            try
            {

                r.ADJ = CastDbValueRow(record, "ADJ", true, "decimal");
                r.ADJ_INHOUSE = CastDbValueRow(record, "ADJ_INHOUSE", true, "decimal");
                r.ADJ_OFFSHORE = CastDbValueRow(record, "ADJ_OFFSHORE", true, "decimal");
                r.ADJ_SR = CastDbValueRow(record, "ADJ_SR", true, "decimal");
                r.ADJ_SR_INHOUSE = CastDbValueRow(record, "ADJ_SR_INHOUSE", true, "decimal");
                r.ADJ_SR_OFFSHORE = CastDbValueRow(record, "ADJ_SR_OFFSHORE", true, "decimal");
                r.CONTRACT_VOLUME_EVT_ID = CastDbValueRow(record, "CONTRACT_VOLUME_EVT_ID", true, "decimal");
                r.TIER = CastDbValueRow(record, "TIER", true, "decimal");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<VolumeEvtRate> FetchVolumeEvtRates(decimal contractVolumeEvtId)
        {
            ObservableCollection<VolumeEvtRate> VolumeEvtRates;
            try
            {
                string sql = String.Format("Select * from {0}.VOLUME_EVT_RATE where CONTRACT_VOLUME_EVT_ID={1}", SchemaName, contractVolumeEvtId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                VolumeEvtRates = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VolumeEvtRates;
        }


        public override VolumeEvtRate Get(VolumeEvtRate entity)
        {
            throw new NotImplementedException();
        }
    }
}
