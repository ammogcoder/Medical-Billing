using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// TAT_COMP_METHOD
    /// KEY - TAT_COMP_METHOD
    /// </summary>
    public class TatCompMethod : ObjectBase<TatCompMethod>
    {
        /// <summary>
        /// TAT_COMP_METHOD.AllowDBNull = false;
        /// TAT_COMP_METHOD.Unique = true;
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

        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 100;
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
        /// SHORT_DESCR.MaxLength = 12;
        /// </summary>
        private string _SHORT_DESCR;
        public string SHORT_DESCR
        {
            get
            {
                return _SHORT_DESCR;
            }
            set
            {
                _SHORT_DESCR = value;
                OnPropertyChanged("SHORT_DESCR");
            }
        }

        /// <summary>
        /// LONG_DESCR.MaxLength = 1000;
        /// </summary>
        private string _LONG_DESCR;
        public string LONG_DESCR
        {
            get
            {
                return _LONG_DESCR;
            }
            set
            {
                _LONG_DESCR = value;
                OnPropertyChanged("LONG_DESCR");
            }
        }

        /// <summary>
        /// SHOW_TAT_COLUMN_ON_INVOICE.AllowDBNull = false;
        /// SHOW_TAT_COLUMN_ON_INVOICE.MaxLength = 1;
        /// </summary>
        private bool _SHOW_TAT_COLUMN_ON_INVOICE;
        public bool SHOW_TAT_COLUMN_ON_INVOICE
        {
            get
            {
                return _SHOW_TAT_COLUMN_ON_INVOICE;
            }
            set
            {
                _SHOW_TAT_COLUMN_ON_INVOICE = value;
                OnPropertyChanged("SHOW_TAT_COLUMN_ON_INVOICE");
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
            return 0.CombineHashCode(TAT_COMP_METHOD);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as TatCompMethod;
            if (t == null)
                return false;
            if (TAT_COMP_METHOD == t.TAT_COMP_METHOD)
                return true;
            return false;
        }
        #endregion

    }
}
