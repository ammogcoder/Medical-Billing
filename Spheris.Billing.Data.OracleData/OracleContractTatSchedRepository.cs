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
    public class OracleContractTatSchedRepository : ContractTatSchedRepositoryBase
    {
        public override void Add(ContractTatSched r)
        {
            string sql = String.Format("insert into {0}.CONTRACT_TAT_SCHED "
                                      + "("
            + " CONTRACT_ID"
            + ", TAT_SCHED_ID "
            + ",  BEGIN_ON"
            + ", END_AFTER "
            + ", AUTO_CHG "
            + " ) "
            + "values "
            + " ( "
            + "  :CONTRACT_ID"
            + ", :TAT_SCHED_ID"
            + ", :BEGIN_ON"
            + ", :END_AFTER "
            + ", :AUTO_CHG "
            + " ) "
            , SchemaName);
            try
            {
                List<OracleParameter> parameters = new List<OracleParameter>();

                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", CheckNull(r.TAT_SCHED_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":BEGIN_ON", CheckNull(r.BEGIN_ON), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":END_AFTER", CheckNull(r.END_AFTER), OracleType.DateTime, ParameterDirection.Input));
                
                if( r.AUTO_CHG != null)
                    parameters.Add(OracleHelper.CreateParameter(":AUTO_CHG",(r.AUTO_CHG)?'Y':'N', OracleType.Char, ParameterDirection.Input));
                else
                    parameters.Add(OracleHelper.CreateParameter(":AUTO_CHG",DBNull.Value, OracleType.Char, ParameterDirection.Input));

                OracleParameterCollection outParams = OracleHelper.ExecuteNonQuery(base.ConnectionString.Value, sql, parameters.ToArray<OracleParameter>());
                r.bWasAdded = r.Modified = false;
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

        public override void Remove(ContractTatSched package)
        {
            string sql;
            try
            {
                sql = String.Format("DELETE FROM {0}.CONTRACT_TAT_SCHED WHERE "
                + " CONTRACT_ID = :CONTRACT_ID"
                + " and TAT_SCHED_ID = :TAT_SCHED_ID"
                + " and BEGIN_ON = :BEGIN_ON"
                , SchemaName);
                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", package.CONTRACT_ID, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", package.TAT_SCHED_ID, OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":BEGIN_ON", package.BEGIN_ON, OracleType.DateTime, ParameterDirection.Input));

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
        public override void Update(ContractTatSched r)
        {

            try
            {
                string sql = String.Format("update {0}.CONTRACT_TAT_SCHED set "
                //+ "  CONTRACT_ID = :CONTRACT_ID "
                //+ "  TAT_SCHED_ID = :TAT_SCHED_ID "
                //+ " ,BEGIN_ON = :BEGIN_ON "
                + " END_AFTER = :END_AFTER "
                + " ,AUTO_CHG = :AUTO_CHG "
                + " where "
                + " CONTRACT_ID = :CONTRACT_ID AND"
                + " TAT_SCHED_ID = :TAT_SCHED_ID AND"
                + " BEGIN_ON = :BEGIN_ON"
                ,SchemaName);

                List<OracleParameter> parameters = new List<OracleParameter>();
                parameters.Add(OracleHelper.CreateParameter(":TAT_SCHED_ID", CheckNull(r.TAT_SCHED_ID), OracleType.Number, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":BEGIN_ON", CheckNull(r.BEGIN_ON), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":END_AFTER", CheckNull(r.END_AFTER), OracleType.DateTime, ParameterDirection.Input));
                parameters.Add(OracleHelper.CreateParameter(":CONTRACT_ID", CheckNull(r.CONTRACT_ID), OracleType.Number, ParameterDirection.Input));

                if (r.AUTO_CHG != null)
                    parameters.Add(OracleHelper.CreateParameter(":AUTO_CHG", (r.AUTO_CHG) ? 'Y' : 'N', OracleType.Char, ParameterDirection.Input));
                else
                    parameters.Add(OracleHelper.CreateParameter(":AUTO_CHG", 'N', OracleType.Char, ParameterDirection.Input));


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


 

        protected override void RowConverter(ContractTatSched r, DataRow record)
        {
            try
            {
                r.CONTRACT_ID = CastDbValueRow(record, "CONTRACT_ID", true, "decimal");
                r.TAT_SCHED_ID = CastDbValueRow(record, "TAT_SCHED_ID", true, "decimal");
                r.BEGIN_ON = CastDbValueRow(record, "BEGIN_ON", true, "DateTime");
                r.END_AFTER = CastDbValueRow(record, "END_AFTER", true, "DateTime");
                r.AUTO_CHG = CastDbValueRow(record, "AUTO_CHG", true, "Bool");
                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<ContractTatSched> FetchTatScheds(decimal contractId)
        {
            ObservableCollection<ContractTatSched> ContractTatScheds;
            try
            {
                string sql = String.Format("Select * from {0}.CONTRACT_TAT_SCHED where CONTRACT_ID={1}", SchemaName, contractId);

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                ContractTatScheds = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ContractTatScheds;
        }

        public override ContractTatSched Get(ContractTatSched entity)
        {
            throw new NotImplementedException();
        }


    }
}
