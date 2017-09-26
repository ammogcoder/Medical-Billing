using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ReportTypeRepositoryBase : RepositoryBase<ReportType>, IReportTypeRepository
    {
        public abstract ObservableCollection<ReportType> GetReportTypes();
        public abstract List<ReportType> GetReportTypesAsList();
        public abstract ReportType Get(ReportType entity);
        public abstract void Add(ReportType entity);
        public abstract void Update(ReportType entity);
        public abstract void Remove(ReportType entity);
    }
}
