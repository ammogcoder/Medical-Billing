using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleBillingSpecialistRepository : BillingSpecialistRepositoryBase 
    {
        public override ObservableCollection<BillingSpecialist> GetSpecialists()
        {
#if !ORACLE
            return null;
#endif

            ObservableCollection<BillingSpecialist> specialists = new ObservableCollection<BillingSpecialist>();
            try
            {
                const string sql = "select * from  sphrsbilling.bill_specialist";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {

                    BillingSpecialist item = new BillingSpecialist 
                        { 
                            Id = int.Parse(row["BILL_SPECIALIST_ID"].ToString()), 
                            Name = row["NAME"].ToString() ,
                            Phone = row["PHONE"].ToString(),
                            Email = row["EMAIL"].ToString(),
                            DisplayTitle = row["DISPLAY_TITLE"].ToString(),
                            InvoiceEmailBody = row["INVOICE_EMAIL_BODY"].ToString()
                        };
                    specialists.Add(item);

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
            return specialists;
        }
        public override BillingSpecialist Get(BillingSpecialist entity)
        { return null; }
        public override void Add(BillingSpecialist entity)
        { }
        public override void Update(BillingSpecialist entity)
        { }
        public override void Remove(BillingSpecialist entity)
        { }



        protected override void RowConverter(BillingSpecialist item, DataRow row)
        {
            int parsed = 0;
            try
            {
                if (int.TryParse(row["BILL_SPECIALIST_ID"].ToString(), out parsed))
                    item.Id = parsed;

                item.Name = row["NAME"].ToString();
                item.Phone = row["PHONE"].ToString();
                item.Email = row["INVOICE_EMAIL_BODY"].ToString();
                item.DisplayTitle = row["DISPLAY_TITLE"].ToString();
                item.InvoiceEmailBody = row["INVOICE_EMAIL_BODY"].ToString();

            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }

        }
    }
}
