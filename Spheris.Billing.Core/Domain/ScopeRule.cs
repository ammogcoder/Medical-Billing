using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Linq.Expressions;

namespace Spheris.Billing.Core.Domain
{
    public class ScopeRule : ObjectBase<ScopeRule>
    {
        public ScopeRule Clone()
        {
            return BaseClone<ScopeRule>();
        }

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

        public override string GetValidationError(string propertyName)
        {
            return null;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(SCOPE_RULE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ScopeRule;
            if (t == null)
                return false;
            if (SCOPE_RULE == t.SCOPE_RULE)
                return true;
            return false;
        }

    }
}


