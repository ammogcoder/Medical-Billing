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
    [Export(typeof(IClientDefView))]
    public partial class ClientDefViewXX : UserControl, IClientDefView
    {
        private readonly Lazy<ClientDefViewModel> viewModel;

        public ClientDefViewXX()
        {

            InitializeComponent();
            viewModel = new Lazy<ClientDefViewModel>(() => this.GetViewModel<ClientDefViewModel>());
        }


        public string MyTitle
        {
            get { return "Client Locations"; }
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

