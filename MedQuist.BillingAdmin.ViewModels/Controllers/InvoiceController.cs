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
    public class InvoiceController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        // The View Models
        private InvoiceGroupViewModel invoiceGroupViewModel;
        private InvoiceViewModel invoiceViewModel;
        private InvoiceGrpDetailViewModel invoiceGrpDetailViewModel;
        private InvoiceHistoryViewModel invoiceHistoryViewModel;
        private AddOnChargesViewModel addOnChargesViewModel;
        private OverridesViewModel overridesViewModel;

        private IExtClientRepository extClients;
        private ObservableCollection<ClientLocation> notAssignedLocations;


        [ImportingConstructor]
        public InvoiceController(CompositionContainer container, 
            ShellViewModel shellViewModel,
            InvoiceViewModel _invoiceViewModel,
            InvoiceGroupViewModel _invoiceGroupViewModel, 
            InvoiceGrpDetailViewModel _invoiceGrpDetailViewModel,
            InvoiceHistoryViewModel _invoiceHistoryViewModel,
            AddOnChargesViewModel _addOnChargesViewModel,
            OverridesViewModel _overridesViewModel)
        {
            this.container = container;
            overridesViewModel = _overridesViewModel;
            invoiceHistoryViewModel = _invoiceHistoryViewModel;
            invoiceViewModel = _invoiceViewModel;
            this.invoiceGroupViewModel = _invoiceGroupViewModel; ;
            invoiceGrpDetailViewModel = _invoiceGrpDetailViewModel;
            addOnChargesViewModel = _addOnChargesViewModel;
            this.shellViewModel = shellViewModel;

        }


        public void Initialize()
        {

            shellViewModel.Sections.Add(new SectionPair((IView)invoiceGroupViewModel.View, 
                (IView)invoiceViewModel.View, 
                ((IView)invoiceViewModel.View).MyTitle));

            /////////////////////////////////////////////

            invoiceViewModel.MainViewModel = shellViewModel;
            invoiceViewModel.InvoiceGroupViewDetailTab = invoiceGrpDetailViewModel.View;
            invoiceViewModel.AddOnChargesViewTab = addOnChargesViewModel.View;
            invoiceViewModel.OverridesViewTab = overridesViewModel.View;
            invoiceViewModel.InvoiceHistoryViewTab = invoiceHistoryViewModel.View;
            invoiceGroupViewModel.addOnChargesViewModel = addOnChargesViewModel;
            invoiceGroupViewModel.overridesViewModel = overridesViewModel;
            invoiceGroupViewModel.invoiceHistoryViewModel = invoiceHistoryViewModel;
            invoiceGrpDetailViewModel.MainViewModel = shellViewModel;
                                                                           
            invoiceGrpDetailViewModel.GroupVM = invoiceGroupViewModel;

        }
    }
}
