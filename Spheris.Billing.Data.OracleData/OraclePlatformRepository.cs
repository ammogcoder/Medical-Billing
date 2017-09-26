using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace Spheris.Billing.Data.OracleData
{
    public class OraclePlatformRepository : PlatformRepositoryBase 
    {
        public override ObservableCollection<Platform> GetPlatforms()
        {
#if !ORACLE
            return null;
#endif

            ObservableCollection<Platform> platforms = new ObservableCollection<Platform>();
            try
            {
                const string sql = "select * from  sphrsbilling.platform";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);

                foreach (DataRow row in dt.Rows)
                {

                    Platform item = new Platform 
                        { 
                        PLATFORM = row["PLATFORM"].ToString(),
                        DESCR = row["DESCR"].ToString(),
                        GL_DISTR_NBR_DOM_TR  = row["GL_DISTR_NBR_DOM_TR"].ToString(),
                        GL_DISTR_NBR_GLOBAL_TR = row["GL_DISTR_NBR_GLOBAL_TR"].ToString(),
                        GL_DISTR_NBR_DOM_SR = row["GL_DISTR_NBR_DOM_SR"].ToString(),
                        GL_DISTR_NBR_GLOBAL_SR = row["GL_DISTR_NBR_GLOBAL_SR"].ToString(),
                        GL_DISTR_NBR_INHOUSE = row["GL_DISTR_NBR_INHOUSE"].ToString()
                        };
                    platforms.Add(item);

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
            return platforms;
        }
        public override Platform Get(Platform entity)
        { throw new NotImplementedException();  }
        public override void Add(Platform entity)
        { throw new NotImplementedException(); }
        public override void Update(Platform entity)
        { throw new NotImplementedException(); }
        public override void Remove(Platform entity)
        { throw new NotImplementedException(); }
        protected override void RowConverter(Platform item, DataRow row)
        { throw new NotImplementedException(); }
    }
}
