using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleQtyRuleRepository : QtyRuleRepositoryBase
    {
        public override ObservableCollection<QtyRule> FetchQtyRules()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<QtyRule> qtyRules = new ObservableCollection<QtyRule>();
            try
            {
                const string sql = "select * from  sphrsbilling.QTY_RULE";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                qtyRules = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return qtyRules;
        }
        public override QtyRule Get(QtyRule entity)
        { throw new NotImplementedException(); }
        public override void Add(QtyRule entity)
        { throw new NotImplementedException(); }
        public override void Update(QtyRule entity)
        { throw new NotImplementedException(); }
        public override void Remove(QtyRule entity)
        { throw new NotImplementedException(); }
        protected override void RowConverter(QtyRule item, DataRow row)
        {
            item.QTY_RULE = CastDbValueRow(row, "QTY_RULE");
            item.DESCR = CastDbValueRow(row, "DESCR");
        }
    }
}
