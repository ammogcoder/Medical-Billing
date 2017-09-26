using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// REMIT_TO
    /// KEY - REMIT_TO_ID
    /// </summary>
    public class RemitTo : ObjectBase<RemitTo>
    {
        //REMIT_TO
        /// <summary>
        /// 36 varchars
        /// Address
        /// </summary>
        private string _LINE1;
        public string LINE1
        {
            get
            {
                return _LINE1;
            }
            set
            {
                _LINE1 = value;
                OnPropertyChanged("LINE1");
            }
        }


        /// <summary>
        /// 36 varchars
        /// Address
        /// </summary>
        private string _LINE2;
        public string LINE2
        {
            get
            {
                return _LINE2;
            }
            set
            {
                _LINE2 = value;
                OnPropertyChanged("LINE2");
            }
        }


        /// <summary>
        /// 36 varchars
        /// Address
        /// </summary>
        private string _LINE3;
        public string LINE3
        {
            get
            {
                return _LINE3;
            }
            set
            {
                _LINE3 = value;
                OnPropertyChanged("LINE3");
            }
        }


        /// <summary>
        /// 36 varchars
        /// Address
        /// </summary>
        private string _LINE4;
        public string LINE4
        {
            get
            {
                return _LINE4;
            }
            set
            {
                _LINE4 = value;
                OnPropertyChanged("LINE4");
            }
        }

        public string MixedAddress
        {
            get
            {
                return LINE1 + "|" + LINE2 + "|" + LINE3 + "|" + LINE4;
            }
        }

        /// <summary>
        /// id non null
        /// </summary>
        private int _REMIT_TO_ID;
        public int REMIT_TO_ID
        {
            get
            {
                return _REMIT_TO_ID;
            }
            set
            {
                _REMIT_TO_ID = value;
                OnPropertyChanged("REMIT_TO_ID");
            }
        }


        /// <summary>
        /// Used by the for selection only
        /// </summary>
        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(REMIT_TO_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as RemitTo;
            if (t == null)
                return false;
            if (REMIT_TO_ID == t.REMIT_TO_ID)
                return true;
            return false;
        }
        #endregion
    }
}
