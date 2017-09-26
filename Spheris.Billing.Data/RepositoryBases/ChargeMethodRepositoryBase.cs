using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ChargeMethodRepositoryBase : RepositoryBase<ChargeMethod>,IChargeMethodRepository
    {
        public abstract ObservableCollection<ChargeMethod> GetChargeMethods();

        public abstract ChargeMethod Get(ChargeMethod entity);

        public abstract void Add(ChargeMethod entity);

        public abstract void Remove(ChargeMethod entity);

        public abstract void Update(ChargeMethod entity);

    }
}
