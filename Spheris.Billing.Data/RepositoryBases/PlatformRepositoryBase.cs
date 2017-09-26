using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class PlatformRepositoryBase : RepositoryBase<Platform>, IPlatformRepository
    {
        public abstract ObservableCollection<Platform> GetPlatforms();
        public abstract Platform Get(Platform entity);
        public abstract void Add(Platform entity);
        public abstract void Update(Platform entity);
        public abstract void Remove(Platform entity);
    }
}
