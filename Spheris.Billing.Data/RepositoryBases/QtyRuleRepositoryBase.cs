using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class QtyRuleRepositoryBase : RepositoryBase<QtyRule>, IQtyRuleRepository
    {
        public abstract ObservableCollection<QtyRule> FetchQtyRules();

        public abstract QtyRule Get(QtyRule entity);

        public abstract void Add(QtyRule entity);

        public abstract void Remove(QtyRule entity);

        public abstract void Update(QtyRule entity);
    }
}
