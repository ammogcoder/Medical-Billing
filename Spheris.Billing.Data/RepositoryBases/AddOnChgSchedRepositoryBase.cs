
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class AddOnChgSchedRepositoryBase : RepositoryBase<AddOnChgSched>, IAddOnChgSchedRepository
    {
        public abstract ObservableCollection<AddOnChgSched> GetAddOnChgScheds(decimal invoiceGrpId);
        public abstract AddOnChgSched Get(AddOnChgSched entity);
        public abstract void Add(AddOnChgSched entity);
        public abstract void Update(AddOnChgSched entity);
        public abstract void Remove(AddOnChgSched entity);
    }
}
