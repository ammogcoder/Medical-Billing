using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class InvoiceDetailRepositoryBase : RepositoryBase<ReportTypePackage>, IInvoiceDetailRepositoryBase
    {
        //public abstract ReportTypeTable GetReportTypeTable();
        public abstract ObservableCollection<string> GetAvailableFields();
        public abstract ReportTypePackage RowConverter(IDataRecord record);
        public abstract ReportTypeTable MakeReportTable();
        public abstract void Add(ReportTypePackage package);
        public abstract void Update(ReportTypePackage package);
        public abstract void Delete(ReportTypePackage package);


#if DEAD
        public abstract void Add(ReportTypePackage item);
        public abstract void Update(ReportTypePackage item);
        public abstract void Remove(ReportTypePackage item);
        public abstract ReportTypePackage Get(ReportTypePackage entity);
#endif
    }
}
