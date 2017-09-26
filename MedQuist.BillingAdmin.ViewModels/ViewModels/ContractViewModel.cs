using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using Spheris.Billing.Data.RepositoryInterfaces;
using MedQuist.BillingAdmin.ViewModels;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for Contract Section
    /// </summary>
    [Export]
    public class ContractViewModel : ViewModel<IContractView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        [ImportingConstructor]
        public ContractViewModel(IContractView view)
            : base(view)
        {
        }
        #endregion

        public ContractListViewModel contractListViewModel { get; set; }

        public ShellViewModel shellViewModel { get; set; }


        #region Data Target

        #endregion


        private bool isValid = true;
        public bool IsValid
        {
            get { return isValid; }
            set
            {
                if (isValid != value)
                {
                    isValid = value;
                    RaisePropertyChanged("IsValid");
                }
            }
        }

        
        private object ratesView;
        public object RatesView
        {
            get
            {
                return ratesView;
            }
            set
            {
                ratesView = value;
                RaisePropertyChanged("RatesView");
            }
        }


        private object groupsOnContractView;
        public object GroupsOnContractView
        {
            get
            {
                return groupsOnContractView;
            }
            set
            {
                groupsOnContractView = value;
                RaisePropertyChanged("GroupsOnContractView");
            }
        }

        private object tatSchedView;
        public object TatSchedView
        {
            get
            {
                return tatSchedView;
            }
            set
            {
                tatSchedView = value;
                RaisePropertyChanged("TatSchedView");
            }
        }
     
        private object volumeAdjView;
        public object VolumeAdjView
        {
            get
            {
                return volumeAdjView;
            }
            set
            {
                volumeAdjView = value;
                RaisePropertyChanged("VolumeAdjView");
            }
        }


        public void Dispose()
        {
        }
    }
}