
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ErrClientErrorTypeRepositoryBase : RepositoryBase<ClientErrorType>, IErrClientErrorTypeRepository
    {

        public abstract ObservableCollection<ClientErrorType> GetClientErrorTypes();

        public  ClientErrorType Get(ClientErrorType entity)
        {
            throw new NotImplementedException();
        }

        public  void Add(ClientErrorType entity)
        {
            throw new NotImplementedException();
        }

        public  void Remove(ClientErrorType entity)
        {
            throw new NotImplementedException();
        }

        public  void Update(ClientErrorType entity)
        {
            throw new NotImplementedException();
        }
    }
}
