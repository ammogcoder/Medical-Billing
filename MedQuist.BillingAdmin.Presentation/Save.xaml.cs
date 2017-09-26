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
using System.Windows.Shapes;

namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Save : Window
    {
        public enum States
        {
            SaveIt = 0,
            NoSave = 1,
            Cancel = 2
        }
        public States State
        {
            get;
            set;
        }

        public Save() 
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            this.State = States.SaveIt;
            Close();
        }

        private void nobutton_Click(object sender, RoutedEventArgs e)
        {
            this.State = States.NoSave;
            Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.State = States.Cancel;
            Close();

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
