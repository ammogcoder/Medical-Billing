namespace MedQuist.BillingAdmin.WinForms
{
    partial class AuditLogViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.filterByContract = new System.Windows.Forms.RadioButton();
            this.filterByChangedBy = new System.Windows.Forms.RadioButton();
            this.ChangedBy = new System.Windows.Forms.TextBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterByNone = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Contracts = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Spheris.BillingAdmin.AuditLogRpt.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 82);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(912, 481);
            this.reportViewer1.TabIndex = 0;
            // 
            // StartDate
            // 
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDate.Location = new System.Drawing.Point(70, 19);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(88, 20);
            this.StartDate.TabIndex = 2;
            this.StartDate.ValueChanged += new System.EventHandler(this.StartDate_ValueChanged);
            // 
            // EndDate
            // 
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDate.Location = new System.Drawing.Point(71, 43);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(87, 20);
            this.EndDate.TabIndex = 4;
            this.EndDate.Value = new System.DateTime(2009, 7, 30, 0, 0, 0, 0);
            this.EndDate.ValueChanged += new System.EventHandler(this.EndDate_ValueChanged);
            // 
            // filterByContract
            // 
            this.filterByContract.AutoSize = true;
            this.filterByContract.Location = new System.Drawing.Point(63, 19);
            this.filterByContract.Name = "filterByContract";
            this.filterByContract.Size = new System.Drawing.Size(68, 17);
            this.filterByContract.TabIndex = 5;
            this.filterByContract.Text = "Contract:";
            this.filterByContract.UseVisualStyleBackColor = true;
            this.filterByContract.CheckedChanged += new System.EventHandler(this.filterByContract_CheckedChanged);
            // 
            // filterByChangedBy
            // 
            this.filterByChangedBy.AutoSize = true;
            this.filterByChangedBy.Location = new System.Drawing.Point(63, 43);
            this.filterByChangedBy.Name = "filterByChangedBy";
            this.filterByChangedBy.Size = new System.Drawing.Size(86, 17);
            this.filterByChangedBy.TabIndex = 6;
            this.filterByChangedBy.Text = "Changed By:";
            this.filterByChangedBy.UseVisualStyleBackColor = true;
            this.filterByChangedBy.CheckedChanged += new System.EventHandler(this.filterByChangedBy_CheckedChanged);
            // 
            // ChangedBy
            // 
            this.ChangedBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ChangedBy.Enabled = false;
            this.ChangedBy.Location = new System.Drawing.Point(155, 43);
            this.ChangedBy.Name = "ChangedBy";
            this.ChangedBy.Size = new System.Drawing.Size(276, 20);
            this.ChangedBy.TabIndex = 8;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(652, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(115, 64);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Contracts);
            this.groupBox1.Controls.Add(this.filterByNone);
            this.groupBox1.Controls.Add(this.ChangedBy);
            this.groupBox1.Controls.Add(this.filterByChangedBy);
            this.groupBox1.Controls.Add(this.filterByContract);
            this.groupBox1.Location = new System.Drawing.Point(209, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 72);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter By";
            // 
            // filterByNone
            // 
            this.filterByNone.AutoSize = true;
            this.filterByNone.Checked = true;
            this.filterByNone.Location = new System.Drawing.Point(6, 19);
            this.filterByNone.Name = "filterByNone";
            this.filterByNone.Size = new System.Drawing.Size(51, 17);
            this.filterByNone.TabIndex = 9;
            this.filterByNone.TabStop = true;
            this.filterByNone.Text = "None";
            this.filterByNone.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.EndDate);
            this.groupBox2.Controls.Add(this.StartDate);
            this.groupBox2.Location = new System.Drawing.Point(12, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 70);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date Range";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Start Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Date:";
            // 
            // Contracts
            // 
            this.Contracts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Contracts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Contracts.DisplayMember = "Value";
            this.Contracts.Enabled = false;
            this.Contracts.FormattingEnabled = true;
            this.Contracts.Location = new System.Drawing.Point(155, 15);
            this.Contracts.Name = "Contracts";
            this.Contracts.Size = new System.Drawing.Size(276, 21);
            this.Contracts.TabIndex = 10;
            this.Contracts.ValueMember = "Key";
            // 
            // AuditLogViewer
            // 
            this.AcceptButton = this.RefreshButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 563);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AuditLogViewer";
            this.Text = "AuditLogViewer";
            this.Load += new System.EventHandler(this.ReportViewer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.RadioButton filterByContract;
        private System.Windows.Forms.RadioButton filterByChangedBy;
        private System.Windows.Forms.TextBox ChangedBy;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton filterByNone;
        private System.Windows.Forms.ComboBox Contracts;
    }
}