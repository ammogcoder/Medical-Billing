// TODO:  Read - Deprecated !!
// http://msdn.microsoft.com/en-us/library/77d8yct7.aspx
// The types in System.Data.OracleClient are deprecated. The types are 
// supported in version 4 of the .NET Framework but will be removed in 
// a future release. Microsoft recommends that you use a third-party 
// Oracle provider.

using Spheris.Common;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.OracleData;
using System.Collections.Generic;
using System;
using System.Data.OracleClient;
using System.Data;
using System.Text;
//TODO: Remove this reference and the System.Windows.Forms
using System.Windows.Forms;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data;
using System.Data.Odbc;

#pragma warning disable 618

namespace MedQuist.BillingAdmin.ViewModels
{
	/// <summary>
	/// Summary description for DB.
	/// </summary>
	public class DB
	{
#if MSACCESS
    OdbcConnection Conn = new System.Data.Odbc.OdbcConnection("DSN=MedQuist;UID=;PWD=");
#endif
        private static OracleSessionInfo _oracleSession;
        private static DataSet DS = null;
        private static OracleConnection OraConx = null;
        private static DataViewManager DVM = null;

        public static IAccessSettings Settings = null;
		public static OracleSessionInfo OracleSession
		{
			get
			{
				return _oracleSession;
			}
		}
		
		public DB()
		{
			
			if (DS == null)
			{
				DS	= new DataSet();
				DVM = DS.DefaultViewManager;
			}
		}

		public static bool MakeOraConx(string	UserId
							,string Password
							,string TNSName)
		{
            try
            {
                OraConx = null;
                OraConx = new OracleConnection();
                string connString = String.Format("Data Source={0};User Id={1};Password={2};"
                                                 , TNSName, UserId, Password);
                OraConx.ConnectionString = connString;

                OraConx.Open();
                if (OraConx.State == ConnectionState.Open)
                {
                    _oracleSession = new OracleSessionInfo(OraConx);
                    // HACK:  This is temporary until everything is converted over to the new DAL.
                    Spheris.Billing.Data.DalBridge.ConnectionBridge.Value = connString;

                    EventLog.WriteEntry(UserId +
                                        " successfully logged in to server " +
                                        TNSName +
                                        ".", System.Diagnostics.EventLogEntryType.SuccessAudit);

                }
            }
            catch (OracleException ex)
            {
                switch (ex.Code)
                {
                    case 1005:      // Invalid password.
                    case 1017:
                        //Cursor.Current = Cursors.Default;
                        MessageBox.Show("Invalid user name or password.", "Login unsuccessful");
                        EventLog.WriteEntry(UserId +
                                        " failed to log in to server " +
                                        TNSName +
                                        ".", System.Diagnostics.EventLogEntryType.FailureAudit);
                        return false;
                }
                throw ex;
            }
            catch
            {
                throw;
            }
            if (Settings != null)
            {
                Settings.setValue("UserName", UserId);
                Settings.setValue("Database", TNSName);
            }

            return true;

		}
	}
}
