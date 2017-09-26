using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IReportTypeRepository
    {
        ObservableCollection<ReportType> GetReportTypes();
        List<ReportType> GetReportTypesAsList();
    }
}
