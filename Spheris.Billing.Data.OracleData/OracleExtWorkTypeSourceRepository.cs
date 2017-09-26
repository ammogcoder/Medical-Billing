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
    public class OracleExtWorkTypeSourceRepository : ExtWorkTypeSourceRepositoryBase
    {
        public override ObservableCollection<ExtWorkTypeSource> GetWorkTypeSources()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<ExtWorkTypeSource> workTypes = new ObservableCollection<ExtWorkTypeSource>();
            string sql = String.Format("Select * from {0}.EXT_WORK_TYPE_SOURCE",SchemaName);
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
            foreach (DataRow row in dt.Rows)
            {
                ExtWorkTypeSource item = new ExtWorkTypeSource
                    {
                        PLATFORM = row["PLATFORM"].ToString(),
                        DESCR = row["DESCR"].ToString()
                    };
                workTypes.Add(item);
            }
            return workTypes;
        }

        public override ExtWorkTypeSource Get(ExtWorkTypeSource entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(ExtWorkTypeSource entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ExtWorkTypeSource entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(ExtWorkTypeSource entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(ExtWorkTypeSource item, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
