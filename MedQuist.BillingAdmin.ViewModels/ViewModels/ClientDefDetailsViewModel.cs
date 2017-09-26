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
    public class ClientDefDetailsViewModel : ViewModel<IClientDefDetailsView>, IDisposable
    {
        #region Fields
        private IClientDefDetailsView _view;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<GroupsOnClientDefNote></param>
        [ImportingConstructor]
        public ClientDefDetailsViewModel(IClientDefDetailsView view)
            : base(view)
        {
            _view = view;

            ExtSysTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtSysRepository();

            ExtWorkTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtWorkTypeRepository();

            ExtWorkTypeSourceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtWorkTypeSourceRepository();

            OverRideKeySourceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateOverRideKeySourceRepository();

            ClientTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();

            InvoiceGroupTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();

        }
        #endregion

        #region Data Targets
        public IExtSysRepository ExtSysTarget { get; set; }

        public IExtWorkTypeRepository ExtWorkTypeTarget { get; set; }

        public IOverRideKeySourceRepository OverRideKeySourceTarget { get; set; }

        public IExtClientRepository ClientTarget { get; set; }

        public IExtWorkTypeSourceRepository ExtWorkTypeSourceTarget { get; set; }

        public ClientDefListViewModel ClientDefListViewModel { get; set; }

        public InvoiceGroupViewModel invoiceGrpViewModel { get; set; }

        public IInvoiceGroupRepository InvoiceGroupTarget { get; set; }
        #endregion

        #region Properties
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


        private ExtWorkTypeSource selectedExtWorkTypeSource;
        public ExtWorkTypeSource SelectedExtWorkTypeSource
        {
            get
            {
                return selectedExtWorkTypeSource;
            }
            set
            {
                if (selectedExtWorkTypeSource != value)
                {
                    selectedExtWorkTypeSource = value;

                    if (currentClientDef != null && selectedExtWorkTypeSource != null)
                        currentClientDef.ExtWorkTypeSourceDescr = selectedExtWorkTypeSource.DESCR;

                    RaisePropertyChanged("SelectedExtWorkTypeSource");
                }
            }
        }

        private ObservableCollection<ExtWorkTypeSource> extWorkTypeSources;
        public ObservableCollection<ExtWorkTypeSource> ExtWorkTypeSources
        {
            get
            {
#if ORACLE
                if (extWorkTypeSources == null)
                    extWorkTypeSources = this.ExtWorkTypeSourceTarget.GetWorkTypeSources();
#endif
                return extWorkTypeSources;
            }
            set
            {
                if (extWorkTypeSources != value)
                {
                    extWorkTypeSources = value;
                    RaisePropertyChanged("ExtWorkTypeSources");
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

        private ObservableCollection<OverRideKeySource> overRideKeySources;
        public ObservableCollection<OverRideKeySource> OverRideKeySources
        {
            get
            {
#if ORACLE
                if (overRideKeySources == null)
                    overRideKeySources = OverRideKeySourceTarget.GetOverRides();
#endif
                return overRideKeySources;
            }
            set
            {
                if (overRideKeySources != value)
                {
                    overRideKeySources = value;


                    RaisePropertyChanged("OverRideKeySources");
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
                if (extSyss == null)
                    extSyss = ExtSysTarget.GetExtSys();
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

        private string defaultInvoiceGroupName;
        private string DefaultInvoiceGroupName
        {
            get
            {
                return defaultInvoiceGroupName;
            }
            set
            {
                defaultInvoiceGroupName = value;
                RaisePropertyChanged("DefaultInvoiceGroupName");
            }
        }

        /// <summary>
        /// TABLE EXT_SYS
        /// </summary>
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
                        copyClientDef = currentClientDef.Clone();
                    SetClientDefDetails();
                    RaisePropertyChanged("CurrentClientDef");
                }
            }
        }

        public bool Modified
        {
            get
            {
                if (this.CurrentClientDef != null && this.CurrentClientDef.Modified)
                {
                    return true;
                }
                return false;
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

        #endregion

        #region Methods
        void SetClientDefDetails()
        {
            if (currentClientDef == null)
            {
                SelectedExtSys = null;
                SelectedOverRideKeySource = null;
                SelectedExtWorkTypeSource = null;
                return;
            }

            SelectedExtSys = (ExtSyss.First(es => es.EXT_SYS == currentClientDef.ExtSys) as ExtSys);
            SelectedOverRideKeySource = (OverRideKeySources.First(scm => scm.OVERRIDE_KEY_SOURCE == currentClientDef.OverrideKeySource) as OverRideKeySource);
            SelectedExtWorkTypeSource = (ExtWorkTypeSources.First(ewts => ewts.DESCR == currentClientDef.ExtWorkTypeSourceDescr) as ExtWorkTypeSource);

            if (CurrentClientDef.DefaultInvoiceGrpID > 0)
            {
                InvoiceGroup group = new InvoiceGroup { InvoiceGrpId = (int)CurrentClientDef.DefaultInvoiceGrpID };
                group = InvoiceGroupTarget.Get(group);
                CurrentClientDef.DefaultInvoiceGrpName = group.Description;
            }

            currentClientDef.Modified = false;
        }
        #endregion

        #region Command Methods
        bool CanAddExecute(object param)
        {
            return (Modified) ? false : true;
        }

        void AddExecute(object param)
        {
            ClientLocation clientLocation;
            if (currentClientDef != null)
            {
                clientLocation = currentClientDef.Clone();
            }
            else
            {
                clientLocation = new ClientLocation();
                clientLocation.ExtSys = ExtSyss[0].EXT_SYS;
                clientLocation.OverrideKeySource = OverRideKeySources[0].OVERRIDE_KEY_SOURCE;
                clientLocation.ExtWorkTypeSourceDescr = ExtWorkTypeSources[0].DESCR;
            }
            clientLocation.ExtClientKey = null;// Force them to type in a new  one
            clientLocation.IsAdding = true;

            this.ClientDefListViewModel.ClientLocations.Add(clientLocation);
            ClientDefListViewModel.CurrentClientLocation = clientLocation;
            //this.ClientDefListViewModel.clientLocations.Add(clientLocation);
            //ClientDefListViewModel.currentClientLocation = clientLocation;

            ClientDefListViewModel.EnablePicker = false;
            CurrentClientDef = clientLocation;
            CurrentClientDef.Modified = true;
        }

        bool CanRestoreExecute(object param)
        {
            return (Modified) ? true : false;
        }

        void RestoreExecute(object param)
        {
            //bool bWasCloned = (copyClientDef == null) ? false : copyClientDef.WasCloned;
            ClientDefListViewModel.SwapClientDefItem(this.copyClientDef);
            //if (!bWasCloned)
            // {
            //     SelectedExtSys = null;
            //     SelectedExtWorkTypeSource = null;
            //     SelectedOverRideKeySource = null;
            // }
        }


        bool CanDeleteExecute(object param)
        {
            return (Modified || currentClientDef == null) ? false : true;
        }

        void DeleteExecute(object param)
        {
            try
            {
                this.ClientTarget.Remove(currentClientDef);
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
        }

        bool CanSaveExecute(object param)
        {
            return (Modified && CurrentClientDef.ValidateKeys() == null) ? true : false;
        }

        void SaveExecute(object param)
        {
            try
            {
                if (currentClientDef.IsAdding)
                    ClientTarget.Add(currentClientDef);
                else
                    ClientTarget.Update(currentClientDef);
                currentClientDef.Modified = false;
                this.ClientDefListViewModel.EnablePicker = true;
            }
            catch (Exception x)
            {
                _view.ShowMsg(x.ToString());
            }
        }

        bool CanPopExecute(object param)
        {
            return true;
        }
        void PopExecute(object parma)
        {
            //RaisePropertyChanged("InvoiceGrpViewModel");
            _view.Pop();
        }


        /// <summary>
        /// Exposed for codeBehind - through the routed message
        /// </summary>
        /// <param name="invoiceId"></param>
        public void SetDefaultInvoiceGroup(int invoiceId,string invcoiceGrpName)
        {
            if (CurrentClientDef == null)
                return;
            if (invoiceId < 0)
            {
                CurrentClientDef.DefaultInvoiceGrpName = String.Empty;
                CurrentClientDef.DefaultInvoiceGrpID = null;
            }
            else
            {
                CurrentClientDef.DefaultInvoiceGrpID = invoiceId;
                CurrentClientDef.DefaultInvoiceGrpName = invcoiceGrpName;
            }
        }

        #endregion

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

        #endregion

        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}