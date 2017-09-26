using System;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using System.ComponentModel;
using System.Windows.Input;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Waf.Applications;
using System.Windows;

namespace MedQuist.BillingAdmin.ViewModels
{
    public enum BillingFrequencyEnum
    {
        Annually,
        BiWeekly,
        Monthly,
        Never,
        Onetime,
        Quarterly,
        SemiMonthly,
        Weekly
    }

    /// <summary>
    /// ViewModel for editing of Notes associated to a contract.
    /// </summary>
    public class InvoiceGrpVM : ViewModelBase
    {
        #region Fields
        RelayCommand _closeCommand;
        bool IsBusy = false;
        #endregion

        #region ctor

        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        public InvoiceGrpVM()
        {
            exitCommand = new DelegateCommand(Close);
            searchCommand = new DelegateCommand(Search);
            showMoreCommand = new DelegateCommand(ShowMore);
            Show();

        }

        #endregion


        #region Properties

        private bool modified;
        public bool Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
                OnPropertyChanged("Modified");
            }
        }

        #endregion
        #region Data Target
        private IInvoiceGroupRepository DataTarget
        {
            get;
            set;
        }
        #endregion

        #region Explosed Properties
        private ObservableCollection<BillingSpecialist> specialists;
        public ObservableCollection<BillingSpecialist> Specialists
        {
            get
            {
                return specialists;
            }
            set
            {
                specialists = value;
                OnPropertyChanged("Specialists");
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
                OnPropertyChanged("SelectedSpecialist");
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
                OnPropertyChanged( "SelectedDeliveryMethod");
            }
        }


        private ObservableCollection<DeliveryMethod> deliveryMethods;
        public ObservableCollection<DeliveryMethod> DeliveryMethods
        {
            get
            {
                return deliveryMethods;
            }
            set
            {
                deliveryMethods = value;
                OnPropertyChanged( "DeliveryMethods");
            }
        }

        private ObservableCollection<Spheris.Billing.Core.Domain.InvoiceGroup> iGroups;
        public ObservableCollection<Spheris.Billing.Core.Domain.InvoiceGroup> IGroups
        {
            get
            {
                return iGroups;
            }
            set
            {
                iGroups = value;
                OnPropertyChanged( "IGroups");
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
                OnPropertyChanged( "IsExpanded");
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

                OnPropertyChanged("ContractString");
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
                if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 3)
                    Search();
                else
                    IGroups = null;
                OnPropertyChanged("SearchString");
            }
        }

        public string Title { get { return ApplicationInfo.ProductName; } }
        #endregion

        #region Functional Methods
        public void ShowMore()
        {
            if (IsExpanded == Visibility.Collapsed)
                IsExpanded = Visibility.Visible;
            else
                IsExpanded = Visibility.Collapsed;
        }

        public void Search()
        {
            BillingSpecialist billSp = (SelectedSpecialist == null || SelectedSpecialist.Id == 0) ? null : SelectedSpecialist;
            DeliveryMethod delMethod = (SelectedDeliveryMethod == null || SelectedDeliveryMethod.Descr == "*") ? null : SelectedDeliveryMethod;
            string cntString = (string.IsNullOrEmpty(ContractString)) ? null : ContractString;
            string srchString = (string.IsNullOrEmpty(SearchString) || SearchString.Length < 3) ? null : SearchString;
            if (billSp == null && delMethod == null && string.IsNullOrEmpty(cntString) && string.IsNullOrEmpty(srchString))
                IGroups = null;
            else
                IGroups = DataTarget.FetchGroups(billSp,
                                                delMethod,
                                                (string.IsNullOrEmpty(ContractString)) ? null : ContractString,
                                                (string.IsNullOrEmpty(SearchString)) ? null : SearchString);
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

        #region Show the window
        public void Show()
        {

            DataTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();

            Specialists = DataTarget.GetSpecialists();
            BillingSpecialist allSpecialists = new Spheris.Billing.Core.Domain.BillingSpecialist("*", 0);
            Specialists.Insert(0, allSpecialists);
            SelectedSpecialist = allSpecialists;

            DeliveryMethods = DataTarget.GetDeliveryMethods();
            DeliveryMethod allDeliverys = new DeliveryMethod("*", "*");
            DeliveryMethods.Insert(0, allDeliverys);
            SelectedDeliveryMethod = allDeliverys;

            //ViewCore.Show();

            OnPropertyChanged("IsExpanded");
            OnPropertyChanged("IGroups");
            OnPropertyChanged("DeliveryMethods");
            OnPropertyChanged("Specialists");
        }

        public void Close()
        {
            //ViewCore.Close();
        }
        #endregion

    }
}
