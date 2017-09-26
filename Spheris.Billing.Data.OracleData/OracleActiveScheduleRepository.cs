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
    public class OracleActiveScheduleRepository : ActiveScheduleRepositoryBase
    {
        public override void Add(ActiveSchedule r)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ActiveSchedule package)
        {
            throw new NotImplementedException();
        }

        public override void Update(ActiveSchedule r)
        {
            throw new NotImplementedException();
        }


 

        protected override void RowConverter(ActiveSchedule r, DataRow record)
        {
            try
            {

                r.INVOICE_ID = CastDbValueRow(record, "INVOICE_ID",true,"decimal");
                r.BEGINS_ON = CastDbValueRow(record, "BEGINS_ON", true, "DateTime");
                r.TYPE = CastDbValueRow(record, "TYPE");

                r.Modified = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<ActiveSchedule> FetchActiveSchedules(decimal invoiceGrpId)
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<ActiveSchedule> ActiveSchedules;
            try
            {
                string sql = String.Format("select AO.DESCR as TYPE,A.INVOICE_ID,NVL(A.FOR_PERIOD_BEGINNING,I.BILL_PERIOD_START  ) "
                    + "as BEGINS_ON from {0}.ADD_ON_CHG A "
                    + "JOIN {0}.INVOICE I "
                    + " ON I.INVOICE_ID=A.INVOICE_ID "
                    + " JOIN {0}.ADD_ON_CHG_TYPE AO "
                    + " ON A.ADD_ON_CHG_TYPE_ID=AO.ADD_ON_CHG_TYPE_ID "
                    + " WHERE I.INVOICE_GRP_ID={1}",SchemaName,invoiceGrpId); 

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                ActiveSchedules = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException orex)
            {
                throw orex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ActiveSchedules;
        }


        public override ActiveSchedule Get(ActiveSchedule entity)
        {
            throw new NotImplementedException();
        }


    }
}
