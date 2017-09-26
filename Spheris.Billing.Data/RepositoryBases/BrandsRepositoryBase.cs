using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class BrandsRepositoryBase : RepositoryBase<Brand>, IBrandsRepository
    {
        public abstract ObservableCollection<Brand> GetBrands();
        public abstract Brand Get(Brand entity);
        public abstract void Add(Brand entity);
        public abstract void Update(Brand entity);
        public abstract void Remove(Brand entity);
    }
}
