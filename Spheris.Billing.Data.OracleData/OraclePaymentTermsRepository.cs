using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OraclePaymentTermsRepository : PaymentTermsRepositoryBase
    {
        public override ObservableCollection<PaymentTerm> GetAllPaymentTerms()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<PaymentTerm> PaymentTerms = new ObservableCollection<PaymentTerm>();
            try
            {
                const string sql = "select * from  sphrsbilling.Payment_Terms";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {

                    PaymentTerm item = new PaymentTerm
                    {
                        PAYMENT_TERMS = row["PAYMENT_TERMS"].ToString(),
                        DESCR = row["DESCR"].ToString(),
                    };
                    PaymentTerms.Add(item);
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
            return PaymentTerms;
        }
        public override PaymentTerm Get(PaymentTerm entity)
        { throw new NotImplementedException(); }
        public override void Add(PaymentTerm entity)
        { throw new NotImplementedException(); }
        public override void Update(PaymentTerm entity)
        { throw new NotImplementedException(); }
        public override void Remove(PaymentTerm entity)
        { throw new NotImplementedException(); }
        protected override void RowConverter(PaymentTerm item, DataRow row)
        { throw new NotImplementedException(); }

    }
}
