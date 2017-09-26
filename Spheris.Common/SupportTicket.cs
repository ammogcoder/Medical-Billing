using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Spheris.Common
{
    public abstract class SupportTicket : ISupportTicket
    {
        private string _requester = "";
        private string _emailAddress = "";
        private string _category = "";
        private string _type = "";
        private string _item = "";
        private string _operationalStatus = "";
        private string _description = "";
        private string _assignedGroup = "";
        private string _logOnName = "";

        public string Requester
        {
            get { return _requester; }
            set { _requester = value; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public string OperationalStatus
        {
            get { return _operationalStatus; }
            set { _operationalStatus = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string AssignedGroup
        {
            get { return _assignedGroup; }
            set { _assignedGroup = value; }
        }

        public string LogOnName
        {
            get { return _logOnName; }
            set { _logOnName = value; }
        }

        public abstract string Create();

        public string Create(string description)
        {
            _description = description;
            return(Create());
        }

        public static string DescribeException(Exception ex)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(Application.ProductName + " experienced an unexpected error." + (char)10 + (char)10);
            Exception e = ex;
            while( e != null )
            {
                msg.Append(e.ToString() + (char)13 + (char)10);
                e = e.InnerException;
            }
            return(msg.ToString());
        }
    }
}
