using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IErrNoValidContractView))]
    public partial class ErrNoValidContractView : UserControl, IErrNoValidContractView
    {
        private readonly Lazy<ErrNoValidContractViewModel> viewModel;

        public ErrNoValidContractView()
        {

            InitializeComponent();
            viewModel = new Lazy<ErrNoValidContractViewModel>(() => this.GetViewModel<ErrNoValidContractViewModel>());
            MsgBox.SetParent(MainGrid);


        }


        public string MyTitle
        {
            get { return "No Valid Contract errors"; }
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

