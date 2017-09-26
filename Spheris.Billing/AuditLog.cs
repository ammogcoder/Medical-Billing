using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public class AuditLog
    {
        #region Static Methods

        private static AuditLogDal GetDalInstance()
        {
            return BillingDataFactory.NewInstance().CreateAuditLogDal();
        }

        private static List<AuditLogEntry> ConvertDalItemsToObjects(List<AuditLogItem> items)
        {
            List<AuditLogEntry> objs = new List<AuditLogEntry>();
            foreach (AuditLogItem item in items)
            {
                AuditLogEntry obj = new AuditLogEntry();
                obj.AuditAction = item.AuditAction;
                obj.ChangedBy = item.ChangedBy;
                obj.ChangedTime = item.ChangedTime;
                obj.ContractID = item.ContractID;
                obj.Contract = item.Contract;
                obj.Description = item.Description;
                obj.ID = item.ID;
                obj.NewValue = item.NewValue;
                obj.OldValue = item.OldValue;
                obj.ParsePkValues(item.PkValue);
                obj.Source1 = item.Source1;
                obj.Source2 = item.Source2;
                objs.Add(obj);
            }
            return objs;
        }

        public static List<AuditLogEntry> GetEntries(DateTime beginDate, DateTime endDate)
        {
            using (AuditLogDal dal = GetDalInstance())
            {
                List<AuditLogItem> dalItems = dal.GetItems(beginDate, endDate);
                return ConvertDalItemsToObjects(dalItems);
            }
        }

        public static List<AuditLogEntry> GetEntries(DateTime beginDate, DateTime endDate, long contractID)
        {
            using (AuditLogDal dal = GetDalInstance())
            {
                List<AuditLogItem> dalItems = dal.GetItems(beginDate, endDate, contractID);
                return ConvertDalItemsToObjects(dalItems);
            }
        }

        public static List<AuditLogEntry> GetEntries(DateTime beginDate, DateTime endDate, string changedBy)
        {
            using (AuditLogDal dal = GetDalInstance())
            {
                List<AuditLogItem> dalItems = dal.GetItems(beginDate, endDate, changedBy);
                return ConvertDalItemsToObjects(dalItems);
            }
        }

        public static List<KeyValuePair<long, string>> GetContractsInRange(DateTime beginDate, DateTime endDate)
        {
#if !ORACLE
            return null;
#endif
            using (AuditLogDal dal = GetDalInstance())
            {
                List<KeyValuePair<long, string>> items = dal.GetContractsInRange(beginDate, endDate);
                return items;
            }
        }

        #endregion
    }
}
