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
    public class OracleInvoiceStatusRepository : InvoiceStatusRepositoryBase
    {
        public override void Remove(InvoiceStatus item)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<InvoiceStatus> GetInvoiceStatuss()
        {
            ObservableCollection<InvoiceStatus> remits = null;
            try
            {

                string sql = string.Format("select  * from sphrsbilling.INVOICE_STATUS");
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

        public override InvoiceStatus Get(InvoiceStatus item)
        {
            throw new NotImplementedException();
        }

        public override void Add(InvoiceStatus item)
        {
            throw new NotImplementedException();
        }

        public override void Update(InvoiceStatus item)
        {
            throw new NotImplementedException();
        }

        static int cycles = 0;

        protected override void RowConverter(InvoiceStatus item, DataRow row)
        {
            try
            {
                item.DESCR = row["DESCR"].ToString();
                item.INVOICE_STATUS = row["INVOICE_STATUS"].ToString();
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }
    }
}
