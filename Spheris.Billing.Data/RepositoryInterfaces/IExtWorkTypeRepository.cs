using Spheris.Billing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
#if WAS
    public interface IExtWorkTypeRepository
    {
        List<WorkType> Get(string platform);
        IDataReader GetReader(IDbConnection cnn, string platform);
        bool Add(WorkType wt);
        bool Update(WorkType wt);
        WorkType RowConverter(IDataRecord record);
    }
#else
    public interface IExtWorkTypeRepository : ICrudRepository<ExtWorkType>
    {
        ObservableCollection<ExtWorkType> GetWorkTypes(string extSys, string extClientKey = null);
    }
#endif
}
