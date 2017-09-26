using Spheris.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    /// <summary>
    /// Abstract base class used as the basis for any Data Access Layer class.
    /// </summary>
    /// <typeparam name="T">The row structure of the DAL object.</typeparam>
    public abstract class DatabaseObject<T> : IDisposable
    {
        public DatabaseObject() { }
        
        #region Private Fields

        protected ConnectionString _connectionString;
        protected string _schemaName;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Connection string to the data source.
        /// </summary>
        public virtual ConnectionString ConnectionString 
        { 
            get
            {
#if MSACCESS
                return "DSN=MedQuist;UID=;PWD=";
#else
                foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
                {
                    bool lookat = true;
                }

                // HACK: This is to allow interop until everything is converted over to the new DAL framework.
                if (!String.IsNullOrEmpty(DalBridge.ConnectionBridge.Value))
                {
                    _connectionString = DalBridge.ConnectionBridge;
                }
                
                if (_connectionString == null)
                {
                    // Get the connection string name from config.
                    string connStringName = "";
                    try
                    {
                        connStringName = ConfigurationManager.AppSettings["BillingDatabase"].ToString();
                    }
                    catch (NullReferenceException)
                    {
                        throw new NullReferenceException("The 'BillingDatabase' key is missing from the application configuration file.");
                    }

                    if (String.IsNullOrEmpty(connStringName))
                    {
                        throw new System.Configuration.SettingsPropertyNotFoundException("'BillingDatabase' setting not defined in the config file.");
                    }

                    foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
                    {
                        bool lookat = true;
                    }
                    if (ConfigurationManager.ConnectionStrings[connStringName] != null)
                    {
                    string str = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;

                    // Get the connection string from the config.
                        _connectionString = new ConnectionString(Encryption.DecryptConnectionStringPassword(str));

                    if (String.IsNullOrEmpty(_connectionString.Value))
                    {
                        throw new System.Configuration.SettingsPropertyNotFoundException(String.Format("'{0}' connection string not defined in the config file.", connStringName));
                    }
                }
                }
                return _connectionString;
#endif
            }
            set
            {
#if MSACCESS
                _connectionString = "DSN=MedQuist;UID=;PWD=";
#else
                _connectionString = value;   
#endif
            }
        }

        public virtual IDbConnection OpenConnection(IDbConnection cnn = null)
        {
            if (cnn != null && cnn.State == ConnectionState.Closed) cnn.Open();
            return cnn;
        }

        /// <summary>
        /// The name of the schema that holds the billing database objects.
        /// </summary>
        public virtual string SchemaName 
        {
            get
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = ConfigurationManager.AppSettings["DatabaseSchemaName"].ToString();
                    if (string.IsNullOrEmpty(_schemaName))
                    {
                        throw new System.Configuration.SettingsPropertyNotFoundException("'DatabaseSchemaName' setting not defined in the config file.");
                    }
                }
                return _schemaName;
            }
            set
            {
                _schemaName = value;
            }
        }

        protected dynamic CheckNull(dynamic value, dynamic defaultValue = null)
        {
            if (value == null)
            {
                if (defaultValue != null)
                {
                    return defaultValue;
                }
                else
                {
                    return DBNull.Value;
                }
            }
            else
            {
                return value;
            }
        }

        #endregion

        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources

            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DatabaseObject()
        {
            Dispose(false);
        }

        #endregion
    }
}
