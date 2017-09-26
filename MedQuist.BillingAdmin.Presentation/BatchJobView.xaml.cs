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
    [Export(typeof(IBatchJobView))]
    public partial class BatchJobView : UserControl, IBatchJobView
    {
        private readonly Lazy<BatchJobViewModel> viewModel;

        public BatchJobView()
        {

            InitializeComponent();
            viewModel = new Lazy<BatchJobViewModel>(() => this.GetViewModel<BatchJobViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get { return "Batch Jobs"; }
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

