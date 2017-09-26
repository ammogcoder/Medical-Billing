using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using System.Windows.Input;
using Spheris.Billing.Data.RepositoryInterfaces;
using Spheris.Billing.Core.Domain;
using System.Collections.ObjectModel;
using Spheris.Billing.Data;
using System.ComponentModel;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for AddOnCharges Section
    /// </summary>
    [Export]
    public class AddOnChargesViewModel : ViewModel<IAddOnChargesView>, IDisposable
    {

        IAddOnChargesView ThisView;
        IAccessSettings Settings;
        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<AddOnChargesNote></param>
        [ImportingConstructor]
        public AddOnChargesViewModel(IAddOnChargesView view )
            : base(view)
        {
            ThisView = view;
        
            QtyRuleTarget =  Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateQtyRuleRepository();

            AddOnChgSchedTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateAddOnChgSchedRepository();

            AddOnChgTypeTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateAddOnChgTypeRepository();

            FreqsTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateFreqRepository();

            ActiveScheduleTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateActiveScheduleRepository();

            SaveCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (Modified && SelectedAddOnChgType != null && SelectedFreq != null && SelectedQtyRule != null),
                ExecuteDelegate = x => Save()
            };
            DeleteCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (SelectedAddOnChgSched != null),
                ExecuteDelegate = x => Remove()
            };
            NewCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (!Modified),
                ExecuteDelegate = x => New()
            };
            CancelCommand = new SimpleCommand
            {
                CanExecuteDelegate = x => (Modified),
                ExecuteDelegate = x => Restore()
            };
        }
        #endregion


        private void Save()
        {
            try
            {
                if( selectedAddOnChgSched.ADD_ON_CHG_SCHED_ID == 0)
                    AddOnChgSchedTarget.Add(SelectedAddOnChgSched);
                else
                    AddOnChgSchedTarget.Update(SelectedAddOnChgSched);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private void Remove()
        {
            try
            {
                RemoveWeakEventListener(selectedAddOnChgSched, AddOnSchedListener);
                AddOnChgSchedTarget.Remove(SelectedAddOnChgSched);
                Restore();
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private void Restore()
        {
            try
            {
                RemoveWeakEventListener(selectedAddOnChgSched, AddOnSchedListener );

                ActiveSchedules = ActiveScheduleTarget.FetchActiveSchedules((decimal)selectedInvoiceGroup.InvoiceGrpId);
                AddOnChgScheds = AddOnChgSchedTarget.GetAddOnChgScheds((decimal)selectedInvoiceGroup.InvoiceGrpId);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }
        private void New()
        {
            try
            {
                AddOnChgSched newAddOn = new AddOnChgSched 
                { 
                    ADDED_BY = DB.OracleSession.UserName
                    ,INVOICE_GRP_ID = SelectedInvoiceGroup.InvoiceGrpId
                    ,STARTS_ON = DateTime.Now
                    ,ENDS_AFTER = DateTime.Today.AddDays(60)
                };
                newAddOn.INVOICE_GRP_ID = SelectedInvoiceGroup.InvoiceGrpId;
                AddOnChgScheds.Insert(0, newAddOn);
                SelectedAddOnChgSched = newAddOn;
                RaisePropertyChanged("AddOnChgScheds");
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        public bool Modified
        {
            get
            {
                return (SelectedAddOnChgSched !=null) ? SelectedAddOnChgSched.Modified : false;
            }
        }
        public ICommand SaveCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand NewCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }


        #region Data Target

        private IActiveScheduleRepository ActiveScheduleTarget { get; set; }
        private IQtyRuleRepository QtyRuleTarget { get; set; }
        private IAddOnChgSchedRepository AddOnChgSchedTarget { get; set; }
        private IAddOnChgTypeRepository AddOnChgTypeTarget { get; set; }
        private IFreqRepository FreqsTarget { get; set; }
        #endregion

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
                if (selectedInvoiceGroup != null)
                {
                    try
                    {  
                        ActiveSchedules = ActiveScheduleTarget.FetchActiveSchedules((decimal)selectedInvoiceGroup.InvoiceGrpId);
                        AddOnChgScheds = AddOnChgSchedTarget.GetAddOnChgScheds((decimal)selectedInvoiceGroup.InvoiceGrpId);
                    }
                    catch (Exception x)
                    {
                        ThisView.ShowMsg(x.ToString());
                    }

                }
            }
        }

        private ObservableCollection<ActiveSchedule> activeSchedules;
        public ObservableCollection<ActiveSchedule> ActiveSchedules
        {
            get
            {
                return activeSchedules;
            }
            set
            {
                activeSchedules = value;
                RaisePropertyChanged("ActiveSchedules");
            }
        }

        private ObservableCollection<AddOnChgSched> addOnChgScheds;
        public ObservableCollection<AddOnChgSched> AddOnChgScheds
        {
            get
            {
                return addOnChgScheds;
            }
            set
            {
                addOnChgScheds = value;
                RaisePropertyChanged("AddOnChgScheds");
            }
        }

        private AddOnChgSched selectedAddOnChgSched;
        public AddOnChgSched SelectedAddOnChgSched
        {
            get
            {
                return selectedAddOnChgSched;
            }
            set
            {
                if(selectedAddOnChgSched != null)
                    RemoveWeakEventListener(selectedAddOnChgSched, AddOnSchedListener);

                selectedAddOnChgSched = value;

                SelectedAddOnChgType = null;
                if (selectedAddOnChgSched != null)
                {
                    SelectedAddOnChgType = (AddOnChgTypes.FirstOrDefault(p => p.ADD_ON_CHG_TYPE_ID == selectedAddOnChgSched.ADD_ON_CHG_TYPE_ID) as AddOnChgType);
                    SelectedQtyRule = (QtyRules.FirstOrDefault(p => p.QTY_RULE == selectedAddOnChgSched.QTY_RULE) as QtyRule);
                    SelectedFreq = (Freqs.FirstOrDefault(p => p.FREQ == selectedAddOnChgSched.FREQ) as Freq);
                    selectedAddOnChgSched.Modified = false;
                    AddWeakEventListener(selectedAddOnChgSched, AddOnSchedListener);
                }
                RaisePropertyChanged("EnableList");
                RaisePropertyChanged("SelectedAddOnChgSched");
            }
        }

        private ObservableCollection<AddOnChgType> addOnChgTypes;
        public ObservableCollection<AddOnChgType> AddOnChgTypes
        {
            get
            {
                if (addOnChgTypes == null)
                {
                    try
                    {
                        addOnChgTypes = AddOnChgTypeTarget.GetAddOnChgTypes();
                    }
                    catch (Exception x)
                    {
                        ThisView.ShowMsg(x.ToString());
                    }
                }
                return addOnChgTypes;
            }
            set
            {
                addOnChgTypes = value;
                RaisePropertyChanged("AddOnChgTypes");
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
                selectedAddOnChgType = value;
                if (SelectedAddOnChgSched != null && selectedAddOnChgType != null)
                    SelectedAddOnChgSched.ADD_ON_CHG_TYPE_ID = selectedAddOnChgType.ADD_ON_CHG_TYPE_ID;
                RaisePropertyChanged("SelectedAddOnChgType");
            }
        }

        private ObservableCollection<QtyRule> qtyRules;
        public ObservableCollection<QtyRule> QtyRules
        {
            get
            {
                if (qtyRules == null)
                {
                    try
                    {
                        qtyRules = QtyRuleTarget.FetchQtyRules();
                    }
                    catch (Exception x)
                    {
                        ThisView.ShowMsg(x.ToString());
                    }
                }
                return qtyRules;
            }
            set
            {
                qtyRules = value;
                RaisePropertyChanged("QtyRules");
            }
        }

        private QtyRule selectedQtyRule;
        public QtyRule SelectedQtyRule
        {
            get
            {
                return selectedQtyRule;
            }
            set
            {
                selectedQtyRule = value;
                if (selectedQtyRule != null && SelectedAddOnChgSched != null)
                    SelectedAddOnChgSched.QTY_RULE = selectedQtyRule.QTY_RULE;
                RaisePropertyChanged("SelectedQtyRule");
            }
        }


        private ObservableCollection<Freq> freqs;
        public ObservableCollection<Freq> Freqs
        {
            get
            {
                if (freqs == null)
                {
                    try
                    {
                        freqs = FreqsTarget.GetFreqs();
                    }
                    catch (Exception x)
                    {
                        ThisView.ShowMsg(x.ToString());
                    }
                }
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
                    if (selectedFreq != null && SelectedAddOnChgSched != null)
                        SelectedAddOnChgSched.FREQ = selectedFreq.FREQ;
                    RaisePropertyChanged("SelectedFreq");
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

        private void AddOnSchedListener(object sender, PropertyChangedEventArgs e)
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



        public void Dispose()
        {
        }
    }
}