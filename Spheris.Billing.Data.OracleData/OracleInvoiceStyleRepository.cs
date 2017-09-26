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
    public class OracleInvoiceStyleRepository : InvoiceStyleRepositoryBase
    {
        public override void Remove(InvoiceStyle item)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<InvoiceStyle> GetInvoiceStyles()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<InvoiceStyle> styles = null;
            try
           {

                string sql = string.Format("select  * from sphrsbilling.invoice_style");
                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);

                styles = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return styles;
        }

        public override InvoiceStyle Get(InvoiceStyle item)
        {
            throw new NotImplementedException();
        }

        public override void Add(InvoiceStyle item)
        {
            throw new NotImplementedException();
        }

        public override void Update(InvoiceStyle item)
        {
            throw new NotImplementedException();
        }

        static int cycles = 0;

        protected override void RowConverter(InvoiceStyle item, DataRow row)
        {
            try
            {
                item.MUST_ENCRYPT = (row["INVOICE_STYLE"].ToString() == "Y") ? true : false;
                item.INVOICE_STYLE          = row["INVOICE_STYLE"].ToString();
                item.DESCR                  = row["DESCR"].ToString();
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
        }
    }
}
