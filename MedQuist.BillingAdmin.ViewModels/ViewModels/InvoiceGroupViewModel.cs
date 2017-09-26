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
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace MedQuist.BillingAdmin.ViewModels
{


    /// <summary>
    /// ViewModel for editing of Notes associated to a contract.
    /// </summary>
    [Export]
    public class InvoiceGroupViewModel : ViewModel<IInvoiceGroupView>, IDisposable, GongSolutions.Wpf.DragDrop.IDropTarget
    {
        #region Fields
        IInvoiceGroupView ThisView;
        public enum RouteTo
        {
            UsingClientDefDetailsView,
            UsingClientDefWorkTypesView,
            UsingOverridesView
        }
        #endregion

        #region ctor

        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        [ImportingConstructor]
        public InvoiceGroupViewModel(IInvoiceGroupView view,
                            InvoiceGrpDetailViewModel iInvoiceGrpDetailVM,
                            IAccessSettings accessSettings)
            : base(view)
        {
            AccessSettings = accessSettings;
            this.DetailVM = iInvoiceGrpDetailVM;

            exitCommand = new DelegateCommand(Close);
            searchCommand = new DelegateCommand(Search);
            showMoreCommand = new DelegateCommand(ShowMore);

            Setup(view);
        }

        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        public InvoiceGroupViewModel(IInvoiceGroupView view)
            : base(view)
        {
            exitCommand = new DelegateCommand(Close);
            searchCommand = new DelegateCommand(Search);
            showMoreCommand = new DelegateCommand(ShowMore);
            Setup(view);
        }

        private void Setup(IInvoiceGroupView view)
        {
            ThisView = view;
            SelectedData = new ObservableCollection<object>();
            DeliveryMethodsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateDeliveryMethodsRepository();
            BillingSpecialistTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateBillingSpecialistRepository();
            DataTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();
            ExtClientTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();

        }
 
        #endregion

        #region Properties
        IAccessSettings AccessSettings { get; set; }

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
                return (CurrentGroupItem == null) ? false : CurrentGroupItem.Modified;
                //return modified;
            }
            set
            {
                //modified = value;
                if (CurrentGroupItem != null)
                    CurrentGroupItem.Modified = value;
                RaisePropertyChanged("Modified");
            }
        }

        #endregion

        #region Data Target

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

        private IInvoiceGroupRepository DataTarget
        {
            get;
            set;
        }

        private IExtClientRepository ExtClientTarget
        {
            get;
            set;
        }
        #endregion

        #region Exposed Properties

        private object contractPopped;
        public object ContractPopped
        {
            get
            {
                return contractPopped;
            }
            set
            {
                contractPopped = value;
                RaisePropertyChanged("ContractPopped");
            }
        }

        private string selectionMode = "Single";
        public string SelectionMode
        {
            get
            {
                return selectionMode;
            }
            set
            {
                selectionMode = value;
                RaisePropertyChanged("SelectionMode");
            }
        }


        private ObservableCollection<BillingSpecialist> specialists;
        public ObservableCollection<BillingSpecialist> Specialists
        {
            get
            {
                if (specialists == null /*&& DB.Connnected*/)
                {
                    specialists = BillingSpecialistTarget.GetSpecialists();
                    BillingSpecialist allSpecialists = new Spheris.Billing.Core.Domain.BillingSpecialist("*", 0);
                    if (specialists != null && allSpecialists != null)
                    {
                        specialists.Insert(0, allSpecialists);
                        //SelectedSpecialist = allSpecialists;
                        selectedSpecialist = allSpecialists;
                    }
                }
                return specialists;
            }
            set
            {
                specialists = value;
                RaisePropertyChanged("Specialists");
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
                selectedSpecialist = value;
                Search();
                RaisePropertyChanged("SelectedSpecialist");
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
                selectedDeliveryMethod = value;
                Search();
                RaisePropertyChanged("SelectedDeliveryMethod");
            }
        }

        public RouteTo RoutingTo { get; set; }

        private ObservableCollection<DeliveryMethod> deliveryMethods;
        public ObservableCollection<DeliveryMethod> DeliveryMethods
        {
            get
            {
                if (deliveryMethods == null /*&& DB.Connnected*/)
                {
                    deliveryMethods = DeliveryMethodsTarget.GetDeliveryMethods();
                    DeliveryMethod allDeliverys = new DeliveryMethod("*", "*");

                    if (deliveryMethods != null)
                    {
                        deliveryMethods.Insert(0, allDeliverys);
                        //SelectedDeliveryMethod = allDeliverys;
                        selectedDeliveryMethod = allDeliverys;
                    }
                }
                return deliveryMethods;
            }
            set
            {
                deliveryMethods = value;
                RaisePropertyChanged("DeliveryMethods");
            }
        }

        public AddOnChargesViewModel addOnChargesViewModel { get; set; }
        public OverridesViewModel overridesViewModel { get; set; }
        public InvoiceHistoryViewModel invoiceHistoryViewModel { get; set; }

        private InvoiceGrpDetailViewModel iinvoiceGrpDetailVM;
        public InvoiceGrpDetailViewModel DetailVM
        {
            get
            {
                return iinvoiceGrpDetailVM;
            }
            set
            {
                iinvoiceGrpDetailVM = value;
                RaisePropertyChanged("DetailVM");
            }
        }

        private int previousGroupItemIndex;
        private int currentGroupItemIndex;
        public int CurrentGroupItemIndex
        {
            get
            {
                return currentGroupItemIndex;
            }
            set
            {
                previousGroupItemIndex = currentGroupItemIndex;
                currentGroupItemIndex = value;
                RaisePropertyChanged("CurrentGroupItemIndex");
            }
        }

        private ObservableCollection<object> selectedData;
        public ObservableCollection<object> SelectedData
        {
            get
            {
                return selectedData;
            }
            set
            {
                selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        private InvoiceGroup valueGroupItem;
        public InvoiceGroup ValueGroupItem
        {
            get
            {
                return valueGroupItem;
            }
            set
            {
                valueGroupItem = value;
                RaisePropertyChanged("ValueGroupItem");
            }
        }

        private string viewTip;
        public string ViewTip
        {
            get
            {
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
                enablePicker = value;
                RaisePropertyChanged("EnablePicker");
            }
        }

        private InvoiceGroup PreviousGroupItem;
        private InvoiceGroup currentGroupItem;
        public  InvoiceGroup CurrentGroupItem
        {
            get
            {
                return currentGroupItem;// DetailVM.CurrentGroupItem;
            }
            set
            {
                
                PreviousGroupItem = currentGroupItem;
                currentGroupItem = value;
                if (DetailVM != null)
                {
                    DetailVM.CurrentGroupItem = value;
                }
                if (invoiceHistoryViewModel != null)
                    invoiceHistoryViewModel.CurrentGroupItem = currentGroupItem;
                // Used by the Routed Event system
                if (currentGroupItem == null)
                {
                    DefId = -1;
                    DefGrpName = string.Empty;
                }
                else
                {
                    DefId = currentGroupItem.InvoiceGrpId;
                    DefGrpName = currentGroupItem.Description;
                }
                if(addOnChargesViewModel != null)
                    addOnChargesViewModel.SelectedInvoiceGroup = value;
                if (overridesViewModel != null)
                    overridesViewModel.SelectedInvoiceGroup = value;
                RaisePropertyChanged("CurrentGroupItem");
            }
        }

        private InvoiceGroup currentGroupItemValue;
        public  InvoiceGroup CurrentGroupItemValue
        {
            get
            {
                return currentGroupItemValue;
            }
            set
            {

                currentGroupItemValue = value;
                if (currentGroupItemValue == null)
                    DefId = -1;
                else
                    DefId = currentGroupItemValue.InvoiceGrpId;
                RaisePropertyChanged("CurrentGroupItemValue");
            }
        }

        public int DefId
        {
            get;
            set;
        }

        public string DefGrpName { get; set; }


        private ObservableCollection< InvoiceGroup> iGroups;
        public ObservableCollection<  InvoiceGroup> IGroups
        {
            get
            {
                return iGroups;
            }
            set
            {
                iGroups.ForEach((groupList, ig) => RemoveWeakEventListener(ig, IGroupListener));
                iGroups = value;
                if (iGroups.ForEach((groupList, ig) => AddWeakEventListener(ig, IGroupListener)) < 0 && DetailVM != null)
                    DetailVM.SelectedClientLocation = null;
                RaisePropertyChanged("IGroups");
            }
        }

        private Visibility isExpanded = Visibility.Collapsed;
        public Visibility IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        private string contractString;
        public string ContractString
        {
            get
            {
                return contractString;
            }
            set
            {
                contractString = value;
                Search();

                RaisePropertyChanged("ContractString");
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
                searchString = value;
                if ((!string.IsNullOrEmpty(searchString) && searchString.Length >= 3) ||
                        (SelectedSpecialist != null) ||
                        (SelectedDeliveryMethod != null) ||
                        (!string.IsNullOrEmpty(ContractString))
                    )
                    Search();
                else
                    IGroups = null;
                RaisePropertyChanged("SearchString");
            }
        }

        public string Title { get { return ApplicationInfo.ProductName; } }
        #endregion

        #region Functional Methods
        public void ResetLastGroupItem()
        {
            CurrentGroupItemIndex = previousGroupItemIndex;
        }

        public void ResetGroupItem()
        {
            DetailVM.CurrentGroupItem = PreviousGroupItem;
            RaisePropertyChanged("CurrentGroupItem");
        }

        public void DeleteCurrentItem()
        {
            if (CurrentGroupItem != null)
            {
                try
                {
                    DataTarget.Remove(CurrentGroupItem);
                    int index = CurrentGroupItemIndex;
                    RemoveWeakEventListener(CurrentGroupItem, IGroupListener);
                    IGroups.Remove(CurrentGroupItem);

                    if (IGroups.Count > 0)
                    {
                        if (index >= IGroups.Count)
                            index = 0;
                        CurrentGroupItem = IGroups[index];
                    }
                    EnablePicker = true;
                }
                catch (Exception x)
                {
                    ThisView.ShowMsg(x.ToString());
                }
            }

        }

        public void BumpIndex()
        {
            CurrentGroupItemIndex += 1;
        }

        public void ShowMore()
        {
            if (IsExpanded == Visibility.Collapsed)
                IsExpanded = Visibility.Visible;
            else
                IsExpanded = Visibility.Collapsed;
        }

        public void ClearOutPendingAdds()
        {
            foreach (InvoiceGroup grp in IGroups)
            {
                if (grp.InvoiceGrpId == 0)
                {
                    IGroups.Remove(grp);
                    break;
                }
            }
            EnablePicker = true;
        }

        public void FilterByCurrentContract()
        {
            if (CurrentGroupItem == null)
                return;
            int id = CurrentGroupItem.InvoiceGrpId;
            IGroups = DataTarget.FetchGroups(null,
                                            null,
                                            CurrentGroupItem.ContractID.ToString(),
                                            null);
            foreach (InvoiceGroup item in IGroups)
                if (item.InvoiceGrpId == id)
                {
                    CurrentGroupItem = item;
                    break;
                }

        }

        public void SearchInvoicesByContract(decimal? invoiceId)
        {
            searchString = invoiceId.ToString();
            selectedDeliveryMethod = null;
            selectedSpecialist = null;
            contractString = null;
            SearchString = invoiceId.ToString();
        }



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
                        IGroups = new ObservableCollection<InvoiceGroup> { new InvoiceGroup { Description = "Searching...", IsSpoof = true } };
                    RaisePropertyChanged("IsLoadingData");
                }

            }
        }

        public void Search()
        {
#if ORACLE
            IsLoadingData = true;
            var payload = SearchT(iGroups, () => this.IsLoadingData = false);
#endif
        }

        private Dispatcher uiDispatcher;
        private Thread workerThread;


        public ObservableCollection<InvoiceGroup> SearchT(
            ObservableCollection<InvoiceGroup> output,
            Action onCompleted
            )
        {
#if !ORACLE
    return null;
#endif
            IInvoiceGroupRepository DataTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();
            var syncContext = SynchronizationContext.Current;
            if (syncContext == null)
                throw new InvalidOperationException("...");

            BillingSpecialist billSp = (SelectedSpecialist == null || SelectedSpecialist.Id == 0) ? null : SelectedSpecialist;
            DeliveryMethod delMethod = (SelectedDeliveryMethod == null || SelectedDeliveryMethod.Descr == "*") ? null : SelectedDeliveryMethod;
            string cntString = (string.IsNullOrEmpty(ContractString)) ? null : ContractString;
            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (billSp == null && delMethod == null && string.IsNullOrEmpty(cntString) && string.IsNullOrEmpty(srchString))
            {
                IGroups = null;
                return null;
            }
            uiDispatcher = Dispatcher.CurrentDispatcher;
            if (workerThread != null)
            {
                Debug.WriteLine("Killing thread");
                workerThread.Abort();
            }
            workerThread = new Thread(new ThreadStart(delegate
            {
                try
                {
                    var payload = DataTarget.FetchGroups(billSp,
                                                delMethod,
                                                (string.IsNullOrEmpty(contractString)) ? null : contractString,
                                                (string.IsNullOrEmpty(searchString)) ? null : searchString);

                    if (payload != null)
                        syncContext.Post(arg => CreateOutput(payload as ObservableCollection<InvoiceGroup>/* output*/, onCompleted), null);
                }
                catch (Exception x)
                {
                   // throw x;// ThisView.ShowMsg(x.ToString());
                }
            }));
            workerThread.Priority = ThreadPriority.Normal;
            workerThread.Start();
            return null;
        }

        void CreateOutput(
            ObservableCollection<InvoiceGroup> output,
            Action onCompleted)
        {
            IGroups = output;
            if (onCompleted != null)
                onCompleted();
        }


        public void AddNewInvoice()
        {
            InvoiceGroup newGroup = new InvoiceGroup();
            newGroup.DefaultBillFilePath = (string)AccessSettings.GetValue("DefaultBillPath");
            if (DB.OracleSession == null)
                return;
            newGroup.ChangedBy = DB.OracleSession.UserName;

            if (IGroups == null)
                IGroups = new ObservableCollection<InvoiceGroup>();
            IGroups.Add(newGroup);
            CurrentGroupItem = newGroup;
            AddWeakEventListener(CurrentGroupItem, IGroupListener);
            // Make it dirty
            CurrentGroupItem.Description = "Please fill out the description";
        }

        public void SwapIGItem(InvoiceGroup item)
        {
            if (CurrentGroupItem != null)
                RemoveWeakEventListener(CurrentGroupItem, IGroupListener);
            CurrentGroupItem = item;
            CurrentGroupItem.Modified = false;
            ClearOutPendingAdds();
            if (CurrentGroupItem != null)
                AddWeakEventListener(CurrentGroupItem, IGroupListener);

        }

        private void IGroupListener(object sender, PropertyChangedEventArgs e)
        {
            InvoiceGroup g = sender as InvoiceGroup;
            if (g.Modified)
                EnablePicker = false;
            else
                EnablePicker = true;
        }

        public void Close()
        {
        }
        #endregion

        #region Commands

        private readonly DelegateCommand showMoreCommand;
        public ICommand ShowMoreCommand
        {
            get
            {
                return showMoreCommand;
            }
        }

        private readonly DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return searchCommand;
            }
        }

        private readonly DelegateCommand exitCommand;
        public ICommand ExitCommand { get { return exitCommand; } }
        #endregion

        #region DRAG&DROP
        void GongSolutions.Wpf.DragDrop.IDropTarget.DragOver(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
            //If dragging from Available -> Reported 
            if ((dropInfo.Data is ClientLocation || dropInfo.Data is List<ClientLocation>) && dropInfo.TargetItem is InvoiceGroup)
            {
                if ((dropInfo.TargetItem as InvoiceGroup).ContractID == CurrentGroupItem.ContractID)
                {
                    dropInfo.DropTargetAdorner = GongSolutions.Wpf.DragDrop.DropTargetAdorners.Highlight;
                    dropInfo.Effects = System.Windows.DragDropEffects.Move;
                }
            }

        }

        void GongSolutions.Wpf.DragDrop.IDropTarget.Drop(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
            if (dropInfo.Data is List<ClientLocation>)
            {
                foreach (ClientLocation loc in dropInfo.Data as List<ClientLocation>)
                {
                    loc.DefaultInvoiceGrpID = (dropInfo.TargetItem as InvoiceGroup).InvoiceGrpId;
                    ExtClientTarget.Update(loc);
                }
            }
            else if (dropInfo.Data is ClientLocation)
            {
                ClientLocation loc = dropInfo.Data as ClientLocation;
                loc.DefaultInvoiceGrpID = (dropInfo.TargetItem as InvoiceGroup).InvoiceGrpId;
                ExtClientTarget.Update(loc);
            }
            DetailVM.SetItemDetails();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            iGroups.ForEach((thislist, ig) => RemoveWeakEventListener(ig, IGroupListener));
        }
        #endregion
    }
}
