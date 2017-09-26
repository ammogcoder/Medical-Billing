using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// EXT_SYS
    /// KEY - EXT_SYS
    /// </summary>
    public class ExtSys : ObjectBase<ExtSys>
    {
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

        private bool _INTF_ROLE;
        public bool INTF_ROLE
        {
            get
            {
                return _INTF_ROLE;
            }
            set
            {
                if (_INTF_ROLE != value)
                {
                    _INTF_ROLE = value;
                    OnPropertyChanged("INTF_ROLE");
                }
            }
        }

        private string _PLATFORM;
        public string PLATFORM
        {
            get
            {
                return _PLATFORM;
            }
            set
            {
                if (_PLATFORM != value)
                {
                    _PLATFORM = value;
                    OnPropertyChanged("PLATFORM");
                }
            }
        }

        private DateTime? _BEGIN_SYNC_ON;
        public DateTime? BEGIN_SYNC_ON
        {
            get
            {
                return _BEGIN_SYNC_ON;
            }
            set
            {
                if (_BEGIN_SYNC_ON != value)
                {
                    _BEGIN_SYNC_ON = value;
                    OnPropertyChanged("BEGIN_SYNC_ON");
                }
            }
        }

        private string _REMOTE_LINK;
        public string REMOTE_LINK
        {
            get
            {
                return _REMOTE_LINK;
            }
            set
            {
                if (_REMOTE_LINK != value)
                {
                    _REMOTE_LINK = value;
                    OnPropertyChanged("REMOTE_LINK");
                }
            }
        }

        private DateTime? _DONT_SYNC_AFTER;
        public DateTime? DONT_SYNC_AFTER
        {
            get
            {
                return _DONT_SYNC_AFTER;
            }
            set
            {
                if (_DONT_SYNC_AFTER != value)
                {
                    _DONT_SYNC_AFTER = value;
                    OnPropertyChanged("DONT_SYNC_AFTER");
                }
            }
        }

        private string _SR_SYS_DESCR;
        public string SR_SYS_DESCR
        {
            get
            {
                return _SR_SYS_DESCR;
            }
            set
            {
                if (_SR_SYS_DESCR != value)
                {
                    _SR_SYS_DESCR = value;
                    OnPropertyChanged("SR_SYS_DESCR");
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
            return 0.CombineHashCode(EXT_SYS);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ExtSys;
            if (t == null)
                return false;
            if (EXT_SYS == t.EXT_SYS)
                return true;
            return false;
        }
        #endregion

    }
}
