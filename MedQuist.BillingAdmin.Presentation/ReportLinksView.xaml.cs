using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IReportLinksView))]
    public partial class ReportLinksView : UserControl, IReportLinksView
    {
        private readonly Lazy<ReportLinksViewModel> viewModel;
        //private 

        public ReportLinksView()
        {

            InitializeComponent();
            viewModel = new Lazy<ReportLinksViewModel>(() => this.GetViewModel<ReportLinksViewModel>());
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(
            ReportsInView);
        }
 
        public string MyTitle
        {
            get
            {
                return "Report Links";
            }
        }

        public void ShowMsg(string errMsg)
        {
 
        }
 

        public bool IsInView
        {
            get
            {
                return IsVisible;
            }
        }

        private void ReportsGotFocus(object sender, RoutedEventArgs e)
        {
            viewModel.Value.ResetView();
        }

        private void ReportsInView(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool vis = (bool) e.NewValue;
            if(e.OldValue != e.NewValue && vis == true)
                viewModel.Value.ResetView();
        }
    }
}

