using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// Table CHAR_COMP_METHOD
    /// KEYS CHAR_COMP_METHOD
    /// </summary>
    public class ChargeMethod : ObjectBase<ChargeMethod>
    {

        private int _BILLING_PRECISION;
        public int BILLING_PRECISION
        {
            get
            {
                return _BILLING_PRECISION;
            }
            set
            {
                if (_BILLING_PRECISION != value)
                {
                    _BILLING_PRECISION = value;
                    OnPropertyChanged("BILLING_PRECISION");
                }
            }
        }

        /// <summary>
        /// key
        /// </summary>
        private string _CHAR_COMP_METHOD;
        public string CHAR_COMP_METHOD
        {
            get
            {
                return _CHAR_COMP_METHOD;
            }
            set
            {
                if (_CHAR_COMP_METHOD != value)
                {
                    _CHAR_COMP_METHOD = value;
                    OnPropertyChanged("CHAR_COMP_METHOD");
                }
            }
        }

        private int? _CHARS_PER_LINE;
        public int? CHARS_PER_LINE
        {
            get
            {
                return _CHARS_PER_LINE;
            }
            set
            {
                if (_CHARS_PER_LINE != value)
                {
                    _CHARS_PER_LINE = value;
                    OnPropertyChanged("CHARS_PER_LINE");
                }
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
                if (_DESCR != value)
                {
                    _DESCR = value;
                    OnPropertyChanged("DESCR");
                }
            }
        }

        private string _GP_ITEM_DESCR;
        public string GP_ITEM_DESCR
        {
            get
            {
                return _GP_ITEM_DESCR;
            }
            set
            {
                if (_GP_ITEM_DESCR != value)
                {
                    _GP_ITEM_DESCR = value;
                    OnPropertyChanged("GP_ITEM_DESCR");
                }
            }
        }


        private string _INVOICE_COLUMN_HEADER;
        public string INVOICE_COLUMN_HEADER
        {
            get
            {
                return _INVOICE_COLUMN_HEADER;
            }
            set
            {
                if (_INVOICE_COLUMN_HEADER != value)
                {
                    _INVOICE_COLUMN_HEADER = value;
                    OnPropertyChanged("INVOICE_COLUMN_HEADER");
                }
            }
        }



        private bool? _ROUNDING_STYLE;
        public bool? ROUNDING_STYLE
        {
            get
            {
                return _ROUNDING_STYLE;
            }
            set
            {
                if (_ROUNDING_STYLE != value)
                {
                    _ROUNDING_STYLE = value;
                    OnPropertyChanged("ROUNDING_STYLE");
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
            return 0.CombineHashCode(CHAR_COMP_METHOD);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ChargeMethod;
            if (t == null)
                return false;
            if (CHAR_COMP_METHOD == t.CHAR_COMP_METHOD )
                return true;
            return false;
        }
        #endregion
    }
    
}
