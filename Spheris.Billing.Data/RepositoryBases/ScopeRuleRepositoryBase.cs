using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ScopeRuleRepositoryBase : RepositoryBase<ScopeRule>, IScopeRuleRepository
    {
        public abstract ObservableCollection<ScopeRule> GetScopeRules();
        public abstract ScopeRule Get(ScopeRule entity);
        public abstract void Add(ScopeRule entity);
        public abstract void Update(ScopeRule entity);
        public abstract void Remove(ScopeRule entity);
    }
}
