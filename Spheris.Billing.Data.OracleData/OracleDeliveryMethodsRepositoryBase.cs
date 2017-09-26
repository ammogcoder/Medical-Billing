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
    public class OracleDeliveryMethodsRepositoryBase : DeliveryMethodRepositoryBase
    {

        public override ObservableCollection<DeliveryMethod> GetDeliveryMethods()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<DeliveryMethod> deliverys = new ObservableCollection<DeliveryMethod>();
            try
            {
                string sql = "select * from sphrsbilling.delivery_method";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {
                    DeliveryMethod item = new DeliveryMethod();
                    item.TheDeliveryMethod = row["DELIVERY_METHOD"].ToString();
                    item.Descr = row["DESCR"].ToString();
                    deliverys.Add(item);
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

            return deliverys;
        }


        public override DeliveryMethod Get(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Add(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(DeliveryMethod entity)
        {
            throw new NotImplementedException();
        }

        protected override void RowConverter(DeliveryMethod item, DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
