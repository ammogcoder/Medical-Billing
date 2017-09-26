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
using System.Windows.Threading;
using System.Threading;

namespace SpherisConstrols
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DiscardControl : UserControl
    {
        public DiscardControl()
		{
			InitializeComponent();
			Visibility = Visibility.Hidden;
		}

        private bool _hideRequest = false;
        private UIElement _parent;

		public void SetParent(UIElement parent)
		{
			_parent = parent;
		}


        public bool ShowHandlerDialog( )
        {
            Visibility = Visibility.Visible;

            _parent.IsEnabled = false;
            IsEnabled = true;
            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return IsDiscard;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
        }

        static FrameworkPropertyMetadata propertymetadata = 
            new FrameworkPropertyMetadata(false, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, 
                new PropertyChangedCallback(MyVariable_PropertyChanged), 
                new CoerceValueCallback(MyVariable_CoerceValue),
                false, UpdateSourceTrigger.PropertyChanged);


        public static readonly DependencyProperty MyVariableProperty = DependencyProperty.Register("Modified", 
            typeof(bool?), typeof(DiscardControl),
            propertymetadata, new ValidateValueCallback(MyVariable_Validate));



        private static void MyVariable_PropertyChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            //To be called whenever the DP is changed.
            //MessageBox.Show(string.Format("Property changed is fired : OldValue {0} NewValue : {1}", e.OldValue, e.NewValue));
        }



        private static object MyVariable_CoerceValue(DependencyObject dobj, object Value)
        {
            //called whenever dependency property value is reevaluated. The return value is the
            //if (Value as bool? == true)
            //{
            //    DiscardControl thisWindow = dobj as DiscardControl;
            //    thisWindow.ShowHandlerDialog();
            //}
            //latest value set to the dependency property
            //MessageBox.Show(string.Format("CoerceValue is fired : Value {0}", Value));
            return Value;
        }



        private static bool MyVariable_Validate(object Value)
        {
            //Custom validation block which takes in the value of DP
            //Returns true / false based on success / failure of the validation
            //MessageBox.Show(string.Format("DataValidation is Fired : Value {0}", Value));
            return true;
        }


        public bool? Modified
        {
            get
            {
                return this.GetValue(MyVariableProperty) as bool?;
            }
            set
            {
                this.SetValue(MyVariableProperty, value);
            }
        }

        public bool IsDiscard { get; set; }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            IsDiscard = true;
            HideHandlerDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            IsDiscard = false;
            HideHandlerDialog();
        }
    }
}
