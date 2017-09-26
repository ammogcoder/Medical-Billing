using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Core.Domain
{

    /// <summary>
    /// INVOICE_GRP
    /// KEY - INVOICE_GRP_ID
    /// </summary>
    public class InvoiceGroup : ObjectBase<InvoiceGroup>
    {
        #region Fields

        #endregion

        public InvoiceGroup()
        {
            InvoiceGrpId = 0;
        }

        /// <summary>
        /// New Group
        /// </summary>
        public InvoiceGroup(InvoiceGroup copyMe)
        {
            this.AttnLine = copyMe.AttnLine;
            this.AttnLineHeading = copyMe.AttnLineHeading;
            this.BillEmail = copyMe.BillEmail;
            this.BillingFrequency = copyMe.BillingFrequency;
            this.BillingSpecialistID = copyMe.BillingSpecialistID;
            this.Brand = copyMe.Brand;
            this.DefaultBillFilePath = copyMe.DefaultBillFilePath;
            this.ChangedBy = copyMe.ChangedBy;
            this.ChangedOn = copyMe.ChangedOn;
            this.ContractID = copyMe.ContractID;
            this.DeliveryMethod = copyMe.DeliveryMethod;
            this.Description = copyMe.Description;
            this.DoNotBillBefore = copyMe.DoNotBillBefore;
            this.GLDistributionNumber = copyMe.GLDistributionNumber;
            this.GPCustomerNumber = copyMe.GPCustomerNumber;
            this.InvoiceGrpId = copyMe.InvoiceGrpId;
            this.InvoiceGroupStatus = copyMe.InvoiceGroupStatus;
            this.InvoiceStyle = copyMe.InvoiceStyle;
            this.IsBWInvoiceStyle = copyMe.IsBWInvoiceStyle;
            this.LegacyCustomerNumber = copyMe.LegacyCustomerNumber;
            this.PrimaryPlatform = copyMe.PrimaryPlatform;
            this.RemitToID = copyMe.RemitToID;
            this.TimeZone = copyMe.TimeZone;
            this.UseDst = copyMe.UseDst;
            this.EncryptionOptout = copyMe.EncryptionOptout;

            // WAY TOO SLOW
            //this.InvoiceGrpReports = new ObservableCollection<InvoiceGrpReport>();
            //foreach (InvoiceGrpReport report in copyMe.InvoiceGrpReports)
            //    this.InvoiceGrpReports.Add( new InvoiceGrpReport(report));
        }

        
        public InvoiceGroup Clone()
        {
            return BaseClone<InvoiceGroup>();
        }
        
        #region Properties

        // used to spoof an entry for displaying text in the output window
        public bool IsSpoof { get; set; }


        /// <summary>
        /// INVOICE_GRP_ID	- no NULL
        /// type - int
        /// DB Defaults to: GENERATED
        /// AllowNull No
        /// </summary>
        public int InvoiceGrpId
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("InvoiceGrpId"); }
        }
        private int _id = 0;

        string ValidateID()
        {
            //TODO:
            return null;
        }

        /// <summary>
        /// DESCR - No Null
        /// VARCHAR
        /// DB Defaults to: empty
        /// AllowNull No
        /// </summary>
        public string Description
        {
            get 
            { 
                return _description; 
            }
            set 
            { 
                _description = value; 
                OnPropertyChanged("Description"); 
            }
        }
        string ValidateDescription()
        {
            //TODO:
            return null;
        }
        private string _description = "Please fill out description";

        /// <summary>
        /// CHANGED_ON	- SYSDATE
        /// DATE
        /// AllowNull No
        /// </summary>
        public DateTime ChangedOn
        {
            get { return _changedOn; }
            set { _changedOn = value; OnPropertyChanged("ChangedOn"); }
        }
        string ValidateChangedOn()
        {
            //TODO:
            return null;
        }
        private DateTime _changedOn = DateTime.Now;

        /// <summary>
        /// CHANGED_BY - 
        /// type VARCHAR2(255 Byte)	    
        /// Default value SYS_CONTEXT('USERENV', 'OS_USER')
        /// AllowNull No
        /// </summary>
        public string ChangedBy
        {
            get { return _changedBy; }
            set { _changedBy = value; OnPropertyChanged("ChangedBy"); }
        }
        string ValidateChangedBy()
        {
            //TODO:
            return null;
        }
        private string _changedBy;// Default set by creator


        /// <summary>
        /// USE_DST
        /// type - CHAR (1 Byte)	
        /// DB Defaults to: 'Y'
        /// AllowNull No
        /// </summary>
        public bool UseDst
        {
            get { return _useDst; }
            set { _useDst = value; OnPropertyChanged("UseDst"); }
        }
        string ValidateUseDst()
        {
            //TODO:
            return null;
        }
        private bool _useDst = true;

        /// <summary>
        /// BILLING_FREQ
        /// type - CHAR(4)
        /// DB Defaults to: 'MNTH'
        /// AllowNull N
        /// </summary>
        public string BillingFrequency
        {
            get { return _billingFreq; }
            set { _billingFreq = value; OnPropertyChanged("BillingFrequency"); }
        }
        string ValidateBillingFrequency()
        {
            //TODO:
            return null;
        }
        private string _billingFreq = "MNTH";

        /// <summary>
        /// TIMEZONE - 
        /// type - VARCHAR2 (4 Byte)
        /// DB Defaults to: 'CST'	
        /// AllowNul No
        /// </summary>
        public string TimeZone
        {
            get { return _timezone; }
            set { _timezone = value; OnPropertyChanged("TimeZone"); }
        }
        string ValidateTimeZone()
        {
            //TODO:
            return null;
        }
        private string _timezone = "CST";

        /// <summary>
        /// GL_DISTR_NBR	
        /// type - VARCHAR2 (15 Byte)
        /// DB Defaults to: '?'
        /// AllowNull N
        /// </summary>
        public string GLDistributionNumber
        {
            get { return _glDistrNbr; }
            set { _glDistrNbr = value; OnPropertyChanged("GLDistributionNumber"); }
        }
        string ValidateGLDistributionNumber()
        {
            //TODO:
            return null;
        }
        private string _glDistrNbr = "?";

        /// <summary>
        /// BILL_SPECIALIST_ID
        /// type - INT
        /// DB Defaults to: 0
        /// AllowNull N
        /// </summary>
        public int BillingSpecialistID
        {
            get { return _billSpecialistID; }
            set { _billSpecialistID = value; OnPropertyChanged("BillingSpecialistID"); }
        }
        string ValidateBillingSpecialistID()
        {
            //TODO:
            return null;
        }
        private int _billSpecialistID = 0;

        /// <summary>
        /// LEGACY_CUSTOMER_NBR
        /// type - VARCHAR2 (10 Byte)
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public string LegacyCustomerNumber
        {
            get { return _legacyCustomerNbr; }
            set { _legacyCustomerNbr = value; OnPropertyChanged("LegacyCustomerNumber"); }
        }
        string ValidateLegacyCustomerNumber()
        {
            //TODO:
            return null;
        }
        private string _legacyCustomerNbr;

        /// <summary>
        /// CONTRACT_ID
        /// type - INT
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public decimal? ContractID
        {
            get { return _contractID; }
            set { _contractID = value; OnPropertyChanged("ContractID"); }
        }
        string ValidateContractID()
        {
            //TODO:
            return null;
        }
        private decimal? _contractID = null;

        /// <summary>
        /// INVOICE_STYLE
        /// type - CHAR(4 BYTE)
        /// DB Defaults to: 'DEFM'
        /// AllowNull Y
        /// </summary>
        public string InvoiceStyle
        {
            get { return _invoiceStyle; }
            set { _invoiceStyle = value; OnPropertyChanged("InvoiceStyle"); }
        }
        string ValidateInvoiceStyle()
        {
            //TODO:
            return null;
        }
        private string _invoiceStyle = "SCHC";

        /// <summary>
        /// BRAND
        /// type - CHAR(5 Byte)
        /// DB Defaults to: 'MEDQ'
        /// AllowNull Y
        /// </summary>
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; OnPropertyChanged("Brand"); }
        }

        string ValidateBrand()
        {
            //TODO:
            return null;
        }
        private string _brand = "MEDQ";

        /// <summary>
        /// DEFAULT_BILL_FILE_PATH
        /// This is set from the CONFIG
        /// type - VARCHAR(255)
        /// DB Defaults to: 
        /// AllowNull 
        /// </summary>
        public string DefaultBillFilePath
        {
            get
            {
                return _defaultBillFilePath;
            }
            set
            {
                _defaultBillFilePath = value;
                OnPropertyChanged("DefaultBillFilePath");
            }
        }
        string ValidateDefaultBillFilePath()
        {
            //TODO:
            return null;
        }
        private string _defaultBillFilePath;

        /// <summary>
        /// INVOICE_GRP_STATUS
        /// type - CHAR (4 Btye)
        /// DB Defaults to: 'ACTV'
        /// AllowNull N
        /// </summary>
        public string InvoiceGroupStatus
        {
            get { return _invoiceGrpStatus; }
            set { _invoiceGrpStatus = value; OnPropertyChanged("InvoiceGroupStatus"); }
        }
        string ValidateInvoiceGroupStatus()
        {
            //TODO:
            return null;
        }
        private string _invoiceGrpStatus = "PEND";

        /// <summary>
        /// GP_CUSTOMER_NBR
        /// type -
        /// DB Defaults to: 
        /// AllowNull 
        /// </summary>
        public string GPCustomerNumber
        {
            get { return _gpCustomerNbr; }
            set { _gpCustomerNbr = value; OnPropertyChanged("GPCustomerNumber"); }
        }
        string ValidateGPCustomerNumber()
        {
            //TODO:
            return null;
        }
        private string _gpCustomerNbr = "?";

        /// <summary>
        /// DO_NOT_BILL_BEFORE
        /// type - DATE
        /// DB Defaults to: 
        /// AllowNull 7
        /// </summary>
        public DateTime? DoNotBillBefore
        {
            get 
            { 
                return _doNotBillBefore; 
}
            set 
            { 
                _doNotBillBefore = value; 
                OnPropertyChanged("DoNotBillBefore"); 
            }
        }
        string ValidateDoNotBillBefore()
        {
            //TODO:
            return null;
        }
        private DateTime? _doNotBillBefore = null;

        /// <summary>
        /// ATTN_LINE_HEADING
        /// type - VARCHAR(20)
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public string AttnLineHeading
        {
            get { return _attnLineHeading; }
            set { _attnLineHeading = value; OnPropertyChanged("AttnLineHeading"); }
        }
        string ValidateAttnLineHeading()
        {
            //TODO:
            return null;
        }
        private string _attnLineHeading;

        /// <summary>
        /// ATTN_LINE
        /// type -VARCHAR(44)
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public string AttnLine
        {
            get { return _attnLine; }
            set { _attnLine = value; OnPropertyChanged("AttnLine"); }
        }
        string ValidateAttnLine()
        {
            //TODO:
            return null;
        }
        private string _attnLine;

        /// <summary>
        /// REMIT_TO_ID
        /// type - INT
        /// DB Defaults to: 2
        /// AllowNull N
        /// </summary>
        public int RemitToID
        {
            get { return _remitToID; }
            set { _remitToID = value; OnPropertyChanged("RemitToID"); }
        }
        string ValidateRemitToID()
        {
            //TODO:
            return null;
        }
        private int _remitToID = 9;

        /// <summary>
        /// PRIMARY_PLATFORM
        /// type - VARCHAR(5)
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public string PrimaryPlatform
        {
            get { return _primaryPlatform; }
            set { _primaryPlatform = value; OnPropertyChanged("PrimaryPlatform"); }
        }
        string ValidatePrimaryPlatform()
        {
            //TODO:
            return null;
        }
        private string _primaryPlatform = "ASP";

        /// <summary>
        /// DELIVERY_METHOD
        /// type - CHAR(5)
        /// DB Defaults to: 'PAPER'
        /// AllowNull N
        /// </summary>
        public string DeliveryMethod
        {
            get { return _deliveryMethod; }
            set { _deliveryMethod = value; OnPropertyChanged("DeliveryMethod"); }
        }
        string ValidateDeliveryMethod()
        {
            //TODO:
            return null;
        }
        private string _deliveryMethod = "PAPER";

        /// <summary>
        /// BILL_EMAIL
        /// type - VARCHAR(256)
        /// DB Defaults to: 
        /// AllowNull Y
        /// </summary>
        public string BillEmail
        {
            get { return _billEmail; }
            set { _billEmail = value; OnPropertyChanged("BillEmail"); }
        }
        string ValidateBillEmail()
        {
            //TODO:
            return null;
        }
        private string _billEmail;

        /// <summary>
        /// BW_INVOICE_STYLE
        /// type - CHAR(1)
        /// DB Defaults to: 'N'
        /// AllowNull N
        /// </summary>
        public bool IsBWInvoiceStyle
        {
            get { return _isBWInvoiceStyle; }
            set { _isBWInvoiceStyle = value; OnPropertyChanged("IsBWInvoiceStyle"); }
        }
        string ValidateIsBWInvoiceStyle()
        {
            //TODO:
            return null;
        }
        private bool _isBWInvoiceStyle = false;

        /// <summary>
        /// ENCRYPTION_OPTOUT	
        /// type -CHAR (1 Byte)
        /// DB Defaults to: 'N'
        /// AllowNull N
        /// </summary>
        public bool EncryptionOptout
        {
            get
            {
                return _encryptionOptout;
            }
            set
            {
                _encryptionOptout = value;
                OnPropertyChanged("EncryptionOptout");
            }
        }
        string ValidateEncryptionOptout()
        {
            //TODO:
            return null;
        }
        private bool _encryptionOptout = false;


        #endregion

        #region IDataErrorInfo Members

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

        public override string GetValidationError(string propertyName)
        {
            if(!ValidatedProperties.Contains(propertyName))
                return null;

            string error = null;

            switch (propertyName)
            {

                case "AttnLine":
                    error = ValidateAttnLine();
                    break;
                case "AttnLineHeading":
                    error = ValidateAttnLineHeading();
                    break;
                case "BillEmail":
                    error = ValidateBillEmail();
                    break;
                case "BillingFrequency":
                    error = ValidateBillingFrequency();
                    break;
                case "BillingSpecialistID":
                    error = ValidateBillingSpecialistID();
                    break;
                case "Brand":
                    error = ValidateBrand();
                    break;
                case "DefaultBillFilePath":
                    error = ValidateDefaultBillFilePath();
                    break;
                case "ChangedBy":
                    error = ValidateChangedBy();
                    break;
                case "ChangedOn":
                    error = ValidateChangedOn();
                    break;
                case "ContractID":
                    error = ValidateContractID();
                    break;
                case "DeliveryMethod":
                    error = ValidateDeliveryMethod();
                    break;
                case "Description":
                    error = ValidateDescription();
                    break;
                case "DoNotBillBefore":
                    error = ValidateDoNotBillBefore();
                    break;
                case "GLDistributionNumber":
                    error = ValidateGLDistributionNumber();
                    break;
                case "GPCustomerNumber":
                    error = ValidateGPCustomerNumber();
                    break;
                case "InvoiceGrpId":
                    error = ValidateID();
                    break;
                case "InvoiceGroupStatus":
                    error = ValidateInvoiceGroupStatus();
                    break;
                case "InvoiceStyle":
                    error = ValidateInvoiceStyle();
                    break;
                case "IsBWInvoiceStyle":
                    error = ValidateIsBWInvoiceStyle();
                    break;
                case "LegacyCustomerNumber":
                    error = ValidateLegacyCustomerNumber();
                    break;
                case "PrimaryPlatform":
                    error = ValidatePrimaryPlatform();
                    break;
                case "RemitToID":
                    error = ValidateRemitToID();
                    break;
                case "TimeZone":
                    error = ValidateTimeZone();
                    break;
                case "UseDst":
                    error = ValidateUseDst();
                    break;
                case "EncryptionOptout":
                    error = ValidateEncryptionOptout();
                    break;
            }
            return error;
        }

        #endregion
        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(InvoiceGrpId);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceGroup;
            if (t == null)
                return false;
            if (InvoiceGrpId == t.InvoiceGrpId )
                return true;
            return false;
        }
        #endregion

    }


}