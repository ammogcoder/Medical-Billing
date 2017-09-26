using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class VolumeEvtRateRepositoryBase : RepositoryBase<VolumeEvtRate>, IVolumeEvtRateRepository
    {
        public abstract ObservableCollection<VolumeEvtRate> FetchVolumeEvtRates(decimal contractVolumeEvtId);

        public abstract VolumeEvtRate Get(VolumeEvtRate entity);
        public abstract void Add(VolumeEvtRate entity);
        public abstract void Remove(VolumeEvtRate entity);
        public abstract void Update(VolumeEvtRate entity);
    }
}
