using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface ICrudRepository<T>
    {
        T Get(T entity);
        void Add(T entity); 
        void Remove(T entity);
        void Update(T entity);

        ConnectionString ConnectionString { get; set; }
    }
}
