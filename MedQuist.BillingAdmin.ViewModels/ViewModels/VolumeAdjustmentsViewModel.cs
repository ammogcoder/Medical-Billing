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
using System.ComponentModel;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// </summary>
    [Export]
    public class VolumeAdjustmentsViewModel : ViewModel<IVolumeAdjustmentsView>, IDisposable
    {
        #region Fields
        private IVolumeAdjustmentsView ThisView;
        private string NotApplicable = "<not applicable>";
        #endregion
        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<GroupsOnClientDefNote></param>
        [ImportingConstructor]
        public VolumeAdjustmentsViewModel(IVolumeAdjustmentsView view)
            : base(view)
        {
            ThisView = view;


            AddOnChgTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateAddOnChgTypeRepository();

            AdjTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateAdjTypeRepository();

            ScopeRuleTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateScopeRuleRepository();

            VolumeEvtTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateVolumeEvtTypeRepository();

        }
        #endregion

        private IAddOnChgTypeRepository AddOnChgTypeTarget { get; set; }

        private IAdjTypeRepository AdjTypeTarget { get; set; }

        private IScopeRuleRepository ScopeRuleTarget  { get; set; }

        private IVolumeEvtTypeRepository VolumeEvtTypeTarget { get; set; }


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

        private ObservableCollection<VolumeEvtType> volumeEvtTypes;
        public ObservableCollection<VolumeEvtType> VolumeEvtTypes
        {
            get
            {
                if (volumeEvtTypes == null)
                    volumeEvtTypes = VolumeEvtTypeTarget.FetchVolumeEvtType();
                return volumeEvtTypes;
            }
            set
            {
                if (volumeEvtTypes != value)
                {
                    volumeEvtTypes = value;
                    RaisePropertyChanged("VolumeEvtTypes");
                    RaisePropertyChanged("EnableList");
                }
            }
        }


        private VolumeEvtType selectedVolumeEvtType;
        public VolumeEvtType SelectedVolumeEvtType
        {
            get
            {
                return selectedVolumeEvtType;
            }
            set
            {
                if (selectedVolumeEvtType != value)
                {
                    SelectedAdjType = null;
                    SelectedScopeRule = null;
                    SelectedAddOnChgType = null;
                    if (selectedVolumeEvtType != null)
                        RemoveWeakEventListener(selectedVolumeEvtType, VolumeEvtListener);

                    selectedVolumeEvtType = value;
                    if (selectedVolumeEvtType != null)
                    {
                        SelectedAdjType = (AdjTypes.First(p => p.ADJ_TYPE == selectedVolumeEvtType.ADJ_TYPE) as AdjType);
                        SelectedScopeRule = (ScopeRules.FirstOrDefault(p => p.SCOPE_RULE == selectedVolumeEvtType.SCOPE_RULE) as ScopeRule);

                        SelectedAddOnChgType = (AddOnChgTypes.FirstOrDefault(p => p.ADD_ON_CHG_TYPE_ID == selectedVolumeEvtType.ADD_ON_CHG_TYPE_ID) as AddOnChgType);
                        if (SelectedAddOnChgType == null)
                            SelectedAddOnChgType = AddOnChgTypes[0];
                        selectedVolumeEvtType.Modified = false;
                        AddWeakEventListener(selectedVolumeEvtType, VolumeEvtListener);
                    }
                    else
                    {
                        SelectedAdjType = null;
                        SelectedScopeRule = null;
                        SelectedAddOnChgType = null;
                    }
                    RaisePropertyChanged("EnableList");
                    RaisePropertyChanged("SelectedVolumeEvtType");
                }
            }
        }

        private ObservableCollection<ScopeRule> scopeRules;
        public ObservableCollection<ScopeRule> ScopeRules
        {
            get
            {
                if (scopeRules == null)
                    scopeRules = ScopeRuleTarget.GetScopeRules();
                return scopeRules;
            }
            set
            {
                if (scopeRules != value)
                {
                    scopeRules = value;
                    RaisePropertyChanged("ScopeRules");
                }
            }
        }

        private ScopeRule selectedScopeRule;
        public ScopeRule SelectedScopeRule
        {
            get
            {
                return selectedScopeRule;
            }
            set
            {
                if (selectedScopeRule != value)
                {
                    selectedScopeRule = value;
                    if (selectedScopeRule != null && selectedVolumeEvtType != null)
                        selectedVolumeEvtType.SCOPE_RULE = selectedScopeRule.SCOPE_RULE;
                    RaisePropertyChanged("SelectedScopeRule");
                }
            }
        }

        private ObservableCollection<AdjType> adjTypes;
        public ObservableCollection<AdjType> AdjTypes
        {
            get
            {
                if (adjTypes == null)
                    adjTypes = AdjTypeTarget.GetAdjTypes();
                return adjTypes;
            }
            set
            {
                if (adjTypes != value)
                {
                    adjTypes = value;
                    RaisePropertyChanged("AdjTypes");
                }
            }
        }

        private AdjType selectedAdjType;
        public AdjType SelectedAdjType
        {
            get
            {
                return selectedAdjType;
            }
            set
            {
                if (selectedAdjType != value)
                {
                    selectedAdjType = value;
                    if (selectedAdjType != null && selectedVolumeEvtType != null)
                        selectedVolumeEvtType.ADJ_TYPE = SelectedAdjType.ADJ_TYPE;
                    RaisePropertyChanged("SelectedAdjType");
                }
            }
        }

        private ObservableCollection<AddOnChgType> addOnChgTypes;
        public ObservableCollection<AddOnChgType> AddOnChgTypes
        {
            get
            {
                if (addOnChgTypes == null)
                {
                    addOnChgTypes = AddOnChgTypeTarget.GetAddOnChgTypes();
                    if(addOnChgTypes != null)
                        addOnChgTypes.Insert(0, new AddOnChgType { DESCR = NotApplicable });
                }
                return addOnChgTypes;
            }
            set
            {
                if (addOnChgTypes != value)
                {
                    addOnChgTypes = value;
                    RaisePropertyChanged("AddOnChgTypes");
                }
            }
        }

        private AddOnChgType selectedAddOnChgType;
        public AddOnChgType SelectedAddOnChgType
        {
            get
            {
                return selectedAddOnChgType;
            }
            set
            {
                if (selectedAddOnChgType != value)
                {
                    selectedAddOnChgType = value;
                    if (selectedAddOnChgType != null && selectedVolumeEvtType != null)
                        selectedVolumeEvtType.ADD_ON_CHG_TYPE_ID = selectedAddOnChgType.ADD_ON_CHG_TYPE_ID;
                    RaisePropertyChanged("SelectedAddOnChgType");
                }
            }
        }

        private void VolumeEvtListener(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("EnableList");
        }

        public bool EnableList
        {
            get
            {
                return !Modified;
            }
        }

        public bool Modified
        {
            get
            {
                return (selectedVolumeEvtType == null) ? false : selectedVolumeEvtType.Modified;
            }
        }

        bool CanAddExecute(object param)
        {
            return (Modified && selectedVolumeEvtType != null) ? false : true;
        }


        void AddExecute(object param)
        {
            VolumeEvtType vet = new VolumeEvtType { DESCR = "<none>" };
            VolumeEvtTypes.Insert(0, vet);
            SelectedVolumeEvtType = vet;
            SelectedVolumeEvtType.Modified = true;
        }


        bool CanRestoreExecute(object param)
        {
            return (Modified && selectedVolumeEvtType != null) ? true : false;
        }

        void RestoreExecute(object param)
        {
            try
            {
                int ind = listIndex;
                RemoveWeakEventListener(selectedVolumeEvtType, VolumeEvtListener);
                VolumeEvtTypes = VolumeEvtTypeTarget.FetchVolumeEvtType();
                SelectedVolumeEvtType = VolumeEvtTypes[ 
                    (ind < VolumeEvtTypes.Count) ? ind : 0
                    ];
                ListIndex = (ind < VolumeEvtTypes.Count) ? ind : 0;
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private int listIndex;
        public int ListIndex
        {
            get
            {
                return listIndex;
            }
            set
            {
                listIndex = value;
                RaisePropertyChanged("ListIndex");
            }
        }

        bool CanDeleteExecute(object param)
        {
            return (selectedVolumeEvtType != null) ? true : false;
        }

        void DeleteExecute(object param)
        {
            try
            {
                RemoveWeakEventListener(selectedVolumeEvtType, VolumeEvtListener);
                VolumeEvtTypeTarget.Remove(selectedVolumeEvtType);
                RestoreExecute(param);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        bool CanSaveExecute(object param)
        {
            return (Modified && SelectedAdjType != null && SelectedScopeRule != null) ? true : false;
        }

        void SaveExecute(object param)
        {
            try
            {
                if(selectedVolumeEvtType.VOLUME_EVT_TYPE_ID == 0)
                    VolumeEvtTypeTarget.Add(selectedVolumeEvtType);
                else
                    VolumeEvtTypeTarget.Update(selectedVolumeEvtType);
                RestoreExecute(param);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }

        }

        bool CanPopExecute(object param)
        {
            return  false;
        }

        void PopExecute(object parma)
        {
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

        public void Dispose()
        {
        }
    }
}