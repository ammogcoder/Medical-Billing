using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// decimal ADD_ON_CHG_SCHED_ID;
    /// decimal INVOICE_GRP_ID;
    /// decimal ADD_ON_CHG_TYPE_ID;
    /// string FREQ;
    /// DateTime STARTS_ON;
    /// DateTime ENDS_AFTER;
    /// string NOTES;
    /// string ADDED_BY;
    /// decimal AMT_EACH;
    /// decimal QTY;
    /// string COMMENTS_FOR_BILLPRINT;
    /// string QTY_RULE;
    /// </summary>
    public class AddOnChgSched : ObjectBase<AddOnChgSched>
    {

        public AddOnChgSched Clone()
        {
            return BaseClone<AddOnChgSched>();
        }

        private decimal _ADD_ON_CHG_SCHED_ID;
        public decimal ADD_ON_CHG_SCHED_ID
        {
            get
            {
                return _ADD_ON_CHG_SCHED_ID;
            }
            set
            {
                _ADD_ON_CHG_SCHED_ID = value;
                OnPropertyChanged("ADD_ON_CHG_SCHED_ID");
            }
        }

        private decimal _INVOICE_GRP_ID;
        public decimal INVOICE_GRP_ID
        {
            get
            {
                return _INVOICE_GRP_ID;
            }
            set
            {
                _INVOICE_GRP_ID = value;
                OnPropertyChanged("INVOICE_GRP_ID");
            }
        }

        private decimal? _ADD_ON_CHG_TYPE_ID;
        public decimal? ADD_ON_CHG_TYPE_ID
        {
            get
            {
                return _ADD_ON_CHG_TYPE_ID;
            }
            set
            {
                _ADD_ON_CHG_TYPE_ID = value;
                OnPropertyChanged("ADD_ON_CHG_TYPE_ID");
            }
        }

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

        private DateTime _STARTS_ON;
        public DateTime STARTS_ON
        {
            get
            {
                return _STARTS_ON;
            }
            set
            {
                _STARTS_ON = value;
                OnPropertyChanged("STARTS_ON");
            }
        }

        private DateTime _ENDS_AFTER;
        public DateTime ENDS_AFTER
        {
            get
            {
                return _ENDS_AFTER;
            }
            set
            {
                _ENDS_AFTER = value;
                OnPropertyChanged("ENDS_AFTER");
            }
        }
        private string _NOTES;
        public string NOTES
        {
            get
            {
                return _NOTES;
            }
            set
            {
                _NOTES = value;
                OnPropertyChanged("NOTES");
            }
        }

        private string _ADDED_BY;
        public string ADDED_BY
        {
            get
            {
                return _ADDED_BY;
            }
            set
            {
                _ADDED_BY = value;
                OnPropertyChanged("ADDED_BY");
            }
        }

        private decimal _AMT_EACH;
        public decimal AMT_EACH
        {
            get
            {
                return _AMT_EACH;
            }
            set
            {
                _AMT_EACH = value;
                OnPropertyChanged("AMT_EACH");
            }
        }

        private decimal _QTY;
        public decimal QTY
        {
            get
            {
                return _QTY;
            }
            set
            {
                _QTY = value;
                OnPropertyChanged("QTY");
            }
        }

        private string _COMMENTS_FOR_BILLPRINT;
        public string COMMENTS_FOR_BILLPRINT
        {
            get
            {
                return _COMMENTS_FOR_BILLPRINT;
            }
            set
            {
                _COMMENTS_FOR_BILLPRINT = value;
                OnPropertyChanged("COMMENTS_FOR_BILLPRINT");
            }
        }

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

        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(ADD_ON_CHG_SCHED_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AddOnChgSched;
            if (t == null)
                return false;
            if (ADD_ON_CHG_SCHED_ID == t.ADD_ON_CHG_SCHED_ID)
                return true;
            return false;
        }

    }
}


