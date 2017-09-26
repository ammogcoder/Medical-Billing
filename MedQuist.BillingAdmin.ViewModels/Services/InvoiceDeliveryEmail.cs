using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace MedQuist.ViewModels.Services
{
    class InvoiceDeliveryEmail:Spheris.Common.Email
    {
        // TODO: Put these two constants in the database.  Build a UI to allow users to edit.
        private const string bodyTemplate = "<body style=\"font-family: Arial, Helvetica, sans-serif\">"
                                          + "<img src=\"https://customer.spheris.com/images/medquist250.jpg\"><br/>"
                                          + "<p>Your MedQuist invoices for the period ending {0:d} are attached.</p>"
                                          + "<p>If you have any questions or concerns related to your current invoice, please feel free to contact us at <a href=\"mailto:{2}\">{2}</a>.</p>"
                                          + "<p>{3}</p>"                                                             // Invoice body from database.
                                          + "Regards, <br/><br/>"
                                          + "<span style=\"color:#0033CC; font-weight:bold;\">{4}</span><br/>"       // Billing Specialist
                                          + "<span style=\"font-size:small;\">MedQuist Billing</span><br/>"
                                          + "<span style=\"font-size:small;\">{5}</span><br/>"                       // Phone
                                          + "<a href='mailto:{2}'><span style=\"font-size:small;\">{2}</span></a>"   // Email
                                          + "<br/>"
                                          + "</body>";

        private const string bodyTemplateArguments = "{InvoicePeriodEndDate}, {BillingEmailAddress}, {BodyInsert}, {BillingSpecialist}, {BillingPhoneNumber}";

        private string _billingSpecialistName;
        private DateTime _invoicePeriodEnd;
        private MailAddress _billingEmailAddress = new MailAddress("invoice@medquist.microsoftonline.com", "MedQuist Billing Department");
        private int _invoiceGroupId;
        private string _bodyInsert;

        public InvoiceDeliveryEmail(string to, DateTime invoicePeriodEnd, string bodyInsert, string billingSpecialistName, int invoiceGroupId, string[] attachments)
            : base()
        {
            if (String.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("To:", "No recipient email address was provided.");
            }

            this.From = _billingEmailAddress;
            this.To.Add(to.Replace(";",","));
            this.Bcc.Add(_billingEmailAddress);
            this.Attachments.Add(new Attachment(ZipFiles(attachments)
                                ,String.Format("{0}-{1:d} Invoice.zip", invoiceGroupId, invoicePeriodEnd)
                                ,"application/x-zip-compressed"));
            
            //foreach( string attachment in attachments)
            //{
            //    this.Attachments.Add(new System.Net.Mail.Attachment(attachment));
            //}

            _invoicePeriodEnd = invoicePeriodEnd;
            _billingSpecialistName = billingSpecialistName;
            _invoiceGroupId = invoiceGroupId;
            _bodyInsert = bodyInsert;
        }

        public override void Send()
        {
            object[] args = {_invoicePeriodEnd, "https://customer.spheris.com/sites/invoices/", _billingEmailAddress.Address, _bodyInsert, _billingSpecialistName, "(800) 368-1717"};
            base.Subject = String.Format("Your MedQuist invoice ({0}) for the period {1:d} is attached.", _invoiceGroupId, _invoicePeriodEnd);
            base.IsBodyHtml = true;
            base.Body = String.Format(bodyTemplate, args);
            base.Send();
        }

        public Stream ZipFiles(string[] files)
        {
            MemoryStream stream = new MemoryStream();
            using (ZipFile zip = new ZipFile())
            {
                foreach (string f in files)
                {
                    if (File.Exists(f))
                    {
                        zip.AddFile(f, "");
                    }
                }
                zip.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            return stream;
        }
    }
}
