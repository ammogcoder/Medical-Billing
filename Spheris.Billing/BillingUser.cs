using Spheris.Common;
using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public class BillingUser
    {
        #region Private Fields

        private UserItem _user;

        #endregion

        #region Public Methods

        public void GetUser()
        {
            UserDal dal = GetDalInstance();
            _user = dal.GetCurrentUser();
        }

        public void ChangePassword()
        {
            Spheris.Billing.ChangePasswordForm frm = new Spheris.Billing.ChangePasswordForm();
            frm.ShowDialog();
        }

        public bool ChangePassword(string currentPassword, string newPassword)
        {
            // Make sure the new password isn't blank.
            if (String.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException(newPassword, "The new password cannot be blank.");
            }

            // Change the password.
            using (UserDal dal = GetDalInstance())
            {
                try
                {
                    dal.ChangePassword(currentPassword, newPassword);
                    return true;
                }
                catch
                {
                    return false;
                    throw;
                }
            }
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            // Make sure the user name isn't blank.
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(userName, "A username was not provided.");
            }

            // Make sure the new password isn't blank.
            if (String.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException(newPassword, "The new password cannot be blank.");
            }

            // Change the password.
            using (UserDal dal = GetDalInstance())
            {
                try
                {
                    dal.ChangePassword(userName, currentPassword, newPassword);
                    return true;
                }
                catch
                {
                    return false;
                    throw;
                }
            }
        }

        
        #endregion

        #region Private Methods

        private static UserDal GetDalInstance()
        {
            return BillingDataFactory.NewInstance().CreateUserDal();
        }

        /// <summary>
        /// Checks when the user's account will expire and gives them the option to reset their password if near expiration.
        /// </summary>
        private void CheckAccountExpiration()
        {

        }

        #endregion
    }
}
