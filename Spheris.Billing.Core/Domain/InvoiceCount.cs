
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// </summary>
    public class InvoiceCount : ObjectBase<Brand>
    {
        public bool IsAdding { get; set; }

        public InvoiceCount Clone()
        {
            return BaseClone<InvoiceCount>();
        }


        private DateTime _BILL_PERIOD_START;
        public DateTime BILL_PERIOD_START
        {
            get
            {
                return _BILL_PERIOD_START;
            }
            set
            {
                _BILL_PERIOD_START = value;
                OnPropertyChanged("BILL_PERIOD_START");
            }
        }
        private DateTime _BILL_PERIOD_END_BEFORE;
        public DateTime BILL_PERIOD_END_BEFORE
        {
            get
            {
                return _BILL_PERIOD_END_BEFORE;
            }
            set
            {
                _BILL_PERIOD_END_BEFORE = value;
                OnPropertyChanged("BILL_PERIOD_END_BEFORE");
            }
        }
        private int _COUNT;
        public int COUNT
        {
            get
            {
                return _COUNT;
            }
            set
            {
                _COUNT = value;
                OnPropertyChanged("COUNT");
            }
        }
        
        public override string GetValidationError(string propertyName)
        {
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(BILL_PERIOD_START).CombineHashCode(BILL_PERIOD_END_BEFORE).CombineHashCode(COUNT); 
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceCount;
            if (t == null)
                return false;
            if (BILL_PERIOD_START == t.BILL_PERIOD_START
                && BILL_PERIOD_END_BEFORE == t.BILL_PERIOD_END_BEFORE 
                && COUNT == t.COUNT)
                return true;
            return false;
        }
        #endregion

    }
}
