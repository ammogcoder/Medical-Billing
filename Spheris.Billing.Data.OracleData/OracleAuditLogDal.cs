using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleAuditLogDal : AuditLogDal
    {
        public OracleAuditLogDal() {}

        public override List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate)
        {
            return ConvertDataReaderToList(GetAuditLogRecords(beginDate, endDate, ""));
        }

        public override List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate, long contractId)
        {
            string whereSql = String.Format("and al.contract_id = {0}", contractId);
            return ConvertDataReaderToList(GetAuditLogRecords(beginDate, endDate, whereSql));
        }

        public override List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate, string changedBy)
        {
            string whereSql = String.Format("and al.changed_by = '{0}'", changedBy);
            return ConvertDataReaderToList(GetAuditLogRecords(beginDate, endDate, whereSql));
        }

        public override List<KeyValuePair<long, string>> GetContractsInRange(DateTime beginDate, DateTime endDate)
        {
#if !ORACLE
            return null;
#endif
            string sql = String.Format("select  distinct al.contract_id ,c.descr as contract_descr \r\n"
                                      + "from    {0}.audit_log al \r\n"
                                      + "left join {0}.contract c \r\n"
                                      + "  on    c.contract_id = al.contract_id \r\n"
                                      + "where   al.changed_time >= to_date('{1}', 'yyyymmdd') \r\n"
                                      + "  and   al.changed_time <  to_date('{2}', 'yyyymmdd') \r\n"
                                      + "order by c.descr", SchemaName, beginDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"));

            OracleConnection cnn = new OracleConnection(base.ConnectionString.Value);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cnn.Open();
            return DataHelper.BuildKeyValuePairListFromDataReader<long, string>(cmd.ExecuteReader(), "contract_id", "contract_descr");
        }

        /// <summary>
        /// Converts an IDataReader to a list of AuditLogItems.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected List<AuditLogItem> ConvertDataReaderToList(IDataReader reader)
        {
            List<AuditLogItem> list = new List<AuditLogItem>();
            while (reader.Read())
            {
                AuditLogItem item = new AuditLogItem();
                item.ID = long.Parse(reader["audit_log_id"].ToString());
                item.ChangedTime = DateTime.Parse(reader["changed_time"].ToString());
                item.ChangedBy = reader["changed_by"].ToString();
                item.ContractID = long.Parse(reader["contract_id"].ToString());
                item.Contract = reader["contract_descr"].ToString();
                item.Source1 = reader["source1"].ToString();
                item.Source2 = reader["source2"].ToString();
                item.PkValue = reader["pk_value"].ToString();
                item.AuditAction = reader["audit_action"].ToString();
                item.Description = reader["descr"].ToString();
                item.OldValue = reader["old_value"].ToString();
                item.NewValue = reader["new_value"].ToString();
                list.Add(item);
            }
            reader.Dispose();
            return list;
        }

        private OracleDataReader GetAuditLogRecords(DateTime beginDate, DateTime endDate, string whereSql)
        {
            string sql = String.Format( "select  al.audit_log_id        ,al.changed_time           ,al.changed_by \r\n"
                                      + "        ,al.contract_id        ,al.source1                ,al.source2 \r\n"
                                      + "        ,al.pk_value           ,al.audit_action           ,al.descr \r\n"
                                      + "        ,al.old_value          ,al.new_value              ,c.descr as contract_descr\r\n"
                                      + "from    {0}.audit_log al \r\n"
                                      + "join    {0}.contract c \r\n"
                                      + "  on    c.contract_id = al.contract_id \r\n"
                                      + "where   al.changed_time >= to_date('{1}', 'yyyymmdd') \r\n"
                                      + "  and   al.changed_time <  to_date('{2}', 'yyyymmdd') \r\n"
                                      + "  {3} \r\n"
                                      + "order by al.changed_time", SchemaName, beginDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), whereSql);

            OracleConnection cnn = new OracleConnection(base.ConnectionString.Value);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cnn.Open();
            return cmd.ExecuteReader();
        }
    }
}
