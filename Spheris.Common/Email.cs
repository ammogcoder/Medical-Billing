using System;
using System.Configuration;
using System.Net.Mail;

namespace Spheris.Common
{
    public class Email : MailMessage
    {
        public string SmtpServer;
        public int SmtpPort;
        public string SmtpUsername;
        public string SmtpPassword;
        public bool Encrypt = false;

        public Email()
            : base()
        {
            GetAppSettings();
        }

        public Email(string smtpServer)
            :base()
        {
            SmtpServer = smtpServer;
            GetAppSettings();
        }

        public Email(string from, string to, string subject, string body)
            : base(from, to, subject, body)
        {
            GetAppSettings();
        }

        public Email(string from, string to)
            : base(from, to)
        {
            GetAppSettings();
        }

        public Email(MailAddress from, MailAddress to)
            : base(from, to)
        {
            GetAppSettings();
        }

        public Email(string from, string to, string subject, string body, string smtpServer)
            : base(from, to, subject, body)
        {
            SmtpServer = smtpServer;
            GetAppSettings();
        }

        ~Email()
        {
            base.Dispose(false);
        }

        private void GetAppSettings()
        {
            SmtpUsername= ConfigurationManager.AppSettings["SmtpUsername"];
            SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            if (String.IsNullOrEmpty(SmtpServer))
            {
                throw new System.Configuration.SettingsPropertyNotFoundException("The SmtpServer configuration value is not defined in the AppSettings section of the config file.");
            }
        }

        public virtual void Send()
        {
#if(DEBUG)
            System.Diagnostics.Debugger.Break();
            this.To.Clear();
            this.To.Add("ilee@medquist.com");
            this.To.Add("ian@houseoflees.net");
            this.Bcc.Clear();
            //base.Attachments.Clear();
            //string[] attachments = new string[2] {@"\\tncifs01\finance\Revenue & Receivables\Customer Files\Trinity Health (6046)\Trinity Health 10-04-30 6046-71790.XLS"
            //                                     ,@"\\tncifs01\finance\Revenue & Receivables\Customer Files\Trinity Health (6046)\Trinity Health 10-04-30 6046-71790.PDF"};
            //base.Attachments.Add(new Attachment(ZipFiles(attachments), "6049-71790.zip", "application/x-zip-compressed"));
#endif
            // Force all emails containing attachments to be encrypted.
            if (Encrypt) this.Subject += " [ENCRYPTED]";
            SmtpClient client = new SmtpClient(SmtpServer, SmtpPort);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
            client.Send((MailMessage)this);
        }
    }
}
