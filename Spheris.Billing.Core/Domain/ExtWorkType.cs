using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// EXT_WORK_TYPE
    /// KEY - EXT_SYS, EXT_CLIENT_KEY, EXT_WORK_TYPE
    /// </summary>
    public class ExtWorkType : ObjectBase<Brand>
    {
        public bool IsAdding { get; set; }
        
        public ExtWorkType Clone()
        {
            return BaseClone<ExtWorkType>();
        }
        

        /// <summary>
        /// KEY
        /// </summary>
        private string _EXT_SYS;
        public string EXT_SYS
        {
            get
            {
                return _EXT_SYS;
            }
            set
            {
                if (_EXT_SYS != value)
                {
                    _EXT_SYS = value;
                    OnPropertyChanged("EXT_SYS");
                }

            }
        }
        
        /// <summary>
        /// KEY
        /// </summary>
        private string _EXT_CLIENT_KEY;
        public string EXT_CLIENT_KEY
        {
            get
            {
                return _EXT_CLIENT_KEY;
            }
            set
            {
                if (_EXT_CLIENT_KEY != value)
                {
                    _EXT_CLIENT_KEY = value;
                    OnPropertyChanged("EXT_CLIENT_KEY");
                }
            }
        }

        /// <summary>
        /// KEY
        /// </summary>
        private string _EXT_WORK_TYPE;
        public string EXT_WORK_TYPE
        {
            get
            {
                return _EXT_WORK_TYPE;
            }
            set
            {
                if (_EXT_WORK_TYPE != value)
                {
                    _EXT_WORK_TYPE = value;
                    OnPropertyChanged("EXT_WORK_TYPE");
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
                if(_DESCR != value)
                {
                    _DESCR = value;
                    OnPropertyChanged("DESCR");
                }
            }
        }

        private decimal? _TAT_THRESHOLD;
        public decimal? TAT_THRESHOLD
        {
            get
            {
                return _TAT_THRESHOLD;
            }
            set
            {
                if (_TAT_THRESHOLD != value)
                {
                    _TAT_THRESHOLD = value;
                    OnPropertyChanged("TAT_THRESHOLD");
                }
            }
        }

        private string _STD_WORK_TYPE;
        public string STD_WORK_TYPE
        {
            get
            {
                return _STD_WORK_TYPE;
            }
            set
            {
                if (_STD_WORK_TYPE != value)
                {
                    _STD_WORK_TYPE = value;
                    OnPropertyChanged("STD_WORK_TYPE");
                }
            }
        }

        private decimal? _INVOICE_GRP_ID;
        public decimal? INVOICE_GRP_ID
        {
            get
            {
                return _INVOICE_GRP_ID;
            }
            set
            {
                if (_INVOICE_GRP_ID != value)
                {
                    _INVOICE_GRP_ID = value;
                    OnPropertyChanged("INVOICE_GRP_ID");
                }
            }
        }

        private decimal? _STAT_TAT_THRESHOLD;
        public decimal? STAT_TAT_THRESHOLD
        {
            get
            {
                return _STAT_TAT_THRESHOLD;
            }
            set
            {
                if (_STAT_TAT_THRESHOLD != value)
                {
                    _STAT_TAT_THRESHOLD = value;
                    OnPropertyChanged("STAT_TAT_THRESHOLD");
                }
            }
        }

        private decimal? _RATE_NBR;
        public decimal? RATE_NBR
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

        private bool _TALLY_FOR_VOLUME_EVT;
        public bool TALLY_FOR_VOLUME_EVT
        {
            get
            {
                return _TALLY_FOR_VOLUME_EVT;
            }
            set
            {
                if (_TALLY_FOR_VOLUME_EVT != value)
                {
                    _TALLY_FOR_VOLUME_EVT = value;
                    OnPropertyChanged("TALLY_FOR_VOLUME_EVT");
                }
            }
        }

        private string _PLATFORM_WT_ID;
        public string PLATFORM_WT_ID
        {
            get
            {
                return _PLATFORM_WT_ID;
            }
            set
            {
                if (_PLATFORM_WT_ID != value)
                {
                    _PLATFORM_WT_ID = value;
                    OnPropertyChanged("PLATFORM_WT_ID");
                }
            }
        }


        public override string GetValidationError(string propertyName)
        {
            switch (propertyName)
            {
                case "EXT_SYS":
                    return (HasValue(EXT_SYS)) ? null : "System must be populated";
                case "EXT_CLIENT_KEY":
                    return (HasValue(EXT_CLIENT_KEY)) ? null : "Client Key must be populated";
                case "EXT_WORK_TYPE":
                    return (HasValue(EXT_WORK_TYPE)) ? null : "Work Type must be populated";
                default:
                    break;
            }
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(EXT_SYS)
                .CombineHashCode(EXT_CLIENT_KEY)
                .CombineHashCode(EXT_WORK_TYPE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ExtWorkType;
            if (t == null)
                return false;
            if (EXT_SYS == t.EXT_SYS && EXT_CLIENT_KEY == t.EXT_CLIENT_KEY && EXT_WORK_TYPE == t.EXT_WORK_TYPE)
                return true;
            return false;
        }
        #endregion

    }
}
