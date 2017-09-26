using Spheris.Billing.Core.Domain;
using Spheris.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IWorkUnitRepository
    {
        IDataReader Get(IDbConnection cnn, string platform, DateRange dateRange, string siteID = "");
        WorkUnit Get(string extSys, string extWorkUnitKey);
        bool Add(WorkUnit item);
        bool Update(WorkUnit item, DateRange range);
        bool Remove(WorkUnit item, DateRange range);
        bool Remove(string extSys, string extWorkUnitKey, DateRange range);
        WorkUnit RowConverter(IDataRecord row);
    }
}
