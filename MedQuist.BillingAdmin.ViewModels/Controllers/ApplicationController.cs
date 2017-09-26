using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Input;
using System.Diagnostics;
using System;
using MedQuist.ViewModels.Controllers;
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

namespace MedQuist.BillingAdmin.ViewModels 
{
//    [Export(typeof(ApplicationController))]
    [Export]
    public class ApplicationController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly InvoiceController invoiceController;
        private readonly ContractController contractController;
        private readonly ClientDefController clientDefController;
        private readonly ReportsController reportsController;
        private readonly AdministrationController administrationController;
        private readonly CompositionContainer container;
        private readonly ErrorManController errorManController;
        private readonly BatchJobController batchJobController;
        private readonly AuditLogController auditLogController;

        [ImportingConstructor]
        public ApplicationController(CompositionContainer _container,
            ShellViewModel shellViewModel
            , InvoiceController _invoiceController
            , ClientDefController _clientDefController
//#if ONE_AT_A_TIME
            , ContractController _contractController
            ,ReportsController _reportsController
            ,AdministrationController _administrationController
//#endif
            ,ErrorManController _errorManController
            ,BatchJobController _batchJobController 
            ,AuditLogController _auditLogController
            )
        {
            if (shellViewModel == null) { throw new ArgumentNullException("shellViewModel"); }
            container = _container;
            //loginController = _loginController;
            this.shellViewModel = shellViewModel;
            this.invoiceController = _invoiceController;
            clientDefController = _clientDefController;

//#if ONE_AT_A_TIME
            this.contractController = _contractController;
            reportsController = _reportsController;
            administrationController = _administrationController;
//#endif
            errorManController = _errorManController;
            batchJobController = _batchJobController;
            auditLogController = _auditLogController;                                                    

        }

        public void Initialize()
        {
            IAccessSettings accessSettings = container.GetExportedValue<IAccessSettings>();
            DB.Settings = accessSettings;
            //loginController.Initialize();
            invoiceController.Initialize();
            clientDefController.Initialize();

//#if ONE_AT_A_TIME
            contractController.Initialize();
            reportsController.Initialize();
            administrationController.Initialize();
//#endif
            errorManController.Initialize();
            batchJobController.Initialize();
            auditLogController.Initialize();
        }

        public void Run()
        {
         
            shellViewModel.Show();
        }

        public void Shutdown()
        {
        }

    }
}
