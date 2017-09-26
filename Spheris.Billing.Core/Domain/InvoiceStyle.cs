using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// INVOICE_STYLE / BILL_STYLE
    /// KEY - INVOICE_STYLE
    /// </summary>
    public class InvoiceStyle : ObjectBase<InvoiceGrpStatus>
    {
        /// <summary>
        /// INVOICE_STYLE.AllowDBNull = false;
        /// INVOICE_STYLE.Unique = true;
        /// INVOICE_STYLE.MaxLength = 4;
        /// </summary>
        private string _INVOICE_STYLE;
        public string INVOICE_STYLE
        {
            get
            {
                return _INVOICE_STYLE;
            }
            set
            {
                _INVOICE_STYLE = value;
                OnPropertyChanged("INVOICE_STYLE");
            }
        }


        /// <summary>
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
        /// MUST_ENCRYPT.AllowDBNull = false;
        /// MUST_ENCRYPT.MaxLength = 1;
        /// </summary>
        private bool _MUST_ENCRYPT;
        public bool MUST_ENCRYPT
        {
            get
            {
                return _MUST_ENCRYPT;
            }
            set
            {
                _MUST_ENCRYPT = value;
                OnPropertyChanged("MUST_ENCRYPT");
            }
        }



        public override string GetValidationError(string propertyName)
        {
            if (!ValidatedProperties.Contains(propertyName))
                return null;
            string error = null;
            switch (propertyName)
            {
                case "INVOICE_STYLE":
                    error = (!HasValue(this.INVOICE_STYLE as object)) ? "Invoice style has no value" : null;
                    break;
                case "DESCR":
                    break;
                case "MUST_ENCRYPT":
                    error = (MUST_ENCRYPT == null) ? "Must Encrypt cannot be null" : null;
                    break;
                default:
                    break;
            }
            return error;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_STYLE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceStyle;
            if (t == null)
                return false;
            if (INVOICE_STYLE == t.INVOICE_STYLE)
                return true;
            return false;
        }
        #endregion

    }
}
