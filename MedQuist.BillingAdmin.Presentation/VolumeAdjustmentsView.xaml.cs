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
    [Export(typeof(IVolumeAdjustmentsView))]
    public partial class VolumeAdjustmentsView : System.Windows.Controls.UserControl,IVolumeAdjustmentsView
    {
        private readonly Lazy<VolumeAdjustmentsViewModel> viewModel;

        public VolumeAdjustmentsView()
        {

            InitializeComponent();
            viewModel = new Lazy<VolumeAdjustmentsViewModel>(() => this.GetViewModel<VolumeAdjustmentsViewModel>());

            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get
            {
                return "VolumeAdjustments";
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

