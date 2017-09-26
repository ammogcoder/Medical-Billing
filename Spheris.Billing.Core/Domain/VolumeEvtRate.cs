using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// VOLUME_EVT_RATE
    /// key - TIER,CONTRACT_VOLUME_EVT_ID
    /// </summary>
    public class VolumeEvtRate : ObjectBase<VolumeEvtRate>
    {
        public VolumeEvtRate Clone()
        {
            return BaseClone<VolumeEvtRate>();
        }

        /// <summary>
        /// TIER
        /// </summary>
        private decimal _TIER;
        public decimal TIER
        {
            get
            {
                return _TIER;
            }
            set
            {
                _TIER = value;
                OnPropertyChanged("TIER");
            }
        }

        /// <summary>
        /// ADJ
        /// </summary>
        private decimal _ADJ;
        public decimal ADJ
        {
            get
            {
                return _ADJ;
            }
            set
            {
                _ADJ = value;
                OnPropertyChanged("ADJ");

            }
        }

        /// <summary>
        /// CONTRACT_VOLUME_EVT_ID
        /// </summary>
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

        /// <summary>
        /// ADJ_OFFSHORE
        /// </summary>
        private decimal _ADJ_OFFSHORE;
        public decimal ADJ_OFFSHORE
        {
            get
            {
                return _ADJ_OFFSHORE;
            }
            set
            {
                _ADJ_OFFSHORE = value;
                OnPropertyChanged("ADJ_OFFSHORE");
            }
        }

        /// <summary>
        /// ADJ_SR
        /// </summary>
        private decimal _ADJ_SR;
        public decimal ADJ_SR
        {
            get
            {
                return _ADJ_SR;
            }
            set
            {
                _ADJ_SR = value;
                OnPropertyChanged("ADJ_SR");
            }
        }

        /// <summary>
        /// ADJ_SR_OFFSHORE
        /// </summary>
        private decimal _ADJ_SR_OFFSHORE;
        public decimal ADJ_SR_OFFSHORE
        {
            get
            {
                return _ADJ_SR_OFFSHORE;
            }
            set
            {
                _ADJ_SR_OFFSHORE = value;
                OnPropertyChanged("ADJ_SR_OFFSHORE");
            }
        }

        /// <summary>
        /// ADJ_INHOUSE
        /// </summary>
        private decimal _ADJ_INHOUSE;
        public decimal ADJ_INHOUSE
        {
            get
            {
                return _ADJ_INHOUSE;
            }
            set
            {
                _ADJ_INHOUSE = value;
                OnPropertyChanged("ADJ_INHOUSE");
            }
        }

        /// <summary>
        /// ADJ_SR_INHOUSE
        /// </summary>
        private decimal _ADJ_SR_INHOUSE;
        public decimal ADJ_SR_INHOUSE
        {
            get
            {
                return _ADJ_SR_INHOUSE;
            }
            set
            {
                _ADJ_SR_INHOUSE = value;
                OnPropertyChanged("ADJ_SR_INHOUSE");
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
            return 0.CombineHashCode(TIER)
                .CombineHashCode(CONTRACT_VOLUME_EVT_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as VolumeEvtRate;
            if (t == null)
                return false;
            if (TIER == t.TIER
                && CONTRACT_VOLUME_EVT_ID == t.CONTRACT_VOLUME_EVT_ID)
                return true;
            return false;
        }
        #endregion

    }
}
