using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// FREQ
    /// key - FREQ
    /// </summary>
    public class Freq : ObjectBase<Freq>
    {
        public Freq()
        { }
        /// <summary>
        /// FREQ.AllowDBNull = false;
        /// FREQ.Unique = true;
        /// FREQ.MaxLength = 4;
        /// </summary>
        private string _FREQ;
        public string FREQ
        {
            get
            {
                return _FREQ;
            }
            set
            {
                _FREQ = value;
                OnPropertyChanged("FREQ");
            }
        }


        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 20;
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
        /// RECURRANCE_RULE.MaxLength = 150;
        /// </summary>
        private string _RECURRANCE_RULE;
        public string RECURRANCE_RULE
        {
            get
            {
                return _RECURRANCE_RULE;
            }
            set
            {
                _RECURRANCE_RULE = value;
                OnPropertyChanged("RECURRANCE_RULE");
            }
        }


        /// <summary>
        /// VALID_FOR_ADD_ON_CHG.MaxLength = 1;
        /// </summary>
        private bool _VALID_FOR_ADD_ON_CHG;
        public bool VALID_FOR_ADD_ON_CHG
        {
            get
            {
                return _VALID_FOR_ADD_ON_CHG;
            }
            set
            {
                _VALID_FOR_ADD_ON_CHG = value;
                OnPropertyChanged("VALID_FOR_ADD_ON_CHG");
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
            return 0.CombineHashCode(FREQ);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Freq;
            if (t == null)
                return false;
            if (FREQ == t.FREQ)
                return true;
            return false;
        }
        #endregion

    }
}
