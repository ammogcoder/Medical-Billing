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
    public class ClientDefController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private readonly ClientDefViewModel clientDefViewModel;
        private readonly ClientDefListViewModel clientDefListViewModel;
        private readonly ClientDefDetailsViewModel clientDefDetailsViewModel;
        private readonly ClientDefWorkTypeViewModel clientDefWorkTypeViewModel;
        // Hope this works
        private readonly InvoiceGroupViewModel invoiceGroupViewModel;

        [ImportingConstructor]
        public ClientDefController(CompositionContainer container 
            ,ShellViewModel shellViewModel
            ,ClientDefViewModel _clientDefViewModel
            ,ClientDefListViewModel _clientDefListViewModel
            , ClientDefDetailsViewModel _clientDefDetailsViewModel
            , ClientDefWorkTypeViewModel _clientDefWorkTypeViewModel
            , InvoiceGroupViewModel _invoiceGroupViewModel
            )
        {
            this.invoiceGroupViewModel = _invoiceGroupViewModel;
            this.container = container;
            clientDefWorkTypeViewModel = _clientDefWorkTypeViewModel;
            this.clientDefDetailsViewModel = _clientDefDetailsViewModel;
            this.clientDefViewModel = _clientDefViewModel;
            this.clientDefListViewModel = _clientDefListViewModel;
            clientDefDetailsViewModel = _clientDefDetailsViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void Initialize()
        {
            /////////////////////////////////////////////
            //IClientDefView iClientDefView = container.GetExportedValue<IClientDefView>();
            //clientDefViewModel = new ClientDefViewModel(iClientDefView);

            shellViewModel.Sections.Add(new SectionPair((IView)clientDefListViewModel.View, (IView)clientDefViewModel.View, ((IView)clientDefViewModel.View).MyTitle));
            clientDefViewModel.ClientDefDetailsTab = clientDefDetailsViewModel.View;
            clientDefViewModel.ClientDefWorkTypeTab = clientDefWorkTypeViewModel.View;

            clientDefListViewModel.clientDefDetailsViewModel = clientDefDetailsViewModel;
            clientDefDetailsViewModel.ClientDefListViewModel = clientDefListViewModel;
            clientDefDetailsViewModel.invoiceGrpViewModel = invoiceGroupViewModel;
            clientDefWorkTypeViewModel.invoiceGrpViewModel = invoiceGroupViewModel;

            clientDefWorkTypeViewModel.clientDefListViewModel = clientDefListViewModel;
            clientDefWorkTypeViewModel.clientDefViewModel = clientDefViewModel;

            clientDefListViewModel.clientDefWorkTypeViewModel = clientDefWorkTypeViewModel;
            ////////////////////////////////////////////////
            //IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            ////////////////////////////////////////////////

        }
    }
}
