using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Input;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IClientDefDetailsView))]
    public partial class ClientDefDetailsView : UserControl, IClientDefDetailsView
    {
        private readonly Lazy<ClientDefDetailsViewModel> viewModel;
        //private 

        public ClientDefDetailsView()
        {

            InitializeComponent();
            viewModel = new Lazy<ClientDefDetailsViewModel>(() => this.GetViewModel<ClientDefDetailsViewModel>());
            MsgBox.SetParent(MainGrid);

            EventManager.RegisterClassHandler(
                typeof(UserControl),
                InvoiceGroupView.RoutedMsgEvent,
                new MedQuist.BillingAdmin.Presentation.InvoiceGroupView.RoutedMsgEventHandler(MsgChanged));

        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (viewModel != null && viewModel.Value != null)
            {
                invoiceGroupView.DataContext = viewModel.Value.invoiceGrpViewModel;
                (invoiceGroupView.DataContext as InvoiceGroupViewModel).RoutingTo = InvoiceGroupViewModel.RouteTo.UsingClientDefDetailsView;
            }
        }


        // The DefaultIdChanged from the other view.
        public void MsgChanged(object sender, RoutedMsgEventArgs e)
        {
            Debug.WriteLine("MsgChanged hit at ClientDefDetailsView");
            viewModel.Value.SetDefaultInvoiceGroup(e.Defid,e.DefName);
            popLines.IsOpen = false;
        }

        public string MyTitle
        {
            get
            {
                return "Client Locations";
            }
        }

        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
        }


        public bool Pop()
        {
            popLines.DataContext = (DataContext as ClientDefDetailsViewModel).invoiceGrpViewModel;
            (DataContext as ClientDefDetailsViewModel).invoiceGrpViewModel.SearchString = DefaultInvoiceId.Text.ToString();
            this.popLines.IsOpen = true;
            return false;
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

