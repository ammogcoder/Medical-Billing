using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// PAYMENT_TERMS
    /// KEY - PAYMENT_TERMS
    /// </summary>
    public class PaymentTerm : ObjectBase<PaymentTerm>
    {
        /// <summary>
        /// PAYMENT_TERMS.AllowDBNull = false;
        /// PAYMENT_TERMS.Unique = true;
        /// PAYMENT_TERMS.MaxLength = 4;
        /// </summary>
        private string _PAYMENT_TERMS;
        public string PAYMENT_TERMS
        {
            get
            {
                return _PAYMENT_TERMS;
            }
            set
            {
                _PAYMENT_TERMS = value;
                OnPropertyChanged("PAYMENT_TERMS");
            }
        }

        /// <summary>
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

        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(PAYMENT_TERMS);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as PaymentTerm;
            if (t == null)
                return false;
            if (PAYMENT_TERMS == t.PAYMENT_TERMS)
                return true;
            return false;
        }
        #endregion
    }
}
