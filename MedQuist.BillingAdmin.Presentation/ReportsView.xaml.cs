using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Windows.Forms.Integration;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
//using MedQuist.BillingAdmin.WinForms;
//using MedQuist.BillingAdmin.WinForms;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IReportsView))]
    public partial class ReportsView : System.Windows.Controls.UserControl, IReportsView
    {
        private readonly Lazy<ReportsViewModel> viewModel;
        private Microsoft.Reporting.WinForms.ReportViewer auditLogViewer;
        private IReportLinksView iReportLinksView;

        WindowsFormsHost host;

        static public Form GetForm(IntPtr handle)
        {
            return handle == IntPtr.Zero ?
                null :
                System.Windows.Forms.Control.FromHandle(handle) as Form;
        }

        public ReportsView()
        {

            InitializeComponent();
            viewModel = new Lazy<ReportsViewModel>(() => this.GetViewModel<ReportsViewModel>());

#if CODE_BEHIND
            //WindowsFormsHost host = new WindowsFormsHost();
            //Microsoft.Reporting.WinForms.ReportViewer reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            // Create instance of WindowsFormsHost to integrate the report viewer control with the WPF form.
            host = new WindowsFormsHost();

            

            // Create instance of Report Viewer Control
            Microsoft.Reporting.WinForms.ReportViewer reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();

            // Create instance of ReportViewer.xaml Window. This window will contain the output of the report.

            // Specifying local processing mode for the ReportViewer
            reportViewer.ProcessingMode = ProcessingMode.Local;

            reportViewer.LocalReport.ReportEmbeddedResource = "C:\\TSFDATA\\BillingAdmin\\AuditLogRpt.rdlc";

            //reportViewer.Load += 
            // Create a new ReportDataSource with the name of the DataSource and the object  that is to be used as the DataSource
            //ReportDataSource ds = new ReportDataSource("BELocation", locationList);

            // Add the ReportDataSource to the DataSoure of the ReportViewer
            //reportViewer.LocalReport.DataSources.Add(ds);

            // Causes the current report in the Report Viewer to be processed and rendered.
            reportViewer.RefreshReport();

            // Sets the child control hosted by the WindowsFormsHost element.
            host.Child = reportViewer;

            // Add the WindowsFormsHost element to the Grid in the ReportViewer.xaml
            this.MasterGrid.Children.Add(host);
            //MasterGrid.Children;//.Add(host);
            //Grid rGrid = (Grid)win.FindName("Master");
            //rGrid.Children.Add(host);
#endif
        }


        public string MyTitle
        {
            get
            {
                return "Reports";
            }
        }


        public void ShowMsg(string errMsg)
        {
            throw new NotImplementedException();
        }

        private void windowsFormsHost1_Loaded(object sender, RoutedEventArgs e)
        {


        }

        public bool IsInView
        {
            get
            {
                return IsVisible;
            }
        }

        private void ReportsGotFocus(object sender, RoutedEventArgs e)
        {
            viewModel.Value.reportLinksViewModel.ResetView();
        }
    }
}

