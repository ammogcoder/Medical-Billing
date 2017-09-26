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
    public class ErrorManController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private ErrNoValidContractViewModel errorManViewModel;

        [ImportingConstructor]
        public ErrorManController(CompositionContainer container, ShellViewModel shellViewModel,
            ErrNoValidContractViewModel _errorManViewModel)
        {
            this.container = container;
            this.errorManViewModel = _errorManViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void Initialize()
        {
            /////////////////////////////////////////////
            //IErrNoValidContractView iErrorManView = container.GetExportedValue<IErrNoValidContractView>();
            //errorManViewModel = new ErrNoValidContractViewModel(iErrorManView);
            //shellViewModel.Sections.Add(new SectionPair(null, iErrorManView));

            //shellViewModel.Sections.Add(new SectionPair(null, (IView)errorManViewModel.View, ((IView)errorManViewModel.View).MyTitle));

            ////////////////////////////////////////////////
            //IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            ////////////////////////////////////////////////

        }
    }
}
