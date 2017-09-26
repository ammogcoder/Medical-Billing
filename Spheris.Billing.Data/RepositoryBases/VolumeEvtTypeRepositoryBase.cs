using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class VolumeEvtTypeRepositoryBase : RepositoryBase<VolumeEvtType>, IVolumeEvtTypeRepository
    {
        public abstract ObservableCollection<VolumeEvtType> FetchVolumeEvtType();

        public abstract VolumeEvtType Get(VolumeEvtType entity);
        public abstract void Add(VolumeEvtType entity);
        public abstract void Remove(VolumeEvtType entity);
        public abstract void Update(VolumeEvtType entity);
    }
}
