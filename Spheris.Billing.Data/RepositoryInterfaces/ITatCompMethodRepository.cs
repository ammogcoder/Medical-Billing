﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface ITatCompMethodRepository : ICrudRepository<TatCompMethod>
    {
        ObservableCollection<TatCompMethod> GetTATCompMethods();
    }
}
