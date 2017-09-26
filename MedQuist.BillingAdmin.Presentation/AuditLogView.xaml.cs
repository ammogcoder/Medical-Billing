using System.ComponentModel.Composition;
using System.Windows;
using MedQuist.BillingAdmin.ViewModels.Views;
using System;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Controls;
using MedQuist.ViewModels.Views;
using MedQuist.BillingAdmin.ViewModels;
using System.Waf.Applications;
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace MedQuist.BillingAdmin.Presentation
{
    [Export(typeof(IAuditLogView))]
    public partial class AuditLogView : System.Windows.Controls.UserControl, IAuditLogView
    {
        private readonly Lazy<AuditLogViewModel> viewModel;
        // private Microsoft.Reporting.WinForms.ReportViewer auditLogViewer;

        static public Form GetForm(IntPtr handle)
        {
            return handle == IntPtr.Zero ?
                null :
                System.Windows.Forms.Control.FromHandle(handle) as Form;
        }

        public AuditLogView()
        {
            InitializeComponent();
            viewModel = new Lazy<AuditLogViewModel>(() => this.GetViewModel<AuditLogViewModel>());
            MsgBox.SetParent(MainGrid);

        }


        public string MyTitle
        {
            get
            {
                return "Audit Log";
            }
        }


        public void ShowMsg(string errMsg)
        {
            MsgBox.ShowHandlerDialog(errMsg);
        }

        private void windowsFormsHost1_Loaded(object sender, RoutedEventArgs e)
        {
            if (viewModel != null && viewModel.Value != null)
            {
                viewModel.Value.AuditLogViewer = new Microsoft.Reporting.WinForms.ReportViewer();
                //viewModel.Value.AuditLogViewer.ProcessingMode = ProcessingMode.Local;
                //viewModel.Value.AuditLogViewer.LocalReport.ReportEmbeddedResource = "C:\\TSFDATA\\BillingAdmin\\AuditLogRpt.rdlc";
                viewModel.Value.AuditLogViewer.LocalReport.ReportEmbeddedResource = "Spheris.BillingAdmin.AuditLogRpt.rdlc";

                viewModel.Value.AuditLogViewer.Parent = GetForm(windowsFormsHost1.Handle);
                windowsFormsHost1.Child = viewModel.Value.AuditLogViewer;
            }
        }

        //void SetAuditLog( List<AuditLogEntry> entries)
        //{ 

        //}

        private void Refresh()
        {
            viewModel.Value.AuditLogViewer.LocalReport.DataSources.Clear();
            //auditLogViewer.LocalReport.DataSources.Add(new ReportDataSource("Spheris_Billing_AuditLogEntry", entries));

        }

        public bool IsInView
        {
            get
            {
                return IsVisible;

            }
        }
    }
}

