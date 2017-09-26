using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{                                                    
    public class OracleClientErrorTypeRepository : ErrClientErrorTypeRepositoryBase
    {

        protected override void RowConverter(ClientErrorType r, DataRow record)
        {
            try
            {
                r.DESC = CastDbValueRow(record, "DESCR");
                r.EXT_CLIENT_ERR_TYPE = CastDbValueRow(record, "EXT_CLIENT_ERR_TYPE");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ObservableCollection<ClientErrorType> GetClientErrorTypes()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<ClientErrorType> errorTypes;
            string sql = String.Format(
                    "select  * FROM {0}.EXT_CLIENT_ERR_TYPE\r\n", SchemaName);
            try
            {
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                errorTypes = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ox)
            {
                throw ox;
            }
            catch (Exception x)
            {
                throw x;
            }
            return errorTypes;
        }
    }
}
