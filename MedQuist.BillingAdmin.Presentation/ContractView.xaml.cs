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
    [Export(typeof(IContractView))]
    public partial class ContractView : UserControl, IContractView
    {
        private readonly Lazy<ContractViewModel> viewModel;

        public ContractView()
        {
            InitializeComponent();
            viewModel = new Lazy<ContractViewModel>(() => this.GetViewModel<ContractViewModel>());
        }

        private void ContractView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        public string MyTitle
        {
            get { return "Contract"; }
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

