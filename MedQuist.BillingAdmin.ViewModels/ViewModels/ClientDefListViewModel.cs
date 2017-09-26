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
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;

namespace MedQuist.BillingAdmin.ViewModels
{

    /// <summary>
    /// </summary>
    [Export]
    public class ClientDefListViewModel : ViewModel<IClientDefListView>, IDisposable, GongSolutions.Wpf.DragDrop.IDropTarget
    {
        #region Fields
        RelayCommand _closeCommand;
        bool IsBusy = false;
        public ClientDefDetailsViewModel clientDefDetailsViewModel;
        public ClientDefWorkTypeViewModel clientDefWorkTypeViewModel;
        private IClientDefListView thisView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ClientDefNote></param>
        [ImportingConstructor]
        public ClientDefListViewModel(IClientDefListView view, IAccessSettings accessSettings)
            : base(view)
        {
            thisView = view;
            searchCommand = new DelegateCommand(Search);
            IExtClientTarget= Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateExtClientRepository();
        }
        #endregion


        //private void FilterByNotAssigned(object param)
        //{
            //searchString = string.Empty;
            //RaisePropertyChanged("SearchString");
        //    ClientLocations = IExtClientTarget.FetchLocations(0, false);
        //    CurrentClientLocation = (ClientLocations.Count > 0) ? ClientLocations[0] : null;
        //}


        #region Properties

        private bool notAssignedFilter;
        public bool NotAssignedFilter
        {
            get
            {
                return notAssignedFilter;
            }
            set
            {
                if (notAssignedFilter != value)
                {
                    notAssignedFilter = value;
                    RaisePropertyChanged("NotAssignedFilter");                    
                    Search();
                }
            }
        }

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

        //private bool modified;
        public bool Modified
        {
            get
            {
                return false;
            }
            set
            {
                RaisePropertyChanged("Modified");
            }
        }


        private ClientLocation currentClientLocation;
        public ClientLocation CurrentClientLocation
        {
            get
            {
                return currentClientLocation;
            }
            set
            {
                if (currentClientLocation != value)
                {
                    if (currentClientLocation != null)
                        RemoveWeakEventListener(currentClientLocation, ClientLocListener);
                    currentClientLocation = value;

                    //Because its already set - Removes redundant Event calling
                    //if (currentClientLocation.IsAdding == false)
                    
                    //if (currentClientLocation == currentClientLocation.IsAdding == false)
                    this.clientDefDetailsViewModel.CurrentClientDef = currentClientLocation;

                    if((currentClientLocation != null && currentClientLocation.IsAdding == false) || currentClientLocation == null )
                        this.clientDefWorkTypeViewModel.CurrentClientDef = currentClientLocation;
                    
                    if (currentClientLocation != null)
                        AddWeakEventListener(currentClientLocation, ClientLocListener);

                    if (currentClientLocation != null)
                        ClientDefId = "Id - ExtSys=" + currentClientLocation.ExtSys + " ExtClientKey=" + currentClientLocation.ExtClientKey;
                    else
                        ClientDefId = "Search:";
                    RaisePropertyChanged("CurrentClientLocation");
                }
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

        private ObservableCollection<ClientLocation> clientLocations;
        public ObservableCollection<ClientLocation> ClientLocations
        {
            get
            {
                if (clientLocations == null)
                    clientLocations = new ObservableCollection<ClientLocation>();
                return clientLocations;
            }
            set
            {
                clientLocations = value;
                RaisePropertyChanged("ClientLocations");
            }
        }

        private string clientDefId = "Search: ";
        public string ClientDefId
        {
            get
            {
                return clientDefId;
            }
            set
            {
                if (clientDefId != value)
                {
                    clientDefId = value;
                    RaisePropertyChanged("ClientDefId");
                }
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
                if (searchString != value)
                {
                    searchString = value;
                    if ((!string.IsNullOrEmpty(searchString) && searchString.Length >= 3))
                        Search();
                    else
                        ClientLocations = null;
                    RaisePropertyChanged("SearchString");
                }
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

        private IExtClientRepository IExtClientTarget
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void ClearOutPendingAdds()
        {
            // TODO - Use a yield
            foreach (ClientLocation c in clientLocations)
            {
                if (c.IsAdding)
                {
                    clientLocations.Remove(c);
                    break;
                }
            }
            EnablePicker = true;
            RaisePropertyChanged("ClientLocations");
        }

        public void SwapClientDefItem(ClientLocation item)
        {
            if (CurrentClientLocation != null)
                RemoveWeakEventListener(CurrentClientLocation, ClientLocListener);
            CurrentClientLocation = item;
            CurrentClientLocation.Modified = false;
            ClearOutPendingAdds();
            if (CurrentClientLocation != null)
                AddWeakEventListener(CurrentClientLocation, ClientLocListener);
        }

        private void ClientLocListener(object sender, PropertyChangedEventArgs e)
        {
            ClientLocation c = sender as ClientLocation;
            if (c.Modified)
                EnablePicker = false;
            else
                EnablePicker = true;
        }
        
#if DIRECT
        public void Search()
        {
            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (string.IsNullOrEmpty(srchString))
                ClientLocations = null;
            else
            {
                ClientLocations = IExtClientTarget.FetchClients(SearchString);
                CurrentClientLocation = (ClientLocations.Count > 0) ? ClientLocations[0] : null;
            }
        }
#else
        private Dispatcher uiDispatcher;
        private Thread workerThread;


        public void Search()
        {
#if ORACLE
            IsLoadingData = true;
            var payload = SearchT(ClientLocations, () => this.IsLoadingData = false);
#endif
            //thisView.FocusReset();
        }

 
        public ObservableCollection<ClientLocation> SearchT(
            ObservableCollection<ClientLocation>   output, 
            Action                          onCompleted
            )
        {
#if !ORACLE
            return null;
#endif
            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (string.IsNullOrEmpty(srchString) && !NotAssignedFilter)
            {
                ClientLocations = null;
                return null;
            }
            var syncContext = SynchronizationContext.Current;
            if (syncContext == null)
                throw new InvalidOperationException("...");

            uiDispatcher = Dispatcher.CurrentDispatcher;
            if (workerThread != null)
            {
                Debug.WriteLine("Killing thread");
                workerThread.Abort();
            }
            workerThread = new Thread(new ThreadStart(   delegate
            {
                try
                {
                    var payload = IExtClientTarget.FetchClients(SearchString, NotAssignedFilter);
                    if (payload != null)
                        syncContext.Post(arg => CreateOutput(payload as ObservableCollection<ClientLocation>,  onCompleted), null);
                }
                catch(Exception x)
                {
                    throw x;
                }
            }   ));
            workerThread.Priority = ThreadPriority.Normal;
            workerThread.Start();
            return null;
        }

        void CreateOutput(
            ObservableCollection<ClientLocation>   output,
            Action                          onCompleted)
        {
            ClientLocations = output;
            if (onCompleted != null)
                onCompleted();
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
                        ClientLocations= new ObservableCollection<ClientLocation> { new ClientLocation{ Description = "Searching...", IsSpoof = true } };
                    RaisePropertyChanged("IsLoadingData");
                }

            }
        }


#endif

        void GongSolutions.Wpf.DragDrop.IDropTarget.DragOver(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
        }

        void GongSolutions.Wpf.DragDrop.IDropTarget.Drop(GongSolutions.Wpf.DragDrop.DropInfo dropInfo)
        {
        }        
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (currentClientLocation != null)
                RemoveWeakEventListener(currentClientLocation, ClientLocListener);
        }
        #endregion
    }
}
