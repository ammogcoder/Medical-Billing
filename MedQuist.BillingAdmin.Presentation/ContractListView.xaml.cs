using System;
using System.ComponentModel.Composition;
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
using MedQuist.BillingAdmin.ViewModels;
using System.ComponentModel;
using MedQuist.ViewModels.Views;
using System.Waf.Applications;
using MedQuist.BillingAdmin.Presentation.CustomEventArgs;

namespace MedQuist.BillingAdmin.Presentation
{
    public delegate void WindowAdjustedEventHandler(object sender, WindowAdjustedEventArgs e);

    /// <summary>
    /// Interaction logic for ContractListView.xaml
    /// </summary>
    [Export(typeof(IContractListView))]
    public partial class ContractListView : UserControl, IContractListView
    {
        private readonly HashSet<ValidationError> errors = new HashSet<ValidationError>();
        private readonly Lazy<ContractListViewModel> viewModel;


        public ContractListView()
        {
            InitializeComponent();

            //in real mode
            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                viewModel = new Lazy<ContractListViewModel>(() => this.GetViewModel<ContractListViewModel>());
                Validation.AddErrorHandler(this.TheList, ErrorChangedHandler);

            }
        }

        ///////////////////////////// ROUTED EVENT //////////////////////
        public delegate void ContractSelectedMsgHandler(object sender, RoutedContractEventArgs e);

        public static readonly RoutedEvent ContractSelectedEvent =
            EventManager.RegisterRoutedEvent(
            "ContractIdPicked", RoutingStrategy.Bubble,
            typeof(ContractSelectedMsgHandler),
            typeof(UserControl));

        //add remove handlers
        public event ContractSelectedMsgHandler ContractIdPicked
        {
            add { AddHandler(ContractSelectedEvent, value); }
            remove { RemoveHandler(ContractSelectedEvent, value); }
        }


        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void ErrorChangedHandler(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                errors.Add(e.Error);
            }
            else
            {
                errors.Remove(e.Error);
            }

            viewModel.Value.IsValid = !errors.Any();
        }

       
        private void TheList_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        { 
            if (viewModel.Value != null)
            {
                switch (viewModel.Value.RoutingTo)
                {
                    case ContractListViewModel.RouteTo.UsingInvoiceGroupView:
                        {
                            RoutedContractEventArgs args = new RoutedContractEventArgs(ContractSelectedEvent, viewModel.Value.SelectedContractId);
                            RaiseEvent(args);

                        }
                    break;
                }
            }
        }

        private void EnablePicker()
        {
            if (viewModel.Value.Modified)
            {
                //TheList.ToolTip
                viewModel.Value.ViewTip = "Please Save or Restore your changes.";
                viewModel.Value.EnablePicker = false;
            }
            else
            {
                viewModel.Value.ViewTip = "Select a contract for editing.";
                viewModel.Value.EnablePicker = true;
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void UserControl_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }


        public string MyTitle
        {
            get { return "Contract"; }
        }




        public void ShowMsg(string errMsg)
        {
            throw new NotImplementedException();
        }

        public void SetContractId(int contractId)
        {
            viewModel.Value.SetByContractId(contractId);
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
