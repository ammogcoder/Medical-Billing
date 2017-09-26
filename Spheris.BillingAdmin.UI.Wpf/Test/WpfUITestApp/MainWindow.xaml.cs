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

//using Spheris.Billing.Core.Domain;
using Spheris.BillingAdmin.UI.Wpf.ViewModels;
using Spheris.BillingAdmin.UI.Wpf.Views;


namespace WpfUITestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DetailInvoiceFileModel model = new DetailInvoiceFileModel();
            //Close();

//#if ASDF
            ContractNotesViewModel VM = new ContractNotesViewModel(1, "Frank");
            DisplayNotes mainWindow = new DisplayNotes(VM);

            mainWindow.ShowDialog();
//#endif

        }
    }
}
