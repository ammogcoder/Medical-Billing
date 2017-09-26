using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// QTY_RULE
    /// key - QTY_RULE
    /// </summary>
    public class QtyRule : ObjectBase<QtyRule>
    {
        public QtyRule()
        { }

        private string _QTY_RULE;
        public string QTY_RULE
        {
            get
            {
                return _QTY_RULE;
            }
            set
            {
                _QTY_RULE = value;
                OnPropertyChanged("QTY_RULE");
            }
        }

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

        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }

        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(QTY_RULE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as QtyRule;
            if (t == null)
                return false;
            if (QTY_RULE == t.QTY_RULE)
                return true;
            return false;
        }
        #endregion

    }
}
