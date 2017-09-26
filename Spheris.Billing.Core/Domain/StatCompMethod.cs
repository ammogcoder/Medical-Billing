using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// STAT_COMP_METHOD
    /// KEY - STAT_COMP_METHOD
    /// </summary>
    public class StatCompMethod : ObjectBase<StatCompMethod>
    {
        private string _DESCR;
        public string DESCR
        {
            get
            {
                return _DESCR;
            }
            set
            {
                if (_DESCR != value)
                {
                    _DESCR = value;
                    OnPropertyChanged("DESCR");
                }
            }
        }

        private string _STAT_COMP_METHOD;
        public string STAT_COMP_METHOD
        {
            get
            {
                return _STAT_COMP_METHOD;
            }
            set
            {
                if (_STAT_COMP_METHOD != value)
                {
                    _STAT_COMP_METHOD = value;
                    OnPropertyChanged("STAT_COMP_METHOD");
                }
            }
        }

        public override string GetValidationError(string propertyName)
        {
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(STAT_COMP_METHOD);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as StatCompMethod;
            if (t == null)
                return false;
            if (STAT_COMP_METHOD == t.STAT_COMP_METHOD)
                return true;
            return false;
        }
        #endregion
    }
}
