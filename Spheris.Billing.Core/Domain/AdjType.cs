using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    
    public class AdjType : ObjectBase<AdjType>
    {
    
        public AdjType Clone()
        {
            return BaseClone<AdjType>();
        }

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
        private bool _APPLY_AFTER_FINAL_COST;
        public bool APPLY_AFTER_FINAL_COST
        {
            get
            {
                return _APPLY_AFTER_FINAL_COST;
            }
            set
            {
                _APPLY_AFTER_FINAL_COST = value;
                OnPropertyChanged("APPLY_AFTER_FINAL_COST");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(ADJ_TYPE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AdjType;
            if (t == null)
                return false;
            if (ADJ_TYPE == t.ADJ_TYPE )
                return true;
            return false;
        }

    }
}


