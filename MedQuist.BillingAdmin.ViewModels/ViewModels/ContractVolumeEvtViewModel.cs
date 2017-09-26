using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
//using Spheris.Billing;
using Spheris.Billing.Core.Domain;
using System.Windows.Input;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for ContractVolumeEvt Section
    /// </summary>
    [Export]
    public class ContractVolumeEvtViewModel : ViewModel<IContractVolumeEvtView>, IDisposable
    {
        #region Fields
        private IContractVolumeEvtView ThisView;
        private bool bWasAdded = false;
        private bool bWasDeleted = false;
        private bool bRateWasAdded = false;
        private bool bRateWasDeleted = false;
        private bool bRatesModified = false;
        #endregion
        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractVolumeEvtNote></param>
        [ImportingConstructor]
        public ContractVolumeEvtViewModel(IContractVolumeEvtView view)
            : base(view)
        {
            ThisView = view;
            VolumeEvtRateTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateVolumeEvtRateRepository();
            VolumeEvtTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateVolumeEvtTypeRepository();
            ContractVolumeEvtTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractVolumeEvtRepository();

            SaveCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified),
                ExecuteDelegate = x => Save()
            };
            
            CancelCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified),
                ExecuteDelegate = x => Restore()
            };

            BackupContractVolumeEvts = new ObservableCollection<ContractVolumeEvt>();
            BackUpvolumeEvtRates = new ObservableCollection<VolumeEvtRate>();

        }
        #endregion

        void Save()
        {
            try
            {
                if (bRatesModified && selectedContractVolumeEvt != null)
                {
                    foreach (VolumeEvtRate ver in BackUpvolumeEvtRates)
                        if (!VolumeEvtRates.Contains(ver))
                            VolumeEvtRateTarget.Remove(ver);
                    foreach (VolumeEvtRate ver in VolumeEvtRates)
                        if (!BackUpvolumeEvtRates.Contains(ver))
                        {
                            ver.CONTRACT_VOLUME_EVT_ID = selectedContractVolumeEvt.CONTRACT_VOLUME_EVT_ID;
                            VolumeEvtRateTarget.Add(ver);
                        }
                    foreach (VolumeEvtRate ver in VolumeEvtRates)
                        if (ver.Modified)
                            VolumeEvtRateTarget.Update(ver);
                }
                foreach (ContractVolumeEvt cve in BackupContractVolumeEvts)
                    if (!ContractVolumeEvts.Contains(cve))
                        ContractVolumeEvtTarget.Remove(cve);
                foreach(ContractVolumeEvt cve in ContractVolumeEvts)
                    if (!BackupContractVolumeEvts.Contains(cve))
                    {
                        cve.CONTRACT_ID = CurrentContract.CONTRACT_ID;
                        ContractVolumeEvtTarget.Add(cve);
                    }
                foreach(ContractVolumeEvt cve in ContractVolumeEvts)
                    if(cve.Modified)
                        ContractVolumeEvtTarget.Update(cve);
                Restore(); 
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        void Restore()
        {
            try
            {
                if(bRatesModified && selectedContractVolumeEvt != null)
                    VolumeEvtRates = VolumeEvtRateTarget.FetchVolumeEvtRates(selectedContractVolumeEvt.CONTRACT_VOLUME_EVT_ID);
                bRatesModified = bRateWasAdded = bRateWasDeleted =false;
                ContractVolumeEvts = ContractVolumeEvtTarget.FetchVolumeEvts((decimal)currentContract.CONTRACT_ID);
                bWasAdded = bWasDeleted = false;
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private void VolumeEvtListener(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedContractVolumeEvt != null)
            {
                //CurrentGroupItem.Modified = true;
                //GroupVM.EnablePicker = false;
            }

        }
        
        private void VolumeEvtRateListener(object sender, PropertyChangedEventArgs e)
        {
            if (SelectedVolumeEvtRate != null)
            {
                //CurrentGroupItem.Modified = true;
                //GroupVM.EnablePicker = false;
            }

        }


        private void ContractVolumeEvtListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    bWasDeleted = true;
                    break;
                case NotifyCollectionChangedAction.Add:
                    bWasAdded = true;
                    break;
            }
        }

        private void VolumeEvtRatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    bRateWasDeleted = true;
                    break;
                case NotifyCollectionChangedAction.Add:
                    bRateWasAdded = true;
                    break;
            }
        }

        #region Properties

        #region Data Targets
        public IContractVolumeEvtRepository ContractVolumeEvtTarget { get; set; }
        public IVolumeEvtRateRepository VolumeEvtRateTarget { get; set; }
        public IVolumeEvtTypeRepository VolumeEvtTypeTarget { get; set; }
        public ContractListViewModel contractListViewModel { get; set; }

        #endregion

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
                    if (currentContract != null)
                    {
                        try
                        {
                            ContractVolumeEvts = ContractVolumeEvtTarget.FetchVolumeEvts((decimal)currentContract.CONTRACT_ID);
                        }
                        catch (Exception x)
                        {
                            ThisView.ShowMsg(x.ToString());
                        }
                    }
                    else
                        ContractVolumeEvts = null;
                    RaisePropertyChanged("CurrentContract");
                }
            }
        }

        private ObservableCollection<VolumeEvtType> volumeEvtTypes;
        public ObservableCollection<VolumeEvtType> VolumeEvtTypes
        {
            get
            {
                if (volumeEvtTypes == null)
                {
                    try
                    {
                        volumeEvtTypes = VolumeEvtTypeTarget.FetchVolumeEvtType();
                    }
                    catch (Exception x)
                    {
                        volumeEvtTypes = null;
                        ThisView.ShowMsg(x.ToString());
                    }
                }
                return volumeEvtTypes;
            }
            set
            {
                volumeEvtTypes = value;
                RaisePropertyChanged("VolumeEvtTypes");
            }
        }        
        
        private /*VolumeEvtType*/ decimal selectedVolumeEvtType;
        public /*VolumeEvtType*/ decimal SelectedVolumeEvtType
        {
            get
            {
                return selectedVolumeEvtType;
            }
            set
            {
                selectedVolumeEvtType = value;
                RaisePropertyChanged("SelectedVolumeEvtType");
            }
        }

        
        private ObservableCollection<VolumeEvtRate> BackUpvolumeEvtRates;
        private ObservableCollection<VolumeEvtRate> volumeEvtRates;
        public ObservableCollection<VolumeEvtRate> VolumeEvtRates
        {
            get
            {
                return volumeEvtRates;
            }
            set
            {
                if (volumeEvtRates != null)
                    volumeEvtRates.CollectionChanged -= VolumeEvtRatesChanged;
                volumeEvtRates = value;
                
                BackUpvolumeEvtRates.Clear();
                if (volumeEvtRates != null)
                {
                    volumeEvtRates.ForEach((lst, item) => BackUpvolumeEvtRates.Add(item.Clone()));
                    volumeEvtRates.CollectionChanged += VolumeEvtRatesChanged;
                }

                RaisePropertyChanged("VolumeEvtRates");
            }
        }

        private VolumeEvtRate selectedVolumeEvtRate;
        public VolumeEvtRate SelectedVolumeEvtRate
        {
            get
            {
                return selectedVolumeEvtRate;
            }
            set
            {
                if(selectedVolumeEvtRate != null)
                    RemoveWeakEventListener(selectedVolumeEvtRate, VolumeEvtRateListener); 

                selectedVolumeEvtRate = value;                    
                if(selectedVolumeEvtRate != null)
                    AddWeakEventListener(selectedVolumeEvtRate, VolumeEvtRateListener); 

                RaisePropertyChanged("SelectedVolumeEvtRate");
            }
        }

        void SetContractVolumeEvtsDetails()
        {
            ContractVolumeEvts.ForEach((thisList, thisItem) => 
                {
                    foreach (VolumeEvtType vt in VolumeEvtTypes)
                    {
                        if (vt.VOLUME_EVT_TYPE_ID == thisItem.VOLUME_EVT_TYPE_ID)
                        {
                            thisItem.volumeEvtType = vt;
                            thisItem.Modified = false;
                            break;
                        }
                    }
                }
            );
        }

        private ObservableCollection<ContractVolumeEvt> BackupContractVolumeEvts;
        private ObservableCollection<ContractVolumeEvt> contractVolumeEvts;
        public ObservableCollection<ContractVolumeEvt> ContractVolumeEvts
        {
            get
            {
                return contractVolumeEvts;
            }
            set
            {
                if (contractVolumeEvts != null)
                {
                    contractVolumeEvts.CollectionChanged -= ContractVolumeEvtListChanged;
                    contractVolumeEvts.ForEach((types, item) => RemoveWeakEventListener(item, VolumeEvtListener));
                }
                contractVolumeEvts = value;
                BackUpvolumeEvtRates.Clear();
                if (contractVolumeEvts != null)
                {
                    contractVolumeEvts.CollectionChanged += ContractVolumeEvtListChanged;
                    contractVolumeEvts.ForEach((types, item) => AddWeakEventListener(item, VolumeEvtListener));
                    ContractVolumeEvts.ForEach((types, item) => BackupContractVolumeEvts.Add(item.Clone()));
                }


                SetContractVolumeEvtsDetails();
                RaisePropertyChanged("ContractVolumeEvts");
            }
        }

        private ContractVolumeEvt selectedContractVolumeEvt;
        public ContractVolumeEvt SelectedContractVolumeEvt
        {
            get
            {
                return selectedContractVolumeEvt;
            }
            set
            {
                selectedContractVolumeEvt = value;
                if (selectedContractVolumeEvt != null)
                {
                    try
                    {
                        VolumeEvtRates = VolumeEvtRateTarget.FetchVolumeEvtRates(selectedContractVolumeEvt.CONTRACT_VOLUME_EVT_ID);
                    }
                    catch (Exception x)
                    {
                        ThisView.ShowMsg(x.ToString());
                    }
                }
                else
                    VolumeEvtRates = null;
                RaisePropertyChanged("SelectedContractVolumeEvt");
            }
        }

        public bool AreRatesClean
        {
            get
            {
                return !bRatesModified;
            }
        }

        public bool Modified
        {
            get
            {
                bool bMod = bWasAdded | bWasDeleted | bRateWasAdded | bRateWasDeleted;
                if (ContractVolumeEvts != null)
                {
                    foreach (ContractVolumeEvt vts in ContractVolumeEvts)
                    {
                        if (vts.Modified)
                        {
                            bMod = true;
                            break;
                        }
                    }

                    if (VolumeEvtRates != null)
                    {
                        foreach (VolumeEvtRate ver in VolumeEvtRates)
                        {
                            if (ver.Modified)
                            {
                                bRatesModified = true;
                                break;
                            }
                        }
                    }

                }
                bRatesModified = bRatesModified | bRateWasDeleted | bRateWasAdded;
                if (ThisView.IsInView)
                    contractListViewModel.EnablePicker = !bMod & !bRatesModified;
                RaisePropertyChanged("AreRatesClean");

                return bMod | bRatesModified;
            }
        }

        public ICommand CancelCommand
        {
            private get;
            set;
        }

        public ICommand SaveCommand
        {
            private get;
            set;
        }

        #endregion


        #region Dispose
        public void Dispose()
        {
        }
        #endregion

    }
}