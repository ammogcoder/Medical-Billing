using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class PaymentTermsRepositoryBase : RepositoryBase<PaymentTerm>, IPaymentTermsRepository
    {
        public abstract ObservableCollection<PaymentTerm>  GetAllPaymentTerms();
        public abstract PaymentTerm  Get(PaymentTerm entity);
        public abstract void  Add(PaymentTerm entity);
        public abstract void  Remove(PaymentTerm entity);
        public abstract void  Update(PaymentTerm entity);
    }
}

