
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data.RepositoryInterfaces;
using System.ComponentModel;
using System.Collections.Specialized;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for Overrides Section
    /// </summary>
    [Export]
    public class OverridesViewModel : ViewModel<IOverridesView>, IDisposable
    {
        #region Fields
        IOverridesView ThisView;
        private bool bWasDeleted;
        private bool bWasAdded;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<OverridesNote></param>
        [ImportingConstructor]
        public OverridesViewModel(IOverridesView view)
            : base(view)
        {
            ThisView = view;

            InvoiceGrpOverrideTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGrpOverrideRepository();
            InvoiceGroupTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();

            SaveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (Modified ),
                ExecuteDelegate = x => Save()
            };
            CancelCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (Modified),
                ExecuteDelegate = x => Restore()
            };

        }
        #endregion

        #region Commands
        public ICommand SaveCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }
        #endregion

        #region Data Target
        private IInvoiceGrpOverrideRepository InvoiceGrpOverrideTarget { get; set; }
        private IInvoiceGroupRepository InvoiceGroupTarget { get; set; }
        #endregion

        #region Properties
        public bool Modified
        {
            get
            {
                bool bMod = bWasAdded | bWasDeleted;
                if (InvoiceGrpOverrides != null)
                    foreach (InvoiceGrpOverride igo in InvoiceGrpOverrides)
                        if (igo.Modified)
                            bMod = true;
                return bMod;
            }
        }

        private InvoiceGrpOverride selectedInvoiceGrpOverride;
        public InvoiceGrpOverride SelectedInvoiceGrpOverride
        {
            get
            {
                return selectedInvoiceGrpOverride;
            }
            set
            {
                selectedInvoiceGrpOverride = value;
                RaisePropertyChanged("SelectedInvoiceGrpOverride");
            }
        }

        private ObservableCollection<InvoiceGrpOverride> invoiceGrpOverrides;
        public ObservableCollection<InvoiceGrpOverride> InvoiceGrpOverrides
        {
            get
            {
                if (invoiceGrpOverrides == null)
                {
                    invoiceGrpOverrides = new ObservableCollection<InvoiceGrpOverride>();
                }
                return invoiceGrpOverrides;
            }
            set
            {
                invoiceGrpOverrides = value;
                RaisePropertyChanged("InvoiceGrpOverrides");
            }
        }

        private object invGroup;
        public object InvGroup
        {
            get
            {
                return invGroup;
            }
            set
            {
                invGroup = value;
                RaisePropertyChanged("InvGroup");
            }
        }

        private InvoiceGroup selectedInvoiceGroup;
        public InvoiceGroup SelectedInvoiceGroup
        {
            get
            {
                return selectedInvoiceGroup;
            }
            set
            {
                selectedInvoiceGroup = value;
                SetIGOs();
                RaisePropertyChanged("InvoiceGrpOverrides");
                RaisePropertyChanged("SelectedInvoiceGroup");
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
        #endregion 

        #region Methods
        public void SetOverride(int id, string desc)
        {
            SelectedInvoiceGrpOverride.SEND_TO_INVOICE_GRP_ID = id;
            SelectedInvoiceGrpOverride.Description = desc;
            RaisePropertyChanged("SelectedInvoiceGrpOverride");
        }

        void Save()
        {
            try
            {
                ObservableCollection<InvoiceGrpOverride> wasGrpOverrides = InvoiceGrpOverrideTarget.FetchOverRides(selectedInvoiceGroup.InvoiceGrpId);
                if (bWasDeleted)
                {
                    foreach (InvoiceGrpOverride igo in wasGrpOverrides)
                        if (!InvoiceGrpOverrides.Contains(igo))
                            InvoiceGrpOverrideTarget.Remove(igo);
                }

                foreach (InvoiceGrpOverride igo in InvoiceGrpOverrides)
                    if (igo.DEFAULT_INVOICE_GRP_ID == 0)
                    {
                        igo.DEFAULT_INVOICE_GRP_ID = SelectedInvoiceGroup.InvoiceGrpId;
                        InvoiceGrpOverrideTarget.Add(igo);
                    }
                    else if (igo.Modified)
                    {
                        InvoiceGrpOverrideTarget.Update(igo);
                    }
                SetIGOs();
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        void Restore()
        {
            SetIGOs();
        }

        private void IGOSListChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        void SetIGOs()
        {
            if (selectedInvoiceGroup != null)
            {
                try
                {
                    if (invoiceGrpOverrides != null)
                        invoiceGrpOverrides.CollectionChanged -= IGOSListChanged;
                    invoiceGrpOverrides = InvoiceGrpOverrideTarget.FetchOverRides(selectedInvoiceGroup.InvoiceGrpId);
                    if (invoiceGrpOverrides != null)
                    {
                        invoiceGrpOverrides.ForEach((lst, item) =>
                        {
                            InvoiceGroup sendto = new InvoiceGroup { InvoiceGrpId = (int)item.SEND_TO_INVOICE_GRP_ID };
                            sendto = InvoiceGroupTarget.Get(sendto);
                            item.Description = sendto.Description;
                            item.Modified = false;
                        });
                        invoiceGrpOverrides.CollectionChanged += IGOSListChanged;
                        bWasDeleted = bWasAdded = false;
                        RaisePropertyChanged("InvoiceGrpOverrides");
                    }
                }
                catch (Exception x)
                {
                    ThisView.ShowMsg(x.ToString());
                }
            }
        }
        #endregion

        #region Goodbye
        public void Dispose()
        {
            if (invoiceGrpOverrides != null)
                invoiceGrpOverrides.CollectionChanged -= IGOSListChanged;
        }
        #endregion
    }
}