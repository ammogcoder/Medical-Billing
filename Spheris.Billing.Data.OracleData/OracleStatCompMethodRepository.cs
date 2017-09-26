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
    public class OracleStatCompMethodRepository : StatCompMethodRepositoryBase
    {

        public override ObservableCollection<StatCompMethod> GetStatCompMethods()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<StatCompMethod> statCompMethods = new ObservableCollection<StatCompMethod>();
            try
            {
                string sql = "select * from sphrsbilling.stat_comp_method";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                statCompMethods = ConvertDataTableToObservableCollection(dt);

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }

            return statCompMethods;
        }


        public override StatCompMethod Get(StatCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(StatCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(StatCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(StatCompMethod entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(StatCompMethod item, DataRow row)
        {
            item.DESCR = CastDbValueRow(row, "DESCR");
            item.STAT_COMP_METHOD = CastDbValueRow(row, "STAT_COMP_METHOD");
        }
    }
}
