using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// INVOICE_STATUS
    /// KEY - INVOICE_STATUS
    /// </summary>
    public class InvoiceStatus : ObjectBase<InvoiceStatus>
    {
        /// <summary>
        /// INVOICE_STATUS.AllowDBNull = false;
        /// INVOICE_STATUS.Unique = true;
        /// INVOICE_STATUS.MaxLength = 4;
        /// </summary>
        private string _INVOICE_STATUS;
        public string INVOICE_STATUS
        {
            get
            {
                return _INVOICE_STATUS;
            }
            set
            {
                _INVOICE_STATUS = value;
                OnPropertyChanged("INVOICE_STATUS");
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

        #region Validation

        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        #endregion


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_STATUS);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceStatus;
            if (t == null)
                return false;
            if (INVOICE_STATUS == t.INVOICE_STATUS )
                return true;
            return false;
        }
        #endregion
    }
}
