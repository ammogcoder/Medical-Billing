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
    public class OracleInvoiceGrpStatusRepository : InvoiceGrpStatusRepositoryBase
    {
        public override void Remove(InvoiceGrpStatus item)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<InvoiceGrpStatus> GetInvoiceGrpStatuss()
        {
#if !ORACLE
            return null;
#endif

            ObservableCollection<InvoiceGrpStatus> remits = null;
            try
            {

                string sql = string.Format("select  * from sphrsbilling.INVOICE_GRP_STATUS");
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                remits = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return remits;
        }

        public override InvoiceGrpStatus Get(InvoiceGrpStatus item)
        {
            throw new NotImplementedException();
        }

        public override void Add(InvoiceGrpStatus item)
        {
            throw new NotImplementedException();
        }

        public override void Update(InvoiceGrpStatus item)
        {
            throw new NotImplementedException();
        }

        static int cycles = 0;

        protected override void RowConverter(InvoiceGrpStatus item, DataRow row)
        {
            try
            {
                item.DESCR = row["DESCR"].ToString();
                item.INVOICE_GRP_STATUS = row["INVOICE_GRP_STATUS"].ToString();
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }
    }
}
