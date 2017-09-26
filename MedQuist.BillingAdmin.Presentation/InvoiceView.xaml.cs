using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IInvoiceView))]
    public partial class InvoiceView : UserControl, IInvoiceView
    {
        private readonly Lazy<InvoiceViewModel> viewModel;

        public InvoiceView()
        {

            InitializeComponent();
            viewModel = new Lazy<InvoiceViewModel>(() => this.GetViewModel<InvoiceViewModel>());
        }

        private void InvoiceView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        public string MyTitle
        {
            get { return "Invoice Grouping"; }
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

