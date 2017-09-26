using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// </summary>
    public class AddOnChgType : ObjectBase<AddOnChgType>
    {
        private decimal _ADD_ON_CHG_TYPE_ID;
        public decimal ADD_ON_CHG_TYPE_ID
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


        public AddOnChgType Clone()
        {
            return BaseClone<AddOnChgType>();
        }

        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(ADD_ON_CHG_TYPE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AddOnChgType;
            if (t == null)
                return false;
            if (ADD_ON_CHG_TYPE_ID == t.ADD_ON_CHG_TYPE_ID )
                return true;
            return false;
        }

    }
}


