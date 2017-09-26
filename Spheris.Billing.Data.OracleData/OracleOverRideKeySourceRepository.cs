using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using System.Data;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleOverRideKeySourceRepository : OverRideKeySourceRepositoryBase
    {
        public override ObservableCollection<OverRideKeySource> GetOverRides()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<OverRideKeySource> overrides = new ObservableCollection<OverRideKeySource>();
            string sql = String.Format("Select * from {0}.OVERRIDE_KEY_SOURCE",SchemaName);
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            foreach (DataRow row in dt.Rows)
            {
                OverRideKeySource item = new OverRideKeySource
                    {
                        OVERRIDE_KEY_SOURCE = row["OVERRIDE_KEY_SOURCE"].ToString()
                    };
                overrides.Add(item);
            }
            return overrides;
        }

        public override OverRideKeySource Get(OverRideKeySource entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(OverRideKeySource entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(OverRideKeySource entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(OverRideKeySource entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(OverRideKeySource item, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
