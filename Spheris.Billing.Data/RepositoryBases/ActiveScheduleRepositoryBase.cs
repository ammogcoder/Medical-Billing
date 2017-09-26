using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ActiveScheduleRepositoryBase : RepositoryBase<ActiveSchedule>, IActiveScheduleRepository
    {

        public abstract ObservableCollection<ActiveSchedule> FetchActiveSchedules(decimal invoiceGrpId);

        public abstract ActiveSchedule Get(ActiveSchedule entity);

        public abstract void Add(ActiveSchedule entity);

        public abstract void Remove(ActiveSchedule entity);

        public abstract void Update(ActiveSchedule entity);
    }
}
