using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// INVOICE_GRP_OVERRIDE
    /// KEY - DEFAULT_INVOICE_GRP_ID,OVERRIDE_KEY
    /// </summary>
    public class InvoiceGrpOverride : ObjectBase<InvoiceGrpOverride>
    {
        /// <summary>
        /// DEFAULT_INVOICE_GRP_ID.AllowDBNull = false;
        /// </summary>
        private decimal _DEFAULT_INVOICE_GRP_ID;
        public decimal DEFAULT_INVOICE_GRP_ID
        {
            get
            {
                return _DEFAULT_INVOICE_GRP_ID;
            }
            set
            {
                _DEFAULT_INVOICE_GRP_ID = value;
                OnPropertyChanged("DEFAULT_INVOICE_GRP_ID");
            }
        }


        /// <summary>
        ///  OVERRIDE_KEY.AllowDBNull = false;
        ///  OVERRIDE_KEY.MaxLength = 50;
        /// </summary>
        private string _OVERRIDE_KEY;
        public string OVERRIDE_KEY
        {
            get
            {
                return _OVERRIDE_KEY;
            }
            set
            {
                _OVERRIDE_KEY = value;
                OnPropertyChanged("OVERRIDE_KEY");
            }
        }


        /// <summary>
        /// SEND_TO_INVOICE_GRP_ID.AllowDBNull = false;
        /// </summary>
        private decimal _SEND_TO_INVOICE_GRP_ID;
        public decimal SEND_TO_INVOICE_GRP_ID
        {
            get
            {
                return _SEND_TO_INVOICE_GRP_ID;
            }
            set
            {
                _SEND_TO_INVOICE_GRP_ID = value;
                OnPropertyChanged("SEND_TO_INVOICE_GRP_ID");
            }
        }

        /// <summary>
        /// Not used in the database
        /// </summary>
        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
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
            return 0.CombineHashCode(OVERRIDE_KEY)
                .CombineHashCode(DEFAULT_INVOICE_GRP_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceGrpOverride;
            if (t == null)
                return false;
            if (OVERRIDE_KEY == t.OVERRIDE_KEY && DEFAULT_INVOICE_GRP_ID == t.DEFAULT_INVOICE_GRP_ID)
                return true;
            return false;
        }
        #endregion

    }
}
