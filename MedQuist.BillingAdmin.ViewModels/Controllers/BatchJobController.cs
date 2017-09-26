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
    public class BatchJobController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private BatchJobViewModel batchJobViewModel;

        [ImportingConstructor]
        public BatchJobController(CompositionContainer container, ShellViewModel shellViewModel,
            BatchJobViewModel _batchJobViewModel)
        {
            this.container = container;
            this.batchJobViewModel = _batchJobViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void Initialize()
        {
            /////////////////////////////////////////////
            //IBatchJobView iBatchJobView = container.GetExportedValue<IBatchJobView>();
            //passwordChangeViewModel = new BatchJobViewModel(iBatchJobView);

            //shellViewModel.Sections.Add(new SectionPair(null, (IView)passwordChangeViewModel.View, ((IView)passwordChangeViewModel.View).MyTitle));

            //shellViewModel.Sections.Add(new SectionPair(null, iBatchJobView,"Batch Title"));

            ////////////////////////////////////////////////
            IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            ////////////////////////////////////////////////

        }
    }
}
