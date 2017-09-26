using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for Invoice Section
    /// </summary>
    [Export]
    public class InvoiceViewModel : ViewModel<IInvoiceView>, IDisposable
    {

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<ContractNote></param>
        [ImportingConstructor]
        public InvoiceViewModel(IInvoiceView view)
            : base(view)
        {
        }
        #endregion


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

        private int groupWidth = 200;
        public int GroupWidth
        {
            get
            {
                return groupWidth;
            }
            set
            {
                if (groupWidth != value)
                {
                    groupWidth = value;
                    RaisePropertyChanged("GroupWidth");
                }
            }
        }

        private object overridesView;
        public object OverridesViewTab
        {
            get
            {
                return overridesView;
            }
            set
            {
                overridesView = value;
                RaisePropertyChanged("OverridesViewTab");
            }
        }

        private object addOnChargesView;
        public object AddOnChargesViewTab
        {
            get
            {
                return addOnChargesView;
            }
            set
            {
                addOnChargesView = value;
                RaisePropertyChanged("AddOnChargesViewTab");
            }
        }

        private object invoiceGrpDetailView;
        public object InvoiceGroupViewDetailTab
        {
            get
            {
                return invoiceGrpDetailView;
            }
            set
            {
                invoiceGrpDetailView = value;
                RaisePropertyChanged("InvoiceGroupViewDetailTab");
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

        private object invoiceHistoryView;
        public object InvoiceHistoryViewTab
        {
            get { return invoiceHistoryView; }
            set
            {
                if (invoiceHistoryView != value)
                {
                    invoiceHistoryView = value;
                    RaisePropertyChanged("InvoiceHistoryViewTab");
                }
            }
        }


        public void Dispose()
        {
        }
    }
}