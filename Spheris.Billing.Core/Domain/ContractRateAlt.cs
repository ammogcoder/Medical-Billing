using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// CONTRACT_RATE_ALT
    /// KEY - RATE_NBR,CONTRACT_RATE_ID
    /// </summary>
    public class ContractRateAlt : ObjectBase<ContractRateAlt>
    {
        
        public ContractRateAlt Clone()
        {
            return BaseClone<ContractRateAlt>();
        }
        

        private int _CONTRACT_RATE_ID;
        public int CONTRACT_RATE_ID
        {
            get
            {
                return _CONTRACT_RATE_ID;
            }
            set
            {
                if (_CONTRACT_RATE_ID != value)
                {
                    _CONTRACT_RATE_ID = value;
                    OnPropertyChanged("CONTRACT_RATE_ID");
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

        private decimal? _INHOUSE_SR_RATE = 0;
        public decimal? INHOUSE_SR_RATE
        {
            get
            {
                return _INHOUSE_SR_RATE;
            }
            set
            {
                if (_INHOUSE_SR_RATE != value)
                {
                    _INHOUSE_SR_RATE = value;
                    OnPropertyChanged("INHOUSE_SR_RATE");
                }
            }
        }

        private decimal? _INHOUSE_SR_RATE_DELTA = 0;
        public decimal? INHOUSE_SR_RATE_DELTA
        {
            get
            {
                return _INHOUSE_SR_RATE_DELTA;
            }
            set
            {
                if (_INHOUSE_SR_RATE_DELTA != value)
                {
                    _INHOUSE_SR_RATE_DELTA = value;
                    OnPropertyChanged("INHOUSE_SR_RATE_DELTA");
                }
            }
        }

        private decimal? _INHOUSE_TR_RATE = 0;
        public decimal? INHOUSE_TR_RATE
        {
            get
            {
                return _INHOUSE_TR_RATE;
            }
            set
            {
                if (_INHOUSE_TR_RATE != value)
                {
                    _INHOUSE_TR_RATE = value;
                    OnPropertyChanged("INHOUSE_TR_RATE");
                }
            }
        }

        private decimal? _INHOUSE_TR_RATE_DELTA = 0;
        public decimal? INHOUSE_TR_RATE_DELTA
        {
            get
            {
                return _INHOUSE_TR_RATE_DELTA;
            }
            set
            {
                if (_INHOUSE_TR_RATE_DELTA != value)
                {
                    _INHOUSE_TR_RATE_DELTA = value;
                    OnPropertyChanged("INHOUSE_TR_RATE_DELTA");
                }
            }
        }

        private decimal? _OFFSHORE_RATE = 0;
        public decimal? OFFSHORE_RATE
        {
            get
            {
                return _OFFSHORE_RATE;
            }
            set
            {
                if (_OFFSHORE_RATE != value)
                {
                    _OFFSHORE_RATE = value;
                    OnPropertyChanged("OFFSHORE_RATE");
                }
            }
        }

        private decimal? _OFFSHORE_RATE_DELTA = 0;
        public decimal? OFFSHORE_RATE_DELTA
        {
            get
            {
                return _OFFSHORE_RATE_DELTA;
            }
            set
            {
                if (_OFFSHORE_RATE_DELTA != value)
                {
                    _OFFSHORE_RATE_DELTA = value;
                    OnPropertyChanged("OFFSHORE_RATE_DELTA");
                }
            }
        }

        private decimal _RATE_NBR = 0;
        public decimal RATE_NBR
        {
            get
            {
                return _RATE_NBR;
            }
            set
            {
                if (_RATE_NBR != value)
                {
                    _RATE_NBR = value;
                    OnPropertyChanged("RATE_NBR");
                }
            }
        }


        private decimal? _SR_OFFSHORE_RATE = 0;
        public decimal? SR_OFFSHORE_RATE
        {
            get
            {
                return _SR_OFFSHORE_RATE;
            }
            set
            {
                if (_SR_OFFSHORE_RATE != value)
                {
                    _SR_OFFSHORE_RATE = value;
                    OnPropertyChanged("SR_OFFSHORE_RATE");
                }
            }
        }

        private decimal? _SR_OFFSHORE_RATE_DELTA = 0;
        public decimal? SR_OFFSHORE_RATE_DELTA
        {
            get
            {
                return _SR_OFFSHORE_RATE_DELTA;
            }
            set
            {
                if (_SR_OFFSHORE_RATE_DELTA != value)
                {
                    _SR_OFFSHORE_RATE_DELTA = value;
                    OnPropertyChanged("SR_OFFSHORE_RATE_DELTA");
                }
            }
        }

        private decimal? _SR_RATE = 0;
        public decimal? SR_RATE
        {
            get
            {
                return _SR_RATE;
            }
            set
            {
                if (_SR_RATE != value)
                {
                    _SR_RATE = value;
                    OnPropertyChanged("SR_RATE");
                }
            }
        }

        private decimal? _SR_RATE_DELTA = 0;
        public decimal? SR_RATE_DELTA
        {
            get
            {
                return _SR_RATE_DELTA;
            }
            set
            {
                if (_SR_RATE_DELTA != value)
                {
                    _SR_RATE_DELTA = value;
                    OnPropertyChanged("SR_RATE_DELTA");
                }
            }
        }

        private decimal? _STD_RATE = 0;
        public decimal? STD_RATE
        {
            get
            {
                return _STD_RATE;
            }
            set
            {
                if (_STD_RATE != value)
                {
                    _STD_RATE = value;
                    OnPropertyChanged("STD_RATE");
                }
            }
        }

        private decimal? _STD_RATE_DELTA = 0;
        public decimal? STD_RATE_DELTA
        {
            get
            {
                return _STD_RATE_DELTA;
            }
            set
            {
                if (_STD_RATE_DELTA != value)
                {
                    _STD_RATE_DELTA = value;
                    OnPropertyChanged("STD_RATE_DELTA");
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
            return 0.CombineHashCode(RATE_NBR).CombineHashCode(CONTRACT_RATE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContractRateAlt;
            if (t == null)
                return false;
            if (RATE_NBR == t.RATE_NBR && CONTRACT_RATE_ID == t.CONTRACT_RATE_ID)
                return true;
            return false;
        }
        #endregion

    }
}
