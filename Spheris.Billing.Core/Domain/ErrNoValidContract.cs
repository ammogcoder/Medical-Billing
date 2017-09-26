using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// PROCEDURE SPHRSBILLING.ODP_ERR.S_CONTRACT_ERRORS
    /// </summary>
    public class ErrNoValidContract : ObjectBase<ErrNoValidContract>
    {

        private decimal _ID;
        public decimal ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        /// <summary>
        /// key
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
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _Status;
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private string _PrimaryPlatform;
        public string PrimaryPlatform
        {
            get
            {
                return _PrimaryPlatform;
            }
            set
            {
                if (_PrimaryPlatform != value)
                {
                    _PrimaryPlatform = value;
                    OnPropertyChanged("PrimaryPlatform");
                }
            }
        }

        private string _Frequency;
        public string Frequency
        {
            get
            {
                return _Frequency;
            }
            set
            {
                if (_Frequency != value)
                {
                    _Frequency = value;
                    OnPropertyChanged("Frequency");
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
            return 0.CombineHashCode(this.ID)
                .CombineHashCode(this.Description)
                .CombineHashCode(this.Frequency)
                .CombineHashCode(this.PrimaryPlatform)
                .CombineHashCode(this.Status) ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ErrNoValidContract;
            if (t == null)
                return false;
            if (ID == t.ID 
                && Description == t.Description
                && Frequency == t.Frequency
                && PrimaryPlatform == t.PrimaryPlatform
                && Status == t.Status)
                return true;
            return false;
        }
        #endregion
    }
    
}
