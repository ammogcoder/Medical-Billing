using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Spheris.Common
{
    public class RemedyTicket : SupportTicket
    {
        private MailAddress _fromAddress;
        private string _smtpServer;

        public RemedyTicket()
        {
            string appSetting = "";

            base.Requester = "ILEE";
            base.EmailAddress = "ILee@medquist.com";
            base.Category = "CS- Financial Support";
            base.Type = "BAS (Billing System)";
            base.Item = "Problem";
            base.OperationalStatus = "Limited Capacity";
            base.AssignedGroup = "Financial Support";
            base.LogOnName = "ILee";

            // FromAddress
            appSetting = ConfigurationManager.AppSettings["SupportTicketFromAddress"];
            if (String.IsNullOrEmpty(appSetting))
                throw new System.Configuration.SettingsPropertyNotFoundException("The SupportTicketFromAddress configuration value is not defined in the AppSettings section of the config file.");
            else
                _fromAddress = new MailAddress(appSetting);

            // SmtpServer
            appSetting = ConfigurationManager.AppSettings["SmtpServer"];
            if (String.IsNullOrEmpty(appSetting))
                throw new System.Configuration.SettingsPropertyNotFoundException("The SmtpServer configuration value is not defined in the AppSettings section of the config file.");
            else
                _smtpServer = appSetting;
        }

        public string SmtpServer
        {
            get
            {
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;
            }
        }

        public MailAddress FromAddress
        {
            get
            {
                return _fromAddress;
            }
            set
            {
                _fromAddress = value;
            }
        }

        #region CreateViaWebPost Code

        //private Uri _url = new Uri(@"http://tenrem100/CorpSuppSubmit.aspx");

        //public Uri Url
        //{
        //    get { return _url; }
        //    set { _url = value; }
        //}

        //private string CreateTicketViaWebPost()
        //{
        //    // WARNING:  This function is incomplete.  It almost works,
        //    //           but work was halted so that the prefered method
        //    //           CreateTicketByEmail could be created.

        //    System.IO.StreamWriter myWriter;
        //    string result = "";

        //    string formParams = BuildRequestBody();

        //    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url);
        //    request.Method = "POST";
        //    request.ContentLength = formParams.Length;
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    //request.SendChunked = true;
        //    //request.TransferEncoding = "gzip, deflate";
        //    request.CookieContainer = new System.Net.CookieContainer(); // enable cookies
        //    request.KeepAlive = false;
        //    request.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        //    request.Timeout = (int)60 * 1000;
        //    request.AllowAutoRedirect = true;
        //    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; InfoPath.1)";
        //    request.AllowWriteStreamBuffering = true;

        //    myWriter = new System.IO.StreamWriter(request.GetRequestStream());
        //    try
        //    {
        //        myWriter.Write(formParams, 0, formParams.Length);
        //        myWriter.Flush();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        myWriter.Close();
        //    }

        //    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
        //    System.IO.StreamReader myReader = new System.IO.StreamReader(response.GetResponseStream());
        //    result = myReader.ReadToEnd();
        //    myReader.Close();

        //    return result;
        //}

        //private string BuildRequestBody()
        //{
        //    //// Get the initial viewstate parameter.
        //    //string viewstate = InitialViewState(Url);
        //    //viewstate = HttpUtility.UrlEncode(viewstate);

        //    // Build parameter string.
        //    string formParams = HttpUtility.UrlEncode(
        //            "__EVENTTARGET="
        //          + "&__EVENTARGUMENT="
        //          + "&__LASTFOCUS="
        //          + "&__VIEWSTATE=" + InitialViewState(Url)
        //          + "&tbRequester=" + this.Requester
        //          + "&Submit=Submit"
        //          + "&tbEmail=" + this.EmailAddress
        //          + "&ddlCategory=" + this.Category
        //          + "&ddlType=" + this.Type
        //          + "&ddlItem=" + this.Item
        //          + "&ddlOpStatus=" + this.OperationalStatus
        //          + "&tbDescription=" + this.Description
        //          + "&ddlGroup=" + this.AssignedGroup
        //          + "&__EVENTVALIDATION=/wEWcwLosIyfDALejoi1CwK8w4S2BAKMqLLaDgLh2pKgCgLt6NWbDwKlhZbqDQLu6YzZBAKN2Y/tBQK64cPVDQKx6KgOAovZkcgLAvbLz+INAtHjmeUNAr2yz4cLAvSOzKgDArHbiKoDAofAmL4PApjS6IQKAuvXidYIAsHojLcKAp6zhoQIAq74yLsMAuD032ICt7rEogwC7tv38AwC98fiowEC4q+AuwgCvvXEkAUCmZjZ2A0C2vqK6AYCuqWp+Q8C2pq1uwsCjfnepQ8Ci63MqQQCwYvwygkCg7jAHQLVsIKVCQLl8sXDAgK1qYixAwKeoqPnBALZzpbJAQLyyczjDwLui8K+CgKdj9e3BwLwkZCtDAKFvqKhBQKs8br/AwK0gMnQAQLY5Oa1BQKWmvjxCwKf3apkAqvvuogEAtqMiMwDAojL6vIJArG4y4MMAtai2NcFAum2yuYCAvD02+EPAsDirfEFAsr26r4JAonLnNsHArnThpYJAsPA5qMIAtnux+MIAsiW6pMFAo/t29gIAvryvfwCApveto0GApmykJEGAu7O74gMAtaK5SYC+pntyQwCtevsvgECotGTnQ0CkpqBgAcCso7q7ggCjenl+AQCmMGLsgQCiN7gnwoC39CDtwsCmrKarwQC9KTQ0wQClOv9yAkCx8HqWALZ1eTdBALgsZhsAsv23ZMOAtPG/7cMAqHLsqkKAo3bnoQEArzKmOEEApST2rwFAt6DrcgEAo7x168KAtXr19AJAsexl48PAoLfgJUHAqC7vdwCAu681coNArr4lukBAs2cg8gLArzazncCr8KXRwLki5iKAQKaloTlCwLPzoysBAKn5eroCwLp+/3gCwLuyO/kCAK0vpjoBQKR2pSfBgLOo+GMDwKY0ry4DAKXzPuZDaJonGp263MU43hZqQL0wbagcujv");

        //    // Return UTF8 encoded byte array.
        //    return (formParams);
        //}

        //static string InitialViewState(Uri url)
        //{
        //    WebClient wc = new WebClient();
        //    wc.Credentials = CredentialCache.DefaultNetworkCredentials;
        //    System.IO.Stream st = wc.OpenRead(url.AbsoluteUri);
        //    System.IO.StreamReader sr = new System.IO.StreamReader(st);
        //    string line;
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        if (line.IndexOf("__VIEWSTATE") != -1) // found line
        //        {
        //            sr.Close();
        //            st.Close();
        //            int startIndex = line.IndexOf("value=") + 7;
        //            int endIndex = line.IndexOf("\"", startIndex);
        //            int count = endIndex - startIndex;
        //            return line.Substring(startIndex, count);
        //        }

        //    }
        //    sr.Close();
        //    st.Close();
        //    return "not found";
        //}

        #endregion

        public override string Create()
        {
#if(DEBUG)
            // Let's not accidentally create a support ticket.
            System.Diagnostics.Debugger.Break();
#endif
            CreateTicketViaEmail();
            return("");
        }

        private void CreateTicketViaEmail()
        {
            using (MailMessage msg = new MailMessage())
            {
                msg.From = FromAddress;
                msg.To.Add(new MailAddress("remedymail@spheris.com"));
#if(DEBUG)
                msg.Bcc.Add(new MailAddress("ILee@medquist.com"));
#endif
                msg.Subject = "Corporate Support Web Insert";
                msg.Body = BuildMailMessageBody();
                SmtpClient client = new SmtpClient(SmtpServer);
                client.Send(msg);
            }
        }

        private string BuildMailMessageBody()
        {
            return( "Schema: HPD:HelpDesk " + (char)13 +
                    "Server: TENREM100.corp.spheris.com " + (char)13 +
                    "Login: EmailImport     " + (char)13 +
                    "Password: EmailImport01     " + (char)10 +
                    "Action: Submit " + (char)10 +
                    "Format: Short " + (char)10 +
                    "Submitter !        2!: $USER$     " + (char)10 +
                    "Login Name+ !240000005!: " + this.LogOnName + "     " + (char)10 +
                    "Name+ !240000001!: " + "Ian Lee" + "     " + (char)10 +
                    "Urgency !240000009!: High" + (char)10 +
                    "Operational Status !536870990!: " + this.OperationalStatus + "     " + (char)10 +
                    "Source !260000128!: Email" + (char)10 + 
                    "Priority !260000126!: High" + (char)10 + 
                    "Case Type !260000130!: Problem" + (char)10 +
                    "Status !        7!: New     " + (char)10 +
                    "Account !536870991!: Spheris     " + (char)10 +
                    "Group+ !240000006!: " + this.AssignedGroup + (char)10 +
                    "Category !200000003!: " + this.Category + "     " + (char)10 +
                    "Type !200000004!: " + this.Type + "     " + (char)10 +
                    "Item !200000005!: " + this.Item + "     " + (char)10 +
                    "Create Date !260000502!: " + DateTime.Now.ToString("g", System.Globalization.CultureInfo.InvariantCulture) + "     " + (char)10 +
                    "Summary !        8!:  " + this.Description + "     " + (char)10 +
                    "Description !240000007!: " + this.Description + "     " + (char)10 +
                    "Known Error !260800010!: No" );
        }
    }
}
