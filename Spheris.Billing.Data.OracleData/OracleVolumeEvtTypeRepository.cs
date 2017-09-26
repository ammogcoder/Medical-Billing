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
    public class OracleVolumeEvtTypeRepository : VolumeEvtTypeRepositoryBase
    {
        public override void Add(VolumeEvtType r)
        {
            string sql = String.Format("insert into {0}.VOLUME_EVT_TYPE "
                                      + "("
            + " VOLUME_EVT_TYPE_ID"
            + ", ADD_ON_CHG_TYPE_ID "
            + ",  ADJ_TYPE"
            + ", DESCR "
            + ", SCOPE_RULE "
            + " ) "
            + "values "
            + " ( "
            + "  {0}.VOLUME_EVT_TYPE_ID.nextval "
            + ", :ADD_ON_CHG_TYPE_ID"
            + ", :ADJ_TYPE"
            + ", :DESCR "
            + ", :SCOPE_RULE "
            + " ) "
            + " returning VOLUME_EVT_TYPE_ID into :VOLUME_EVT_TYPE_ID "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", CheckNull(r.ADD_ON_CHG_TYPE_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_TYPE", CheckNull(r.ADJ_TYPE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SCOPE_RULE", CheckNull(r.SCOPE_RULE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":VOLUME_EVT_TYPE_ID",CheckNull(r.VOLUME_EVT_TYPE_ID) , OracleType.Number, ParameterDirection.InputOutput));
                
                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.VOLUME_EVT_TYPE_ID = int.Parse(parameters[parameters.Count - 1].Value.ToString());
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

        public override void Remove(VolumeEvtType package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.VOLUME_EVT_TYPE WHERE "
                + " VOLUME_EVT_TYPE_ID = :VOLUME_EVT_TYPE_ID"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":VOLUME_EVT_TYPE_ID", package.VOLUME_EVT_TYPE_ID, OracleType.Number, ParameterDirection.Input));

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
        public override void Update(VolumeEvtType r)
        {

            try
            {
                string sql = String.Format("update {0}.VOLUME_EVT_TYPE set "
                + "  SCOPE_RULE = :SCOPE_RULE "
                + " ,DESCR = :DESCR "
                + " ,ADJ_TYPE = :ADJ_TYPE "
                + " ,ADD_ON_CHG_TYPE_ID = :ADD_ON_CHG_TYPE_ID "
                + " where "
                + " VOLUME_EVT_TYPE_ID = :VOLUME_EVT_TYPE_ID "
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":VOLUME_EVT_TYPE_ID", CheckNull(r.VOLUME_EVT_TYPE_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":SCOPE_RULE", CheckNull(r.SCOPE_RULE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":DESCR", CheckNull(r.DESCR), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADJ_TYPE", CheckNull(r.ADJ_TYPE), OracleType.VarChar, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":ADD_ON_CHG_TYPE_ID", CheckNull(r.ADD_ON_CHG_TYPE_ID), OracleType.Number, ParameterDirection.Input));


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


 

        protected override void RowConverter(VolumeEvtType r, DataRow record)
        {
            try
            {
                r.ADD_ON_CHG_TYPE_ID = CastDbValueRow(record, "ADD_ON_CHG_TYPE_ID", true, "decimal");
                r.ADJ_TYPE = CastDbValueRow(record, "ADJ_TYPE");
                r.DESCR = CastDbValueRow(record, "DESCR");
                r.SCOPE_RULE = CastDbValueRow(record, "SCOPE_RULE");
                r.VOLUME_EVT_TYPE_ID = CastDbValueRow(record, "VOLUME_EVT_TYPE_ID", true, "decimal");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<VolumeEvtType>  FetchVolumeEvtType()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<VolumeEvtType> VolumeEvtTypes;
            try
            {
                string sql = String.Format("Select * from {0}.VOLUME_EVT_TYPE", SchemaName);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                VolumeEvtTypes = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VolumeEvtTypes;
        }

        public override VolumeEvtType Get(VolumeEvtType entity)
        {
            throw new NotImplementedException();
        }


    }
}
