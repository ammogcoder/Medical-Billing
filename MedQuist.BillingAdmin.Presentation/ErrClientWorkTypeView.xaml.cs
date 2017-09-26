using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IErrClientWorkTypeView))]
    public partial class ErrClientWorkTypeView : UserControl, IErrClientWorkTypeView
    {
        private readonly Lazy<ErrClientWorkTypeViewModel> viewModel;

        public ErrClientWorkTypeView()
        {

            InitializeComponent();
            viewModel = new Lazy<ErrClientWorkTypeViewModel>(() => this.GetViewModel<ErrClientWorkTypeViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get { return "Client and Work type errors"; }
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


