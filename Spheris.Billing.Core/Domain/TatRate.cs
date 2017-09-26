using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// TAT_RATE
    /// key - TIER,TAT_SCHED_ID
    /// </summary>
    public class TatRate : ObjectBase<TatRate>
    {
        /// <summary>
        /// TIER.AllowDBNull = false;
        /// </summary>
        private decimal _TIER;
        public decimal TIER
        {
            get
            {
                return _TIER;
            }
            set
            {
                _TIER = value;
                OnPropertyChanged("TIER");
            }
        }


        /// <summary>
        /// PENALTY.AllowDBNull = false;
        /// </summary>
        private decimal _PENALTY;
        public decimal PENALTY
        {
            get
            {
                return _PENALTY;
            }
            set
            {
                _PENALTY = value;
                OnPropertyChanged("PENALTY");

            }
        }

        /// <summary>
        /// TAT_SCHED_ID.AllowDBNull = false;
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

        private decimal? _ALT_PENALTY;
        public decimal? ALT_PENALTY
        {
            get
            {
                return _ALT_PENALTY;
            }
            set
            {
                _ALT_PENALTY = value;
                OnPropertyChanged("ALT_PENALTY");

            }
        }

        public TatRate Clone()
        {
            return BaseClone<TatRate>();
        }

        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }

        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(TIER).CombineHashCode(TAT_SCHED_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as TatRate;
            if (t == null)
                return false;
            if (TIER == t.TIER && TAT_SCHED_ID == t.TAT_SCHED_ID)
                return true;
            return false;
        }
        #endregion
    }
}
