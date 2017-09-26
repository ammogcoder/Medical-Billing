using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using System.Collections.Specialized;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for TatSched Section
    /// </summary>
    [Export]
    public class TatScheduleViewModel : ViewModel<ITatScheduleView>, IDisposable
    {
        #region fields
        ITatScheduleView ThisView;
        private bool bWasAdded = false;
        private bool bWasDeleted = false;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<TatSchedNote></param>
        [ImportingConstructor]
        public TatScheduleViewModel(ITatScheduleView view)
            : base(view)
        {

            ThisView = view;
            ContractRateTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRateRepository();

            TatSchedTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateTatSchedRepository();

            ContractTatSchedTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractTatSchedRepository();

            SaveCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified == true && SelectedTatSched != null),
                ExecuteDelegate = x => SaveContractTatSchedule()
            };

            CancelCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified == true),
                ExecuteDelegate = x => Restore()
            };

        }
        #endregion

        #region COMMANDS
        public ICommand SaveCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }
        #endregion

        #region Targets
        private IContractRateRepository ContractRateTarget { get; set; }
        private IContractTatSchedRepository ContractTatSchedTarget { get; set; }
        private ITatSchedRepository TatSchedTarget { get; set; }
        public ContractListViewModel contractListViewModel{ get; set; }
        #endregion

        #region Properties
        public bool Modified
        {
            get
            {
                bool bMod = bWasAdded | bWasDeleted;
                if (ContractTatScheds != null)
                    foreach (ContractTatSched cts in ContractTatScheds)
                    {
                        if (cts.Modified)
                        {
                            bMod = true;
                            break;
                        }
                    }
                if (ThisView.IsInView)
                    contractListViewModel.EnablePicker = !bMod;

                return bMod;
            }
        }

        private ObservableCollection<TatSched> tatScheds;
        public ObservableCollection<TatSched> TatScheds
        {
            get
            {
                return tatScheds;
            }
            set
            {
                tatScheds = value;
 
                RaisePropertyChanged("TatScheds");
            }
        }

        public string TatSchedToolTip
        {
            get
            {
                return (selectedTatSched != null) ? selectedTatSched.TAT_SCHED_ID.ToString() : null;
            }
        }

        private TatSched selectedTatSched;
        public TatSched SelectedTatSched
        {
            get
            {
                return selectedTatSched;
            }
            set
            {
                selectedTatSched = value;
                if (SelectedContractTatSched != null && selectedTatSched != null)
                {
                    SelectedContractTatSched.TAT_SCHED_ID = selectedTatSched.TAT_SCHED_ID;                    
                }
                RaisePropertyChanged("TatSchedToolTip");
                RaisePropertyChanged("SelectedTatSched");
            }
        }

        private ObservableCollection<ContractTatSched> backUpContractTatSched;
        private ObservableCollection<ContractTatSched> contractTatScheds;
        public ObservableCollection<ContractTatSched> ContractTatScheds
        {
            get
            {
                return contractTatScheds;
            }
            set
            {
                contractTatScheds = value;

                if (contractTatScheds != null)
                    contractTatScheds.CollectionChanged -= ContractTatSchedListChanged;
                contractTatScheds = value;
                if (contractTatScheds != null)
                    contractTatScheds.CollectionChanged += ContractTatSchedListChanged;
                if (contractTatScheds != null)
                    contractTatScheds.ForEach((lst, item) => item.Modified = false);
                backUpContractTatSched = new ObservableCollection<ContractTatSched>();

                contractTatScheds.ForEach((lst, item) =>
                {
                    backUpContractTatSched.Add(item.Clone());
                }
                );
                bWasAdded = false;
                RaisePropertyChanged("ContractTatScheds");
            }
        }

        private ContractTatSched selectedContractTatSched;
        public ContractTatSched SelectedContractTatSched
        {
            get
            {
                return selectedContractTatSched;
            }
            set
            {
                selectedContractTatSched = value;
                if (selectedContractTatSched != null)
                {
                    if (bWasAdded)
                         selectedContractTatSched.bWasAdded = true;

                    selectedTatSched = null;

                    foreach (TatSched ts in TatScheds)
                        if (ts.TAT_SCHED_ID == selectedContractTatSched.TAT_SCHED_ID)
                        {
                           selectedTatSched = ts;
                            break;
                        }
                    RaisePropertyChanged("SelectedTatSched");
                }
                else
                    SelectedTatSched = null;
                RaisePropertyChanged("SelectedContractTatSched");
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
                        ContractTatScheds = null;
                    }
                    else
                    {
                        LoadScheds();
                    }
                    RaisePropertyChanged("CurrentContract");
                }
            }
        }
        #endregion

        #region Methods
        void Restore()
        {
            if (contractTatScheds != null)
                contractTatScheds.CollectionChanged -= ContractTatSchedListChanged;

            bWasDeleted = false;
            bWasAdded = false;
            LoadScheds(); 

            return;

            if (contractTatScheds != null)
                contractTatScheds.CollectionChanged -= ContractTatSchedListChanged;
            contractTatScheds = new ObservableCollection<ContractTatSched>();
            bWasAdded = false;
            bWasDeleted = false;
            backUpContractTatSched.ForEach((lst, item) =>
            {
                contractTatScheds.Add(item.Clone());
            }
            );
         
            if (contractTatScheds != null)
                contractTatScheds.CollectionChanged += ContractTatSchedListChanged;
 
            RaisePropertyChanged("ContractTatScheds");
        }

        void SaveContractTatSchedule()
        {
            try
            {
                foreach (ContractTatSched bcts in backUpContractTatSched)
                {
                    ContractTatSchedTarget.Remove(bcts);
                }
                foreach (ContractTatSched cts in ContractTatScheds)
                {
                        cts.CONTRACT_ID = CurrentContract.CONTRACT_ID;

                        ContractTatSchedTarget.Add(cts);
                        cts.Modified = false;
                    
                }

#if IMPOSSIBLE

                foreach (ContractTatSched bcts in backUpContractTatSched)
                {
                    // Deleted?
                    if (!ContractTatScheds.Contains(bcts))
                    {
                        ContractTatSchedTarget.Remove(bcts);
                    }
                }

                foreach (ContractTatSched cts in ContractTatScheds)
                {
                    if (cts.bWasAdded)
                    {
                        cts.CONTRACT_ID = CurrentContract.CONTRACT_ID;
                        cts.TAT_SCHED_ID = SelectedTatSched.TAT_SCHED_ID;


                        ContractTatSchedTarget.Add(cts);
                        cts.Modified = false;
                    }
                }

                foreach (ContractTatSched cts in ContractTatScheds)
                {
                    if (cts.Modified)
                    {
                        ContractTatSchedTarget.Update(cts);
                        cts.Modified = false;
                    }
                }
#endif
                bWasAdded = bWasDeleted = false;
                RaisePropertyChanged("ContractTatScheds");
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }
        
        public void LoadScheds()
        {
            try
            {
                TatScheds = TatSchedTarget.GetTATSched();
                if (CurrentContract != null)
                {
                    ContractTatScheds = ContractTatSchedTarget.FetchTatScheds((decimal)CurrentContract.CONTRACT_ID);

                }
                else
                    ContractTatScheds = null;
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }
       
        private void ContractTatSchedListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    bWasDeleted = true;
                    break;
                case NotifyCollectionChangedAction.Add:
                    bWasAdded = true;
                    (e.NewItems[0] as ContractTatSched).TAT_SCHED_ID = TatScheds[0].TAT_SCHED_ID;
                    break;
            }
        }

        public void Dispose()
        {
        }
        #endregion
    }
}