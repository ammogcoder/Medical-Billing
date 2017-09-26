using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
 
    /// <summary>
    /// select DISTINCT TC.CONTRACT_ID,C.DESCR from SPHRSBILLING.TODAYS_CONTRACT TC JOIN SPHRSBILLING.CONTRACT C ON C.CONTRACT_ID=TC.CONTRACT_ID WHERE TAT_SCHED_ID=71
    /// </summary>
    public class ContractsUsingTatSched : ObjectBase<ContractsUsingTatSched>
    {
        public ContractsUsingTatSched() { }
         
        public ContractsUsingTatSched Clone()
        {
            return BaseClone<ContractsUsingTatSched>();
        }

        /// <summary>
        /// CONTRACT_ID.AllowDBNull = false;
        /// CONTRACT_ID.Unique = true;
        /// KEY
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
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 150;
        /// </summary>
        public string DESCR
        {
            get
            {
                return _DESCR;
            }
            set
            {
                _DESCR = value;
                OnPropertyChanged("DESCR");
            }
        }
        private string _DESCR;


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(CONTRACT_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContractsUsingTatSched;
            if (t == null)
                return false;
            if (CONTRACT_ID == t.CONTRACT_ID )
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

