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
    [Export(typeof(IAdminLinksView))]
    public partial class AdminLinksView : UserControl, IAdminLinksView
    {
        private readonly Lazy<AdminLinksViewModel> viewModel;
        //private 

        public AdminLinksView()
        {

            InitializeComponent();
            viewModel = new Lazy<AdminLinksViewModel>(() => this.GetViewModel<AdminLinksViewModel>());
            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(AdminInView);
        }
 
        public string MyTitle
        {
            get
            {
                return "Administratinon Links";
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

        private void AdminInView(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool vis = (bool) e.NewValue;
            if(e.OldValue != e.NewValue && vis == true)
                viewModel.Value.ResetView();
        }
    }
}

