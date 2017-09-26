using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Waf.Applications;
using Spheris.Billing.Core.Domain;
using System.Windows;

namespace MedQuist.ViewModels.Views
{
    public interface IInvoiceGrpDetailView : IView
    {
        void UpdateCB();
        Window ShellWindow { get; set; }
        bool Modified { get; set; }
        int AskForSave();
        bool Pop();
    }
}
