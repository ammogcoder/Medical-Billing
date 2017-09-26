using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for GroupsOnContract Section
    /// </summary>
    [Export]
    public class ContractDetailsViewModel : ViewModel<IContractDetailsView>, IDisposable
    {
        #region Fields
        IContractDetailsView _view;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<GroupsOnContractNote></param>
        [ImportingConstructor]
        public ContractDetailsViewModel(IContractDetailsView view)
            : base(view)
        {
            _view = view;
            PaymentTermsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreatePaymentTermsRepository();
            InvoiceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();
            SelectedGroups = new ObservableCollection<InvoiceGroup>();
            ContractTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRepository();

            AddGroupsCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (SelectedGroups.Count > 0 && CurrentContract != null),
                ExecuteDelegate = x => AddGroups()
            };

            RemoveGroupsCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentContract != null && SelectedGroups.Count > 0),
                ExecuteDelegate = x => RemoveGroups()
            };

            PopCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentContract != null),
                ExecuteDelegate = x => view.Pop()
            };


        }
        #endregion

        #region Methods
        public void AddGroups()
        {
            try
            {
                PopSwitch = false;
                foreach (InvoiceGroup group in SelectedGroups)
                {
                    group.ContractID = CurrentContract.CONTRACT_ID;
                    InvoiceTarget.Update(group);
                    //GroupsAssigned.Add(group);
                }
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
            SetContractDetails();

        }

        public void RemoveGroups()
        {
            try
            {
                foreach (InvoiceGroup group in SelectedGroups)
                {
                    group.ContractID = null;
                    InvoiceTarget.Update(group);
                    //SelectedGroups.Remove(group);
                    //GroupsAssigned.Remove(group);
                }
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
            SetContractDetails();
        }

        void SetContractDetails()
        {
            if (currentContract == null)
            {
                //this.GracePeriod = 0;
                this.GroupsAssigned = null;
                this.SelectedPaymentTerm = null;
                return;
            }
            SelectedPaymentTerm = (PaymentTermList.FirstOrDefault(ptl => ptl.PAYMENT_TERMS == currentContract.PAYMENT_TERMS) as PaymentTerm);
            // Because this comes from the INVOICE_GRP table 
            // and if they are adding a NEW contract, then it's ID
            // has not yet been assigned and no fetch action can take place
            GroupsAssigned = (currentContract.CONTRACT_ID == 0 ) ? null : InvoiceTarget.FetchGroups(null,
                                   null,
                                   currentContract.CONTRACT_ID.ToString(),
                                   null);
            currentContract.Modified = false;

        }
#if DOESNT_MAKE_SENSE
        /// <summary>
        /// Exposed for codeBehind - through the routed message
        /// </summary>
        /// <param name="invoiceId"></param>
        public void SetDefaultInvoiceGroup(int invoiceId)
        {
            if (CurrentContract == null)
                return;
            if (invoiceId < 0)
                CurrentContract..DefaultInvoiceGrpID = null;
            else
                CurrentContract.DefaultInvoiceGrpID = invoiceId;
        }
#endif

        #endregion

        #region Data Targets
        public IInvoiceGroupRepository InvoiceTarget { get; set; }

        public IPaymentTermsRepository PaymentTermsTarget { get; set; }

        public ContractListViewModel contractListViewModel { get; set; }
        
        private IContractRepository ContractTarget {get;set;}

        public InvoiceGroupViewModel invoiceGrpViewModel { get; set; }

        #endregion

        #region Properties
        public bool IsValid
        {
            get 
            {
                if (CurrentContract == null)
                    return false;
                return true; 
            }
        }

        private PaymentTerm selectedPaymentTerm;
        public PaymentTerm SelectedPaymentTerm
        {
            get
            {
                return selectedPaymentTerm;
            }
            set
            {
                if (selectedPaymentTerm != value)
                {
                    selectedPaymentTerm = value;
                    if (CurrentContract != null && selectedPaymentTerm != null)
                        CurrentContract.PAYMENT_TERMS = selectedPaymentTerm.PAYMENT_TERMS;
                    RaisePropertyChanged("SelectedPaymentTerm");
                }
            }
        }

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

        private ObservableCollection<InvoiceGroup> groupsNotAssigned;
        public ObservableCollection<InvoiceGroup> NotAssignedGroups
        {
            get
            {
#if ORACLE
                groupsNotAssigned = InvoiceTarget.FetchNullContracts();
#endif
                return groupsNotAssigned;
            }
            set
            {
                if (groupsNotAssigned != value)
                {
                    groupsNotAssigned = value;
                    RaisePropertyChanged("NotAssignedGroups");
                }
            }
        }

        private ObservableCollection<InvoiceGroup> selectedGroups;
        public ObservableCollection<InvoiceGroup> SelectedGroups
        {
            get
            {
                return selectedGroups;
            }
            set
            {
                if (selectedGroups != value)
                {
                    selectedGroups = value;
                    RaisePropertyChanged("SelectedGroups");
                }
            }
        }

        private ObservableCollection<InvoiceGroup> groupsAssigned;
        public ObservableCollection<InvoiceGroup> GroupsAssigned
        {
            get
            {
                return groupsAssigned;
            }
            set
            {
                if (groupsAssigned != value)
                {
                    groupsAssigned = value;
                    RaisePropertyChanged("GroupsAssigned");
                }
            }
        }

        private ObservableCollection<PaymentTerm> paymentTermList;
        public ObservableCollection<PaymentTerm> PaymentTermList
        {
            get
            {
#if ORACLE
                if (paymentTermList == null)
                    paymentTermList = PaymentTermsTarget.GetAllPaymentTerms();
#endif
                return paymentTermList;
            }
            set
            {
                if (paymentTermList != value)
                {
                    paymentTermList = value;
                    RaisePropertyChanged("PaymentTermList");
                }
            }
        }

        private Spheris.Billing.Core.Domain.Contract copyContract;
        private Spheris.Billing.Core.Domain.Contract currentContract;
        public Spheris.Billing.Core.Domain.Contract CurrentContract
        {
            get
            {
                return currentContract;
            }
            set
            {
                if (currentContract != value && value != null)
                {
                }
                currentContract = value;
                if(currentContract != null)
                    copyContract = new Contract(currentContract);
                SetContractDetails();
                RaisePropertyChanged("IsValid");
                RaisePropertyChanged("IsNotAddingNew");
                RaisePropertyChanged("CurrentContract");
            }
        }

        public bool IsNotAddingNew
        {
            get
            {
                if (CurrentContract == null ||
                    CurrentContract.CONTRACT_ID == 0)
                    return false;
                else
                    return true;
            }
        }

        public bool Modified
        {
            get
            {
                if (this.currentContract != null && this.currentContract.Modified)
                {
                    if(_view.IsInView)
                        contractListViewModel.EnablePicker = false;
                    return true;
                }

                if (_view.IsInView)
                    contractListViewModel.EnablePicker = true;
                return false;
            }
        }

        #endregion

        #region Commands
        public ICommand AddGroupsCommand
        {
            private get;
            set;
        }

        public ICommand RemoveGroupsCommand
        {
            private get;
            set;
        }

        public ICommand PopCommand
        {
            private get;
            set;
        }

        public ICommand AddNewCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand<object>(AddExecute, CanAddExecute);
                }
                return _addCommand;
            }
        }
        ICommand _addCommand;

        public ICommand RestoreCommand
        {
            get
            {
                if (_restoreCommand == null)
                {
                    _restoreCommand = new RelayCommand<object>(RestoreExecute, CanRestoreExecute);
                }
                return _restoreCommand;
            }
        }
        ICommand _restoreCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand<object>(DeleteExecute, CanDeleteExecute);
                }
                return _deleteCommand;
            }
        }
        ICommand _deleteCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand<object>(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }
        ICommand _saveCommand;

        #endregion

        #region Command Methods

        bool CanRestoreExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void RestoreExecute(object param)
        {
            CurrentContract = copyContract;
            CurrentContract.Modified = false;
            //copy
            //ClientDefListViewModel.SwapClientDefItem(this.copyClientDef);
            if (_view.IsInView)
                contractListViewModel.EnablePicker = false;
            RaisePropertyChanged("IsNotAddingNew");
        }


        bool CanDeleteExecute(object param)
        {
            return (Modified || currentContract == null) ? false : true;
        }

        bool CanAddExecute(object param)
        {
            return (Modified) ? false : true;
        }

        void AddExecute(object param)
        {
            Contract contract;
            if (CurrentContract != null)
            {
                contract = CurrentContract.Clone();
                contract.CONTRACT_ID = 0;
            }

            else
                contract = new Contract();
            if(_view.IsInView)
                contractListViewModel.EnablePicker = false;
            CurrentContract = contract;
            CurrentContract.Modified = true;
            RaisePropertyChanged("IsNotAddingNew");
        }

        void DeleteExecute(object param)
        {
            try
            {
                ContractTarget.Remove(CurrentContract);
                contractListViewModel.Search();
                RaisePropertyChanged("IsNotAddingNew");
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
        }

        bool CanSaveExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void SaveExecute(object param)
        {
            try
            {
                CurrentContract.CHANGED_BY = DB.OracleSession.UserName;
                CurrentContract.CHANGED_ON = DateTime.Now;

                if (CurrentContract.CONTRACT_ID == 0)
                {
                    ContractTarget.Add(CurrentContract);
                    contractListViewModel.SetToNewlyAdded(CurrentContract);
                }
                else
                    ContractTarget.Update(CurrentContract);
                CurrentContract.Modified = false;
                if(_view.IsInView)
                    contractListViewModel.EnablePicker = true;
                RaisePropertyChanged("IsNotAddingNew");
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}