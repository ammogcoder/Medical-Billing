using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IPasswordChangeView))]
    public partial class PasswordChangeView : UserControl, IPasswordChangeView
    {
        private readonly Lazy<PasswordChangeViewModel> viewModel;
        //private 

        public PasswordChangeView()
        {

            InitializeComponent();
            viewModel = new Lazy<PasswordChangeViewModel>(() => this.GetViewModel<PasswordChangeViewModel>());
        }
 
        public string MyTitle
        {
            get
            {
                return "Password Change";
            }
        }

        public void ShowMsg(string errMsg)
        {
 
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

