using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.OracleData
{
    public static class OracleHelper
    {
        #region CreateParameter()

        public static OracleParameter CreateParameter(string parameterName, object value, OracleType type, int size, ParameterDirection direction)
        {
            OracleParameter prm = new OracleParameter(parameterName, type);
            if (size > 0)
            {
                prm.Size = size;
            }
            prm.Value = value;
            prm.Direction = direction;
            return prm;
        }

        public static OracleParameter CreateParameter(string parameterName, object value, OracleType type, ParameterDirection direction)
        {
            return CreateParameter(parameterName, value, type, 0, direction);
        }

        public static OracleParameter CreateParameter(string parameterName, OracleType type, ParameterDirection direction)
        {
            return CreateParameter(parameterName, null, type, 0, direction);
        }

        public static OracleParameter CreateParameter(string parameterName, object value, OracleType type, int size)
        {
            return CreateParameter(parameterName, value, type, size, ParameterDirection.Input);
        }

        public static OracleParameter CreateParameter(string parameterName, object value, OracleType type)
        {
            return CreateParameter(parameterName, value, type, 0, ParameterDirection.Input);
        }

        #endregion

        #region AddCommandParameter()

        public static void AddCommandParameter(IDbCommand cmd, string parameterName, object value, OracleType type, int size, ParameterDirection direction)
        {
            cmd.Parameters.Add(CreateParameter(parameterName, value, type, size, direction));
        }

        public static void AddCommandParameter(IDbCommand cmd, string parameterName, object value, OracleType type, ParameterDirection direction)
        {
            AddCommandParameter(cmd, parameterName, value, type, 0, direction);
        }

        public static void AddCommandParameter(IDbCommand cmd, string parameterName, OracleType type, ParameterDirection direction)
        {
            AddCommandParameter(cmd, parameterName, null, type, 0, direction);
        }

        public static void AddCommandParameter(IDbCommand cmd, string parameterName, object value, OracleType type, int size)
        {
            AddCommandParameter(cmd, parameterName, value, type, size, ParameterDirection.Input);
        }

        public static void AddCommandParameter(IDbCommand cmd, string parameterName, object value, OracleType type)
        {
            AddCommandParameter(cmd, parameterName, value, type, 0, ParameterDirection.Input);
        }

        #endregion

        #region ExecuteNonQuery()

        public static OracleParameterCollection ExecuteNonQuery(string connectionString, string sql, OracleParameter[] parameters)
        {
            using (OracleConnection cnn = new OracleConnection(connectionString))
            {
                return ExecuteNonQuery(cnn, sql, parameters);
            }
        }

        public static OracleParameterCollection ExecuteNonQuery(OracleConnection cnn, string sql, OracleParameter[] parameters)
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                cmd.Parameters.AddRange(parameters);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                // Return any output parameters.
                OracleParameterCollection outParams = new OracleParameterCollection();
                foreach (OracleParameter p in cmd.Parameters)
                {
                    if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                    {
                        outParams.Add(p);
                    }
                }
                return outParams;
            }
        }

        #endregion

        #region ExecuteQuery()

        public static DataTable ExecuteQuery(string connectionString, string sql, OracleParameter[] parameters)
        {
            return ExecuteQuery(connectionString, sql, parameters, 180);
        }

        public static DataTable ExecuteQuery(string connectionString, string sql, OracleParameter[] parameters, int commandTimeout)
        {
            using (OracleConnection cnn = new OracleConnection(connectionString))
            {
                cnn.Open();
                return ExecuteQuery(cnn, sql, parameters, commandTimeout);
            }
        }

        public static DataTable ExecuteQuery(OracleConnection cnn, string sql, OracleParameter[] parameters)
        {
            return ExecuteQuery(cnn, sql, parameters, 180);
        }

        public static DataTable ExecuteQuery(OracleConnection cnn, string sql, OracleParameter[] parameters, int commandTimeout)
        {
            DataTable dt = new DataTable();
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = commandTimeout;
                cmd.Connection = cnn;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                dt.Load(cmd.ExecuteReader());
            }
            return dt;
        }

        #endregion
    }
}
