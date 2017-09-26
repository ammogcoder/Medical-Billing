
using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(ITatScheduleView))]
    public partial class TatScheduleView : System.Windows.Controls.UserControl,ITatScheduleView
    {
        private readonly Lazy<TatScheduleViewModel> viewModel;

        public TatScheduleView()
        {

            InitializeComponent();
            viewModel = new Lazy<TatScheduleViewModel>(() => this.GetViewModel<TatScheduleViewModel>());

            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get
            {
                return "ContractTatSchedule";
            }
        }


        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
        }

        private void windowsFormsHost1_Loaded(object sender, RoutedEventArgs e)
        {


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

