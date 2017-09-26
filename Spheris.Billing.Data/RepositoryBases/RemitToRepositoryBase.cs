using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class RemitToRepositoryBase : RepositoryBase<RemitTo>, IRemitToRepository
    {
        public abstract ObservableCollection<RemitTo> GetRemitTos();
        public abstract RemitTo Get(RemitTo entity);
        public abstract void Add(RemitTo entity);
        public abstract void Update(RemitTo entity);
        public abstract void Remove(RemitTo entity);
    }
}
