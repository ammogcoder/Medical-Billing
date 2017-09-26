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
    public class OracleErrClientWorkTypeRepository : ErrClientWorkTypeRepositoryBase
    {

        protected override void RowConverter(ErrClientWorkType r, DataRow record)
        {
            try
            {
                r.AsOf = CastDbValueRow(record, "REPORTED_ON", true, "DateTime");
                r.Description = CastDbValueRow(record, "CLIENT_DESCR");
                r.ErrorDescription = CastDbValueRow(record, "ERR_DESCR");
                r.TransactionsAffected = CastDbValueRow(record, "WORK_UNITS_AFFECTED", true, "decimal");

                r.ID = CastDbValueRow(record, "EXT_CLIENT_KEY" );
                r.System = CastDbValueRow(record, "EXT_SYS_DESCR");
                r.WorkType = CastDbValueRow(record, "EXT_WORK_TYPE");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ObservableCollection<ErrClientWorkType> GetClientWTErrors(string errorType)
        {
            ObservableCollection<ErrClientWorkType> errors;
            string sql = string.Format("SELECT "
                + " EC.DESCR           AS CLIENT_DESCR"
                + " ,ES.DESCR           AS EXT_SYS_DESCR"
                + " ,ECE.EXT_CLIENT_KEY"
                + " ,ECE.EXT_WORK_TYPE"
                + " ,ECE.EXT_CLIENT_ERR_TYPE"
                + " ,ECET.DESCR         AS ERR_DESCR"
                + " ,ECE.REPORTED_ON"
                + " ,ECE.WORK_UNITS_AFFECTED "
                + " FROM "
                + " {0}.EXT_CLIENT_ERR      ECE"
                + " ,{0}.EXT_CLIENT          EC"
                + " ,{0}.EXT_CLIENT_ERR_TYPE ECET"
                + " ,{0}.EXT_SYS             ES"
                + " WHERE ECE.EXT_SYS             = EC.EXT_SYS(+)"
                + " AND   ECE.EXT_CLIENT_KEY      = EC.EXT_CLIENT_KEY(+)"
                + " AND   ECE.EXT_CLIENT_ERR_TYPE = ECET.EXT_CLIENT_ERR_TYPE"
                + " AND      ECE.EXT_SYS             = ES.EXT_SYS", SchemaName);
            try
            {

                OracleParameter[] p = null;
                DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p);
                errors = ConvertDataTableToObservableCollection(dt);
            }
            catch (OracleException ox)
            {
                throw ox;
            }
            catch (Exception x)
            {
                throw x;
            }

            return errors;
        }
    }
}
