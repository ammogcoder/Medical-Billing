using Spheris.Billing.Data.RepositoryBases;
using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public class OracleInvoiceDetailReportTypeQuery:InvoiceDetailReportTypeQuery
    {
        public override DataTable Get(int reportTypeID, int invoiceID)
        {
            // DO NOT add "\r\n" newlines to this SQL.  Oracle will not compile.
            string sql = String.Format(
                         "declare "
                       + "  fileTypeSql varchar2(4000);"
                       + "begin "
                       + "    select sql into fileTypeSql from {0}.report_type where report_type_id = :reportTypeID; "
                       + "    open :workUnits for fileTypeSql using :invoiceID; "
                       + "end;"
                       , base.SchemaName);
            OracleParameter[] p = { OracleHelper.CreateParameter(":reportTypeID", reportTypeID, OracleType.Number, ParameterDirection.Input)
                                   ,OracleHelper.CreateParameter(":invoiceID", invoiceID, OracleType.Number, 22, ParameterDirection.Input)
                                   ,OracleHelper.CreateParameter(":workUnits", null, OracleType.Cursor, ParameterDirection.Output)};
            DataTable dt = OracleHelper.ExecuteQuery(base.ConnectionString.Value, sql, p, 300);
            return dt;
        }
    }
}
