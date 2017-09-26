using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryBases
{
    public abstract class ClientLocationRepositoryBase : RepositoryBase<ClientLocation>, IExtClientRepository
    {
        public abstract List<ClientLocation> Get(string platform);
        public abstract ObservableCollection<ClientLocation> FetchLocations(int invoiceId, bool exclusive);
        public abstract ClientLocation UnassignLocation(ClientLocation loc);

        public abstract ClientLocation Update(ClientLocation loc);
        public abstract ObservableCollection<ClientLocation> FetchClients(string clientFilter, bool notAssignedToGroup = false);


        public abstract IDataReader GetReader(IDbConnection cnn, string platform);
        public abstract void Remove(ClientLocation client);
        public abstract bool Add(ClientLocation client);
        public abstract ClientLocation RowConverter(IDataRecord record);
    }
}
