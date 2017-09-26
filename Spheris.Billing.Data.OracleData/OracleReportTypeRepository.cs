using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryBases;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OracleClient;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.Generic;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleReportTypeRepository : ReportTypeRepositoryBase 
    {
        public override ObservableCollection<ReportType> GetReportTypes()
        {
            ObservableCollection<ReportType> ReportTypes = new ObservableCollection<ReportType>();
            try
            {
                const string sql = "select * from  sphrsbilling.Report_Type";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
                ReportTypes = ConvertDataTableToObservableCollection(dt);
#if USING_ROWCONVERTER
                foreach (DataRow row in dt.Rows)
                {

                    ReportType item = new ReportType 
                        {
                            DESCR = row["DESCR"].ToString(),
                            REPORT_TYPE_ID = int.Parse(row["REPORT_TYPE_ID"].ToString()),
                            SHORT_NAME = row["SHORT_NAME"].ToString(),
                            SQL = row["SQL"].ToString(),
                            ALLOW_DYNAMIC_MODS = (row["ALLOW_DYNAMIC_MODS"].ToString() == "Y")  ? true : false,
                            DATA_DUMP_ONLY = (row["DATA_DUMP_ONLY"].ToString() == "Y") ? true : false,
                            MUST_ENCRYPT = (row["MUST_ENCRYPT"].ToString() == "Y") ? true : false,
                        };
                    ReportTypes.Add(item);
                }
#endif
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return ReportTypes;
        }

        public override List<ReportType> GetReportTypesAsList()
        {
#if !ORACLE
            return null;
#endif
            List<ReportType> ReportTypes = new List<ReportType>();
            try
            {
                const string sql = "select * from  sphrsbilling.Report_Type";
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, null);
                ReportTypes = ConvertDataTableToList(dt);

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            catch (Exception sysEx)
            {
                throw sysEx;
            }
            return ReportTypes;
        }
        public override ReportType Get(ReportType entity)
        { return null; }
        public override void Add(ReportType entity)
        { }
        public override void Update(ReportType entity)
        { }
        public override void Remove(ReportType entity)
        { }

        protected override void RowConverter(ReportType item, DataRow row)
        {  
              item.DESCR = row["DESCR"].ToString();
              item.REPORT_TYPE_ID = int.Parse(row["REPORT_TYPE_ID"].ToString());
              item.SHORT_NAME = row["SHORT_NAME"].ToString();
              item.SQL = row["SQL"].ToString();
              item.ALLOW_DYNAMIC_MODS = (row["ALLOW_DYNAMIC_MODS"].ToString() == "Y")  ? true : false;
              item.DATA_DUMP_ONLY = (row["DATA_DUMP_ONLY"].ToString() == "Y") ? true : false;
              item.MUST_ENCRYPT = (row["MUST_ENCRYPT"].ToString() == "Y") ? true : false;
        }
    }
}
