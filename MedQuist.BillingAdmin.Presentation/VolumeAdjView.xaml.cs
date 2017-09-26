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
    [Export(typeof(IVolumeAdjView))]
    public partial class VolumeAdjView : UserControl, IVolumeAdjView
    {
        private readonly Lazy<VolumeAdjViewModel> viewModel;

        public VolumeAdjView()
        {

            InitializeComponent();
            viewModel = new Lazy<VolumeAdjViewModel>(() => this.GetViewModel<VolumeAdjViewModel>());
        }


        public string MyTitle
        {
            get
            {
                return "Add On Charges";
            }
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

