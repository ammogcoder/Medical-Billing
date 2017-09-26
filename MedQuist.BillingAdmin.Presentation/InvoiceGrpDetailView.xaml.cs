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
using System.ComponentModel.Composition;
using MedQuist.ViewModels.Views;
using System.ComponentModel;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;

//using SpherisConstrols;
using System.Windows.Controls.Primitives;
using MedQuist.BillingAdmin.ViewModels;

namespace MedQuist.BillingAdmin.Presentation
{

    /// <summary>
    /// Interaction logic for InvoiceGrpDetailView.xaml
    /// </summary>
    [Export(typeof(IInvoiceGrpDetailView))]
    public partial class InvoiceGrpDetailView : UserControl,IInvoiceGrpDetailView
    {
        private readonly HashSet<ValidationError> errors = new HashSet<ValidationError>();
        private readonly Lazy<InvoiceGrpDetailViewModel> viewModel;

        public InvoiceGrpDetailView()
        {
            
            InitializeComponent();
            if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                viewModel = new Lazy<InvoiceGrpDetailViewModel>(() => this.GetViewModel<InvoiceGrpDetailViewModel>());
                Validation.AddErrorHandler(this, ErrorChangedHandler);
            }
            this.KeyDown += OnKeyDown;
        }
        
        /*
        private ClientDefListViewModel clientDefListViewModel;
        private ClientDefListViewModel ThisClientDefListViewModel
        { 
            get
            {
                if(clientDefListViewModel == null)
                {
                    ClientDefListView cdf = new ClientDefListView();
                    clientDefListViewModel = new ClientDefListViewModel(
                }

        }
        */

        public static Window FindParentWindow(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            //CHeck if this is the end of the tree
            if (parent == null) return null;

            Window parentWindow = parent as Window;
            if (parentWindow != null)
            {
                return parentWindow;
            }
            else
            {
                //use recursion until it reaches a Window
                return FindParentWindow(parent);
            }
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
        }

        public void UpdateCB()
        {
            cbReportTypes.SetText();
        }


        public bool _Modified;
        public bool Modified
        {
            get
            {
                return _Modified;
            }
            set
            {
                _Modified = value;
            }
        }


        /// <summary>
        /// Apply Blur Effect on the window
        /// </summary>
        /// <param name="win"></param>
        private void ApplyEffect(Window win)
        {

            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 2;
            win.Effect = objBlur;
        }
        /// <summary>
        /// Remove Blur Effects
        /// </summary>
        /// <param name="win"></param>
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        public int AskForSave()
        {
            Window parent = FindParentWindow(this);
            ApplyEffect(parent);
            Save save = new Save();
            //Brush oldbrush =  parent.Background ;
            //Brush grayBrush = new Brush();
            
            //parent.Background = Brush. Colors.Gray;
                                            
            save.Owner = parent;
            save.ShowDialog();
            // parent.Background = oldbrush;
            ClearEffect(parent);

            return (int)save.State;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private Window _ShellWindow;
        public Window ShellWindow
        {
            get
            {
                return _ShellWindow;
            }
            set
            {
                _ShellWindow = value;
            }
        }


        public bool Pop()
        {
            this.popLines.IsOpen = true;
            return false;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Escape:
                    this.popLines.IsOpen = false;
                    break;
            }
        }



        public string MyTitle
        {
            get { return "Invoice Group Details"; }
        }


        public void ShowMsg(string errMsg)
        {
            throw new NotImplementedException();
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
