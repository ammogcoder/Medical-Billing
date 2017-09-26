using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Waf.Applications;
using MedQuist.ViewModels.Views;
using System.Collections.ObjectModel;
using Spheris.Billing;
using Spheris.Billing.Data;
using Microsoft.Reporting.WinForms;
using System.Windows.Input;

namespace MedQuist.BillingAdmin.ViewModels
{
    public enum AuditLogFilterType
    {
        None,
        Contract,
        ChangedBy
    }
    /// <summary>
    /// ViewModel for AuditLog Section
    /// </summary>
    [Export]
    public class AuditLogViewModel : ViewModel<IAuditLogView>, IDisposable
    {
        #region fields
        IAuditLogView View;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        /// <param name="notes">List<AuditLogNote></param>
        [ImportingConstructor]
        public AuditLogViewModel(IAuditLogView view)
            : base(view)
        {
            View = view;
            LogFilter = AuditLogFilterType.None;
            StartDate = DateTime.Now.AddDays(-1);//400);
            EndDate = DateTime.Now;
            
            AuditLogDalTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateAuditLogDal();


            // If contracts enabled
            // Contracts = new ObservableCollection<KeyValuePair<long, string>>(_contracts);
            // AuditLog.GetEntries(StartDate, EndDate, SelectedContract.Key);

            // If changed by filter 
            // AuditLog.GetEntries(StartDate, EndDate, ChangedBy);

            RefreshCommand = new MedQuist.BillingAdmin.ViewModels.SimpleCommand
            {
                CanExecuteDelegate = x => (true),
                ExecuteDelegate = x => RefreshAuditLog()
            };


        }
        #endregion

        public ICommand RefreshCommand
        {
            private get;
            set;
        }


        public void RefreshAuditLog()
        {
            try
            {
                List<AuditLogEntry> entries = new List<AuditLogEntry>();

                List<KeyValuePair<long, string>> _contracts = AuditLog.GetContractsInRange(StartDate, EndDate);
                //AuditLogEntries = new ObservableCollection<AuditLogEntry>(_contracts);

                //            IEnumerable<AuditLogEntry> obsCollection = (IEnumerable<AuditLogEntry>)AuditLogEntries;
                //            var list = new List<AuditLogEntry>(obsCollection);

                AuditLogViewer.LocalReport.DataSources.Clear();
                AuditLogViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
                AuditLogViewer.LocalReport.ReportPath = ApplicationInfo.ApplicationPath + "\\AuditLogRpt.rdlc";
                AuditLogViewer.ProcessingMode = ProcessingMode.Local; 
                AuditLogViewer.LocalReport.ReportEmbeddedResource = "Spheris.BillingAdmin.AuditLogRpt.rdlc";
                if(this.EnableContractList)
                    entries = AuditLog.GetEntries(StartDate, EndDate,SelectedContract.Key);
                else if(EnableChangedByList && String.IsNullOrEmpty(ChangedBy) == false)
                    entries = AuditLog.GetEntries(StartDate, EndDate, ChangedBy);
                else
                    entries = AuditLog.GetEntries(StartDate, EndDate);
                AuditLogViewer.LocalReport.DataSources.Add(new ReportDataSource("Spheris_Billing_AuditLogEntry", entries));
                //AuditLogViewer.da
                //ApplicationInfo.ApplicationPath 
                AuditLogViewer.Name = "Spheris_Billing_AuditLogEntry";
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("BeginDate", StartDate.ToShortDateString());
                parameters[1] = new ReportParameter("EndDate", EndDate.ToShortDateString());
                AuditLogViewer.LocalReport.SetParameters(parameters);
                AuditLogViewer.RefreshReport();
            }
            catch (Exception x)
            {
                View.ShowMsg(x.ToString());
            }
            //View.SetAuditLog( list );
        }


        public Microsoft.Reporting.WinForms.ReportViewer AuditLogViewer;

        #region Properties
        public AuditLogDal AuditLogDalTarget;

        private string changedBy;
        public string ChangedBy
        { 
            get
            {
                return changedBy;
            }
            set
            {
                changedBy = value;
                RaisePropertyChanged("ChangedBy");
            }
        }

        private ObservableCollection<AuditLogEntry> auditLogEntries;
        public ObservableCollection<AuditLogEntry> AuditLogEntries
        { 
            get
            {
                return auditLogEntries;
            }
            set
            {
                auditLogEntries = value;
                RaisePropertyChanged("AuditLogEntries");
            }
        }

        private KeyValuePair<long, string> selectedContract;
        public KeyValuePair<long, string> SelectedContract
        { 
            get
            {
                return selectedContract;
            }
            set
            {
                selectedContract = value;
                RaisePropertyChanged("SelectedContract");
            }
        }

        //public ObservableCollection<List<KeyValuePair<long, string>>> Contracts

        private ObservableCollection<KeyValuePair<long, string>> contracts;
        public ObservableCollection<KeyValuePair<long, string>> Contracts
        { 
            get
            {
                return contracts;
            }
            set
            {
                contracts = value;
                RaisePropertyChanged("Contracts");
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                List<KeyValuePair<long, string>> _contracts = AuditLog.GetContractsInRange(StartDate, EndDate);
                if(_contracts != null)
                Contracts = new ObservableCollection<KeyValuePair<long, string>>(_contracts);

                RaisePropertyChanged("StartDate");
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                List<KeyValuePair<long, string>> _contracts = AuditLog.GetContractsInRange(StartDate, EndDate);
                if(_contracts != null)
                Contracts = new ObservableCollection<KeyValuePair<long, string>>(_contracts);

                RaisePropertyChanged("EndDate");
            }
        }

        private AuditLogFilterType logFilter;
        public AuditLogFilterType LogFilter
        {
            get
            {
                return logFilter;
            }
            set
            {
                logFilter = value;
                switch (logFilter)
                { 
                    case AuditLogFilterType.None:
                        EnableChangedByList = false;
                        EnableContractList = false;
                        break;
                    case AuditLogFilterType.Contract:
                        EnableChangedByList = false;
                        EnableContractList = true;
                        break;
                    case AuditLogFilterType.ChangedBy:
                        EnableChangedByList = true;
                        EnableContractList = false;
                        break;
                }
                RaisePropertyChanged("LogFilter");
            }
        }

        private bool enableContractList;
        public bool EnableContractList
        {
            get
            {
                return enableContractList;
            }
            set
            {
                enableContractList = value;
                RaisePropertyChanged("EnableContractList");
            }
        }

        private bool enableChangedByList;
        public bool EnableChangedByList
        {
            get
            {
                return enableChangedByList;
            }
            set
            {
                enableChangedByList = value;
                RaisePropertyChanged("EnableChangedByList");
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


        #region Dispose
        public void Dispose()
        {
        }
        #endregion

    }
}