using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// CONTRACT
    /// KEY - CONTRACT_ID
    /// </summary>
    public class Contract : ObjectBase<Contract>
    {
        public Contract() { }
        public Contract(Contract copy)
        {
            this._CHANGED_BY = copy._CHANGED_BY;
            this._CHANGED_ON = copy._CHANGED_ON;
            this._CONTRACT_ID = copy._CONTRACT_ID;
            this._DESCR = copy._DESCR;
            this._DISPLAY_STD_RATE_ON_INVOICE = copy._DISPLAY_STD_RATE_ON_INVOICE;
            this._PAYMENT_GRACE_PERIOD = copy._PAYMENT_GRACE_PERIOD;
            this._PAYMENT_TERMS = copy._PAYMENT_TERMS;
        }

        
        public Contract Clone()
        {
            return BaseClone<Contract>();
        }

        // used to spoof an entry for displaying text in the output window
        public bool IsSpoof { get; set; }

        /// <summary>
        /// CONTRACT_ID.AllowDBNull = false;
        /// CONTRACT_ID.Unique = true;
        /// KEY
        /// </summary>
        public decimal CONTRACT_ID
        {
            get
            {
                return _CONTRACT_ID;
            }
            set
            {
                _CONTRACT_ID = value;
                OnPropertyChanged("CONTRACT_ID");
            }
        }
        private decimal _CONTRACT_ID;


        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 150;
        /// </summary>
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
        private string _DESCR = "Please enter a Contract Name";


        /// <summary>
        /// CHANGED_ON.AllowDBNull = false;
        /// </summary>
        public System.DateTime CHANGED_ON
        {
            get
            {
                return _CHANGED_ON;
            }
            set
            {
                _CHANGED_ON = value;
                OnPropertyChanged("CHANGED_ON");
            }
        }
        private System.DateTime _CHANGED_ON;


        /// <summary>
        /// CHANGED_BY.AllowDBNull = false;
        /// CHANGED_BY.MaxLength = 255;
        /// </summary>
        public string CHANGED_BY
        {
            get
            {
                return _CHANGED_BY;
            }
            set
            {
                _CHANGED_BY = value;
                OnPropertyChanged("CHANGED_BY");
            }
        }
        private string _CHANGED_BY;


        /// <summary>
        /// PAYMENT_TERMS.AllowDBNull = false;
        /// PAYMENT_TERMS.MaxLength = 4;
        /// </summary>
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
        private string _PAYMENT_TERMS = "30DY";


        /// <summary>
        /// PAYMENT_GRACE_PERIOD.AllowDBNull = false;
        /// </summary>
        public decimal PAYMENT_GRACE_PERIOD
        {
            get
            {
                return _PAYMENT_GRACE_PERIOD;
            }
            set
            {
                _PAYMENT_GRACE_PERIOD = value;
                OnPropertyChanged("PAYMENT_GRACE_PERIOD");
            }
        }
        private decimal _PAYMENT_GRACE_PERIOD;


        /// <summary>
        /// DISPLAY_STD_RATE_ON_INVOICE.MaxLength = 1;
        /// AllowNullable is true
        /// HOWEVER, it defaults to 'N' when null
        /// </summary>
        public bool/*?*/ DISPLAY_STD_RATE_ON_INVOICE
        {
            get
            {
                return _DISPLAY_STD_RATE_ON_INVOICE;
            }
            set
            {
                _DISPLAY_STD_RATE_ON_INVOICE = value;
                OnPropertyChanged("DISPLAY_STD_RATE_ON_INVOICE");
            }
        }
        private bool/*?*/ _DISPLAY_STD_RATE_ON_INVOICE;


        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(CONTRACT_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Contract;
            if (t == null)
                return false;
            if (CONTRACT_ID == t.CONTRACT_ID )
                return true;
            return false;
        }
        #endregion

    }
}
