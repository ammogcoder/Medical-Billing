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
    [Export(typeof(IAdminTatScheduleView))]
    public partial class AdminTatScheduleView : UserControl, IAdminTatScheduleView
    {
        private readonly Lazy<AdminTatScheduleViewModel> viewModel;

        public AdminTatScheduleView()
        {

            InitializeComponent();
            viewModel = new Lazy<AdminTatScheduleViewModel>(() => this.GetViewModel<AdminTatScheduleViewModel>());
            MsgBox.SetParent(MainGrid);
        }


        public string MyTitle
        {
            get { return "TatSched"; }
        }

        public void SetHeader(string header)
        {
            Behave.Header = header;
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

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool visValue = (bool)e.NewValue;
            if (e.NewValue != e.OldValue && visValue == true)
            {
                viewModel.Value.LoadScheds();

                /// Some controls use Dispatcher.Invoke/BeginInvoke internally. 
                /// By adding your task to the end of the Dispatcher queue, 
                /// you allow everything before it to finish up 
                /// (as long as your task is of equal or lower priority than everything else).
                //Dispatcher.BeginInvoke(new Action(() => { viewModel.Value.LoadScheds(); }));
            }

        }

        public void Pop()
        {
            this.popLinesx.IsOpen = true;
            return;
        }
    }
}

