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
    /// <summary>
    /// Interaction logic for ClientDefListView.xaml
    /// </summary>
    [Export(typeof(IClientDefListView))]
    public partial class ClientDefListView : UserControl, IClientDefListView
    {
        /// <summary>
        /// Public for use by InvoiceDetailsView DataContext on popup
        /// </summary>
        public /*private */readonly Lazy<ClientDefListViewModel> viewModel;
        public ClientDefListView()
        {
            InitializeComponent();

            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                viewModel = new Lazy<ClientDefListViewModel>(() => this.GetViewModel<ClientDefListViewModel>());
            }
        }



        private void EnablePicker()
        {
            if (viewModel.Value.Modified)
            {
                viewModel.Value.ViewTip = "Please Save or Restore your changes.";
                viewModel.Value.EnablePicker = false;
            }
            else
            {
                viewModel.Value.ViewTip = "Select a ClientDef for editing.";
                viewModel.Value.EnablePicker = true;
            }
        }



        public string MyTitle
        {
            get { return "ClientDef"; }
        }

        public void ShowMsg(string errMsg)
        {
            throw new NotImplementedException();
        }



        public void FocusReset()
        {
            FocusManager.SetFocusedElement(this,Search);
            //Search.Focus();
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
