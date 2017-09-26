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
    [Export(typeof(IClientDefWorkTypeView))]
    public partial class ClientDefWorkTypeView : UserControl, IClientDefWorkTypeView
    {
        private readonly Lazy<ClientDefWorkTypeViewModel> viewModel;

        public ClientDefWorkTypeView()
        {

            InitializeComponent();
            viewModel = new Lazy<ClientDefWorkTypeViewModel>(() => this.GetViewModel<ClientDefWorkTypeViewModel>());

            //InvoiceGrpIdLabel.En.IsVisible = false;
            //InvoiceGrpIdTextBox.IsVisible = false;
            MsgBox.SetParent(MainGrid);

            // Will Pair up with InvoiceGroupView/InvoiceGroupViewModel
            EventManager.RegisterClassHandler(
                typeof(UserControl),
                InvoiceGroupView.RoutedOverRideEvent,
                new MedQuist.BillingAdmin.Presentation.InvoiceGroupView.RoutedOverrideEventHandler(OverrideIdChanged));

        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (viewModel != null && viewModel.Value != null && IsInView)
            {
                invoiceGroupView.DataContext = viewModel.Value.invoiceGrpViewModel;
                (invoiceGroupView.DataContext as InvoiceGroupViewModel).RoutingTo = InvoiceGroupViewModel.RouteTo.UsingClientDefWorkTypesView;
            }
        }
        

        // The DefaultIdChanged from the other view.
        public void OverrideIdChanged(object sender,RoutedOverrideEventArgs e)
        {
            Debug.WriteLine("OverrideIdChanged hit at ClientDefWorkTypeView");

            if (e.Defid < 0)
            {
                viewModel.Value.SetOverRideGrp(-1,string.Empty);
            }
            else
            {
                viewModel.Value.SetOverRideGrp(e.Defid, e.DefGrpName);

                this.InvoiceGrpIdTextBox.Text = e.Defid.ToString();
                InvoiceGrpName.Text = e.DefGrpName;
            }
            this.popLines.IsOpen = false;
        }

        public string MyTitle
        {
            get
            {
                return "Work Types";
            }
        }

        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
        }


        public void SearchInvoicesByContract(decimal? invoiceId)
        {
            this.popLines.DataContext = viewModel.Value.invoiceGrpViewModel;
            viewModel.Value.invoiceGrpViewModel.SearchInvoicesByContract(invoiceId);
            this.popLines.IsOpen = true;
            //if (!(this.invoiceGroupView.DataContext is InvoiceGroupViewModel) && viewModel.Value != null)
            //    this.invoiceGroupView.DataContext = viewModel.Value.invoiceGrpViewModel;
            //(invoiceGroupView.DataContext as InvoiceGroupViewModel).SearchInvoicesByContract(invoiceId);
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

