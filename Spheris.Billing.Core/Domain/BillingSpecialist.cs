using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// BILL_SPECIALIST
    /// </summary>
    public class BillingSpecialist : ObjectBase<BillingSpecialist>
    {
        public BillingSpecialist(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public BillingSpecialist()
        {

        }

        /// <summary>
        /// BILL_SPECIALIST_ID.AllowDBNull = false;
        /// Key
        /// </summary>
        private int _Id;
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// columnNAME.AllowDBNull = false;
        /// columnNAME.MaxLength = 60;
        /// </summary>
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>
        /// columnPHONE.AllowDBNull = false;
        /// columnPHONE.MaxLength = 20;
        /// </summary>
        private string _Phone;
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                OnPropertyChanged("Phone");
            }
        }
        /// <summary>
        /// columnINVOICE_EMAIL_BODY.MaxLength = 2000;
        /// </summary>
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }


        /// <summary>
        /// columnDISPLAY_TITLE.MaxLength = 60;
        /// </summary>
        private string _DisplayTitle;
        public string DisplayTitle
        {
            get
            {
                return _DisplayTitle;
            }
            set
            {
                _DisplayTitle = value;
                OnPropertyChanged("DisplayTitle");
            }
        }

        /// <summary>
        /// INVOICE_EMAIL_BODY
        /// InvoiceEmailBody.MaxLength = 2000;
        /// </summary>
        private string _InvoiceEmailBody;
        public string InvoiceEmailBody
        {
            get
            {
                return _InvoiceEmailBody;
            }
            set
            {
                _InvoiceEmailBody = value;
                OnPropertyChanged("InvoiceEmailBody");
            }
        }



        #region Validation

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public string HasError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string str = GetValidationError(property);
                    if (str != null)
                        return str;
                }
                return null;
            }
        }

        public string FirstError
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    string msg = GetValidationError(property);
                    if (msg != null)
                        return msg;
                }
                return null;
            }
        }

        public override string GetValidationError(string propertyName)
        {
            if (!ValidatedProperties.Contains(propertyName))
                return null;
            string error = null;
            switch (propertyName)
            {
                case "Id":
                    error = (!HasValue(this.Id as object)) ? "Has no value" : null;
                    break;
                case "Name":
                    error = (!HasValue(this.Name as object)) ? "Has no value" : null;
                    break;
                case "Phone":
                    //TODO: Validate as phone number
                    break;
                case "Email":
                    //TODO: Validate emails - 
                    break;
                case "InvoiceEmailBody":
                    break;
                default:
                    break;
            }
            return error;
        }
        #endregion


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as BillingSpecialist;
            if (t == null)
                return false;
            if (Id == t.Id )
                return true;
            return false;
        }
        #endregion

    }
}
