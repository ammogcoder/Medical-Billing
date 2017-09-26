using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class AdjTypeRepositoryBase : RepositoryBase<AdjType>, IAdjTypeRepository
    {
        public abstract ObservableCollection<AdjType> GetAdjTypes();
        public abstract AdjType Get(AdjType entity);
        public abstract void Add(AdjType entity);
        public abstract void Update(AdjType entity);
        public abstract void Remove(AdjType entity);
    }
}
