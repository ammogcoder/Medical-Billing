using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// INVOICE_GRP_STATUS
    /// KEY - INVOICE_GRP_STATUS
    /// </summary>
    public class InvoiceGrpStatus : ObjectBase<InvoiceGrpStatus>
    {
        /// <summary>
        /// INVOICE_GRP_STATUS.AllowDBNull = false;
        /// INVOICE_GRP_STATUS.Unique = true;
        /// INVOICE_GRP_STATUS.MaxLength = 4;
        /// </summary>
        private string _INVOICE_GRP_STATUS;
        public string INVOICE_GRP_STATUS
        {
            get
            {
                return _INVOICE_GRP_STATUS;
            }
            set
            {
                _INVOICE_GRP_STATUS = value;
                OnPropertyChanged("INVOICE_GRP_STATUS");
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

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public string HasError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string str = GetValidationError(property);
                    if (str != null)
                        return str;
                }
                return null;
            }
        }


        public override string GetValidationError(string propertyName)
        {
            if (!ValidatedProperties.Contains(propertyName))
                return null;
            string error = null;
            switch (propertyName)
            {
                case "INVOICE_GRP_STATUS":
                    error = (!HasValue(this.INVOICE_GRP_STATUS as object)) ? "Invoice group status has no value" : null;
                    break;
                case "DESCR":
                    error = (!HasValue(this.DESCR as object)) ? "Description has no value" : null;
                    break;
                default:
                    break;
            }
            return error;
        }

        #endregion

        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_GRP_STATUS);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceGrpStatus;
            if (t == null)
                return false;
            if (INVOICE_GRP_STATUS == t.INVOICE_GRP_STATUS)
                return true;
            return false;
        }
        #endregion

    }
}
