using Spheris.Billing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Data.RepositoryInterfaces
{
    public interface IExtClientRepository
    {
        List<ClientLocation> Get(string platform);
        IDataReader GetReader(IDbConnection cnn, string platform);
        ObservableCollection<ClientLocation> FetchLocations(int invoiceId, bool exclusive);
        ClientLocation UnassignLocation(ClientLocation loc);
        ObservableCollection<ClientLocation> FetchClients(string clientFilter,bool notAssignedToGroups = false);

        ClientLocation Update(ClientLocation loc);
        void Remove(ClientLocation client);
        bool Add(ClientLocation client);
        ClientLocation RowConverter(IDataRecord record);
    }
}
