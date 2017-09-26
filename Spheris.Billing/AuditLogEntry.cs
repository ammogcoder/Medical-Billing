using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public class AuditLogEntry
    {
        #region Private Members

        private long _id;
        private DateTime _changedTime;
        private string _changedBy;
        private long _contractId;
        private string _contract;
        private string _source1;
        private string _source2;
        private KeyValuePair<string, string>[] _pkValue;
        private string _auditAction;
        private string _description;
        private string _oldValue;
        private string _newValue;

        #endregion

        public AuditLogEntry() { }

        #region Public Properties

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime ChangedTime
        {
            get { return _changedTime; }
            set { _changedTime = value; }
        }

        public string ChangedBy
        {
            get { return _changedBy; }
            set { _changedBy = value; }
        }

        public long ContractID
        {
            get { return _contractId; }
            set { _contractId = value; }
        }

        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        public string Source1
        {
            get { return _source1; }
            set { _source1 = value; }
        }

        public string Source2
        {
            get { return _source2; }
            set { _source2 = value; }
        }

        public KeyValuePair<string, string>[] PkValue
        {
            get { return _pkValue; }
            set { _pkValue = value; }
        }

        public string PkValueFormatted
        {
            get
            {
                string vals = "";
                foreach (KeyValuePair<string, string> kvp in _pkValue)
                {
                    if(!String.IsNullOrEmpty(vals))
                    {
                        vals += "; "; // (char)(13) + (char)(10);
                    }
                    vals += kvp.Key + " = " + kvp.Value;
                }
                return vals;
            }
        }

        public string AuditAction
        {
            get { return _auditAction; }
            set { _auditAction = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string OldValue
        {
            get { return _oldValue; }
            set { _oldValue = value; }
        }

        public string NewValue
        {
            get { return _newValue; }
            set { _newValue = value; }
        }

        #endregion

        #region Public Methods

        public void ParsePkValues(string pkValues)
        {
            string[] pairs = pkValues.Split(';');
            _pkValue = new KeyValuePair<string, string>[pairs.Length];
            int ndx = 0;
            foreach (string pair in pairs)
            {
                string[] keyVal = pair.Split('=');
                if (keyVal[1].Trim().Length > 0)
                {
                    _pkValue[ndx] = new KeyValuePair<string, string>(keyVal[0].Trim(), keyVal[1].Trim());
                    ndx++;
                }
            }
        }

        #endregion
    }
}
