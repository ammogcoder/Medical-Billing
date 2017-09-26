using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MedQuist.BillingAdmin.ViewModels;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.Collections.Specialized;
using FolderBrowser;

namespace MedQuist.BillingAdmin.ViewModels
{
    [Export]
    public class InvoiceGrpDetailViewModel : ViewModel<IInvoiceGrpDetailView> ,IDisposable,IDropTarget
    {
        IInvoiceGrpDetailView ThisView;
        [ImportingConstructor]
        public InvoiceGrpDetailViewModel(IInvoiceGrpDetailView view) 
            :base(view)
        {
            ThisView = view;
            BillingSpecialistTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateBillingSpecialistRepository();
            FreqsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateFreqRepository();
            PlatformsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreatePlatformRepository();
            BrandsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateBrandRepository();
            ReportTypesTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateReportTypeRepository();
            DeliveryMethodsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateDeliveryMethodsRepository();
            InvoiceGrpReportsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGrpReportRepository();
            RemitToTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateRemitToRepository();
            ExtClientTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();
            DataTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();

            ContractTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateContractRepository();
            InvoiceStyleTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceStyleRepository();
            InvoiceGrpStatusTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGrpStatusRepository();
            
            //ContractTarget.ConnectionString.Segments

            if( true/*DB.Connnected*/ )
            {
                List<ReportType> lst = ReportTypesTarget.GetReportTypesAsList();
                if (lst != null)
                {
                ReportTypes = new ObservableReportTypes(ReportTypesTarget.GetReportTypesAsList());
                    ReportTypes.ForEach((types, report) => AddWeakEventListener(report, ReportListener));
                    safeReportTypes = new ObservableReportTypes(ReportTypes, true);
                }

            }



            OnPropertyChanged(new PropertyChangedEventArgs("ItemsSource"));

            SaveCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (HasErrorVM == false && Modified == true),
                ExecuteDelegate = x => SaveInvoice()
            };

            AddNewCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified == false && PopSwitch == false),
                ExecuteDelegate = x => AddNewInvoice()
            };

            // Restore the changes
            RestoreCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (Modified == true),
                ExecuteDelegate = x => CancelChanges()
            };

            DeleteCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null),
                ExecuteDelegate = x => DeleteInvoice()
            };

            PopCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null),
                ExecuteDelegate = x =>
                {
                    if (notAssignedLocations == null)
                        NotAssignedLocations = this.ExtClientTarget.FetchLocations(0, false);
                    view.Pop();
                }
            };

            AddLocationsCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (HasSelections == true && CurrentGroupItem != null ),
                ExecuteDelegate = x => AddLocations()
            };

            UnassignLocationsCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null && SelectedClientLocations.Count > 0),
                ExecuteDelegate = x => UnassignLocations()
            };

            FilterByContractCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null && CurrentGroupItem.ContractID != null ),
                ExecuteDelegate = x => FilterByContract()
            };

            JumpToContractCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null && CurrentGroupItem.ContractID != null),
                ExecuteDelegate = x => JumpToContract()
            };
            
            BrowseFolderCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (CurrentGroupItem != null && CurrentGroupItem.ContractID != null),
                ExecuteDelegate = x => BrowseFolder()
            };

            NoSaveCommand = RestoreCommand;

            SelectedNALocations = new ObservableCollection<ClientLocation> ();
            SelectedClientLocations = new ObservableCollection<ClientLocation>();

        }

        void BrowseFolder()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog().Value == true)
            {
                CurrentGroupItem.DefaultBillFilePath = fd.SelectedFolder;
            }
        }

        public bool IsNotAddingNew
        {
            get
            {
                if (CurrentGroupItem == null ||
                    CurrentGroupItem.InvoiceGrpId == 0)
                    return false;
                else
                    return true;
            }
        }


        void pops()
        { 
        }

        private bool HasErrorVM
        {
            get
            {
                string myError = string.Empty;
                if (currentGroupItem != null && this.currentGroupItem.HasError != null)
                    myError = this.currentGroupItem.HasError;
                return false;
            }
        }

        public InvoiceGroupViewModel GroupVM
        { get; set; }

        private void DeleteInvoice()
        {
            try
            {
                GroupVM.DeleteCurrentItem();
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
            RaisePropertyChanged("IsNotAddingNew");
        }

        /// <summary>
        /// Restore the values
        /// </summary>
        private void CancelChanges()
        {
            GroupVM.SwapIGItem(copyGroupItem);
            RaisePropertyChanged("IsNotAddingNew");
        }

       public void NoSave()
       {
           currentGroupItem.Modified = false;
           RaisePropertyChanged("IsNotAddingNew");
       }

        /// <summary>
        /// If true - allow the change - else don't allow the change
        /// </summary>
        /// <returns></returns>
       public bool AskForSave()
       {

           switch ((View as IInvoiceGrpDetailView).AskForSave())
           {
               //Save
               case 0:
                   return true;
               //no save
               case 1:
                   return true;
               //cancel
               case 2:
                   //GroupVM.ResetGroupItem(currentGroupItem);
                   return false;
               default:
                   break;
           }
           return false;
       }

        private void AddNewInvoice()
        {
            GroupVM.AddNewInvoice();
        }

        private void SaveInvoice()
        {
            CurrentGroupItem.ChangedBy = DB.OracleSession.UserName;
            CurrentGroupItem.ChangedOn = DateTime.Now;

            foreach (ReportType reportType in ReportTypes)
            {
                bool bExists = false;
                foreach (InvoiceGrpReport igr in IgReports)
                {
                    if (igr.REPORT_TYPE_ID == reportType.REPORT_TYPE_ID)
                    {
                        bExists = true;
                        // Did they DESELECT the invoiceGroup??
                        if (reportType.IsSelected == false)
                        {
                            this.InvoiceGrpReportsTarget.Remove(igr);
                            break;
                        }
                    }
                }
                // Add this invoiceGroup - it was not there previously
                if (bExists == false && reportType.IsSelected == true) 
                {
                    InvoiceGrpReport igr = new InvoiceGrpReport();
                    igr.INVOICE_GRP_ID = CurrentGroupItem.InvoiceGrpId;
                    igr.REPORT_TYPE_ID = reportType.REPORT_TYPE_ID;

                    this.InvoiceGrpReportsTarget.Add(igr);
                }
            }
            
                   
            if (CurrentGroupItem.InvoiceGrpId == 0)
                DataTarget.Add(CurrentGroupItem);
            else
                DataTarget.Update(CurrentGroupItem);
            CurrentGroupItem.Modified = false;
            GroupVM.EnablePicker = true;
            RaisePropertyChanged("IsNotAddingNew");
        }

        private ICommand editLocationsCommand;
        public ICommand EditLocationsCommand
        {
            get
            {
                return editLocationsCommand;
            }
            set
            {
                if (editLocationsCommand != value)
                {
                    editLocationsCommand = value;
                    RaisePropertyChanged("EditLocationsCommand");
                }
            }
        }

        private ICommand popCommand;
        public ICommand PopCommand
        {
            private get
            {
                return popCommand;
            }
            set
            {
                if (popCommand != value)
                {
                    popCommand = value;
                }
            }
        }
        public ICommand DeleteCommand
        {
            private get;
            set;
        }
        public ICommand RestoreCommand
        {
            private get;
            set;
        }
        public ICommand AddNewCommand
        {
            private get;
            set;
        }

        public ICommand SaveCommand
        {
            private get;
            set;
        }

       public ICommand NoSaveCommand
       {
           private get;
           set;
       }

        private IInvoiceGrpStatusRepository InvoiceGrpStatusTarget
        {
            get;
            set;
        }

        private IContractRepository ContractTarget
        {
            get;
            set;
        }

        private IInvoiceStyleRepository InvoiceStyleTarget
        {
            get;
            set;
        }

        private IInvoiceGroupRepository DataTarget
        {
            get;
            set;
        }

        private IInvoiceGrpReportRepository InvoiceGrpReportsTarget
        {
            get;
            set;
        }

        private IExtClientRepository ExtClientTarget
        {
            get;
            set;
        }

        private IRemitToRepository RemitToTarget
        {
            get;
            set;
        }

        private IReportTypeRepository ReportTypesTarget
        {
            get;
            set;
        }

        private IBrandsRepository BrandsTarget
        {
            get;
            set;
        }

        private IPlatformRepository PlatformsTarget
        {
            get;
            set;
        }


        private IFreqRepository FreqsTarget
        {
            get;
            set;
        }

        private IDeliveryMethods DeliveryMethodsTarget
        {
            get;
            set;
        }

        private IBillingSpecialistRepository BillingSpecialistTarget
        {
            get;
            set;
        }


        private string billingSpecialistName;
        public string BillingSpecialistName
        {
            get
            {
                return billingSpecialistName;
            }
            set
            {
                if (billingSpecialistName != value)
                {
                    billingSpecialistName = value;
                    RaisePropertyChanged("BillingSpecialistName");
                }
            }
        }


        public void SetItemDetails()
        {
            if (currentGroupItem == null || currentGroupItem.IsSpoof)
            {
                SelectedBrand = null;
                SelectedFreq = null;
                SelectedDeliveryMethod = null;
                SelectedPlatform = null;
                SelectedSpecialist = null;
                ContractName = null;
                if (ReportTypes != null)
                {
                    ReportTypes.ForEach((thislist, thisreport) => RemoveWeakEventListener(thisreport, ReportListener));
                    ReportTypes = new ObservableReportTypes(safeReportTypes, true);
                    ReportTypes.ForEach((thislist, thisreport) => AddWeakEventListener(thisreport, ReportListener));
                }
                ClientLocations = null;

                RaisePropertyChanged("ReportTypes");
                ReportText = "Select Report Types...";
                return;
            }

            if (/*DB.Connnected &&*/ ReportTypes == null)
            {
                ReportTypes = new ObservableReportTypes(ReportTypesTarget.GetReportTypesAsList());
                safeReportTypes = new ObservableReportTypes(ReportTypes, true);
            }

            if (CurrentGroupItem.ContractID == null)
                ContractName = "No Contract assigned.";
            else
            {
                Contract contract = ContractTarget.GetByContractID((int)CurrentGroupItem.ContractID);
                if (contract == null)
                    ContractName = "No contract found for this Group.";
                else
                    ContractName = contract.DESCR;
            }
            AddWeakEventListener(CurrentGroupItem, EncryptListener);

            IgReports  = InvoiceGrpReportsTarget.GetById(currentGroupItem.InvoiceGrpId);

            foreach (ReportType reportType in ReportTypes)
            {
                foreach (InvoiceGrpReport igr in IgReports)
                {
                    if (igr.REPORT_TYPE_ID == reportType.REPORT_TYPE_ID)
                        reportType.IsSelected = true;
                    else
                        reportType.IsSelected = false;
                }
            }
            RaisePropertyChanged("ReportTypes");
            base.ViewCore.UpdateCB();

            SelectedSpecialist = (Specialists.First(ss => ss.Id == currentGroupItem.BillingSpecialistID) as BillingSpecialist);
            SelectedFreq = (Freqs.First(f => f.FREQ == currentGroupItem.BillingFrequency) as Freq);
            SelectedPlatform = (Platforms.First(p => p.PLATFORM == currentGroupItem.PrimaryPlatform) as Platform);
            SelectedDeliveryMethod = (DeliveryMethods.First(dm => dm.TheDeliveryMethod == currentGroupItem.DeliveryMethod) as DeliveryMethod);
            SelectedBrand = (Brands.FirstOrDefault(b => b.BRAND == currentGroupItem.Brand) as Brand);
            SelectedRemitTo = (RemitTos.First(r => r.REMIT_TO_ID == currentGroupItem.RemitToID) as RemitTo);
            SelectedInvoiceStyle = (InvoiceStyles.First(i => i.INVOICE_STYLE == currentGroupItem.InvoiceStyle) as InvoiceStyle);
            SelectedInvoiceGrpStatus = (InvoiceGrpStatuss.First(i => i.INVOICE_GRP_STATUS == currentGroupItem.InvoiceGroupStatus) as InvoiceGrpStatus);

            if (ClientLocations != null)
                ClientLocations.CollectionChanged -= ClientLocCollectionChanged;
            ClientLocations = ExtClientTarget.FetchLocations(CurrentGroupItem.InvoiceGrpId,true);
            ClientLocations.CollectionChanged += ClientLocCollectionChanged;
            
            currentGroupItem.Modified = false;
            GroupVM.EnablePicker = true;
        }

        private void ClientLocCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                

            }
        }
        private void EncryptListener(object sender, PropertyChangedEventArgs e)
        {
            if (CurrentGroupItem != null)
            {
                if (CurrentGroupItem.EncryptionOptout == true && OrgEncryption == false)
                {
                    // TODO - Use the new WPFMessageBox View
                    // Show(string messageBoxText, string caption, MessageBoxButton button);
                    if (System.Windows.MessageBox.Show("In order to check this option you must first have a letter on file from the customer requesting this option.\r\n"
                                      + "Otherwise, we are legally bound to encrypt all emails that contain PHI.\r\n"
                                      + "Do you have this letter on file?"
                                      , "Encryption Opt-Out"
                                      , System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
                    {
                        CurrentGroupItem.EncryptionOptout = false;
                    }
                
                }
            }
        }

        private void ReportListener(object sender, PropertyChangedEventArgs e)
        {
            if (CurrentGroupItem != null)
            {
                CurrentGroupItem.Modified = true;
                GroupVM.EnablePicker = false;
            }

        }

        private ObservableCollection<InvoiceGrpReport> IgReports
        {
            get;
            set;
        }

        private string contractName;
        public string ContractName
        {
            get
            {
                return contractName;
            }
            set
            {
                if (contractName != value)
                {
                    contractName = value;
                    RaisePropertyChanged("ContractName");
                }
            }
        }

        public string ReportText
        {
            get
            {
                return "Select Report Types...";
            }
            set
            {
                RaisePropertyChanged("ReportText");
            }
        }


        public bool Modified
        {
            get
            {
                if (currentGroupItem != null)
                        return currentGroupItem.Modified;
                return false;
            }
        }

        private Spheris.Billing.Core.Domain.InvoiceGroup copyGroupItem;
        private Spheris.Billing.Core.Domain.InvoiceGroup currentGroupItem;
        public Spheris.Billing.Core.Domain.InvoiceGroup CurrentGroupItem
        {
            get
            {
                return currentGroupItem;
            }
            set
            {
                if (currentGroupItem != value && value != null)
                {
                    if(currentGroupItem != null)
                        RemoveWeakEventListener(currentGroupItem, EncryptListener);

                    OrgEncryption = value.EncryptionOptout;
                }
                currentGroupItem = value;
                if(currentGroupItem != null)
                    copyGroupItem = new InvoiceGroup(currentGroupItem);
                SetItemDetails();
                RaisePropertyChanged("IsNotAddingNew");
                RaisePropertyChanged("ClientLocations");
                RaisePropertyChanged("CurrentGroupItem");
            }
        }

        private Platform selectedPlatform;
        public Platform SelectedPlatform
        {
            get
            {
                return selectedPlatform;
            }
            set 
            {
                if (selectedPlatform != value)
                {
                    selectedPlatform = value;
                    if (CurrentGroupItem != null && selectedPlatform != null)
                        CurrentGroupItem.PrimaryPlatform = selectedPlatform.PLATFORM;

                    RaisePropertyChanged("SelectedPlatform");
                }
            }
        }

        private ObservableCollection<Platform> platforms;
        public ObservableCollection<Platform> Platforms
        {
            get
            {
                if (platforms == null /*&& DB.Connnected*/)
                    platforms = PlatformsTarget.GetPlatforms();
                return platforms;
            }
            set
            {
                if (platforms != value)
                {
                    platforms = value;
                    RaisePropertyChanged("Platforms");
                }
            }
        }

        public Freq selectedFreq;
        public Freq SelectedFreq
        {
            get
            {
                return selectedFreq;
            }
            set
            {
                if (selectedFreq != value)
                {
                    selectedFreq = value;
                    if (CurrentGroupItem != null && selectedFreq != null)
                        CurrentGroupItem.BillingFrequency = selectedFreq.FREQ;
                    RaisePropertyChanged("SelectedFreq");
                }
            }
        }

        private ObservableCollection<Freq> freqs;
        public ObservableCollection<Freq> Freqs
        {
            get
            {
                if (freqs == null /*&& DB.Connnected*/)
                    freqs = FreqsTarget.GetFreqs();
                return freqs;
            }
            set
            {
                if (freqs != value)
                {
                    freqs = value;
                    RaisePropertyChanged("Freqs");
                }
            }
        }

        private BillingSpecialist selectedSpecialist;
        public BillingSpecialist SelectedSpecialist
        {
            get
            {
                return selectedSpecialist;
            }
            set
            {
                if (selectedSpecialist != value)
                {
                    selectedSpecialist = value;
                    if (CurrentGroupItem != null && selectedSpecialist != null)
                        CurrentGroupItem.BillingSpecialistID = selectedSpecialist.Id;

                    RaisePropertyChanged("SelectedSpecialist");
                }
            }
        }



        private ObservableCollection<BillingSpecialist> specialists;
        public ObservableCollection<BillingSpecialist> Specialists
        {
            get
            {
                if (specialists == null /*&& DB.Connnected*/)
                    specialists = BillingSpecialistTarget.GetSpecialists();
                return specialists;
            }
            set
            {
                if (specialists != value)
                {
                    specialists = value;
                    RaisePropertyChanged("Specialists");
                }
            }
        }

        private DeliveryMethod selectedDeliveryMethod;
        public DeliveryMethod SelectedDeliveryMethod
        {
            get
            {
                return selectedDeliveryMethod;
            }
            set
            {
                if (selectedDeliveryMethod != value)
                {
                    selectedDeliveryMethod = value;
                    if (CurrentGroupItem != null && selectedDeliveryMethod != null)
                        CurrentGroupItem.DeliveryMethod = selectedDeliveryMethod.TheDeliveryMethod;
                    RaisePropertyChanged("SelectedDeliveryMethod");
                }
            }
        }



        private ObservableCollection<DeliveryMethod> deliveryMethods;
        public ObservableCollection<DeliveryMethod> DeliveryMethods
        {
            get
            {
                if (deliveryMethods == null /*&& DB.Connnected*/)
                    deliveryMethods = DeliveryMethodsTarget.GetDeliveryMethods( );
                return deliveryMethods;
            }
            set
            {
                if (deliveryMethods != value)
                {
                    deliveryMethods = value;
                    RaisePropertyChanged("DeliveryMethods");
                }
            }
        }

        private Brand selectedBrand;
        public Brand SelectedBrand
        {
            get
            {
                return selectedBrand;
            }
            set
            {
                if (selectedBrand != value)
                {
                    selectedBrand = value;
                    if (CurrentGroupItem != null && selectedBrand != null)
                        CurrentGroupItem.Brand = selectedBrand.BRAND;
                    RaisePropertyChanged("SelectedBrand");
                }
            }
        }



        private ObservableCollection<Brand> brands;
        public ObservableCollection<Brand> Brands
        {
            get
            {
                if (brands == null /*&& DB.Connnected*/)
                    brands = BrandsTarget.GetBrands( );
                return brands;
            }
            set
            {
                if (brands != value)
                {
                    brands = value;
                    RaisePropertyChanged("Brands");
                }
            }
        }



        private ReportType selectedReportType;
        public ReportType SelectedReportType
        {
            get
            {
                return selectedReportType;
            }
            set
            {
                if (selectedReportType != value)
                {
                    selectedReportType = value;

                    //TODO - Where is this match - and where is the reassign in the pull down
                    //if(CurrentGroupItem != null && selectedReportType != null)
                    //     CurrentGroupItem.
                    //    selectedReportType.REPORT_TYPE_ID

                    RaisePropertyChanged("SelectedReportType");
                }
            }
        }


        public class ObservableReportTypes : ObservableCollection<ReportType>
        {

            //private ObservableCollection<ReportType> reportTypes;
            public ObservableReportTypes(List<ReportType> lst)
                :base(lst)
            {
            }

            public ObservableReportTypes(ObservableReportTypes other,bool empty)
            { 
                foreach(ReportType rt in other)
                {
                    Add(new ReportType(rt,empty));
                }
            }

            public override string ToString()
            {
                StringBuilder outString = new StringBuilder();
                foreach (ReportType s in this.Items)
                {
                    if (s.IsSelected == true)
                    {
                        outString.Append(s.DESCR);
                        outString.Append(',');
                    }
                }
                return outString.ToString().TrimEnd(new char[] { ',' });
            }

        }

        private ObservableReportTypes safeReportTypes;
        private ObservableReportTypes reportTypes;
        public ObservableReportTypes ReportTypes
        {
            get
            {
                return /*(CurrentGroupItem == null) ? null :*/ reportTypes;
            }
            set
            {
                if (reportTypes != value)
                {
                    reportTypes = value;
                    RaisePropertyChanged("ReportTypes");
                }
            }
        }


        private RemitTo selectedRemitTo;
        public RemitTo SelectedRemitTo
        {
            get
            {
                return selectedRemitTo;
            }
            set
            {
                if (selectedRemitTo != value)
                {
                    selectedRemitTo = value;

                    if (CurrentGroupItem != null && selectedRemitTo != null)
                        CurrentGroupItem.RemitToID = selectedRemitTo.REMIT_TO_ID;

                    RaisePropertyChanged("SelectedRemitTo");
                }
            }
        }



        private ObservableCollection<RemitTo> remitTos;
        public ObservableCollection<RemitTo> RemitTos
        {
            get
            {
                if (remitTos == null /*&& DB.Connnected*/)
                    remitTos = RemitToTarget.GetRemitTos();
                return remitTos;
            }
            set
            {
                if (remitTos != value)
                {
                    remitTos = value;
                    RaisePropertyChanged("RemitTos");
                }
            }
        }

        private InvoiceStyle selectedInvoiceStyle;
        public InvoiceStyle SelectedInvoiceStyle
        {
            get
            {
                return selectedInvoiceStyle;
            }
            set
            {
                if (selectedInvoiceStyle != value)
                {
                    selectedInvoiceStyle = value;

                    if (CurrentGroupItem != null && selectedInvoiceStyle != null)
                        CurrentGroupItem.InvoiceStyle = selectedInvoiceStyle.INVOICE_STYLE;

                    RaisePropertyChanged("SelectedInvoiceStyle");
                }
            }
        }



        private ObservableCollection<InvoiceStyle> invoiceStyles;
        public ObservableCollection<InvoiceStyle> InvoiceStyles
        {
            get
            {
                if (invoiceStyles == null /*&& DB.Connnected*/)
                    invoiceStyles = InvoiceStyleTarget.GetInvoiceStyles();
                return invoiceStyles;
            }
            set
            {
                if (invoiceStyles != null)
                {
                    invoiceStyles = value;
                    RaisePropertyChanged("InvoiceStyles");
                }
            }
        }

        /////////////
        private InvoiceGrpStatus selectedInvoiceGrpStatus;
        public InvoiceGrpStatus SelectedInvoiceGrpStatus
        {
            get
            {
                return selectedInvoiceGrpStatus;
            }
            set
            {
                if (selectedInvoiceGrpStatus != value)
                {
                    selectedInvoiceGrpStatus = value;

                    if (CurrentGroupItem != null && selectedInvoiceGrpStatus != null)
                        CurrentGroupItem.InvoiceGroupStatus = selectedInvoiceGrpStatus.INVOICE_GRP_STATUS;

                    RaisePropertyChanged("SelectedInvoiceGrpStatus");
                }
            }
        }

        private string defaultText;
        private string _DefaultText = "Select a type...";

        public string DefaultText
        {
            get 
            { 
                return _DefaultText; 
            }
            set
            {
                if (_DefaultText != value)
                {
                    _DefaultText = value;
                    RaisePropertyChanged("DefaultText");
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

        private ShellViewModel mainViewModel;
        public ShellViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
            set
            {
                mainViewModel = value;
                RaisePropertyChanged("MainViewModel");
            }
        }

        private void JumpToContract()
        {
            if(GroupVM.CurrentGroupItem != null && GroupVM.CurrentGroupItem.ContractID != null)
            foreach (SectionPair pair in MainViewModel.Sections)
                if ( pair.LeftView as IContractListView != null)
                {
                    MainViewModel.SelectedSection = pair;
                    (pair.LeftView as IContractListView).SetContractId( (int)GroupVM.CurrentGroupItem.ContractID);
                }
            //GroupVM.MainViewModel
            // CurrentGroupItem.ContractID
            //ThisView.ShellWindow.
        }

        private void FilterByContract()
        {
            GroupVM.FilterByCurrentContract();
        }


        private void UnassignLocations()
        {
            foreach (ClientLocation location in SelectedClientLocations)
            {
                //ClientLocation copy = location.Clone();
                ExtClientTarget.UnassignLocation(location);
                location.DefaultInvoiceGrpID = null;
                NotAssignedLocations.Add(location);
                //NotAssignedLocations.
            }

            // Slow sorter :(

            //List<ClientLocation> myList = new List<ClientLocation>(NotAssignedLocations);
            List<ClientLocation> myList = NotAssignedLocations.ToList();
            myList.Sort((a, b) => String.Compare(a.Description, b.Description));
            NotAssignedLocations = new ObservableCollection<ClientLocation>(myList);

            SetItemDetails();    

        }

        private void AddLocations()
        {
            if (CurrentGroupItem == null)
                return;
            // TODO - Should this not signify a FORCED CHANGE on the GroupItem itself?
            // - that is ...it has been modified by this user.
            CurrentGroupItem.ChangedBy = DB.OracleSession.UserName;
            foreach (ClientLocation clientLoc in SelectedNALocations)
            {
                //ExtClientTarget.UpdateByGrpId(clientLoc, CurrentGroupItem.InvoiceGrpId);
                clientLoc.DefaultInvoiceGrpID = CurrentGroupItem.InvoiceGrpId;
                ExtClientTarget.Update(clientLoc);
      //          this.ClientLocations.Add(clientLoc);
      //          NotAssignedLocations.Remove(clientLoc);
            }

            //bool bWasRemoved = false;
            while (SelectedNALocations.Count > 0)
            {
                //bWasRemoved = true;
                NotAssignedLocations.Remove(SelectedNALocations[0]);
            }
            //if(bWasRemoved)
            //    NotAssignedLocations = ExtClientTarget.FetchLocations(0, false);
            SetItemDetails();    

            PopSwitch = false;
        }


        public ICommand AddLocationsCommand
        {
            private get;
            set;
        }

        public ICommand UnassignLocationsCommand
        {
            private get;
            set;
        }

        public ICommand FilterByContractCommand
        {
            private get;
            set;
        }

        public ICommand JumpToContractCommand
        {
            private get;
            set;
        }

        public ICommand BrowseFolderCommand
        {
            private get;
            set;
        }

        private bool HasSelections
        {
            get
            {
                return (SelectedNALocations.Count == 0) ? false : true;
            }
        }

        /*
        private object notUsedLocationsObject;
        public object NotUsedLocationsObject
        {
            get
            {
                return notUsedLocationsObject;
            }
            set
            {
                notUsedLocationsObject = value;
                RaisePropertyChanged("NotUsedLocationsObject");
            }
        }
        */

        private ObservableCollection<InvoiceGrpStatus> invoiceGrpStatuss;
        public ObservableCollection<InvoiceGrpStatus> InvoiceGrpStatuss
        {
            get
            {
                if (invoiceGrpStatuss == null /*&& DB.Connnected*/)
                    invoiceGrpStatuss = InvoiceGrpStatusTarget.GetInvoiceGrpStatuss();
                return invoiceGrpStatuss;
            }
            set
            {
                invoiceGrpStatuss = value;
                RaisePropertyChanged("InvoiceGrpStatuss");
            }
        }

        private bool OrgEncryption { get; set; }

        private ClientLocation selectedClientLocation;
        public ClientLocation SelectedClientLocation
        {
            get
            {
                return selectedClientLocation;
            }
            set
            {
                if (selectedClientLocation != value)
                {
                    selectedClientLocation = value;
                    RaisePropertyChanged("SelectedClientLocation");
                }
            }
        }

        private ObservableCollection<ClientLocation> selectedNALocations;
        public ObservableCollection<ClientLocation> SelectedNALocations
        {
            get
            {
                return selectedNALocations;
            }
            set
            {
                if (selectedNALocations != value)
                {
                    selectedNALocations = value;
                    RaisePropertyChanged("SelectedNALocations");
                }
            }
        }

        private ObservableCollection<ClientLocation> notAssignedLocations ;
        public ObservableCollection<ClientLocation> NotAssignedLocations 
        {
            get
            {
#if MOVE_TO_ADD
                if (notAssignedLocations == null)
                    notAssignedLocations = this.ExtClientTarget.FetchLocations(0, false);
#endif
                return notAssignedLocations;
            }
            set
            {
                if (notAssignedLocations != value)
                {
                    notAssignedLocations = value;
                    RaisePropertyChanged("NotAssignedLocations");
                }
            }
        }
        
        private ObservableCollection<ClientLocation> selectedClientLocations;
        public ObservableCollection<ClientLocation> SelectedClientLocations
        {
            get
            {
                return selectedClientLocations;
            }
            set
            {
                if (selectedClientLocations != value)
                {
                    selectedClientLocations = value;
                    RaisePropertyChanged("SelectedClientLocations");
                }
            }
        }



        private ObservableCollection<ClientLocation> clientLocations;
        public ObservableCollection<ClientLocation> ClientLocations
        {
            get
            {
                //if (/*clientLocations == null && */CurrentGroupItem != null)
                //    clientLocations = ExtClientTarget.FetchLocations(/* 0,false); */CurrentGroupItem.InvoiceGrpId,true);
                return clientLocations;
            }
            set
            {
                if (clientLocations != value)
                {
                    clientLocations = value;
                    RaisePropertyChanged("ClientLocations");
                }
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

         //base.PropertyChanged 
            //if (base.PropertyChanged != null) { PropertyChanged(this, e); }
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            //If dragging from Available -> Reported 
            if ((dropInfo.Data is string || dropInfo.Data is List<string>) && dropInfo.TargetItem is ReportPair)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }

            // If dragging from Reported to Available
            if ((dropInfo.Data is ReportPair || dropInfo.Data is List<ReportPair>) && !(dropInfo.TargetItem is ReportPair))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            if (dropInfo.Data is List<string>)
            {
                //foreach (string field in dropInfo.Data as List<string>)
                //    MoveItemLeft(field);
            }
            //if (dropInfo.Data is string)
            //    MoveItemLeft(dropInfo.Data as string);
            //if (dropInfo.Data is ReportPair)
            //    MoveItemRight(dropInfo.Data as ReportPair);

            //if (dropInfo.Data is List<ReportPair>)
            //    foreach (ReportPair pair in dropInfo.Data as List<ReportPair>)
            //        MoveItemRight(pair);
        }

        public void Dispose()
        {
            ReportTypes.ForEach((thislist, thisreport) => RemoveWeakEventListener(thisreport, ReportListener));
            if (CurrentGroupItem != null)
                RemoveWeakEventListener(CurrentGroupItem,EncryptListener);

        }
    }
}
