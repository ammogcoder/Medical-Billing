using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// PROCEDURE SPHRSBILLING.ODP_ERR.S_CONTRACT_ERRORS
    /// </summary>
    public class ClientErrorType : ObjectBase<ClientErrorType>
    {

        private string _EXT_CLIENT_ERR_TYPE;
        public string EXT_CLIENT_ERR_TYPE
        {
            get
            {
                return _EXT_CLIENT_ERR_TYPE;
            }
            set
            {
                if (_EXT_CLIENT_ERR_TYPE != value)
                {
                    _EXT_CLIENT_ERR_TYPE = value;
                    OnPropertyChanged("EXT_CLIENT_ERR_TYPE");
                }
            }
        }

        /// <summary>
        /// key
        /// </summary>
        private string _DESC;
        public string DESC
        {
            get
            {
                return _DESC;
            }
            set
            {
                if (_DESC != value)
                {
                    _DESC = value;
                    OnPropertyChanged("DESC");
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
            return 0.CombineHashCode(this._DESC)
                .CombineHashCode(this._EXT_CLIENT_ERR_TYPE) ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ClientErrorType;
            if (t == null)
                return false;
            if (_DESC == t._DESC
                && _EXT_CLIENT_ERR_TYPE == t._EXT_CLIENT_ERR_TYPE)
                return true;
            return false;
        }
        #endregion
    }
    
}
