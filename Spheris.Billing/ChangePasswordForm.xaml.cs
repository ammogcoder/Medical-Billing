using Spheris.Billing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spheris.Billing
{
    /// <summary>
    /// Interaction logic for ChangePasswordForm.xaml
    /// </summary>
    public partial class ChangePasswordForm: Window
    {
        public ChangePasswordForm()
        {
            InitializeComponent();

            this.Closing += new System.ComponentModel.CancelEventHandler(ChangePasswordForm_Closing);
            // Set the icon property.
            System.Drawing.Bitmap icon = global::Spheris.Billing.Resources.Locker;
            MemoryStream iconStream = new MemoryStream();
            icon.Save(iconStream, System.Drawing.Imaging.ImageFormat.Png);
            iconStream.Seek(0, SeekOrigin.Begin);
            BitmapFrame newIcon = BitmapFrame.Create(iconStream);
            this.Icon = newIcon;
        }

        void ChangePasswordForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //e.Cancel = true;
        }

        public string CurrentPassword
        {
            get
            {
                return this.txtCurrentPassword.Password;
            }
            set
            {
                this.txtCurrentPassword.Password = value;
            }
        }

        public string NewPassword
        {
            get
            {
                return this.txtNewPassword.Password;
            }
            set
            {
                this.txtNewPassword.Password = value;
                this.txtConfirmPassword.Password = value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            // Make sure the new passwords match.
            if(!String.Equals(this.txtNewPassword.Password, this.txtConfirmPassword.Password))
            {
                MessageBox.Show("The passwords do not match.", "Change Password", MessageBoxButton.OK);
                // TODO: Clear the current values for the new password.
                this.txtNewPassword.Password = "";
                this.txtConfirmPassword.Password = "";
                this.txtNewPassword.Focus();

                // Prompt the user for new passwords.
                return;
            }
            this.DialogResult = true;
            this.Hide();
        }

        private bool Validate()
        {
            bool isValid = true;

            if ((txtCurrentPassword.IsEnabled && String.IsNullOrEmpty(txtCurrentPassword.Password))  // Make sure the old password is provided (if required)
                && (txtNewPassword.Password != txtConfirmPassword.Password))   // Make sure the password and password validation match.

            {
                isValid = false;
            }
            btnOK.IsEnabled = isValid;
            return isValid;
        }

        private void txtCurrentPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }

        private void txtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }
    }
}
