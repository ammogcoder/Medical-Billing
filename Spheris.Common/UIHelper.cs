using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Spheris.Common
{
    public static class UIHelper
    {
        public static void InitializeWpfFormInterop(Window w)
        {
            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(w);
        }
    }
}
