using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{

    public struct AuditLogItem
    {
        public long ID;
        public DateTime ChangedTime;
        public string ChangedBy;
        public long ContractID;
        public string Contract;
        public string Source1;
        public string Source2;
        public string PkValue;
        public string AuditAction;
        public string Description;
        public string OldValue;
        public string NewValue;
    }

    public abstract class AuditLogDal : DatabaseObject<AuditLogItem>
    {
        /// <summary>
        /// Gets all audit log records for a specified date range.
        /// </summary>
        /// <param name="beginDate">The range begin date.</param>
        /// <param name="endDate">The range end date.</param>
        /// <returns>List of AuditLogItem records in the range.</returns>
        public abstract List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Gets all audit log records for a specified date range that belong to a specific contract.
        /// </summary>
        /// <param name="beginDate">The range begin date.</param>
        /// <param name="endDate">The range end date.</param>
        /// <param name="contractId">The InvoiceGrpId of the contract to filter for.</param>
        /// <returns>List of AuditLogItem records in the range.</returns>
        public abstract List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate, long contractId);

        /// <summary>
        /// Gets all audit log records for a specified date range that were created by a specific user.
        /// </summary>
        /// <param name="beginDate">The range begin date.</param>
        /// <param name="endDate">The range end date.</param>
        /// <param name="changedBy">The InvoiceGrpId of the person who initiated the change that triggered the log entry.</param>
        /// <returns>List of AuditLogItem records in the range.</returns>
        public abstract List<AuditLogItem> GetItems(DateTime beginDate, DateTime endDate, string changedBy);

        /// <summary>
        /// Gets a list of all contracts that have audit log entries for the specified date range.
        /// </summary>
        /// <param name="beginDate">The range begin date.</param>
        /// <param name="endDate">The range end date.</param>
        /// <returns>List of KeyValuePair objects where the key is the contract InvoiceGrpId and the value is the contract name.</returns>
        public abstract List<KeyValuePair<long, string>> GetContractsInRange(DateTime beginDate, DateTime endDate);
    }
}
