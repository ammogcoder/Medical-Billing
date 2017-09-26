using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IAdministrationView))]
    public partial class AdministrationView : UserControl, IAdministrationView
    {
        private readonly Lazy<AdministrationViewModel> viewModel;

        public AdministrationView()
        {

            InitializeComponent();
            viewModel = new Lazy<AdministrationViewModel>(() => this.GetViewModel<AdministrationViewModel>());
        }


        public string MyTitle
        {
            get
            {
                return "Administration";
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

