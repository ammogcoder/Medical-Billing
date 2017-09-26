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
    [Export(typeof(IInvoiceHistoryView))]
    public partial class InvoiceHistoryView : UserControl, IInvoiceHistoryView
    {
        private readonly Lazy<InvoiceHistoryViewModel> viewModel;

        public InvoiceHistoryView()
        {

            InitializeComponent();
            viewModel = new Lazy<InvoiceHistoryViewModel>(() => this.GetViewModel<InvoiceHistoryViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get
            {
                return "Invoice History";
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

