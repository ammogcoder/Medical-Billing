using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class FreqRepositoryBase : RepositoryBase<Freq>, IFreqRepository
    {
        public abstract ObservableCollection<Freq> GetFreqs();
        public abstract Freq Get(Freq entity);
        public abstract void Add(Freq entity);
        public abstract void Update(Freq entity);
        public abstract void Remove(Freq entity);
    }
}
