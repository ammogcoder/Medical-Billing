using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MedQuist.BillingAdmin.Presentation.CustomEventArgs
{
    public class RoutedMsgEventArgs : RoutedEventArgs
    {
        public int Defid { get; private set; }
        public string DefName { get; private set; }

        public RoutedMsgEventArgs(RoutedEvent routedEvent, int defId, string defName)
            : base(routedEvent)
        {
            this.DefName = defName;
            this.Defid = defId;
        }
    }
}