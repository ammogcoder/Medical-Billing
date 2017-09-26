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
    public class AuditLogController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private AuditLogViewModel auditLogViewModel;


        [ImportingConstructor]
        public AuditLogController(CompositionContainer container, ShellViewModel shellViewModel,
            AuditLogViewModel _auditLogViewModel)
        {
            this.container = container;
            this.auditLogViewModel = _auditLogViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void Initialize()
        {
            /////////////////////////////////////////////
            //IAuditLogView iAuditLogView = container.GetExportedValue<IAuditLogView>();
            //auditLogViewModel = new AuditLogViewModel(iAuditLogView);

            //shellViewModel.Sections.Add(new SectionPair(null, (IView)auditLogViewModel.View,((IView)auditLogViewModel.View).MyTitle));

            ////////////////////////////////////////////////
            IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            ////////////////////////////////////////////////

        }
    }
}
