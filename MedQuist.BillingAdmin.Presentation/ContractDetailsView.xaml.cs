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

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IContractDetailsView))]
    public partial class ContractDetailsView : UserControl, IContractDetailsView
    {
        private readonly Lazy<ContractDetailsViewModel> viewModel;

        public ContractDetailsView()
        {
            InitializeComponent();
            viewModel = new Lazy<ContractDetailsViewModel>(() => this.GetViewModel<ContractDetailsViewModel>());
            this.KeyDown += OnKeyDown;
            MsgBox.SetParent(MainGrid);
#if DOESNT_MAKE_SENSE
            EventManager.RegisterClassHandler(
    typeof(UserControl),
    InvoiceGroupView.RoutedMsgEvent,
    new MedQuist.BillingAdmin.Presentation.InvoiceGroupView.RoutedMsgEventHandler(MsgChanged));
#endif
        }

#if DOESNT_MAKE_SENSE

        // The DefaultIdChanged from the other view.
        public void MsgChanged(object sender, RoutedMsgEventArgs e)
        {
            //Debug.WriteLine("MsgChanged hit at ClientDefDetailsView");
            viewModel.Value.SetDefaultInvoiceGroup(e.Defid);
            popLines.IsOpen = false;
        }
#endif
        public bool Pop()
        {
#if DOESNT_MAKE_SENSE
            popLines.DataContext = (DataContext as ContractDetailsViewModel).invoiceGrpViewModel;
            //(DataContext as ContractDetailsViewModel).invoiceGrpViewModel.SearchString = DefaultInvoiceId.Text.ToString();
#endif
            this.popLines.IsOpen = true;
            return false;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Escape:
                    this.popLines.IsOpen = false;
                    break;
            }
        }


        public string MyTitle
        {
            get
            {
                return "Contract Details";
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

