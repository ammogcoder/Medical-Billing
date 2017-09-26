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
    /// </summary>
    [Export]
    public class ClientDefWorkTypeViewModel : ViewModel<IClientDefWorkTypeView>, IDisposable
    {
        private IClientDefWorkTypeView _view;
        private ContractRateAlt NoneAltRate;
        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<GroupsOnClientDefNote></param>
        [ImportingConstructor]
        public ClientDefWorkTypeViewModel(IClientDefWorkTypeView view)
            : base(view)
        {
            _view = view;

            NoneAltRate = new ContractRateAlt();
            NoneAltRate.DESCR = "<none>";

            //ExtSysTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtSysRepository();

            ContractRateTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRateRepository();

            ContractRateAltTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRateAltRepository();

            ExtWorkTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtWorkTypeRepository();

            ExtClientTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();

            ExtWorkTypeSourceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtWorkTypeSourceRepository();

            InvoiceGroupTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();
            //OverRideKeySourceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateOverRideKeySourceRepository();

            //ClientTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();


        }
        #endregion


        //public IExtSysRepository ExtSysTarget { get; set; }

        //public IInvoiceGroupView InvoiceGroupViewTarget
        private IContractRateRepository ContractRateTarget { get; set; }

        private IContractRateAltRepository ContractRateAltTarget { get; set; }

        private IInvoiceGroupRepository InvoiceGroupTarget { get; set; }

        public IExtWorkTypeRepository ExtWorkTypeTarget { get; set; }

        public IOverRideKeySourceRepository OverRideKeySourceTarget { get; set; }

        private IExtClientRepository ExtClientTarget { get; set; }


        public IExtWorkTypeSourceRepository ExtWorkTypeSourceTarget { get; set; }

        public ClientDefListViewModel clientDefListViewModel { get; set; }
        
        public ClientDefViewModel clientDefViewModel { get; set; }

        public InvoiceGroupViewModel InvoiceGrpViewModel { get; set; }

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
                    //if (altContractRates != null)
                    //    foreach (ContractRateAlt altContractRate in altContractRates)
                    //        RemoveWeakEventListener(altContractRate, ContractAltRateListener);
                    altContractRates = value;
                    //if (altContractRates != null && altContractRates.Count > 0)
                    // {
                    //    foreach (ContractRateAlt altContractRate in altContractRates)
                    //        AddWeakEventListener(altContractRate, ContractAltRateListener);
                    //    SelectedAltContractRate = altContractRates[0];
                    //}
                    //else
                    //    SelectedAltContractRate = null;
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

                    if (selectedAltContractRate != null && selectedExtWorkType != null)
                    {
                        if (selectedAltContractRate.RATE_NBR == 0)
                            selectedExtWorkType.RATE_NBR = null;
                        else
                            selectedExtWorkType.RATE_NBR = selectedAltContractRate.RATE_NBR;
                    }
                    //if (selectedAltContractRate == null)
                    //    IndexToAltText = "0 of 0";
                    //else
                    //    IndexToAltText = String.Format("{0} of {1}", AltContractRates.IndexOf(selectedAltContractRate) + 1, AltContractRates.Count);
                    //if (selectedAltContractRate != null)
                    //    selectedAltContractRate.Modified = false;

                    //RaisePropertyChanged("AltRateId");
                    RaisePropertyChanged("SelectedAltContractRate");
                    //RaisePropertyChanged("CanEditAlt");
                }
            }
        }

        void ResetAltRateOverrideSelection()
        {
            if (SelectedExtWorkType != null)
            {
                AltContractRates = this.ContractRateAltTarget.GetAltRatesFromClients(selectedExtWorkType.EXT_SYS, selectedExtWorkType.EXT_CLIENT_KEY);
                AltContractRates.Insert(0, NoneAltRate);
                if (selectedExtWorkType.RATE_NBR == null)
                    SelectedAltContractRate = NoneAltRate;
                else
                   SelectedAltContractRate = (AltContractRates.FirstOrDefault(acr => acr.RATE_NBR == selectedExtWorkType.RATE_NBR) as ContractRateAlt);
            }
            else
            {
                AltContractRates = new ObservableCollection<ContractRateAlt>();
                AltContractRates.Insert(0, NoneAltRate);
                SelectedAltContractRate = NoneAltRate;
            }
        
        }


        /// <summary>
        /// This is the DESCR of the "Over the InvoiceGrp to send this Worktype to"
        /// 
        /// </summary>
        public string OverRideInvoiceGroupSentTo
        {
            get
            {
                return overRideInvoiceGroupSentTo;
            }
            set
            {
                overRideInvoiceGroupSentTo = value;
                RaisePropertyChanged("OverRideInvoiceGroupSentTo");
            }
        }
        private string overRideInvoiceGroupSentTo;

        /// <summary>
        /// Exposed for codeBehind - through the routed message
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="description"></param>
        public void SetOverRideGrp(int invoiceId, string description)
        {
            if (SelectedExtWorkType == null)
                return;
            OverRideInvoiceGroupSentTo = description;
            if (invoiceId < 0)
                SelectedExtWorkType.INVOICE_GRP_ID =  null;
            else
                SelectedExtWorkType.INVOICE_GRP_ID =  invoiceId;
        }



        private ExtWorkType copySelectedExtWorkType;
        private ExtWorkType selectedExtWorkType;
        public ExtWorkType SelectedExtWorkType
        {
            get
            {
                return selectedExtWorkType;
            }
            set
            {
                //if (selectedExtWorkType != value)
                //{
                    selectedExtWorkType = value;
                    if (selectedExtWorkType != null)
                    {
                        copySelectedExtWorkType = selectedExtWorkType.Clone();

                        if (selectedExtWorkType.INVOICE_GRP_ID != null)
                        {
                            InvoiceGroup igOverRide = new InvoiceGroup();

                            //TODO - Horrible code!
                            igOverRide.InvoiceGrpId = int.Parse(selectedExtWorkType.INVOICE_GRP_ID.ToString());
                            try
                            {
                                InvoiceGroupTarget.Get(igOverRide);
                                OverRideInvoiceGroupSentTo = igOverRide.Description;
                            }
                            catch (Exception x)
                            {
                                _view.ShowMsg(x.ToString());
                                OverRideInvoiceGroupSentTo = string.Empty;
                            }
                        }
                        else
                        {
                            OverRideInvoiceGroupSentTo = string.Empty;

                        }

                        //ShowOverrideInvoice = (selectedExtWorkType.INVOICE_GRP_ID == null) ? "Collapsed" : "Visible";
                        ResetAltRateOverrideSelection();

                        //selectedExtWorkType.Modified = false;

                    }
                    else
                    {
                        ResetAltRateOverrideSelection();

                        //ShowOverrideInvoice = "Collapsed";
                    }
                    //    ThisView.SearchInvoicesByContract(selectedExtWorkType.INVOICE_GRP_ID);
                    //else
                    //    ThisView.SearchInvoicesByContract(null);

                    //if (currentClientDef != null && selectedExtWorkType != null)
                    //    currentClientDef.ExtWorkTypeSourceDescr = selectedExtWorkTypeSource.DESCR;
                    RaisePropertyChanged("SelectedExtWorkType");
                //}
            }
        }

        private decimal? overRidesInvoice;
        public decimal? OverRidesInvoice
        {
            get
            {
                return overRidesInvoice;
            }
            set
            {
                if (overRidesInvoice != value)
                {
                    overRidesInvoice = value;
                    SelectedExtWorkType.INVOICE_GRP_ID = value;
                    RaisePropertyChanged("SelectedExtWorkType");
                    RaisePropertyChanged("OverRidesInvoice");
                }
                //RaisePropertyChanged("OverRidesInvoice");
                //ShowOverrideInvoice = (selectedExtWorkType.INVOICE_GRP_ID == null) ? "Collapsed" : "Visible";
                
            }
        }

        //SearchContract
        /*
        private ExtWorkType SelectedExtWorkType;
        public ExtWorkType ExtWorkTypes
        {
            get
            {
                return selectedExtWorkType;
            }
            set
            {
                selectedExtWorkType = value;
                ThisView.
            }
        }
        */

        private ObservableCollection<ExtWorkType> extWorkTypes;
        public ObservableCollection<ExtWorkType> ExtWorkTypes
        {
            get
            {
                return extWorkTypes;
            }
            set
            {
                if (extWorkTypes != value)
                {
                    extWorkTypes = value;
                    SelectedExtWorkType = (extWorkTypes != null && extWorkTypes.Count > 0) ? extWorkTypes[0] : null;
                    RaisePropertyChanged("ExtWorkTypes");
                }
            }
        }

        private OverRideKeySource selectedOverRideKeySource;
        public OverRideKeySource SelectedOverRideKeySource
        {
            get
            {
                return selectedOverRideKeySource;
            }
            set
            {
                if (selectedOverRideKeySource != value)
                {
                    selectedOverRideKeySource = value;

                    if (currentClientDef != null && selectedOverRideKeySource != null)
                        currentClientDef.OverrideKeySource = selectedOverRideKeySource.OVERRIDE_KEY_SOURCE;

                    RaisePropertyChanged("SelectedOverRideKeySource");
                }
            }
        }

        private ExtSys selectedExtSys;
        public ExtSys SelectedExtSys
        {
            get
            {
                return selectedExtSys;
            }
            set
            {
                if (selectedExtSys != value)
                {
                    selectedExtSys = value;

                    if (currentClientDef != null && selectedExtSys != null)
                        currentClientDef.ExtSys = selectedExtSys.EXT_SYS;

                    RaisePropertyChanged("SelectedExtSys");
                }
            }
        }

        private ObservableCollection<ExtSys> extSyss;
        public ObservableCollection<ExtSys> ExtSyss
        {
            get
            {
                //if (extSyss == null)
                //    extSyss = ExtSysTarget.GetExtSys();
                return extSyss;
            }
            set
            {
                if (extSyss != value)
                {
                    extSyss = value;
                    RaisePropertyChanged("ExtSyss");
                }
            }
        }


        private bool hasClientDef;
        public bool HasClientDef
        {
            get
            {
                return hasClientDef;
            }
            set
            {
                hasClientDef = value;
                RaisePropertyChanged("HasClientDef");
            }

        }

        private ClientLocation copyClientDef;
        private ClientLocation currentClientDef;
        public ClientLocation CurrentClientDef
        {
            get
            {
                return currentClientDef;
            }
            set
            {
                if (currentClientDef != value)
                {
                    currentClientDef = value;
                    if (currentClientDef != null)
                    {
                        HasClientDef = true;
                        copyClientDef = currentClientDef.Clone();
                    }
                    else
                        HasClientDef = false;

                    SetClientDefWorkType();
                    RaisePropertyChanged("CurrentClientDef");
                }
            }
        }

        void SetClientDefWorkType()
        {
            if (currentClientDef == null)
            {
                AltContractRates = new ObservableCollection<ContractRateAlt>();
                AltContractRates.Insert(0, NoneAltRate);
                return;
            }

            ExtWorkTypes = ExtWorkTypeTarget.GetWorkTypes(currentClientDef.ExtSys, currentClientDef.ExtClientKey);
            ExtWorkTypes.ForEach((thisList, eachExtWorkType) => eachExtWorkType.Modified = false);
            ResetAltRateOverrideSelection();
#if ASDF
            foreach (OverRideKeySource overRideKeySource in OverRideKeySources)
            {
                if (currentClientDef.OverrideKeySource == overRideKeySource.OVERRIDE_KEY_SOURCE)
                {
                    SelectedOverRideKeySource = overRideKeySource;
                }
            }

            foreach (ExtWorkType extWorkType in ExtWorkTypes)
            {
                //if (currentClientDef.ExtWorkTypeSourceDescr == extWorkTypeSource.DESCR)
                //{
                //    SelectedExtWorkTypeSource = extWorkTypeSource;
                //}
            }
            currentClientDef.Modified = false;
#endif
        }

        public bool Modified
        {
            get
            {
                if ((this.CurrentClientDef != null && this.CurrentClientDef.Modified) ||
                    SelectedExtWorkType != null && SelectedExtWorkType.Modified)
                {
                    this.clientDefListViewModel.EnablePicker = false;
                    CanSelectExtWorkType = false;
                    return true;
                }
                if(this.clientDefListViewModel != null)
                    this.clientDefListViewModel.EnablePicker = true;
                CanSelectExtWorkType = true;
                return false;
            }
        }

        bool CanAddExecute(object param)
        {
            return (Modified || CurrentClientDef == null) ? false : true;
        }


        void AddExecute(object param)
        {

            ExtWorkType extWorkType;
            if(selectedExtWorkType != null)
                extWorkType = selectedExtWorkType.Clone();
            else
                extWorkType = new ExtWorkType();
            extWorkType.EXT_CLIENT_KEY = currentClientDef.ExtClientKey;
            extWorkType.EXT_SYS = currentClientDef.ExtSys;
            extWorkType.EXT_WORK_TYPE = string.Empty;
            if (ExtWorkTypes == null)
                ExtWorkTypes = new ObservableCollection<ExtWorkType>();

            ExtWorkTypes.Add(extWorkType);
            clientDefListViewModel.EnablePicker = false;
            SelectedExtWorkType = extWorkType;
            SelectedExtWorkType.Modified = true;
           
        }


        bool CanRestoreExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void RestoreExecute(object param)
        {
            bool wasAdding = false;
            foreach (ExtWorkType extWorkType in ExtWorkTypes)
            {
                if (extWorkType.IsAdding)
                {
                    ExtWorkTypes.Remove(extWorkType);
                    if (copySelectedExtWorkType != null)
                        SelectedExtWorkType = copySelectedExtWorkType;
                    else
                        SelectedExtWorkType = null;
                    wasAdding = true;
                }
            }
            if (!wasAdding)
            {
                int i = ExtWorkTypes.IndexOf(SelectedExtWorkType);
                ExtWorkTypes.Remove(SelectedExtWorkType);
                ExtWorkTypes.Insert(i, copySelectedExtWorkType);
                SelectedExtWorkType = copySelectedExtWorkType;
            }
            SetClientDefWorkType();
        }

        public InvoiceGroupViewModel invoiceGrpViewModel { get; set; }


        bool CanDeleteExecute(object param)
        {
            return (Modified || SelectedExtWorkType == null) ? false : true;
        }

        void DeleteExecute(object param)
        {
            try
            {
                ExtWorkTypeTarget.Remove(SelectedExtWorkType);
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
            SetClientDefWorkType();
        }

        bool CanSaveExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void SaveExecute(object param)
        {
            try
            {
                if (SelectedExtWorkType.IsAdding)
                    ExtWorkTypeTarget.Add(SelectedExtWorkType);
                else
                    ExtWorkTypeTarget.Update(SelectedExtWorkType);
                SelectedExtWorkType.Modified = false;
                this.clientDefListViewModel.EnablePicker = true;
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
            SetClientDefWorkType();

        }

        bool CanPopExecute(object param)
        {
            return (selectedExtWorkType != null) ? true : false;
        }

        void PopExecute(object parma)
        {
            RaisePropertyChanged("invoiceGrpViewModel");
            if (selectedExtWorkType != null)
                _view.SearchInvoicesByContract(selectedExtWorkType.INVOICE_GRP_ID);
            //else
            //    ThisView.SearchInvoicesByContract(null);

            // ThisView.Pop();
        }

        bool CanClearDefInvoice(object param)
        {
            return (SelectedExtWorkType == null || string.IsNullOrEmpty( OverRideInvoiceGroupSentTo ) ) ? false : true;

        }

        void ClearDefInvoice(object param)
        {
            SelectedExtWorkType.INVOICE_GRP_ID = null;
            OverRideInvoiceGroupSentTo = string.Empty;
            //ThisView.SearchInvoicesByContract(null);
        }


        private bool canSelectExtWorkType;
        public bool CanSelectExtWorkType
        {
            get
            {
                return canSelectExtWorkType;
            }
            set
            {
                canSelectExtWorkType = value;
                RaisePropertyChanged("CanSelectExtWorkType");
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

        private string overRideGroupName;
        public string OverRideGroupName
        {
            get
            {
                return overRideGroupName;
            }
            set
            {
                overRideGroupName = value;
                //if(overRideGroupName == null)
                //    ShowOverrideInvoice = "Collapsed";
                //else
                //    ShowOverrideInvoice = "Visible";

                RaisePropertyChanged("OverRideGroupName");
            }
        }

        /*
        private string showOverrideInvoice = "Collapsed";
        public string ShowOverrideInvoice
        {
            get
            {
                return showOverrideInvoice;
            }
            set
            {
                showOverrideInvoice = value;
                if (showOverrideInvoice == "Visible")
                {
                    InvoiceGroup group = new InvoiceGroup();
                    group.InvoiceGrpId = (int)selectedExtWorkType.INVOICE_GRP_ID;
                    InvoiceGroupTarget.Get(group);
                    OverRideGroupName = group.Description;
                }
                RaisePropertyChanged("ShowOverrideInvoice");
            }
        }
        */

        #region Command Properties

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

        public ICommand PopCommand
        {
            get
            {
                if (_popCommand == null)
                {
                    _popCommand = new RelayCommand<object>(PopExecute, CanPopExecute);
                }
                return _popCommand;
            }
        }
        ICommand _popCommand;

        public ICommand ClearDefInvoiceCommand
        {
            get
            {
                if (_clearDefInvoiceCommand == null)
                {
                    _clearDefInvoiceCommand = new RelayCommand<object>(ClearDefInvoice, CanClearDefInvoice);
                }
                return _clearDefInvoiceCommand;
            }
        }
        ICommand _clearDefInvoiceCommand;

        #endregion

        public void Dispose()
        {
        }
    }
}