using Microsoft.Reporting.WinForms;
using Spheris.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedQuist.BillingAdmin.WinForms
{
    public partial class AuditLogViewer : Form
    {
        public AuditLogViewer()
        {
            InitializeComponent();
        }

        public AuditLogViewer(Control parent/*Panel parentPanel*/)
        {
            InitializeComponent();

            this.TopLevel = false;
            this.Parent = parent;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            StartDate.Value = DateTime.Now.AddDays(-1);
            EndDate.Value = DateTime.Now;
            RefreshReport();
            RefreshContractsList();
        }

        private void RefreshContractsList()
        {
            string selectedItem = "";
            if(Contracts.SelectedValue != null)
            {
                selectedItem = Contracts.SelectedValue.ToString();
            }
            //Contracts.Items.Clear();
            //Contracts.BeginUpdate();
            //foreach (KeyValuePair<long, string> contract in AuditLog.GetContractsInRange(StartDate.Value, EndDate.Value))
            //{
            //    Contracts.Items.Add(contract);
            //}
            Contracts.DataSource = AuditLog.GetContractsInRange(StartDate.Value, EndDate.Value);
            Contracts.ValueMember = "Key";
            Contracts.DisplayMember = "Value";
            Contracts.EndUpdate();
        }

        private void RefreshReport()
        {
            List<AuditLogEntry> entries;
            if (filterByContract.Checked)
            {
                // Make sure that a contract has been specified.
                if (Contracts.SelectedValue == null)
                {
                    throw new ArgumentNullException("A contract has not been selected.");
                }

                entries = AuditLog.GetEntries(StartDate.Value, EndDate.Value, long.Parse(Contracts.SelectedValue.ToString()));
            }
            else if (filterByChangedBy.Checked)
            {
                // Make sure that a user name has been specified.
                if (string.IsNullOrEmpty(ChangedBy.Text.Trim()))
                {
                    throw new ArgumentNullException("A user name has not been specified.");
                }

                entries = AuditLog.GetEntries(StartDate.Value, EndDate.Value, ChangedBy.Text);
            }
            else
            {
                entries = AuditLog.GetEntries(StartDate.Value, EndDate.Value);
            }
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Spheris_Billing_AuditLogEntry", entries));
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("BeginDate", StartDate.Value.ToShortDateString());
            parameters[1] = new ReportParameter("EndDate", EndDate.Value.ToShortDateString());
            this.reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.RefreshReport();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshReport();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            catch
            {
                throw;
            }
        }

        private void filterByContract_CheckedChanged(object sender, EventArgs e)
        {
            Contracts.Enabled = filterByContract.Checked;
        }

        private void filterByChangedBy_CheckedChanged(object sender, EventArgs e)
        {
            ChangedBy.Enabled = filterByChangedBy.Checked;
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshContractsList();
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshContractsList();
        }
    }
}
