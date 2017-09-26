using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// OVERRIDE_KEY_SOURCE
    /// KEY - OVERRIDE_KEY_SOURCE
    /// </summary>
    public class OverRideKeySource  : ObjectBase<OverRideKeySource>
    {
        private string _OVERRIDE_KEY_SOURCE;
        public string OVERRIDE_KEY_SOURCE
        {
            get
            {
                return _OVERRIDE_KEY_SOURCE;
            }
            set
            {
                if (_OVERRIDE_KEY_SOURCE != value)
                {
                    _OVERRIDE_KEY_SOURCE = value;
                    OnPropertyChanged("OVERRIDE_KEY_SOURCE");
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

            return 0.CombineHashCode(OVERRIDE_KEY_SOURCE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as OverRideKeySource;
            if (t == null)
                return false;
            if (OVERRIDE_KEY_SOURCE == t.OVERRIDE_KEY_SOURCE)
                return true;
            return false;
        }
        #endregion
    }
}
