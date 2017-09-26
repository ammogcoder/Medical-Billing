using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.ComponentModel.Composition;
using System.Waf.Applications;

namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>
    /// Interaction logic for ContractRatesView.xaml
    /// </summary>
    [Export(typeof(IContractRateView))]
    public partial class ContractRatesView : UserControl,IContractRateView
    {
        private readonly Lazy<ContractRateViewModel> viewModel;

        public ContractRatesView()
        {
            InitializeComponent();
            viewModel = new Lazy<ContractRateViewModel>(() => this.GetViewModel<ContractRateViewModel>());
            MsgBox.SetParent( MainGrid );
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        { 
        }

        public string MyTitle
        {
            get
            {
                return "Contract Rates";
            }
        }

        public bool Pop()
        {
            return false;
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
