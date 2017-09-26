using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// CONTRACT_RATE
    /// KEY - CONTRACT_RATE_ID
    /// </summary>
    public class ContractTatSched : ObjectBase<ContractTatSched>
    {

        public bool bWasAdded { get; set; }
        
        public ContractTatSched Clone()
        {
            return BaseClone<ContractTatSched>();
        }
        
 
        /// <summary>
        /// *KEY*
        /// BEGIN_ON.AllowDBNull = false;
        /// </summary>
        public DateTime BEGIN_ON
        {
            get
            {
                return _BEGIN_ON;
            }
            set
            {
                _BEGIN_ON = value;
                OnPropertyChanged("BEGIN_ON");
            }
        }

        private DateTime _BEGIN_ON = DateTime.Now;

        /// <summary>
        /// *KEY*
        /// ID 
        /// </summary>
        public decimal TAT_SCHED_ID
        {
            get
            {
                return _TAT_SCHED_ID;
            }
            set
            {
                _TAT_SCHED_ID = value;
                OnPropertyChanged("TAT_SCHED_ID");
            }
        }
        private decimal _TAT_SCHED_ID;

        /// <summary>
        /// *KEY*
        /// CONTRACT_ID.AllowDBNull = false;
        /// </summary>
        public decimal CONTRACT_ID
        {
            get
            {
                return _CONTRACT_ID;
            }
            set
            {
                _CONTRACT_ID = value;
                OnPropertyChanged("CONTRACT_ID");
            }
        }
        private decimal _CONTRACT_ID;


        /// <summary>
        /// END_AFTER.AllowDBNull = false;
        /// </summary>
        public DateTime END_AFTER
        {
            get
            {
                return _END_AFTER;
            }
            set
            {
                _END_AFTER = value;
                OnPropertyChanged("END_AFTER");
            }
        }
        private DateTime _END_AFTER = DateTime.Now;


        /// <summary>
        /// AUTO_CHG CHAR 1
        /// </summary>
        public bool AUTO_CHG
        {
            get
            {
                return _AUTO_CHG;
            }
            set
            {
                _AUTO_CHG = value;
                OnPropertyChanged("AUTO_CHG");
            }
        }
        private bool _AUTO_CHG = true;


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(CONTRACT_ID)
                .CombineHashCode(TAT_SCHED_ID)
                .CombineHashCode(BEGIN_ON);

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContractTatSched;
            if (t == null)
                return false;
            if (CONTRACT_ID == t.CONTRACT_ID 
                && TAT_SCHED_ID == t.TAT_SCHED_ID
                && BEGIN_ON == t.BEGIN_ON)
                return true;
            return false;
        }
        #endregion


        public override string GetValidationError(string propertyName)
        {
            return null;
        }
    }
}
