using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// CONTRACT_VOLUME_EVT
    /// key - CONTRACT_VOLUME_EVT_ID
    /// </summary>
    public class ContractVolumeEvt : ObjectBase<ContractVolumeEvt>
    {
        public ContractVolumeEvt Clone()
        {
            return BaseClone<ContractVolumeEvt>();
        }

        private decimal _CONTRACT_VOLUME_EVT_ID;
        public decimal CONTRACT_VOLUME_EVT_ID
        {
            get
            {
                return _CONTRACT_VOLUME_EVT_ID;
            }
            set
            {
                _CONTRACT_VOLUME_EVT_ID = value;
                OnPropertyChanged("CONTRACT_VOLUME_EVT_ID");
            }
        }
       
        private decimal _CONTRACT_ID;
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

        private DateTime _STARTS_ON;
        public DateTime STARTS_ON
        {
            get
            {
                return _STARTS_ON;
            }
            set
            {
                _STARTS_ON = value;
                OnPropertyChanged("STARTS_ON");
            }
        }
        private DateTime _ENDS_AFTER;
        public DateTime ENDS_AFTER
        {
            get
            {
                return _ENDS_AFTER;
            }
            set
            {
                _ENDS_AFTER = value;
                OnPropertyChanged("ENDS_AFTER");
            }
        }
     
        private bool _TALLY_STAT;
        public bool TALLY_STAT
        {
            get
            {
                return _TALLY_STAT;
            }
            set
            {
                _TALLY_STAT = value;
                OnPropertyChanged("TALLY_STAT");
            }
        }

        public VolumeEvtType _volumeEvtType;
        public VolumeEvtType volumeEvtType
        {
            get
            {
                return _volumeEvtType;
            }
            set
            {
                _volumeEvtType = value;
                OnPropertyChanged("volumeEvtType");
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
            return 0.CombineHashCode(CONTRACT_VOLUME_EVT_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ContractVolumeEvt;
            if (t == null)
                return false;
            if (CONTRACT_VOLUME_EVT_ID == t.CONTRACT_VOLUME_EVT_ID
                )
                return true;
            return false;
        }
        #endregion

    }
}
