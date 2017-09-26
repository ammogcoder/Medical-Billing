using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// PROCEDURE SPHRSBILLING.ODP_ERR.S_CONTRACT_ERRORS
    /// </summary>
    public class ErrClientWorkType : ObjectBase<ErrClientWorkType>
    {
        /// <summary>
        /// EXT_CLIENT_KEY
        /// </summary>
        private string _ID;
        public string ID
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

        private string _WorkType;
        public string WorkType
        {
            get
            {
                return _WorkType;
            }
            set
            {
                if (_WorkType != value)
                {
                    _WorkType = value;
                    OnPropertyChanged("WorkType");
                }
            }
        }

        private string _System;
        public string System
        {
            get
            {
                return _System;
            }
            set
            {
                if (_System != value)
                {
                    _System = value;
                    OnPropertyChanged("System");
                }
            }
        }

        private string _ErrorDescription;
        public string ErrorDescription
        {
            get
            {
                return _ErrorDescription;
            }
            set
            {
                if (_ErrorDescription != value)
                {
                    _ErrorDescription = value;
                    OnPropertyChanged("ErrorDescription");
                }
            }
        }

        private decimal? _TransactionsAffected;
        public decimal? TransactionsAffected
        {
            get
            {
                return _TransactionsAffected;
            }
            set
            {
                if (_TransactionsAffected != value)
                {
                    _TransactionsAffected = value;
                    OnPropertyChanged("TransactionsAffected");
                }
            }
        }

        private DateTime _AsOf;
        public DateTime AsOf
        {
            get
            {
                return _AsOf;
            }
            set
            {
                if (_AsOf != value)
                {
                    _AsOf = value;
                    OnPropertyChanged("AsOf");
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
            return 0.CombineHashCode(this._ID)
                .CombineHashCode(this._Description)
                .CombineHashCode(this._ErrorDescription)
                .CombineHashCode(this._AsOf)
                .CombineHashCode(this._System) 
                .CombineHashCode(this._TransactionsAffected) 
                .CombineHashCode(this._WorkType) ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ErrClientWorkType;
            if (t == null)
                return false;
            if (ID == t.ID 
                && Description == t.Description
                && ErrorDescription == t.ErrorDescription
                && TransactionsAffected == t.TransactionsAffected
                && AsOf == t.AsOf
                && System == t.System
                && WorkType == t.WorkType)
                return true;
            return false;
        }
        #endregion
    }
    
}
