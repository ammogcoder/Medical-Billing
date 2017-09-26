using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Spheris.Billing.Data.RepositoryInterfaces;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using Spheris.Billing.Data;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;

namespace MedQuist.ViewModels.Controllers
{
    [Export]
    public class LoginController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;

        [ImportingConstructor]
        public LoginController(CompositionContainer container,ShellViewModel svm)
        {
            this.container = container;
            shellViewModel = svm;
        }

        public void Initialize()
        {

        }
    }
}
