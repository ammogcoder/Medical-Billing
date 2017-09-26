using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Spheris.Billing.Core.Domain;
using Spheris.Billing.Data.RepositoryInterfaces;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for ErrorMan Section
    /// </summary>
    [Export]
    public class ErrNoValidContractViewModel : ViewModel<IErrNoValidContractView>, IDisposable
    {
        #region Fields
        IErrNoValidContractView ThisView;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ErrorManNote></param>
        [ImportingConstructor]
        public ErrNoValidContractViewModel(IErrNoValidContractView view)
            : base(view)
        {
            ThisView = view;

            RefreshCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => Refresh()
            };

            ErrNoValidContractTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateErrNoValidContractTypeRepository();
            AsOfDate = DateTime.Now.AddDays(-2);
        }
        #endregion

        private IErrNoValidContractRepository ErrNoValidContractTarget{get;set;} 

        public void Refresh()
        {
            try
            {
                ErrNoValidContracts = ErrNoValidContractTarget.GetContractErrors(AsOfDate, ShowOnlyActive);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        public ICommand RefreshCommand { private get; set; }

        private ObservableCollection<ErrNoValidContract> errNoValidContracts;
        public ObservableCollection<ErrNoValidContract> ErrNoValidContracts
        {
            get
            {
                return errNoValidContracts;
            }
            set
            {
                errNoValidContracts = value;
                RaisePropertyChanged("ErrNoValidContracts");
            }
        }

        private DateTime asOfDate;
        public DateTime AsOfDate
        {
            get
            {
                return asOfDate;
            }
            set
            {
                asOfDate = value;
                RaisePropertyChanged("AsOfDate");
            }
        }

        private bool showOnlyActive;
        public bool ShowOnlyActive
        {
            get
            {
                return showOnlyActive;
            }
            set
            {
                showOnlyActive = value;
                RaisePropertyChanged("ShowOnlyActive");
            }
        }

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




        public void Dispose()
        {
        }
    }
}