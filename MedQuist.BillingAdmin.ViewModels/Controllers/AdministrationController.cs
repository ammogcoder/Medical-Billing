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
    public class AdministrationController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        private AdministrationViewModel administrationViewModel;
        private readonly AdminTatScheduleViewModel tatScheduleViewModel;
        private readonly VolumeAdjustmentsViewModel volumeAdjustmentsViewModel;
        private readonly AdminLinksViewModel adminLinksViewModel;
        private readonly PasswordChangeViewModel passwordChangeViewModel;

        [ImportingConstructor]
        public AdministrationController(CompositionContainer container
            ,ShellViewModel shellViewModel
            ,AdministrationViewModel _AdministrationViewModel
            ,AdminTatScheduleViewModel _tatScheduleViewModel
            ,VolumeAdjustmentsViewModel _volumeAdjustmentsViewModel 
            ,AdminLinksViewModel _adminLinksViewModel
            ,PasswordChangeViewModel _passwordChangeViewMod
            )
        {
            this.container = container;
            this.administrationViewModel = _AdministrationViewModel;
            tatScheduleViewModel = _tatScheduleViewModel;
            passwordChangeViewModel = _passwordChangeViewMod;
            adminLinksViewModel = _adminLinksViewModel;
            volumeAdjustmentsViewModel = _volumeAdjustmentsViewModel;
            this.shellViewModel = shellViewModel;
        }

        public void Initialize()
        {
            IView adminView = (IView)administrationViewModel.View;
            string title = adminView.MyTitle;
            shellViewModel.Sections.Add(new SectionPair((IView)adminLinksViewModel.View, 
                adminView, 
                title));
            adminLinksViewModel.volumeAdjustsmentViewModel = volumeAdjustmentsViewModel;
            adminLinksViewModel.tatScheduleViewModel = tatScheduleViewModel;
            adminLinksViewModel.shellViewModel = shellViewModel;
            adminLinksViewModel.passwordChangeViewModel = passwordChangeViewModel;
        }
    }
}
