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
using MedQuist.BillingAdmin.Presentation.Helpers;
using MedQuist.BillingAdmin.ViewModels;
using System.ComponentModel;
using Spheris.BillingAdmin.WpfUserControls;


namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>
    /// Interaction logic for ContractNoteForm.xaml
    /// </summary>
    public partial class DisplayNotes : Window
    {

        public DisplayNotes(IViewModelBase VM)
        {
            InitializeComponent();
            SaveChanges.SetParent(ModalDialogParent);

            DataContext = VM;

            SetCloseHandler();
            // When the ViewModel asks to be closed, close the window.
        }

        private void SetCloseHandler()
        {
            EventHandler handler = null;
            handler = delegate
            {
                // Avoid the Leak!
                (DataContext as IViewModelBase).RequestClose -= handler;
                Close();
            };
            (DataContext as IViewModelBase).RequestClose += handler;
        }

        override protected void OnClosing(CancelEventArgs e)
        {
            // if dirty
            if (SaveChanges.Modified == true)
            {
                if (SaveChanges.ShowHandlerDialog() == false)
                {
                    e.Cancel = true;
                    SetCloseHandler();
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
            else
                return;
        }
    }
}
