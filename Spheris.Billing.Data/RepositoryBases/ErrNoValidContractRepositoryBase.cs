
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ErrNoValidContractRepositoryBase : RepositoryBase<ErrNoValidContract>, IErrNoValidContractRepository
    {

        public abstract ObservableCollection<ErrNoValidContract> GetContractErrors(DateTime asOf, bool showOnlyActiveIGs);

        public  ErrNoValidContract Get(ErrNoValidContract entity)
        {
            throw new NotImplementedException();
        }

        public  void Add(ErrNoValidContract entity)
        {
            throw new NotImplementedException();
        }

        public  void Remove(ErrNoValidContract entity)
        {
            throw new NotImplementedException();
        }

        public  void Update(ErrNoValidContract entity)
        {
            throw new NotImplementedException();
        }


    }
}
