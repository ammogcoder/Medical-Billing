using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Spheris.Billing.Core.Domain;
using System.ComponentModel;
using System.Windows.Input;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Waf.Applications;
using System.Windows;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Forms;
using Spheris.Billing.Data;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// </summary>
    [Export]
    public class ContractListViewModel : ViewModel<IContractListView>, IDisposable, GongSolutions.Wpf.DragDrop.IDropTarget
    {
        #region Fields
        RelayCommand _closeCommand;
        bool IsBusy = false;


        #endregion

        #region Routing Messages
        public enum RouteTo
        {
            UsingInvoiceGroupView
        }

        public RouteTo RoutingTo { get; set; }
        
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        [ImportingConstructor]
        public ContractListViewModel(IContractListView view)
            : base(view)
        {
            searchCommand = new DelegateCommand(Search);
            ContractTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRepository();
        }
        #endregion

        #region Properties
        private readonly DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return searchCommand;
            }
        }

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

        public bool Modified
        {
            get
            {
                return false;
            }
            set
            {
                RaisePropertyChanged("Modified");
            }
        }

        private Contract currentContract;
        public Contract CurrentContract
        {
            get
            {
                return currentContract;
            }
            set
            {
                if (currentContract != value)
                {
                    currentContract = value;
                    if (currentContract == null)
                    {
                        SelectedContractId = null;
                        ContractId = "Search: ";
                    }
                    else
                    {
                        SelectedContractId = CurrentContract.CONTRACT_ID;
                        ContractId = CurrentContract.CONTRACT_ID.ToString();
                    }

                    if(groupsOnContractViewModel != null)
                        groupsOnContractViewModel.CurrentContract = currentContract;
                    if(contractRateViewModel != null)
                        contractRateViewModel.CurrentContract = currentContract;
                    if(tatScheduleViewModel != null)
                        tatScheduleViewModel.CurrentContract = currentContract;
                    if(contractVolumeEvtViewModel != null)
                        contractVolumeEvtViewModel.CurrentContract = currentContract;
                    RaisePropertyChanged("CurrentContract");
                }
            }
        }

        // For RoutedEvents
        public decimal? SelectedContractId { get; set; }

        private string viewTip = "Select a contact";
        public string ViewTip
        {
            get
            {
                if (Contracts == null)
                    return "Type in search box for contracts";
                else
                    return viewTip;
            }
            set
            {
                viewTip = value;
                RaisePropertyChanged("ViewTip");
            }
        }

        private bool enablePicker = true;
        public bool EnablePicker
        {
            get
            {
                return enablePicker;
            }
            set
            {
                if (enablePicker != value)
                {
                    enablePicker = value;
                    RaisePropertyChanged("EnablePicker");
                }
            }
        }

        private ObservableCollection<Spheris.Billing.Core.Domain.Contract> contracts;
        public ObservableCollection<Spheris.Billing.Core.Domain.Contract> Contracts
        {
            get
            {
                return contracts;
            }
            set
            {
                contracts = value;
                RaisePropertyChanged("Contracts");
            }
        }

        private string contractId = "Search: ";
        public string ContractId
        {
            get
            {
                return contractId;
            }
            set
            {
                if (contractId != value)
                {
                    contractId = value;
                    RaisePropertyChanged("ContractId");
                }
            }
        }

        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                if (searchString != value)
                {
                    searchString = value;
                    if ((!string.IsNullOrEmpty(searchString) && searchString.Length >= 3))
                        Search();
                    else
                        Contracts = null;
                    RaisePropertyChanged("SearchString");
                }
            }
        }

        public ContractVolumeEvtViewModel contractVolumeEvtViewModel;
        public ContractDetailsViewModel groupsOnContractViewModel{get;set;}
        public ContractRateViewModel  contractRateViewModel{get;set;}
        public TatScheduleViewModel tatScheduleViewModel { get; set; }
        #endregion

        #region DataTargets
        private IContractRepository ContractTarget
        {
            get;
            set;
        }
        #endregion

        #region Methods

        public void SetToNewlyAdded(Contract newContract)
        {
            List<Contract> newList = new List<Contract>();
            newList.Add(newContract);
            Contracts = new ObservableCollection<Contract>(newList);
            CurrentContract = newContract;
        }

        public void SetByContractId(int Id)
        {
            Contracts = new ObservableCollection<Contract>();
            Contracts.Add(  ContractTarget.GetByContractID(Id) );
            CurrentContract = Contracts[0];
        }

#if DIRECT
        public void Search()
        {
            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (string.IsNullOrEmpty(srchString))
                Contracts = null;
            else
            {
                Contracts = this.ContractTarget.FetchContracts(null, SearchString);
                                                
                CurrentContract = (Contracts.Count > 0) ? Contracts[0] : null;
            }

        }
#else
        private bool isLoadingData = false;
        public bool IsLoadingData
        {
            get
            {
                return isLoadingData;
            }
            set
            {
                if (isLoadingData != value)
                {
                    isLoadingData = value;
                    if (isLoadingData)
                        Contracts = new ObservableCollection<Contract> { new Contract{ DESCR = "Searching...", IsSpoof = true } };
                    RaisePropertyChanged("IsLoadingData");
                }

            }
        }

        public void Search()
        {
            try
            {
#if ORACLE
                IsLoadingData = true;
                var payload = SearchT(Contracts, () => this.IsLoadingData = false);
#endif
            }
            catch (Exception ta)
            {
            }
        }

        private Dispatcher uiDispatcher;
        private Thread workerThread;

        public ObservableCollection<Contract> SearchT(
            ObservableCollection<Contract> output,
            Action onCompleted
            )
        {
#if !ORACLE
            return null;
#endif
            IContractRepository DataTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRepository();
            
            var syncContext = SynchronizationContext.Current;
            if (syncContext == null)
                throw new InvalidOperationException("...");

            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (string.IsNullOrEmpty(srchString))
            {
                Contracts = null;
                return null;
            }
            uiDispatcher = Dispatcher.CurrentDispatcher;
            if (workerThread != null)
            {
                //Debug.WriteLine("Killing thread");
                workerThread.Abort();
            }
            //workerThread.i

            workerThread = new Thread(new ThreadStart(delegate
            {
                try
                {

                    var payload = DataTarget.FetchContracts(null, SearchString);
                    if (payload != null)
                        syncContext.Post(arg => CreateOutput(payload as ObservableCollection<Contract>/* output*/, onCompleted), null);
                }
                catch (Exception x)
                {
                    throw x;
                }
            }));
            workerThread.Priority = ThreadPriority.Normal;

            workerThread.Start();
            return null;
        }

        void CreateOutput(
            ObservableCollection<Contract> output,
            Action onCompleted)
        {
            Contracts = output;
            if (onCompleted != null)
                onCompleted();
        }

 
#endif
        void GongSolutions.Wpf.DragDrop.IDropTarget.DragOver(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
        }

        void GongSolutions.Wpf.DragDrop.IDropTarget.Drop(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}
