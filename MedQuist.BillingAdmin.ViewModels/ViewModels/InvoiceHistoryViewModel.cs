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
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace MedQuist.BillingAdmin.ViewModels
{
    /// <summary>
    /// ViewModel for InvoiceHistory Section
    /// </summary>
    [Export]
    public class InvoiceHistoryViewModel : ViewModel<IInvoiceHistoryView>, IDisposable
    {
        #region
        IInvoiceHistoryView ThisView;
        string hidden = "Hidden";
        string visible = "Visible";

        string asBatch = "Invoice Batch";
        string asRange = "Invoice Range";
        #endregion

        #region ctor
        /// <summary>
        /// Ctor uses a of Models 
        /// </summary>
        [ImportingConstructor]
        public InvoiceHistoryViewModel(IInvoiceHistoryView view)
            : base(view)
        {
            ThisView = view;

            bAvoidDoubleRefresh = true;
            ToDateAll = ToDate = DateTime.Now;
            FromDateAll = FromDate = ToDate.AddDays(-90);
            bAvoidDoubleRefresh = false;

            InvoiceTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceRepository();
            InvoiceGroupTarget = Spheris.Billing.Data.BillingDataFactory.NewInstance().CreateInvoiceGroupRepository();

            PreparePrinterList();
            Loading = hidden;
            SearchMethod = asBatch;

            ReportType = ReportTypes.BillOnly;
        }
        #endregion

        private string searchMethod;
        public string SearchMethod
        {
            get
            {
                return searchMethod;
            }
            set
            {
                searchMethod = value;
                RaisePropertyChanged("SearchMethod");
            }
        }

        private ObservableCollection<string> printers;
        public ObservableCollection<string> Printers
        {
            get
            {
                return printers;
            }
            set
            {
                printers = value;
                RaisePropertyChanged("Printers");
            }
        }


        private void PreparePrinterList()
        {
            PrintDocument ExampleDoc = new PrintDocument();
            string strDefaultPrinter = ExampleDoc.PrinterSettings.PrinterName;
            Printers = new ObservableCollection<string>();
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                Printers.Add(strPrinter);
        }



        private string loading;
        public string Loading
        {
            get
            {
                return loading;
            }
            set
            {
                loading = value;
                RaisePropertyChanged("Loading");
            }
        }

        private bool byRange;
        public bool ByRange
        {
            get
            {
                return byRange;
            }
            set
            {
                byRange = value;
                if (byRange)
                    SearchMethod = asRange;
                else
                    SearchMethod = asBatch;

                refresh();
                SetInvoiceCounts();
                RaisePropertyChanged("ByRange");
                RaisePropertyChanged("EnableBatch");


            }
        }

        public bool EnableBatch
        {
            get
            {
                return !ByRange;
            }
        }

        public bool EnableOnlyThisGroup
        {
            get
            {
                return (CurrentGroupItem == null) ? false : true;
            }
        }
        bool bAvoidDoubleRefresh = false;

        private DateTime fromDate;
        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;

                RaisePropertyChanged("FromDate");

                if (!bAvoidDoubleRefresh)
                    refresh();

            }
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
                RaisePropertyChanged("ToDate");
                if (!bAvoidDoubleRefresh)
                    refresh();
            }
        }


        private DateTime fromDateAll;
        public DateTime FromDateAll
        {
            get
            {
                return fromDateAll;
            }
            set
            {
                fromDateAll = value;
                SetInvoiceCounts();
                RaisePropertyChanged("FromDateAll");
            }
        }

        private DateTime toDateAll;
        public DateTime ToDateAll
        {
            get
            {
                return toDateAll;
            }
            set
            {
                toDateAll = value;
                SetInvoiceCounts();
                RaisePropertyChanged("ToDateAll");
            }
        }

        private ObservableCollection<InvoiceCount> invoiceCounts;
        public ObservableCollection<InvoiceCount> InvoiceCounts
        {
            get
            {
                return invoiceCounts;
            }
            set
            {
                invoiceCounts = value;
                RaisePropertyChanged("InvoiceCounts");
            }
        }

        private InvoiceCount selectedInvoiceCount;
        public InvoiceCount SelectedInvoiceCount
        {
            get
            {
                return selectedInvoiceCount;
            }
            set
            {
                selectedInvoiceCount = value;
                if (selectedInvoiceCount != null)
                {
                    ToDate = selectedInvoiceCount.BILL_PERIOD_END_BEFORE;
                    FromDate = selectedInvoiceCount.BILL_PERIOD_START;
                    refresh();
                    bAvoidDoubleRefresh = false;
                }

                RaisePropertyChanged("SelectedInvoiceCount");
            }
        }

        private string CreateFileName(string Path
             , DateTime EndsOn
             , string InvoiceGrpID
             , string InvoiceGrpDescr
             , string InvoiceNbr
             , bool ValidatePath)
        {

            //			92 - \
            //			47 - / 
            //			46 - .
            //			58 - : 
            //			42 - * 
            //			63 - ? 
            //			34 - "
            //			60 - <
            //			62 - >
            //			124 - |
            //some of this may be redundant with UI edits but just making sure
            string CleanGrpName = InvoiceGrpDescr
                .Replace(new string((Char)92, 1), "-")
                .Replace(new string((Char)47, 1), "-")
                .Replace(new string((Char)46, 1), "")
                .Replace(new string((Char)58, 1), "-")
                .Replace(new string((Char)42, 1), "")
                .Replace(new string((Char)63, 1), "")
                .Replace(new string((Char)34, 1), "")
                .Replace(new string((Char)60, 1), "-")
                .Replace(new string((Char)62, 1), "-")
                .Replace(new string((Char)124, 1), "-");

            //make sure path has a forward slash on the tail
            if (Path.Substring(Path.Length - 1) != "\\")
                Path += "\\";

            // Build the path
            Path += CleanGrpName
                    + " (" + InvoiceGrpID + ")\\";

            return (Path
                + CleanGrpName
                + " " + EndsOn.ToString("yy-MM-dd")
                + " " + InvoiceNbr);
        }

        private ICommand printCommand;
        public ICommand PrintCommand
        {
            get
            {
                return printCommand ?? (printCommand = new SimpleCommand()
                {
                    CanExecuteDelegate = x => (SelectedInvoices.Count > 0),
                    ExecuteDelegate = x => PrintDocuments()
                }
                );
            }
        }


        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get
            {
                return viewCommand ?? (viewCommand = new SimpleCommand()
                {
                    CanExecuteDelegate = x => (SelectedInvoices.Count == 1),
                    ExecuteDelegate = x => ViewDocument()
                }
                );
            }
        }



        private void PrintDocuments()
        {
            System.Diagnostics.Process PrintProcess = new Process();
            PrintProcess.StartInfo.Verb = "Print";

            foreach (Invoice invoice in SelectedInvoices)
            {
                InvoiceGroup invoiceGroup = new InvoiceGroup { InvoiceGrpId = (int)invoice.INVOICE_GRP_ID };
                invoiceGroup = InvoiceGroupTarget.Get(invoiceGroup);

                PrintProcess.StartInfo.FileName = CreateFileName(invoiceGroup.DefaultBillFilePath,
                    invoice.BILL_PERIOD_END_BEFORE ?? DateTime.Now,
                    invoiceGroup.InvoiceGrpId.ToString(),
                    invoiceGroup.Description,
                    invoiceGroup.InvoiceGrpId.ToString() + "-" + invoice.INVOICE_ID.ToString(),
                    false);

                try
                {
                    if (ReportType == ReportTypes.BillOnly || ReportType == ReportTypes.BillAndDetail)
                        PrintProcess.StartInfo.FileName += ".pdf";
                    else
                        PrintProcess.StartInfo.FileName += ".xls";
                    PrintProcess.Start();
                }
                catch (Exception x)
                {
                    ThisView.ShowMsg(x.ToString());
                }
            }
        }

        private void ViewDocument()
        {

            if (SelectedInvoices.Count != 1)
                return;
            Invoice invoice = SelectedInvoices[0];
            InvoiceGroup invoiceGroup = new InvoiceGroup { InvoiceGrpId = (int)invoice.INVOICE_GRP_ID };
            invoiceGroup = InvoiceGroupTarget.Get(invoiceGroup);

            string fileName = CreateFileName(invoiceGroup.DefaultBillFilePath,
                    invoice.BILL_PERIOD_END_BEFORE ?? DateTime.Now,
                    invoiceGroup.InvoiceGrpId.ToString(),
                    invoiceGroup.Description,
                    invoiceGroup.InvoiceGrpId.ToString() + "-" + invoice.INVOICE_ID.ToString(),
                    false);
            if (ReportType == ReportTypes.BillOnly || ReportType == ReportTypes.BillAndDetail)
                fileName += ".pdf";
            else if (ReportType == ReportTypes.DetailOnly)
                fileName += ".xls";
            else
                return;
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        private void SetInvoiceCounts()
        {
            if (ToDateAll != null && FromDateAll != null && InvoiceTarget != null)
            {
                InvoiceCounts = InvoiceTarget.FetchCount(FromDateAll, ToDateAll);
            }
        }


        #region Data Target
        private IInvoiceRepository InvoiceTarget { get; set; }
        private IInvoiceGroupRepository InvoiceGroupTarget { get; set; }
        #endregion

        private ObservableCollection<Invoice> invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get
            {
                return invoices;
            }
            set
            {
                invoices = value;
                RaisePropertyChanged("Invoices");
            }
        }

        private ObservableCollection<Invoice> selectedInvoices;
        //public IEnumerable<Invoice> SelectedInvoices
        public ObservableCollection<Invoice> SelectedInvoices
        {
            get
            {
                return selectedInvoices ?? (selectedInvoices = new ObservableCollection<Invoice>());
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

        private bool onlyThisGroup;
        public bool OnlyThisGroup
        {
            get
            {
                return onlyThisGroup;
            }
            set
            {
                if (onlyThisGroup != value)
                {
                    onlyThisGroup = value;
                    refresh();
                    RaisePropertyChanged("OnlyThisGroup");
                }
            }
        }

        private int invoiceCountOf;
        public int InvoiceCountOf
        {
            get
            {
                return invoiceCountOf;
            }
            set
            {
                invoiceCountOf = value;
                RaisePropertyChanged("InvoiceCountOf");
            }
        }

        private InvoiceGroup currentGroupItem;
        public InvoiceGroup CurrentGroupItem
        {
            get
            {
                return currentGroupItem;
            }
            set
            {
                currentGroupItem = value;

                RaisePropertyChanged("EnableOnlyThisGroup");
                RaisePropertyChanged("OnlyThisGroup");
            }
        }


        ICommand _refreshCommand;

        void refresh()
        {
            var task = new Task(Refresh);
            task.Start();
        }

        void Refresh()
        {
            try
            {

                Loading = visible;
                if (OnlyThisGroup)
                    Invoices = InvoiceTarget.FetchInvoices(FromDate, ToDate, CurrentGroupItem.InvoiceGrpId, !ByRange);
                else
                    Invoices = InvoiceTarget.FetchInvoices(FromDate, ToDate, null, !ByRange);
                Loading = hidden;
            }
            catch (Exception x)
            {
                ThisView.ShowMsg(x.ToString());
            }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new DelegateCommand(refresh)); }
        }

        public enum ReportTypes
        {
            BillOnly,
            DetailOnly,
            BillAndDetail
        }

        private ReportTypes reportType;
        public ReportTypes ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
                RaisePropertyChanged("ReportType");
            }
        }

        public void Dispose()
        {
        }
    }
}