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
    public class OracleFaxCompMethodRepository : FaxCompMethodRepositoryBase
    {

        public override ObservableCollection<FaxCompMethod> GetFaxCompMethods()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<FaxCompMethod> faxCompMethods = new ObservableCollection<FaxCompMethod>();
            try
            {
                string sql = "select * from sphrsbilling.fax_comp_method";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                faxCompMethods = ConvertDataTableToObservableCollection(dt);

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }

            return faxCompMethods;
        }


        public override FaxCompMethod Get(FaxCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(FaxCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(FaxCompMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(FaxCompMethod entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(FaxCompMethod item, DataRow row)
        {
            item.BILLING_PRECISION = CastDbValueRow(row, "BILLING_PRECISION", true, "Int32");
            item.FAX_COMP_METHOD = CastDbValueRow(row, "FAX_COMP_METHOD");
            item.CHARS_PER_LINE = CastDbValueRow(row, "CHARS_PER_LINE", true, "Int32");
            item.DESCR = CastDbValueRow(row, "DESCR");
            item.GP_ITEM_DESCR = CastDbValueRow(row, "GP_ITEM_DESCR");
            item.INVOICE_COLUMN_HEADER = CastDbValueRow(row, "INVOICE_COLUMN_HEADER");
            item.ROUNDING_STYLE = CastDbValueRow(row, "ROUNDING_STYLE", true, "Bool");
        }
    }
}
