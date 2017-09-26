using System;
using System.Data;
using System.Data.OracleClient;
using System.Text;

namespace Spheris.Common
{
    public class OracleSessionInfo
    {
        private readonly string _userName;
        private readonly string _database;
        private readonly string _terminal;
        private readonly int _sessionId;

        public int SessionId
        {
            get { return _sessionId; }
        }

        public string Terminal
        {
            get { return _terminal; }
        }

        public string Database
        {
            get { return _database; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public OracleSessionInfo(OracleConnection cnn)
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT USER AS USERNAME, "
                                + "GLOBAL_NAME AS DATABASE, "
                                + "USERENV('TERMINAL') AS TERMINAL, "
                                + "USERENV('SESSIONID') AS SESSIONID "
                                + "FROM GLOBAL_NAME";

                using(OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        _userName = dr["USERNAME"].ToString();
                        _database = dr["DATABASE"].ToString();
                        _terminal = dr["TERMINAL"].ToString();
                        _sessionId = int.Parse(dr["SESSIONID"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        throw new System.Data.RowNotInTableException("There is no current Oracle session.");
                    }
                }
            }
        }
    }
}
