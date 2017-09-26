
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ErrClientWorkTypeRepositoryBase : RepositoryBase<ErrClientWorkType>, IErrClientWorkTypeRepository
    {
        public abstract ObservableCollection<ErrClientWorkType> GetClientWTErrors(string errorType);

        public  ErrClientWorkType Get(ErrClientWorkType entity)
        {
            throw new NotImplementedException();
        }
        public  void Add(ErrClientWorkType entity)
        {
            throw new NotImplementedException();
        }
        public  void Update(ErrClientWorkType entity)
        {
            throw new NotImplementedException();
        }
        public  void Remove(ErrClientWorkType entity)
        {
            throw new NotImplementedException();
        }
    }
}
