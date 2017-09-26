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
using System.Threading;
using System.Windows.Threading;

namespace MedQuist.BillingAdmin.Presentation
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MedQuist.BillingAdmin.Presentation"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MedQuist.BillingAdmin.Presentation;assembly=MedQuist.BillingAdmin.Presentation"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : Control
    {
        public class MyWpfObject : DispatcherObject
        {
            public void DoSomething()
            {
                VerifyAccess();

                // Do some work
            }

            public void DoSomethingElse()
            {
                if (CheckAccess())
                {
                    // Something, only if called 
                    // on the right thread
                }
            }
        }


        static CustomControl1()
        {
            TextBox statusText = new TextBox();
            // The Work to perform on another thread
            ThreadStart start = delegate()
            {
                // ...

                // This will throw an exception 
                // (it's on the wrong thread)
                statusText.Text = "From Other Thread";
            };

            // Create the thread and kick it started!
            new Thread(start).Start();
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }
    }
}
