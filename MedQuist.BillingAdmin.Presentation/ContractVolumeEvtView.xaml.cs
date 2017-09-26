
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
    [Export(typeof(IContractVolumeEvtView))]
    public partial class ContractVolumeEvtView : UserControl, IContractVolumeEvtView
    {
        private readonly Lazy<ContractVolumeEvtViewModel> viewModel;

        public ContractVolumeEvtView()
        {

            InitializeComponent();
            viewModel = new Lazy<ContractVolumeEvtViewModel>(() => this.GetViewModel<ContractVolumeEvtViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get
            {
                return "Volume Based Adjustments";
            }
        }


        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
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

