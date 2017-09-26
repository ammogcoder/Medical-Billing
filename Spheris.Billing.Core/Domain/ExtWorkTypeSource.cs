using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// EXT_WORK_TYPE_SOURCE
    /// key - PLATFORM,DESCR
    /// </summary>
    public class ExtWorkTypeSource : ObjectBase<ExtWorkTypeSource>
    {
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



        public override string GetValidationError(string propertyName)
        {
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(DESCR)
                .CombineHashCode(PLATFORM);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ExtWorkTypeSource;
            if (t == null)
                return false;
            if (DESCR == t.DESCR && PLATFORM == t.PLATFORM)
                return true;
            return false;
        }
        #endregion

    }
}
