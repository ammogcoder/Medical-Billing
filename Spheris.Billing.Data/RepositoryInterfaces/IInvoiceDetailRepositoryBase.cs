using Spheris.Billing.Core.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IInvoiceDetailRepositoryBase 
    {
        ReportTypePackage RowConverter(IDataRecord record);
        ObservableCollection<string> GetAvailableFields();
        ReportTypeTable MakeReportTable();
        void Add(ReportTypePackage package);
        void Update(ReportTypePackage package);
        void Delete(ReportTypePackage package);

    }
}
