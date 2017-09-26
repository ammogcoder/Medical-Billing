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
    public class OracleRemitToRepository : RemitToRepositoryBase
    {
        public override void Remove(RemitTo item)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<RemitTo> GetRemitTos()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<RemitTo> remits = null;
            try
           {

                string sql = string.Format("select  * from sphrsbilling.remit_to");
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                remits = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return remits;
        }

        public override RemitTo Get(RemitTo item)
        {
            throw new NotImplementedException();
        }

        public override void Add(RemitTo item)
        {
            throw new NotImplementedException();
        }

        public override void Update(RemitTo item)
        {
            throw new NotImplementedException();
        }

        static int cycles = 0;

        protected override void RowConverter(RemitTo item, DataRow row)
        {
            try
            {
                item.LINE1 = row["LINE1"].ToString();
                item.LINE2 = row["LINE2"].ToString();
                item.LINE3 = row["LINE3"].ToString();
                item.LINE4 = row["LINE4"].ToString();
                item.REMIT_TO_ID = int.Parse(row["REMIT_TO_ID"].ToString());
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }
    }
}
