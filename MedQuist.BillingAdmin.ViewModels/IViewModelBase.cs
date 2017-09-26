using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedQuist.BillingAdmin.ViewModels
{
    public interface IViewModelBase
    {
        event EventHandler RequestClose;
    }
}
