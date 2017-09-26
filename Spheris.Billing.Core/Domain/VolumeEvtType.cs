using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// VOLUME_EVT_TYPE
    /// key - VOLUME_EVT_TYPE_ID
    /// </summary>
    public class VolumeEvtType : ObjectBase<VolumeEvtType>
    {
        public VolumeEvtType Clone()
        {
            return BaseClone<VolumeEvtType>();
        }

        /// <summary>
        /// VOLUME_EVT_TYPE_ID 
        /// AllowDBNull = false;
        /// VOLUME_EVT_TYPE_ID.Unique = true;
        /// </summary>
        private decimal _VOLUME_EVT_TYPE_ID;
        public decimal VOLUME_EVT_TYPE_ID
        {
            get
            {
                return _VOLUME_EVT_TYPE_ID;
            }
            set
            {
                _VOLUME_EVT_TYPE_ID = value;
                OnPropertyChanged("VOLUME_EVT_TYPE_ID");
            }
        }

        /// <summary>
        /// DESCR.AllowDBNull = false;
        /// DESCR.MaxLength = 75;
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

        /// <summary>
        /// SCOPE_RULE
        /// </summary>
        private string _SCOPE_RULE;
        public string SCOPE_RULE
        {
            get
            {
                return _SCOPE_RULE;
            }
            set
            {
                _SCOPE_RULE = value;
                OnPropertyChanged("SCOPE_RULE");
            }
        }

        /// <summary>
        /// ADD_ON_CHG_TYPE_ID 
        /// AllowDBNull = false;
        /// </summary>
        private decimal? _ADD_ON_CHG_TYPE_ID;
        public decimal? ADD_ON_CHG_TYPE_ID
        {
            get
            {
                return _ADD_ON_CHG_TYPE_ID;
            }
            set
            {
                _ADD_ON_CHG_TYPE_ID = value;
                OnPropertyChanged("ADD_ON_CHG_TYPE_ID");
            }
        }

        /// <summary>
        /// ADJ_TYPE
        /// AllowDBNull = false;
        /// </summary>
        private string _ADJ_TYPE;
        public string ADJ_TYPE
        {
            get
            {
                return _ADJ_TYPE;
            }
            set
            {
                _ADJ_TYPE = value;
                OnPropertyChanged("ADJ_TYPE");
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
            return 0.CombineHashCode(VOLUME_EVT_TYPE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as VolumeEvtType;
            if (t == null)
                return false;
            if (VOLUME_EVT_TYPE_ID == t.VOLUME_EVT_TYPE_ID)
                return true;
            return false;
        }
        #endregion

    }
}
