using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class AddOnChgTypeRepositoryBase : RepositoryBase<AddOnChgType>, IAddOnChgTypeRepository
    {
        public abstract ObservableCollection<AddOnChgType> GetAddOnChgTypes();
        public abstract AddOnChgType Get(AddOnChgType entity);
        public abstract void Add(AddOnChgType entity);
        public abstract void Update(AddOnChgType entity);
        public abstract void Remove(AddOnChgType entity);
    }
}
