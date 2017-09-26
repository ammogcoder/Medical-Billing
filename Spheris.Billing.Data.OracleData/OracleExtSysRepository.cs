using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Core.Domain;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.OracleClient;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleExtSysRepository : ExtSysRepositoryBase
    {
        public override ObservableCollection<ExtSys> GetExtSys()
        {
            ObservableCollection<ExtSys> extSys;
            string sql = String.Format("SELECT * from {0}.Ext_sys", SchemaName);
            OracleParameter[] p = null;
            if (base.ConnectionString == null)
                return null;
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
            extSys = ConvertDataTableToObservableCollection(dt);
            return extSys;
        }

        public override ExtSys Get(ExtSys entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(ExtSys entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ExtSys entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(ExtSys entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(ExtSys item,  DataRow row)
        {
            item.BEGIN_SYNC_ON = CastDbValueRow(row, "BEGIN_SYNC_ON", true, "DateTime");
            item.DESCR = CastDbValueRow(row, "DESCR", true, "String");
            item.DONT_SYNC_AFTER = CastDbValueRow(row, "DONT_SYNC_AFTER", true, "DateTime");
            item.EXT_SYS = CastDbValueRow(row, "EXT_SYS", true, "String");
            item.INTF_ROLE = CastDbValueRow(row, "INTF_ROLE", true, "Bool");
            item.PLATFORM = CastDbValueRow(row, "PLATFORM", true, "String");
            item.REMOTE_LINK = CastDbValueRow(row, "REMOTE_LINK", true, "String");
            item.SR_SYS_DESCR = CastDbValueRow(row, "SR_SYS_DESCR", true, "String");
        }
    }
}
