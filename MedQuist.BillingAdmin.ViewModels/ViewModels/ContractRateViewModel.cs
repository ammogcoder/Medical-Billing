using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using Spheris.Billing.Core.Domain;
using MedQuist.BillingAdmin.ViewModels;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for ContractRate Section
    /// </summary>
    [Export]
    public class ContractRateViewModel : ViewModel<IContractRateView>, IDisposable
    {
        #region Fields
        private IContractRateView _view;
        #endregion

        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"></see> class and
        /// attaches itself as <c>DataContext</c> to the view.
        /// </summary>
        /// <param name="view">The view.</param>
        [ImportingConstructor]
        protected ContractRateViewModel(IContractRateView view)
            : base(view)
        {
            _view = view;
            ContractRateTarget =  Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRateRepository();
            ContractRateAltTarget =  Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRateAltRepository();
            ChargeMethodTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateChargeMethodRepository();
            StatCompMethodTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateStatCompMethodRepository();
            FaxCompMethodTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateFaxCompMethodRepository();

            ChargeMethods = ChargeMethodTarget.GetChargeMethods();
            StatCompMethods = StatCompMethodTarget.GetStatCompMethods();
            FaxCompMethods = FaxCompMethodTarget.GetFaxCompMethods();

            PopCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => ( SelectedContractRate != null),
                ExecuteDelegate = x => view.Pop()
            };
            
        }
        #endregion

        #region Data Targets
        private IContractRateAltRepository ContractRateAltTarget { get; set; }
        private IContractRateRepository ContractRateTarget { get; set; }
        private IChargeMethodRepository ChargeMethodTarget { get; set; }
        private IStatCompMethodRepository StatCompMethodTarget { get; set; }
        private IFaxCompMethodRepository FaxCompMethodTarget { get; set; }
        #endregion

        public ContractListViewModel contractListViewModel
        { get; set; }



        #region Properties

        public bool EnablePicker
        {
            get
            {
                return (contractListViewModel == null) ? false : contractListViewModel.EnablePicker;
            }
            set
            {
                if(_view.IsInView)
                    contractListViewModel.EnablePicker = value;
                RaisePropertyChanged("EnablePicker");
            }
        }

        private ObservableCollection<FaxCompMethod> faxCompMethods;
        public ObservableCollection<FaxCompMethod> FaxCompMethods
        {
            get
            {
                return faxCompMethods;
            }
            set
            {
                if (faxCompMethods != value)
                {
                    faxCompMethods = value;
                    RaisePropertyChanged("FaxCompMethods");
                }
            }
        }

        private FaxCompMethod selectedFaxCompMethod;
        public FaxCompMethod SelectedFaxCompMethod
        {
            get
            {
                return selectedFaxCompMethod;
            }
            set
            {
                if (selectedFaxCompMethod != value)
                {
                    selectedFaxCompMethod = value;
                    if (SelectedContractRate != null && selectedFaxCompMethod != null)
                        SelectedContractRate.FAX_COMP_METHOD = selectedFaxCompMethod.FAX_COMP_METHOD;
                    RaisePropertyChanged("SelectedFaxCompMethod");
                }
            }
        }


        private ObservableCollection<StatCompMethod> statCompMethods;
        public ObservableCollection<StatCompMethod> StatCompMethods
        {
            get
            {
                return statCompMethods;
            }
            set
            {
                if (statCompMethods != value)
                {
                    statCompMethods = value;
                    RaisePropertyChanged("StatCompMethods");
                }
            }
        }

        private StatCompMethod selectedStatCompMethod;
        public StatCompMethod SelectedStatCompMethod
        {
            get
            {
                return selectedStatCompMethod;
            }
            set
            {
                if (selectedStatCompMethod != value)
                {
                    selectedStatCompMethod = value;
                    if (SelectedContractRate != null && selectedStatCompMethod != null)
                        SelectedContractRate.STAT_COMP_METHOD = selectedStatCompMethod.STAT_COMP_METHOD;

                    RaisePropertyChanged("SelectedStatCompMethod");
                }
            }
        }

        private ObservableCollection<ChargeMethod> chargeMethods;
        public ObservableCollection<ChargeMethod> ChargeMethods
        {
            get
            {
                return chargeMethods;
            }
            set
            {
                if (chargeMethods != value)
                {
                    chargeMethods = value;
                    RaisePropertyChanged("ChargeMethods");
                }
            }
        }

        private ChargeMethod selectedChargeMethod;
        public ChargeMethod SelectedChargeMethod
        {
            get
            {
                return selectedChargeMethod;
            }
            set
            {
                if (selectedChargeMethod != value)
                {
                    selectedChargeMethod = value;
                    if (SelectedContractRate != null && selectedChargeMethod != null)
                        SelectedContractRate.CHAR_COMP_METHOD = selectedChargeMethod.CHAR_COMP_METHOD;

                    RaisePropertyChanged("SelectedChargeMethod");
                }
            }
        }
        

        private ObservableCollection<ContractRate> contractRates;
        public ObservableCollection<ContractRate> ContractRates
        {
            get
            {
                return contractRates;
            }
            set
            {
                if (contractRates != value)
                {
                    contractRates.ForEach((thislist, eachContract) => RemoveWeakEventListener(eachContract, ContractRateListener));
                    contractRates = value;
                    contractRates.ForEach((thislist, eachContract) => AddWeakEventListener(eachContract, ContractRateListener));
                    RaisePropertyChanged("ContractRates");
                }
            }
        }

        void ResetRates(int contractRateId)
        {
            ContractRates = this.ContractRateTarget.GetRates((int)currentContract.CONTRACT_ID);
            if (contractRateId <= 0 && ContractRates.Count > 0)
                SelectedContractRate = ContractRates[0];
            else
                SelectedContractRate = null;
            foreach (ContractRate contractRate in ContractRates)
            {
                if (contractRate.CONTRACT_RATE_ID == contractRateId)
                {
                    SelectedContractRate = contractRate;
                    break;
                }
            }
        }

        void ResetAltRates(decimal contractAltRateId = -1)
        {
            AltContractRates = (SelectedContractRate != null) ? this.ContractRateAltTarget.GetAltRates(SelectedContractRate.CONTRACT_RATE_ID) : null;
            if (contractAltRateId <= 0 && AltContractRates != null && AltContractRates.Count > 0)
                SelectedAltContractRate = AltContractRates[0];
            if (AltContractRates != null)
                foreach (ContractRateAlt contractRateAlt in AltContractRates)
                {
                    if (contractRateAlt.RATE_NBR == contractAltRateId && contractRateAlt.CONTRACT_RATE_ID == SelectedContractRate.CONTRACT_RATE_ID)
                    {
                        SelectedAltContractRate = contractRateAlt;
                        break;
                    }
                }
            else
                SelectedAltContractRate = null;
        }

        private Contract currentContract;
        public Contract CurrentContract
        {
            get
            {
                return contractListViewModel.CurrentContract;//. currentContract;
            }
            set
            {
                if (currentContract != value)
                {
                    currentContract = value;
                    if (currentContract == null)
                        ContractRates = null;
                    else
                    {
                        ResetRates(-1);
                        //ContractRates = this.ContractRateTarget.GetRates(currentContract.CONTRACT_ID);
                        //if (ContractRates.Count > 0)
                        //    SelectedContractRate = ContractRates[0];
                    }
                    RaisePropertyChanged("CurrentContract");
                }
            } 
        }

        public bool CanEditAlt
        {
            get
            {
                return (SelectedAltContractRate == null) ? false : true;
            }
        }

        public bool CanEditRate
        {
            get
            {
                return (SelectedContractRate == null) ? false : true;
            }
        }

        public string IndexToAltText
        {
            get
            {
                return indexToAltText;
            }
            set
            {
                if (indexToAltText != value)
                {
                    indexToAltText = value;
                    RaisePropertyChanged("IndexToAltText");
                }
            }
        }
        private string indexToAltText;

        private int selectedIndexOfContractRate;
        public int SelectedIndexOfContractRate
        {
            get
            {
                return selectedIndexOfContractRate;
            }
            set
            {
                selectedIndexOfContractRate = value;
                RaisePropertyChanged("SelectedIndexOfContractRate");
            }
        }

        public ContractRate SelectedContractRate
        {
            get 
            { 
#if DEBUG
                if (selectedContractRate == null)
                    return null;
#endif
                return selectedContractRate; 
            }
            set
            {
#if DEBUG
                if (value == null)
                    selectedContractRate = value;
#endif
                    selectedContractRate = value;
                    if (selectedContractRate != null)
                    {
                        ResetAltRates();
                    }
                    else
                        AltContractRates = null;
                
                    SetRateItemDetails();
                    RaisePropertyChanged("RateId");
                    RaisePropertyChanged("SelectedContractRate");
                    RaisePropertyChanged("CanEditRate");
            }
        }
        ContractRate selectedContractRate;

        private bool rateEditEnable = true;
        public bool RateEditEnable
        {
            get
            {
                return rateEditEnable;
            }
            set
            {
                if (rateEditEnable != value)
                {
#if DEBUG
                    if (value == false)
                        rateEditEnable = value;
#endif
                    rateEditEnable = value;
                    RaisePropertyChanged("RateEditEnable");
                }

            }
        }

        public bool GridModified
        {
            set
            {
                if (value == true)
                    SelectedContractRate.Modified = true;
            }
        }

        /// <summary>
        /// </summary>
        public bool Modified
        {
            get
            {
                
                if (selectedAltContractRate != null && selectedAltContractRate.Modified == true)
                {
                    RatePickEnabled = false;
                    RateEditEnable = false;
                    if(_view.IsInView)
                    contractListViewModel.EnablePicker = false;
                    return true;
                }

                if (selectedContractRate != null && selectedContractRate.Modified == true)
                {
                    RateEditEnable = true;
                    RatePickEnabled = false;
                    if (_view.IsInView)
                        contractListViewModel.EnablePicker = false;
                    return true;
                }
                if (AltContractRates != null && AltContractRates.Count > 0)
                {
                    // Since they have AltContracts - they must have ContractRate
                    // Let them edit rates
                    RateEditEnable = true;

                    // Let them select Other Rates
                    RatePickEnabled = true;

                    // Let them edit Alt
                    //EnableAltRates = true;
                        
                    // Let them select another contract
                    if(contractListViewModel != null && _view.IsInView)
                        contractListViewModel.EnablePicker = true;

                    return false;
                }
                if( ContractRates != null && SelectedContractRate != null)
                {
                    // Let them edit rates
                    RateEditEnable = true;

                    // Let them select Other Rates
                    RatePickEnabled = true;

                    // There are no Alt Rates
                    //EnableAltRates = false;

                    // Let them select another contract
                    if (_view.IsInView)
                        contractListViewModel.EnablePicker = true;

                    return false;
                }
                if (SelectedContractRate == null)
                {
                    // Nothing to edit
                    RateEditEnable = false;

                    // Nothing to select
                    RatePickEnabled = false;

                    // There are no Alt Rates
                    //EnableAltRates = false;

                    // Let them select another contract
                    if(contractListViewModel != null && _view.IsInView)
                        contractListViewModel.EnablePicker = true;

                    return false;
                }
                return false;
            }
        }

        private ObservableCollection<ContractRateAlt> altContractRates;
        public ObservableCollection<ContractRateAlt> AltContractRates
        {
            get
            {
                return altContractRates;
            }
            set
            {
                if (altContractRates != value)
                {
                    altContractRates.ForEach((thislist, eachAltContract) => RemoveWeakEventListener(eachAltContract, ContractAltRateListener));
                    altContractRates = value;
                    if( altContractRates.ForEach((thislist, eachAltContract) => AddWeakEventListener(eachAltContract, ContractAltRateListener)) > 0)
                        SelectedAltContractRate = altContractRates[0];
                    else
                        SelectedAltContractRate = null;
                    RaisePropertyChanged("AltContractRates");
                }
            }
        }

        private ContractRateAlt selectedAltContractRate;
        public ContractRateAlt SelectedAltContractRate
        {
            get
            {
                return selectedAltContractRate;
            }
            set
            {
                if (selectedAltContractRate != value)
                {
                    selectedAltContractRate = value;
                    if (selectedAltContractRate == null)
                        IndexToAltText = "0 of 0";
                    else
                        IndexToAltText = String.Format("{0} of {1}", AltContractRates.IndexOf(selectedAltContractRate) + 1, AltContractRates.Count);
                    if(selectedAltContractRate != null)
                        selectedAltContractRate.Modified = false;

                    RaisePropertyChanged("AltRateId");
                    RaisePropertyChanged("SelectedAltContractRate");
                    RaisePropertyChanged("CanEditAlt");
                }
            }
        }

        private bool ratePickEnabled;
        public bool RatePickEnabled
        {
            get
            {
                return ratePickEnabled;
            }
            set
            {
                if (ratePickEnabled != value)
                {
                    ratePickEnabled = value;
                    RaisePropertyChanged("RatePickEnabled");
                }
            }
        }


        private void ContractAltRateListener(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedAltContractRate != null)
            {
                SelectedAltContractRate.Modified = true;
                if (_view.IsInView)
                contractListViewModel.EnablePicker = false;
            }
        }

        public void SetRateItemDetails()
        {
            if (SelectedContractRate == null)
            {
                SelectedChargeMethod = null;
                SelectedStatCompMethod = null;
                SelectedFaxCompMethod = null;
                return;
            }
            SelectedChargeMethod = (ChargeMethods.First(cm => cm.CHAR_COMP_METHOD == SelectedContractRate.CHAR_COMP_METHOD) as ChargeMethod);
            SelectedStatCompMethod = (StatCompMethods.First(scm => scm.STAT_COMP_METHOD == SelectedContractRate.STAT_COMP_METHOD) as StatCompMethod);
            SelectedFaxCompMethod = (FaxCompMethods.First(fcm => fcm.FAX_COMP_METHOD == SelectedContractRate.FAX_COMP_METHOD) as FaxCompMethod);
            SelectedContractRate.Modified = false;
        }


        #endregion //Properties

        #region Command Properties

        public ICommand AddCommand
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

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand<object>(CancelExecute, CanCancelExecute);
                }
                return _cancelCommand;
            }
        }
        ICommand _cancelCommand;
        
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

        public ICommand PrevAltCommand
        {
            get
            {
                if (_prevAltCommand == null)
                {
                    _prevAltCommand = new RelayCommand<object>(PrevAltExecute, CanPrevAltExecute);
                }
                return _prevAltCommand;
            }
        }
        ICommand _prevAltCommand;

        public ICommand NextAltCommand
        {
            get
            {
                if (_nextAltCommand == null)
                {
                    _nextAltCommand = new RelayCommand<object>(NextAltExecute, CanNextAltExecute);
                }
                return _nextAltCommand;
            }
        }
        ICommand _nextAltCommand;


        public ICommand AddAltCommand
        {
            get
            {
                if (_addAltCommand == null)
                {
                    _addAltCommand = new RelayCommand<object>(AddAltExecute, CanAddAltExecute);
                }
                return _addAltCommand;
            }
        }
        ICommand _addAltCommand;

        public ICommand CancelAltCommand
        {
            get
            {
                if (_cancelAltCommand == null)
                {
                    _cancelAltCommand = new RelayCommand<object>(CancelAltExecute, CanCancelAltExecute);
                }
                return _cancelAltCommand;
            }
        }
        ICommand _cancelAltCommand;

        public ICommand DeleteAltCommand
        {
            get
            {
                if (_deleteAltCommand == null)
                {
                    _deleteAltCommand = new RelayCommand<object>(DeleteAltExecute, CanDeleteAltExecute);
                }
                return _deleteAltCommand;
            }
        }
        ICommand _deleteAltCommand;

        public ICommand SaveAltCommand
        {
            get
            {
                if (_saveAltCommand == null)
                {
                    _saveAltCommand = new RelayCommand<object>(SaveAltExecute, CanSaveAltExecute);
                }
                return _saveAltCommand;
            }
        }
        ICommand _saveAltCommand;

        public ICommand PopCommand
        {
            private get;
            set;
        }

        #endregion //Command Properties




        #region Command Methods

        bool CanAddExecute(object param)
        {
            return (currentContract == null || Modified ) ? false : true;
        }

        void AddExecute(object param)
        {
            ContractRate newRate = new ContractRate();
            newRate.CONTRACT_ID = CurrentContract.CONTRACT_ID;
            ContractRates.Add(newRate);
            newRate.Modified = true;
            SelectedContractRate = newRate;
        }

        bool CanCancelExecute(object param)
        {
            return (Modified) ? true : false;
        }

        private void ContractRateListener(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedContractRate != null)
            {
                SelectedContractRate.Modified = true;

                if (_view.IsInView)

                contractListViewModel.EnablePicker = false;
            }
        }

        private bool enableAltRates;
        public bool EnableAltRates
        {
            get
            {
                return enableAltRates;
            }
            set
            {
                if (enableAltRates != value)
                {
#if DEBUG
                    if(value == false)
                        enableAltRates = false;
#endif
                    enableAltRates = value;
                    RaisePropertyChanged("EnableAltRates");
                }
            }
        }

        void CancelExecute(object param)
        {
            ResetRates(SelectedContractRate.CONTRACT_RATE_ID);
            if (_view.IsInView)

            contractListViewModel.EnablePicker = true;

        }

        bool CanDeleteExecute(object param)
        {
            return (Modified) ? false : true;
        }

        void DeleteExecute(object param)
        {
            int currentIndex = SelectedIndexOfContractRate;
            RemoveWeakEventListener(SelectedContractRate, ContractRateListener);
            this.ContractRateTarget.Remove(SelectedContractRate);
            ContractRates.Remove(SelectedContractRate);

            SelectedIndexOfContractRate = currentIndex - 1;
        }

        bool CanSaveExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void SaveExecute(object param)
        {
            int id = SelectedContractRate.CONTRACT_RATE_ID;
            try
            {
                if (SelectedContractRate.CONTRACT_RATE_ID == 0)
                    ContractRateTarget.Add(SelectedContractRate);
                else
                    this.ContractRateTarget.Update(SelectedContractRate);
                SelectedContractRate.Modified = false;
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
            ResetRates(id);
        }

        bool CanCancelAltExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void CancelAltExecute(object param)
        {
            ResetAltRates(SelectedAltContractRate.RATE_NBR);

        }

        void DeleteAltExecute(object param)
        {
            if (SelectedAltContractRate != null)
            {
                RemoveWeakEventListener(SelectedAltContractRate, ContractAltRateListener);
                this.ContractRateAltTarget.Remove(SelectedAltContractRate);

                AltContractRates.Remove(SelectedAltContractRate);
                if(AltContractRates.Count >= 1)
                    SelectedAltContractRate = AltContractRates[0];
                else
                    SelectedAltContractRate = null;
            }
        }

        public string RateId
        {
            get
            {
                return (SelectedContractRate == null) ? "None" : "RateId: " + SelectedContractRate.CONTRACT_RATE_ID.ToString();
            }
        }

        public string AltRateId
        {
            get
            {
                return (SelectedAltContractRate == null) ? "None" : "AltRateNbr: " + SelectedAltContractRate.RATE_NBR.ToString();
            }
        }

        bool CanAddAltExecute(object param)
        {
            return  (Modified || SelectedContractRate == null) ? false : true;
        }

        void AddAltExecute(object param)
        {
            ContractRateAlt newAltRate;
            if (SelectedAltContractRate != null)
                newAltRate = SelectedAltContractRate.Clone();
            else
                newAltRate = new ContractRateAlt();
            newAltRate.CONTRACT_RATE_ID = SelectedContractRate.CONTRACT_RATE_ID;
            AltContractRates.Add(newAltRate);
            SelectedAltContractRate = newAltRate;
            SelectedAltContractRate.Modified = true;
            SelectedAltContractRate.RATE_NBR = 0;

        }

        void SaveAltExecute(object param)
        {
            decimal id = SelectedAltContractRate.RATE_NBR;
            try
            {
                if (SelectedAltContractRate.RATE_NBR == 0)
                {
                    decimal rateNbr = 1;
                    foreach (ContractRateAlt contractRateAlt in AltContractRates)
                    {
                        if (contractRateAlt.CONTRACT_RATE_ID == SelectedAltContractRate.CONTRACT_RATE_ID)
                            rateNbr = (contractRateAlt.RATE_NBR >= rateNbr) ? contractRateAlt.RATE_NBR + 1 : rateNbr;
                    }
                    SelectedAltContractRate.RATE_NBR = rateNbr;
                    ContractRateAltTarget.Add(SelectedAltContractRate);
                    SelectedAltContractRate.Modified = false;
                }
                else
                {
                    ContractRateAltTarget.Update(SelectedAltContractRate);
                    SelectedAltContractRate.Modified = false;
                }
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }

            ResetAltRates(id);

        }

        bool CanSaveAltExecute(object param)
        {
            return (Modified) ? true : false;
        }


        bool CanPrevAltExecute(object param)
        {
            if (AltContractRates != null && AltContractRates != null && AltContractRates.Count > 0 && AltContractRates.IndexOf(SelectedAltContractRate) > 0 )
                return true;
            return false;
        }



        bool CanDeleteAltExecute(object param)
        {
            return (!Modified && AltContractRates != null && AltContractRates.Count > 0 && SelectedAltContractRate != null) ? true : false;
        }

        void PrevAltExecute(object param)
        {
            SelectedAltContractRate = AltContractRates[AltContractRates.IndexOf(SelectedAltContractRate) - 1];
        }

        bool CanNextAltExecute(object param)
        {
            if(AltContractRates!= null && AltContractRates.Count > 0 && AltContractRates.IndexOf( SelectedAltContractRate ) < AltContractRates.Count - 1)
                return true;
            return false;
        }

        void NextAltExecute(object param)
        {
            int indexOf = AltContractRates.IndexOf(SelectedAltContractRate);
            bool really = (AltContractRates != null && AltContractRates.Count > 0 && AltContractRates.IndexOf(SelectedAltContractRate) < AltContractRates.Count - 1);
            try
            {
                SelectedAltContractRate = AltContractRates[AltContractRates.IndexOf(SelectedAltContractRate) + 1];
            }
            catch (Exception ex)
            { 
            }
        }

        public void ClearOutPendingAdds()
        {
            foreach (ContractRate contractRate in this.ContractRates)
            {
                if (contractRate.CONTRACT_RATE_ID == 0)
                {
                    ContractRates.Remove(contractRate);
                    break;
                }
            }

            foreach (ContractRateAlt contractRateAlt in AltContractRates)
            {
                if (contractRateAlt.RATE_NBR == 0)
                {
                    AltContractRates.Remove(contractRateAlt);
                    break;
                }
            }
        }
        #endregion //Command Methods


        public void Dispose()
        {
            if (AltContractRates != null)
            {
                contractRates.ForEach((thislist, eachContract) => RemoveWeakEventListener(eachContract, ContractRateListener));
                AltContractRates.ForEach((thislist, eachAltContract) => RemoveWeakEventListener(eachAltContract, ContractRateListener));
            }

        }
    }
}
