using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Spheris.Common
{
    public static class AppHelper
    {
        public static bool IsFirstInstance()
        {
            bool firstInstance = false;
            string safeName = @"Global\" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            Mutex mutex = new Mutex(true, safeName, out firstInstance);
            return firstInstance;
        }
    }
}
