using Spheris.Common;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class WorkUnitRepository : RepositoryBase<WorkUnit>, IWorkUnitRepository
    {
        public abstract IDataReader Get(IDbConnection cnn, string platform, DateRange range, string siteID = "");
        public abstract WorkUnit Get(string extSys, string extWorkUnitKey);
        public abstract bool Add(WorkUnit item);
        public abstract bool Update(WorkUnit item, DateRange range);
        public abstract bool Remove(WorkUnit item, DateRange range);
        public abstract bool Remove(string extSys, string extWorkUnitKey, DateRange range);
        public abstract WorkUnit RowConverter(IDataRecord row);
    }
}
