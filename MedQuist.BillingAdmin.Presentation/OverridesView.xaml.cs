
using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IOverridesView))]
    public partial class OverridesView : UserControl, IOverridesView
    {
        private readonly Lazy<OverridesViewModel> viewModel;
        InvoiceGroupView privateInvoiceGroupView;
        InvoiceGroupViewModel privateInvoiceGroupViewModel;

        public OverridesView()
        {

            InitializeComponent();
            viewModel = new Lazy<OverridesViewModel>(() => this.GetViewModel<OverridesViewModel>());
            MsgBox.SetParent(MainGrid);
            
            // Will Pair up with InvoiceGroupView/InvoiceGroupViewModel
            EventManager.RegisterClassHandler(
                typeof(UserControl),
                InvoiceGroupView.RoutedOverRideEvent,
                new MedQuist.BillingAdmin.Presentation.InvoiceGroupView.RoutedOverrideEventHandler(OverrideIdChanged));

        }


        // The DefaultIdChanged from the other view.
        public void OverrideIdChanged(object sender, RoutedOverrideEventArgs e)
        {
            if (IsInView)
            {
                viewModel.Value.SetOverride(e.Defid, e.DefGrpName);
                overRides.CommitEdit();
            }
        }

        public string MyTitle
        {
            get
            {
                return "Overrrides";
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


        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsVisible && privateInvoiceGroupView == null)
            {
                privateInvoiceGroupView = new InvoiceGroupView();
                privateInvoiceGroupViewModel = new InvoiceGroupViewModel(privateInvoiceGroupView);
                viewModel.Value.InvGroup = privateInvoiceGroupView;
                privateInvoiceGroupView.DataContext = privateInvoiceGroupViewModel;
                privateInvoiceGroupViewModel.RoutingTo = InvoiceGroupViewModel.RouteTo.UsingOverridesView;

            }

        }


    }
}

