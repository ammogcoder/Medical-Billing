using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleFreqRepository : FreqRepositoryBase
    {
        public override ObservableCollection<Freq> GetFreqs()
        {
#if !ORACLE
            return null;
#endif
            ObservableCollection<Freq> freqs = new ObservableCollection<Freq>();
            try
            {
                const string sql = "select * from  sphrsbilling.Freq";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {

                    Freq item = new Freq
                    {
                    FREQ = row["FREQ"].ToString(),
                    DESCR = row["DESCR"].ToString(),
                    RECURRANCE_RULE = row["RECURRANCE_RULE"].ToString(),
                    VALID_FOR_ADD_ON_CHG = (row["VALID_FOR_ADD_ON_CHG"].ToString() == "Y") ? true : false
                    };
                    freqs.Add(item);

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
            return freqs;
        }
        public override Freq Get(Freq entity)
        { throw new NotImplementedException(); }
        public override void Add(Freq entity)
        { throw new NotImplementedException(); }
        public override void Update(Freq entity)
        { throw new NotImplementedException(); }
        public override void Remove(Freq entity)
        { throw new NotImplementedException(); }
        protected override void RowConverter(Freq item, DataRow row)
        { throw new NotImplementedException(); }
    }
}
