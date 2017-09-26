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
using System.Diagnostics;

namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>
    /// Interaction logic for InvoiceGroupView.xaml
    /// </summary>
    [Export(typeof(IInvoiceGroupView))]
    public partial class InvoiceGroupView : UserControl, IInvoiceGroupView
    {
        private readonly HashSet<ValidationError> errors = new HashSet<ValidationError>();
        public readonly Lazy<InvoiceGroupViewModel> viewModel;
        private ContractListViewModel privateContractListViewModel;
        private ContractListView privateContractListView;

        public InvoiceGroupView()
        {
            InitializeComponent();

            //in real mode
            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                object obj = this.GetViewModel<InvoiceGroupViewModel>();
                viewModel = new Lazy<InvoiceGroupViewModel>(() => this.GetViewModel<InvoiceGroupViewModel>());
                //Validation.AddErrorHandler(this.TheList, ErrorChangedHandler);

            }
            MsgBox.SetParent(MainGrid);

            EventManager.RegisterClassHandler(
                typeof(UserControl),
                ContractListView.ContractSelectedEvent,
                new ContractListView.ContractSelectedMsgHandler(ContractIdSelected));
        }

        public void ContractIdSelected(object sender,RoutedContractEventArgs e)
        {
            if (IsInView)
            {
                popLines.StaysOpen = false;
                popLines.IsOpen = false;
                viewModel.Value.ContractString = (e.ContractId == null) ? null : e.ContractId.ToString();
            }
        }

        public string ReferenceLazyModel()
        {
            //IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            return viewModel.Value.ContractString;
        }

        ///////////////////////////// ROUTED EVENT //////////////////////
        public static readonly RoutedEvent WindowAdjustedEvent =
            EventManager.RegisterRoutedEvent(
            "WindowMoved", RoutingStrategy.Bubble,
            typeof(WindowAdjustedEventHandler),
            typeof(UserControl));

        //add remove handlers
        public event WindowAdjustedEventHandler WindowMoved
        {
            add { AddHandler(WindowAdjustedEvent, value); }
            remove { RemoveHandler(WindowAdjustedEvent, value); }
        }

        private void UserControl_LayoutUpdated(object sender, EventArgs e)
        {
            WindowAdjustedEventArgs args = new WindowAdjustedEventArgs(WindowAdjustedEvent, ActualWidth);
            RaiseEvent(args);
        }
        ///////////////////////////// ROUTED EVENT //////////////////////
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
                    case InvoiceGroupViewModel.RouteTo.UsingClientDefDetailsView:
                        {
                            Debug.WriteLine("Raising event in InvoiceGroupView");
                            RoutedMsgEventArgs args = new RoutedMsgEventArgs(RoutedMsgEvent, viewModel.Value.DefId,viewModel.Value.DefGrpName);
                            RaiseEvent(args);
                        }
                        break;
                    case InvoiceGroupViewModel.RouteTo.UsingOverridesView:
                    case InvoiceGroupViewModel.RouteTo.UsingClientDefWorkTypesView:
                        {
                            
                                Debug.WriteLine("Raising event in InvoiceGroupView");
                                RoutedOverrideEventArgs args = new RoutedOverrideEventArgs(RoutedOverRideEvent, viewModel.Value.DefId, viewModel.Value.DefGrpName);
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
                viewModel.Value.ViewTip = "Select and Invoice for editing.";
                viewModel.Value.EnablePicker = true;
            }
        }


        public string MyTitle
        {
            get { return "Invoice Group Filter"; }
        }

        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
        }

        ///////////////////////////// ROUTED EVENT //////////////////////
        public delegate void RoutedMsgEventHandler(object sender, RoutedMsgEventArgs e);

        /// <summary>
        /// ///////////// ClentDefDetailsView Only
        /// </summary>
        public static readonly RoutedEvent RoutedMsgEvent =
            EventManager.RegisterRoutedEvent(
            "DefaultIdChanged", RoutingStrategy.Bubble,
            typeof(RoutedMsgEventHandler),
            typeof(UserControl));

        //add remove handlers
        public event RoutedMsgEventHandler DefaultIdChanged
        {
            add { AddHandler(RoutedMsgEvent, value); }
            remove { RemoveHandler(RoutedMsgEvent, value); }
        }


        ///////////////////////////// ROUTED EVENT //////////////////////
        public delegate void RoutedOverrideEventHandler(object sender, RoutedOverrideEventArgs e);
        /// <summary>
        /// ///////////// ClentDefWorkTypeView Only
        /// </summary>
        public static readonly RoutedEvent RoutedOverRideEvent =
            EventManager.RegisterRoutedEvent(
            "OverrideIdChanged", RoutingStrategy.Bubble,
            typeof(RoutedOverrideEventHandler),
            typeof(UserControl));

        //add remove handlers
        public event RoutedOverrideEventHandler OverrideIdChanged
        {
            add { AddHandler(RoutedOverRideEvent, value); }
            remove { RemoveHandler(RoutedOverRideEvent, value); }
        }


        public bool IsInView
        {
            get
            {
                return IsVisible;
            }
        }


        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsVisible && privateContractListView == null)
            {
                privateContractListView = new ContractListView();
                privateContractListViewModel = new ContractListViewModel(privateContractListView);
                viewModel.Value.ContractPopped = privateContractListView;
                //viewModel.Value.invoiceGroupViewModel = privateContractListViewModel;
                privateContractListView.DataContext = privateContractListViewModel;
                privateContractListViewModel.RoutingTo = ContractListViewModel.RouteTo.UsingInvoiceGroupView;

            }
        }

        private void Contract_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popLines.StaysOpen = true;
            popLines.IsOpen = true;
        }


    }
}
