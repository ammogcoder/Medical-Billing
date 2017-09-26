using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// TAT_SCHED
    /// key - TAT_sched_ID
    /// </summary>
    public class TatSched : ObjectBase<TatSched>
    {
        public TatSched Clone()
        {
            return BaseClone<TatSched>();
        }

        /// <summary>
        /// TAT_SCHED_ID.AllowDBNull = false;
        /// TAT_SCHED_ID.Unique = true;
        /// </summary>
        private decimal _TAT_SCHED_ID;
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

        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 75;
        /// </summary>
        private string _DESCR;
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

        /// <summary>
        /// TAT_COMP_METHOD.MaxLength = 4;
        /// </summary>
        private string _TAT_COMP_METHOD;
        public string TAT_COMP_METHOD
        {
            get
            {
                return _TAT_COMP_METHOD;
            }
            set
            {
                _TAT_COMP_METHOD = value;
                OnPropertyChanged("TAT_COMP_METHOD");
            }
        }

        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(TAT_SCHED_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as TatSched;
            if (t == null)
                return false;
            if (TAT_SCHED_ID == t.TAT_SCHED_ID)
                return true;
            return false;
        }
        #endregion

    }
}
