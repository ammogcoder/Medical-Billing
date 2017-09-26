using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MedQuist.BillingAdmin.Presentation.CustomEventArgs
{
    public class WindowAdjustedEventArgs : RoutedEventArgs
    {
        public double MyWidth { get; private set; }

        public WindowAdjustedEventArgs(RoutedEvent routedEvent, double width ) : base(routedEvent)
        {
            this.MyWidth = width;
        }
    }
}
