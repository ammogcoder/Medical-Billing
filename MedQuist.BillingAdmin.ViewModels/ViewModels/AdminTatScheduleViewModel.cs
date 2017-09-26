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
    public class AdminTatScheduleViewModel : ViewModel<IAdminTatScheduleView>, IDisposable
    {
        #region fields
        IAdminTatScheduleView ThisView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<TatSchedNote></param>
        [ImportingConstructor]
        public AdminTatScheduleViewModel(IAdminTatScheduleView view)
            : base(view)
        {
            ThisView = view;
            TatSchedTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateTatSchedRepository();
            TatRateTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateTatRateRepository();

            SaveTRCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (TRModified == true),
                ExecuteDelegate = x => SaveTRChanges()
            };

            CancelTRCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (TRModified == true),
                ExecuteDelegate = x => CancelTRChanges()
            };

            PickTATSchedCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => PickTATSched()
            };

        }

        void PickTATSched()
        {
            ThisView.Pop();
        }

        public bool TRModified
        {
            get
            {
                if (TatRates == null)
                    return false;
                bool modified = false;
                TatRates.ForEach((lst, item) =>
                    {
                        if (item.Modified)
                            modified = true;
                    }
                );
                return modified || bWasAddOrDelete;
            }
        }

        public void CancelTRChanges()
        {
            TatRates = backUpTatRates;
        }

        public void SaveTRChanges()
        {
            try
            {
                backUpTatRates.ForEach((lst, item) =>
                    {
                        if (IsTatRateAdded(item))
                        {
                            item.TAT_SCHED_ID = SelectedTatSched.TAT_SCHED_ID;
                            if (item.ALT_PENALTY == null) item.ALT_PENALTY = 0;
                            TatRateTarget.Add(item);
                        }
                        if (IsTatRateDeleted(item))
                            TatRateTarget.Remove(item);
                    }
                );

                TatRates.ForEach((lst, item) =>
                    {
                        if (item.Modified)
                            this.TatRateTarget.Update(item);
                    }
                );
            }

            catch(Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        bool IsTatRateAdded(TatRate item)
        {
            return !backUpTatRates.Contains(item);
        }

        bool IsTatRateDeleted(TatRate item)
        {
            return backUpTatRates.Contains(item) && !TatRates.Contains(item);
        }

        public ICommand SaveTRCommand { private set; get; }
        public ICommand CancelTRCommand { private set; get; }
        public ICommand PickTATSchedCommand {   set; get; }
        #endregion

        private bool popSwitch = false;
        public bool PopSwitch
        {
            get
            {
                return popSwitch;
            }
            set
            {
                if (popSwitch != value)
                {
                    popSwitch = value;
                    RaisePropertyChanged("PopSwitch");
                }
            }
        }


        public void LoadScheds()
        {
            try
            {
                TatScheds = TatSchedTarget.GetTATSched();
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private ITatSchedRepository TatSchedTarget { get; set; }
        private ITatRateRepository TatRateTarget { get; set; }


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
        public ContractListViewModel contractListViewModel
        { get; set; }

        private ObservableCollection<TatCompMethod> tatCompMethods;
        public ObservableCollection<TatCompMethod> TatCompMethods
        {
            get
            {
                if (tatCompMethods == null)
                {
                    ITatCompMethodRepository tatCompMethodTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateTatCompMethodRepository();
                    tatCompMethods = tatCompMethodTarget.GetTATCompMethods();
                }
                return tatCompMethods;
            }
            set
            {
                tatCompMethods = value;
                RaisePropertyChanged("TatCompMethods");
            }
        }


        private TatCompMethod selectedTatCompMethod;
        public TatCompMethod SelectedTatCompMethod
        {
            get
            {
                return selectedTatCompMethod;
            }
            set
            {

                selectedTatCompMethod = value;
                RaisePropertyChanged("SelectedTatCompMethod");
            }
        }


        private ObservableCollection<TatSched> tatSchedulesReassign;
        public ObservableCollection<TatSched> TatSchedulesReassign
        {
            get
            {
                return tatSchedulesReassign;
            }
            set
            {
                tatSchedulesReassign = value;
                RaisePropertyChanged("TatSchedulesReassign");
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
                TatSchedulesReassign = tatScheds;
                RaisePropertyChanged("TatScheds");
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
                SetSchedItemDetails();
                SelectedTatSchedulesReassign = selectedTatSched;
                RaisePropertyChanged("SelectedTatSched");
            }
        }

        private TatSched selectedTatSchedulesReassign;
        public TatSched SelectedTatSchedulesReassign
        {
            get
            {
                return selectedTatSchedulesReassign;
            }
            set
            {
                selectedTatSchedulesReassign = value;
                RaisePropertyChanged("SelectedTatSchedulesReassign");
            }
        }


        private ObservableCollection<TatRate> backUpTatRates;
        private ObservableCollection<TatRate> tatRates;
        public ObservableCollection<TatRate> TatRates
        {
            get
            {
                return tatRates;
            }
            set
            {
                if (tatRates != null)
                    tatRates.CollectionChanged -= tatRateChanged;
                tatRates = value;
                if (tatRates != null)
                    tatRates.CollectionChanged += tatRateChanged;
                if (tatRates != null)
                    tatRates.ForEach((lst, item) => item.Modified = false);
                backUpTatRates = new ObservableCollection<TatRate>();
                tatRates.ForEach((lst, item) =>
                    {
                        backUpTatRates.Add(item.Clone());
                    }
                );
                bWasAddOrDelete = false;
                RaisePropertyChanged("TatRates");
            }
        }

        private bool bWasAddOrDelete = false;
        private void tatRateChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Add:
                    bWasAddOrDelete = true;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
            }
        }

        void SetSchedItemDetails()
        {
            if (selectedTatSched == null)
            {
                selectedTatCompMethod = null;
                TatRates = null;
            }
            else
            {
                foreach (TatCompMethod tatCompMethod in TatCompMethods)
                    if (selectedTatSched.TAT_COMP_METHOD == tatCompMethod.TAT_COMP_METHOD)
                    {
                        SelectedTatCompMethod = tatCompMethod;
                        break;
                    }
                TatRates = TatRateTarget.GetTatRates(SelectedTatSched.TAT_SCHED_ID);
                if (TatRates.Count > 0)
                    SelectedTatRate = TatRates[0];
                ThisView.SetHeader(SelectedTatCompMethod.SHORT_DESCR);

            }
        }

        private TatRate selectedTatRate;
        public TatRate SelectedTatRate
        {
            get
            {
                return selectedTatRate;
            }
            set
            {
                selectedTatRate = value;
                if (selectedTatRate == null)
                    ContractsUsing = null;
                else
                {
                    ContractsUsing = this.TatRateTarget.GetContractsUsing(selectedTatRate.TAT_SCHED_ID);
                    if (ContractsUsing.Count > 0)
                        SelectedContractUsing = ContractsUsing[0];
                }
                RaisePropertyChanged("SelectedTatRate");
            }
        }

        private ObservableCollection<ContractsUsingTatSched> contractsUsing;
        public ObservableCollection<ContractsUsingTatSched> ContractsUsing
        {
            get
            {
                return contractsUsing;
            }
            set
            {
                contractsUsing = value;
                RaisePropertyChanged("ContractsUsing");
            }
        }

        private ContractsUsingTatSched selectedContractUsing;
        public ContractsUsingTatSched SelectedContractUsing
        {
            get
            {
                return selectedContractUsing;
            }
            set
            {
                selectedContractUsing = value;
                RaisePropertyChanged("SelectedContractUsing");
            }
        }


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




        public void Dispose()
        {
        }
    }
}