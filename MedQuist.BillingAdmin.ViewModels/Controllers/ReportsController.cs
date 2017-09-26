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
    public class ReportsController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private ReportsViewModel reportsViewModel;
        private ReportLinksViewModel reportLinksViewModel;

        private ErrNoValidContractViewModel errNoValidContractViewModel;
        private ErrClientWorkTypeViewModel errClientWorkTypeViewModel;
        private BatchJobViewModel batchJobViewModel;
        private AuditLogViewModel auditLogViewModel;

        [ImportingConstructor]
        public ReportsController(CompositionContainer container, ShellViewModel shellViewModel
            , ReportsViewModel _ReportsViewModel
            , ReportLinksViewModel _reportLinksViewModel
            , ErrNoValidContractViewModel _errNoValidContractViewModel
            , ErrClientWorkTypeViewModel _errClientWorkTypeViewModel
            , BatchJobViewModel _batchJobViewModel
            , AuditLogViewModel _auditLogViewModel
            )
        {
            this.container = container;
            reportLinksViewModel = _reportLinksViewModel;
            this.reportsViewModel = _ReportsViewModel;
            this.shellViewModel = shellViewModel;

            errNoValidContractViewModel = _errNoValidContractViewModel;
            errClientWorkTypeViewModel = _errClientWorkTypeViewModel;
            batchJobViewModel = _batchJobViewModel;
            auditLogViewModel = _auditLogViewModel;
        }

        public void Initialize()
        {

            reportLinksViewModel.shellViewModel = shellViewModel;
            reportsViewModel.reportLinksViewModel = reportLinksViewModel;
            

            reportLinksViewModel.errNoValidContractViewModel = errNoValidContractViewModel;
            reportLinksViewModel.errClientWorkTypeViewModel = errClientWorkTypeViewModel;
            reportLinksViewModel.batchJobViewModel = batchJobViewModel;
            reportLinksViewModel.auditLogViewModel = auditLogViewModel;

            shellViewModel.Sections.Add(new SectionPair((IView)reportLinksViewModel.View, (IView)reportsViewModel.View, ((IView)reportsViewModel.View).MyTitle));
             

        }
    }
}
