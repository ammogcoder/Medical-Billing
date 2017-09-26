using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{
    public struct UserItem
    {
        /// <summary>
        /// The InvoiceGrpId of the user.
        /// </summary>
        public string ID;

        /// <summary>
        /// The user name of the user.
        /// </summary>
        public string UserName;

        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name;

        /// <summary>
        /// The status of the user's account.
        /// </summary>
        public string AccountStatus;

        /// <summary>
        /// The date/time that the user's account was locked.
        /// </summary>
        public DateTime? AccountLocked;

        /// <summary>
        /// The date when the user's account will expire.
        /// </summary>
        public DateTime? AccountExpires;

        /// <summary>
        /// The database the user is currently using.
        /// </summary>
        public string CurrentDatabase;

        /// <summary>
        /// The InvoiceGrpId of the current database session.
        /// </summary>
        public string SessionID;

        /// <summary>
        /// The InvoiceGrpId of the user's terminal.
        /// </summary>
        public string TerminalID;

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email;

        /// <summary>
        /// The roles the user has been granted.
        /// </summary>
        public List<string> Roles;
    }

    public abstract class UserDal : DatabaseObject<UserItem>
    {
        #region Private Members

        private UserItem _currentUser;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get user identified by the current config settings.
        /// </summary>
        /// <returns>A UserItem structure.</returns>
        public abstract UserItem GetCurrentUser();

        /// <summary>
        /// Login using the supplied credentials.
        /// </summary>
        /// <param name="userName">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="connectionInfo">The connection string or connection string name from the config file to connect with.</param>
        /// <returns>A UserItem structure.</returns>
        public abstract UserItem GetUser(string userName, string password, string connectionInfo);

        /// <summary>
        /// Changes the current user's password.
        /// </summary>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The user's new password.</param>
        public abstract void ChangePassword(string currentPassword, string newPassword);

        /// <summary>
        /// Changes a user's password
        /// </summary>
        /// <param name="userName">The user's username.</param>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The user's new password.</param>
        public abstract void ChangePassword(string userName, string currentPassword, string newPassword);
        
        #endregion

        #region Public Properties

        /// <summary>
        /// The current user of the application.
        /// </summary>
        public UserItem CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        #endregion

    }
}
