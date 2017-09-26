using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Collections.Generic;
using System.Security;


namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>  
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    [Export(typeof(ILoginView))]
    public partial class LoginView : Window, ILoginView
    {
        bool Connected = false;

        public LoginView()
        {
            InitializeComponent();
            List<string> source = new List<string>();
#if !DEBUG
            source.Add("SBPROD");
#endif
#if DEBUG
            txtUserName.Text = "wlounsbury";
            txtPassword.Password = "Ch@ngeMe";
#endif
            source.Add("SBTEST");
            source.Add("SBDEV"); 
            
            this.database.ItemsSource = source;
            this.database.SelectedIndex = 0;
        }

        private bool Connect()
        {
            if (Connected)
            {
                return true;
            }
            try
            {
#if ORACLE
#if DEBUG
                DB.MakeOraConx("wlounsbury",
                    "Ch@ngeMe",
                    "SBTEST" );
#else
                DB.MakeOraConx(txtUserName.Text.ToString(), txtPassword.Password.ToString(), database.SelectionBoxItem.ToString());
#endif
#endif
            }
            catch
            {
                throw;
            }
            Connected = true;
            return true;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Connect())
            {
                // Write code here to authenticate user
                // If authenticated, then set DialogResult=true
                DialogResult = true;
                this.Close();
            }
        }

        public string MyTitle
        {
            get 
            { 
                return "Logging in"; 
            }
        }


        public void ShowMsg(string errMsg)
        {
            throw new NotImplementedException();
        }

        public bool IsInView
        {
            get
            {
                return IsVisible;
            }
        }
    }
}
