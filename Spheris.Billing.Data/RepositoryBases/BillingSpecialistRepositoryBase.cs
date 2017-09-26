using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class BillingSpecialistRepositoryBase : RepositoryBase<BillingSpecialist>, IBillingSpecialistRepository
    {
        public abstract ObservableCollection<BillingSpecialist> GetSpecialists();
        public abstract BillingSpecialist Get(BillingSpecialist entity);
        public abstract void Add(BillingSpecialist entity);
        public abstract void Update(BillingSpecialist entity);
        public abstract void Remove(BillingSpecialist entity);
    }
}
