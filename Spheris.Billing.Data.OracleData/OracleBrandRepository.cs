using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleBrandRepository : BrandsRepositoryBase 
    {
        public override ObservableCollection<Brand> GetBrands()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<Brand> brands = new ObservableCollection<Brand>();
            try
            {
                const string sql = "select * from  sphrsbilling.Brand";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {

                    Brand item = new Brand 
                        { 
                            BRAND = row["BRAND"].ToString(),
                            DESCR = row["DESCR"].ToString(),
                            GRAPHIC_FILENAME = row["GRAPHIC_FILENAME"].ToString()
                        };
                    brands.Add(item);

                }
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return brands;
        }
        public override Brand Get(Brand entity)
        { return null; }
        public override void Add(Brand entity)
        { }
        public override void Update(Brand entity)
        { }
        public override void Remove(Brand entity)
        { }

        protected override void RowConverter(Brand item, DataRow row)
        {  throw new NotImplementedException(); }
    }
}
