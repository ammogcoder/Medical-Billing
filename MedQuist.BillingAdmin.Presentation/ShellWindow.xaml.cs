using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;
using CustomControls;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IShellView))]
    public partial class ShellWindow : CustomWindow, IShellView
    {
        private readonly Lazy<ShellViewModel> viewModel;

        public ShellWindow()
        {
            InitializeComponent();

            LoginView lv = new LoginView();
            
            lv.ShowDialog();
            if (lv.DialogResult.HasValue &&
            !lv.DialogResult.Value)
            {
                Application.Current.Shutdown();
            }


            viewModel = new Lazy<ShellViewModel>(() => this.GetViewModel<ShellViewModel>());

            MsgBox.SetParent(MainGrid);
            //Wire up the List was adjusted Event, which will come from the 
            //baseControl on the AdornerLayer
            EventManager.RegisterClassHandler(
                typeof(UserControl),
                InvoiceGroupView.WindowAdjustedEvent,
                new WindowAdjustedEventHandler( WindowWasMoved ));

        }

        public void WindowWasMoved(object sender, WindowAdjustedEventArgs e)
        {
            //ActiveSection.Width = e.MyWidth;
            //e.Handled = true;
        }

        //Window IShellView.ShellWindow
        //{
        //    get
        //    {
        //        return this;
        //    }
        //}

        public string MyTitle
        {
            get { return "Main"; }
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
