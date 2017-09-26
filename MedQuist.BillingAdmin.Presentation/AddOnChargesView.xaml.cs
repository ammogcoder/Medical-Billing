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
    [Export(typeof(IAddOnChargesView))]
    public partial class AddOnChargesView : UserControl, IAddOnChargesView
    {
        private readonly Lazy<AddOnChargesViewModel> viewModel;

        public AddOnChargesView()
        {

            InitializeComponent();
            viewModel = new Lazy<AddOnChargesViewModel>(() => this.GetViewModel<AddOnChargesViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get
            {
                return "Add On Charges";
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

