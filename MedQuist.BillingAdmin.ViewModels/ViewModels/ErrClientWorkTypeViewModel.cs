using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for ErrorMan Section
    /// </summary>
    [Export]
    public class ErrClientWorkTypeViewModel : ViewModel<IErrClientWorkTypeView>, IDisposable
    {
#region Fields
        IErrClientWorkTypeView ThisView;
#endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ErrorManNote></param>
        [ImportingConstructor]
        public ErrClientWorkTypeViewModel(IErrClientWorkTypeView view)
            : base(view)
        {

            ThisView = view;

            RefreshCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => Refresh()
            };

            ErrClientErrorTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateErrClientErrorRepository();
            ClientErrorTypes = ErrClientErrorTypeTarget.GetClientErrorTypes();

            //ClientErrorTypes.ForEach((thislist, eachContract) => RemoveWeakEventListener(eachContract, ContractRateListener));

            SelectedClientErrorType = new ClientErrorType() { DESC = "All", EXT_CLIENT_ERR_TYPE = "*" };
            if(ClientErrorTypes != null)
            ClientErrorTypes.Insert(0, SelectedClientErrorType);
            ErrClientWorkTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateErrClientWorkTypeRepository();

        }
        #endregion


        public IErrClientErrorTypeRepository ErrClientErrorTypeTarget { get; set; }
        public IErrClientWorkTypeRepository ErrClientWorkTypeTarget { get; set; }

        public ICommand RefreshCommand { private get; set; }


        public void Refresh()
        {
            try
            {
                ErrClientWorkTypes = ErrClientWorkTypeTarget.GetClientWTErrors(SelectedClientErrorType.EXT_CLIENT_ERR_TYPE);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }


        private ObservableCollection<ErrClientWorkType> errClientWorkTypes;
        public ObservableCollection<ErrClientWorkType> ErrClientWorkTypes
        {
            get
            {
                return errClientWorkTypes;
            }
            set
            {
                errClientWorkTypes = value;
                RaisePropertyChanged("ErrClientWorkTypes");
            }
        }

        private ErrClientWorkType selectederrClientWorkType;
        public ErrClientWorkType SelectedErrClientWorkType
        {
            get
            {
                return selectederrClientWorkType;
            }
            set
            {
                selectederrClientWorkType = value;
                RaisePropertyChanged("SelectederrClientWorkType");
            }
        }

        private ObservableCollection<ClientErrorType> clientErrorTypes;
        public ObservableCollection<ClientErrorType> ClientErrorTypes
        {
            get
            {
                return clientErrorTypes;
            }
            set
            {
                clientErrorTypes = value;
                RaisePropertyChanged("ClientErrorTypes");
            }
        }

        private ClientErrorType selectedClientErrorType;
        public ClientErrorType SelectedClientErrorType
        {
            get
            {
                return selectedClientErrorType;
            }
            set
            {
                selectedClientErrorType = value;
                RaisePropertyChanged("SelectedClientErrorType");
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