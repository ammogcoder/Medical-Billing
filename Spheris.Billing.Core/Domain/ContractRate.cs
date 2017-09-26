using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// CONTRACT_RATE
    /// KEY - CONTRACT_RATE_ID
    /// </summary>
    public class ContractRate : ObjectBase<ContractRate>
    {
 
        
        public ContractRate Clone()
        {
            return BaseClone<ContractRate>();
        }
        
 
        public ContractRate()
        { 
        }
        /// <summary>
        /// BEGIN_ON.AllowDBNull = false;
        /// </summary>
        public DateTime BEGIN_ON
        {
            get
            {
                return _BEGIN_ON;
            }
            set
            {
                _BEGIN_ON = value;
                OnPropertyChanged("BEGIN_ON");
            }
        }

        private DateTime _BEGIN_ON = DateTime.Now;

        /// <summary>
        /// CHAR_COMP_METHOD.AllowDBNull = false;
        /// </summary>
        public string CHAR_COMP_METHOD
        {
            get
            {
                return _CHAR_COMP_METHOD;
            }
            set
            {
                _CHAR_COMP_METHOD = value;
                OnPropertyChanged("CHAR_COMP_METHOD");
            }
        }
        private string _CHAR_COMP_METHOD = "90SU";

        /// <summary>
        /// STD_RATE.AllowDBNull = false;
        /// </summary>
        public decimal STD_RATE
        {
            get
            {
                return _STD_RATE;
            }
            set
            {
                _STD_RATE = value;
                OnPropertyChanged("STD_RATE");
            }
        }
        private decimal _STD_RATE = 0;


        /// <summary>
        /// SYLCOUNT.AllowDBNull = false;
        /// SYLCOUNT.Unique = true;
        /// </summary>
        public decimal SYLCOUNT
        {
            get
            {
                return _SYLCOUNT;
            }
            set
            {
                _SYLCOUNT = value;
                OnPropertyChanged("SYLCOUNT");
            }
        }
        private decimal _SYLCOUNT = 1;

        /// <summary>
        /// CONTRACT_ID.AllowDBNull = false;
        /// CONTRACT_ID.Unique = true;
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
        /// CONTRACT_RATE_ID.AllowDBNull = false;
        /// CONTRACT_RATE_ID.Unique = true;
        /// </summary>
        public int CONTRACT_RATE_ID
        {
            get
            {
                return _CONTRACT_RATE_ID;
            }
            set
            {
                _CONTRACT_RATE_ID = value;
                OnPropertyChanged("CONTRACT_RATE_ID");
            }
        }
        private int _CONTRACT_RATE_ID;


        /// <summary>
        /// DICTATOR_SR_THRESHOLD.AllowDBNull = true;
        /// </summary>
        public decimal? DICTATOR_SR_THRESHOLD
        {
            get
            {
                return _DICTATOR_SR_THRESHOLD;
            }
            set
            {
                _DICTATOR_SR_THRESHOLD = value;
                OnPropertyChanged("DICTATOR_SR_THRESHOLD");
            }
        }
        private decimal? _DICTATOR_SR_THRESHOLD = 0;


        /// <summary>
        /// DOCUMENT_SR_THRESHOLD.AllowDBNull = true;
        /// </summary>
        public decimal? DOCUMENT_SR_THRESHOLD
        {
            get
            {
                return _DOCUMENT_SR_THRESHOLD;
            }
            set
            {
                _DOCUMENT_SR_THRESHOLD = value;
                OnPropertyChanged("DOCUMENT_SR_THRESHOLD");
            }
        }
        private decimal? _DOCUMENT_SR_THRESHOLD = 0;


        /// <summary>
        /// END_AFTER.AllowDBNull = false;
        /// </summary>
        public DateTime END_AFTER
        {
            get
            {
                return _END_AFTER;
            }
            set
            {
                _END_AFTER = value;
                OnPropertyChanged("END_AFTER");
            }
        }
        private DateTime _END_AFTER = DateTime.Now;


        /// <summary>
        /// FAX_COMP_METHOD.AllowDBNull = false;
        /// FAX_COMP_METHOD.Unique = false;
        /// FAX_COMP_METHOD.MaxLength = 4;
        /// </summary>
        public string FAX_COMP_METHOD
        {
            get
            {
                return _FAX_COMP_METHOD;
            }
            set
            {
                _FAX_COMP_METHOD = value;
                OnPropertyChanged("FAX_COMP_METHOD");
            }
        }
        private string _FAX_COMP_METHOD = "NONE";


        /// <summary>
        /// FAX_RATE.AllowDBNull = false;
        /// </summary>
        public decimal FAX_RATE
        {
            get
            {
                return _FAX_RATE;
            }
            set
            {
                _FAX_RATE = value;
                OnPropertyChanged("FAX_RATE");
            }
        }
        private decimal _FAX_RATE;


        /// <summary>
        /// FRONTEND_SR_RATE.AllowDBNull = true;
        /// </summary>
        public decimal? FRONTEND_SR_RATE
        {
            get
            {
                return _FRONTEND_SR_RATE;
            }
            set
            {
                _FRONTEND_SR_RATE = value;
                OnPropertyChanged("FRONTEND_SR_RATE");
            }
        }
        private decimal? _FRONTEND_SR_RATE = 0;


        /// <summary>
        /// HOLIDAY_RATE_DELTA.AllowDBNull = false;
        /// </summary>
        public decimal HOLIDAY_RATE_DELTA
        {
            get
            {
                return _HOLIDAY_RATE_DELTA;
            }
            set
            {
                _HOLIDAY_RATE_DELTA = value;
                OnPropertyChanged("HOLIDAY_RATE_DELTA");
            }
        }
        private decimal _HOLIDAY_RATE_DELTA = 0;


        /// <summary>
        /// INCLUDE_INHOUSE_IN_TAT_CALC.AllowDBNull = false;
        /// </summary>
        public bool INCLUDE_INHOUSE_IN_TAT_CALC
        {
            get
            {
                return _INCLUDE_INHOUSE_IN_TAT_CALC;
            }
            set
            {
                if (_INCLUDE_INHOUSE_IN_TAT_CALC != value)
                {
#if DEBUG
                    if (_INCLUDE_INHOUSE_IN_TAT_CALC == true)
                        _INCLUDE_INHOUSE_IN_TAT_CALC = true;//breakpoint
#endif
                    _INCLUDE_INHOUSE_IN_TAT_CALC = value;
                    OnPropertyChanged("INCLUDE_INHOUSE_IN_TAT_CALC");
                }
            }
        }
        private bool _INCLUDE_INHOUSE_IN_TAT_CALC = false;


        /// <summary>
        /// INHOUSE_SR_RATE.AllowDBNull = false;
        /// </summary>
        public decimal INHOUSE_SR_RATE
        {
            get
            {
                return _INHOUSE_SR_RATE;
            }
            set
            {
                _INHOUSE_SR_RATE = value;
                OnPropertyChanged("INHOUSE_SR_RATE");
            }
        }
        private decimal _INHOUSE_SR_RATE = 0;


        /// <summary>
        /// INHOUSE_TR_RATE.AllowDBNull = false;
        /// </summary>
        public decimal INHOUSE_TR_RATE
        {
            get
            {
                return _INHOUSE_TR_RATE;
            }
            set
            {
                _INHOUSE_TR_RATE = value;
                OnPropertyChanged("INHOUSE_TR_RATE");
            }
        }
        private decimal _INHOUSE_TR_RATE = 0;


        /// <summary>
        /// OFFSHORE_RATE.AllowDBNull = true;
        /// </summary>
        public decimal? OFFSHORE_RATE
        {
            get
            {
                return _OFFSHORE_RATE;
            }
            set
            {
                _OFFSHORE_RATE = value;
                OnPropertyChanged("OFFSHORE_RATE");
            }
        }
        private decimal? _OFFSHORE_RATE = 0;


        /// <summary>
        /// SR_OFFSHORE_RATE.AllowDBNull = false;
        /// </summary>
        public decimal SR_OFFSHORE_RATE
        {
            get
            {
                return _SR_OFFSHORE_RATE;
            }
            set
            {
                _SR_OFFSHORE_RATE = value;
                OnPropertyChanged("SR_OFFSHORE_RATE");
            }
        }
        private decimal _SR_OFFSHORE_RATE = 0;


        /// <summary>
        /// SR_RATE.AllowDBNull = false;
        /// </summary>
        public decimal SR_RATE
        {
            get
            {
                return _SR_RATE;
            }
            set
            {
                _SR_RATE = value;
                OnPropertyChanged("SR_RATE");
            }
        }
        private decimal _SR_RATE = 0;


        /// <summary>
        /// STAT_COMP_METHOD.AllowDBNull = false;
        /// STAT_COMP_METHOD.MaxLength = 4;
        /// </summary>
        public string STAT_COMP_METHOD
        {
            get
            {
                return _STAT_COMP_METHOD;
            }
            set
            {
                _STAT_COMP_METHOD = value;
                OnPropertyChanged("STAT_COMP_METHOD");
            }
        }
        private string _STAT_COMP_METHOD = "FPPT";


        /// <summary>
        /// STAT_PREMIUM.AllowDBNull = false;
        /// </summary>
        public decimal STAT_PREMIUM
        {
            get
            {
                return _STAT_PREMIUM;
            }
            set
            {
                _STAT_PREMIUM = value;
                OnPropertyChanged("STAT_PREMIUM");
            }
        }
        private decimal _STAT_PREMIUM = 0;


        /// <summary>
        /// STAT_THRESHOLD.AllowDBNull = false;
        /// </summary>
        public decimal STAT_THRESHOLD
        {
            get
            {
                return _STAT_THRESHOLD;
            }
            set
            {
                _STAT_THRESHOLD = value;
                OnPropertyChanged("STAT_THRESHOLD");
            }
        }
        private decimal _STAT_THRESHOLD = 1;


        /// <summary>
        /// TECHNOLOGY_CHG.AllowDBNull = false;
        /// </summary>
        public decimal TECHNOLOGY_CHG
        {
            get
            {
                return _TECHNOLOGY_CHG;
            }
            set
            {
                _TECHNOLOGY_CHG = value;
                OnPropertyChanged("TECHNOLOGY_CHG");
            }
        }
        private decimal _TECHNOLOGY_CHG = 0;

        /// <summary>
        /// EER_SR_THRESHOLD.AllowDBNull = true;
        /// </summary>
        public decimal? EER_SR_THRESHOLD
        {
            get
            {
                return _EER_SR_THRESHOLD;
            }
            set
            {
                _EER_SR_THRESHOLD = value;
                OnPropertyChanged("EER_SR_THRESHOLD");
            }
        }
        private decimal? _EER_SR_THRESHOLD = 0;
        

        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(CONTRACT_RATE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContractRate;
            if (t == null)
                return false;
            if (CONTRACT_RATE_ID == t.CONTRACT_RATE_ID )
                return true;
            return false;
        }
        #endregion

    }
}
