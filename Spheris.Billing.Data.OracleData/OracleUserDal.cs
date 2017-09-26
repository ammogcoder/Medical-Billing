using Spheris.Billing.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;


namespace Spheris.Billing.Data.OracleData
{
    public class OracleUserDal : UserDal
    {
        public override void ChangePassword(string currentPassword, string newPassword)
        {
            UserItem user = GetCurrentUser();
            ChangePassword(user.UserName, currentPassword, newPassword);
        }

        public override void ChangePassword(string userName, string currentPassword, string newPassword)
        {
            // Validate that a username is passed.
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName", "The user name was not specified.");
            }

            // Make sure the newPassword is not blank.
            if (String.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException("newPassword", "The new password cannot be blank.");
            }

            // Make sure the current password is valid.
            ConnectionString cs = new ConnectionString(base.ConnectionString.Value);
            cs.SetSegmentValue("User InvoiceGrpId", userName);
            cs.SetSegmentValue("Password", currentPassword);
            using (OracleConnection cnn = new OracleConnection(cs.Value))
            {
                try
                {
                    cnn.Open();
                }
                catch (OracleException ex)
                {
                    switch (ex.Code)
                    {
                        case 1017:     // Invalid login.
                            throw new ArgumentOutOfRangeException("The current user name or password is invalid.", ex);
                        default:
                            throw;
                    }
                }
                catch
                {
                    throw;
                }

                // Change the password.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "declare " +
                                      "     v_current_user  varchar(20); " +
                                      "     v_sql varchar(200); " +
                                      "begin " +
                                      "     select  user into v_current_user " +
                                      "     from    global_name; " +
                                      "     v_sql := 'alter user ' || v_current_user || ' identified by \"" + newPassword + "\" replace \"" + currentPassword + "\"';" +
                                      "     execute immediate v_sql; " +
                                      "end;";
                    // HACK: Eliminate SQL injection possibility by figuring out why this will not take a parameter.
                    //                OracleHelper.AddCommandParameter(cmd, ":pwd", newPassword, OracleType.VarChar);
                    cmd.Connection = cnn;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        switch (ex.Code)
                        {
                            case 28003:
                                throw new ArgumentOutOfRangeException("New Password", String.Format("The new password is not valid.\r\n"
                                                                     + "It must be at least 7 characters long and contain at least one digit, one character, and one punctuation.\r\n"
                                                                     + "It should differ by at least 3 characters from your previous password."));
                        }
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            }
        }

        public override UserItem GetCurrentUser()
        {
            UserItem user = new UserItem();

            using (OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = String.Format("select  username, user_id, account_status, lock_date, expiry_date "
                                              + "       ,global_name, userenv('terminal') as terminal, userenv('sessionID') as sessionID "
                                              + "from    user_users, global_name");
                cnn.Open();
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        user.ID = dr["user_id"].ToString();
                        user.UserName = dr["username"].ToString();
                        user.AccountStatus = dr["account_status"].ToString();
                        if (dr["lock_date"] != DBNull.Value)
                        {
                            user.AccountLocked = DateTime.Parse(dr["lock_date"].ToString());
                        }
                        if (dr["expiry_date"] != DBNull.Value)
                        {
                            user.AccountExpires = DateTime.Parse(dr["expiry_date"].ToString());
                        }
                        user.CurrentDatabase = dr["global_name"].ToString();
                        user.TerminalID = dr["terminal"].ToString();
                        user.SessionID = dr["sessionID"].ToString();
                    }
                    else
                    {
                        throw new System.Data.RowNotInTableException("There is no current Oracle session.");
                    }
                }
                cnn.Close();
                user.Roles = GetUserRoles();
                return user;
            }
        }

        public override UserItem GetUser(string userName, string password, string connectionInfo)
        {
            throw new NotImplementedException();
        }

        private List<string> GetUserRoles()
        {
            List<string> roles = new List<string>();

            using (OracleConnection cnn = new OracleConnection(base.ConnectionString.Value))
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select  granted_role "
                                + "from    user_role_privs "
                                + "order by granted_role";
                cnn.Open();
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        roles.Add(dr["granted_role"].ToString());
                    }
                }
                cnn.Close();
                return roles;
            }
        }
    }
}
