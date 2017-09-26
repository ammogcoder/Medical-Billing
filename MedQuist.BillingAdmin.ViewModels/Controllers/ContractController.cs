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
    public class ContractController : Controller
    {
        private readonly ShellViewModel shellViewModel;
        private readonly CompositionContainer container;
        // View Models
        //private readonly VolumeAdjViewModel volumeAdjViewModel;
        private readonly ContractViewModel contractViewModel;
        private readonly ContractDetailsViewModel groupsOnContractViewModel;
        private readonly ContractListViewModel contractListViewModel;
        private readonly ContractRateViewModel contractRateViewModel;
        private readonly TatScheduleViewModel tatScheduleViewModel;
        private readonly ContractVolumeEvtViewModel contractVolumeEvtViewModel;

        [ImportingConstructor]
        public ContractController(CompositionContainer container 
            ,ShellViewModel shellViewModel 
            ,ContractListViewModel _contractListViewModel
            ,ContractViewModel _contractViewModel
            ,ContractDetailsViewModel _groupsOnContractViewModel
            //,VolumeAdjViewModel _volumeAdjViewModel
            ,ContractRateViewModel _contractRateViewModel
            ,TatScheduleViewModel _tatScheduleViewModel
            ,ContractVolumeEvtViewModel _contractVolumeEvtViewModel
            )
        {
            this.container = container;
            tatScheduleViewModel = _tatScheduleViewModel;
            contractRateViewModel = _contractRateViewModel;
            //volumeAdjViewModel = _volumeAdjViewModel;
            this.contractViewModel = _contractViewModel;
            contractListViewModel = _contractListViewModel;
            contractVolumeEvtViewModel = _contractVolumeEvtViewModel;
            this.shellViewModel = shellViewModel;
            groupsOnContractViewModel = _groupsOnContractViewModel;
        }

        public void Initialize()
        {
            shellViewModel.Sections.Add(new SectionPair((IView)contractListViewModel.View, (IView)contractViewModel.View, ((IView)contractListViewModel.View).MyTitle));

            contractViewModel.RatesView = contractRateViewModel.View;
            contractViewModel.GroupsOnContractView = groupsOnContractViewModel.View;
            contractViewModel.VolumeAdjView = contractVolumeEvtViewModel.View;
            contractViewModel.TatSchedView = tatScheduleViewModel.View;

            contractVolumeEvtViewModel.contractListViewModel = contractListViewModel;

            contractListViewModel.groupsOnContractViewModel = groupsOnContractViewModel;
            
            contractListViewModel.tatScheduleViewModel = tatScheduleViewModel;
            tatScheduleViewModel.contractListViewModel = contractListViewModel;

            contractListViewModel.contractVolumeEvtViewModel = contractVolumeEvtViewModel;
            contractListViewModel.contractRateViewModel = contractRateViewModel;
            //contractListViewModel.volumeAdjViewModel = volumeAdjViewModel;
            contractViewModel.shellViewModel = shellViewModel;
            

            //volumeAdjViewModel.contractListViewModel = contractListViewModel;
            contractViewModel.contractListViewModel = contractListViewModel;

            groupsOnContractViewModel.contractListViewModel = contractListViewModel;
            contractRateViewModel.contractListViewModel = contractListViewModel;

        }
    }
}
