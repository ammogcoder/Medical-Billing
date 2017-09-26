using System;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// INVOICE_GRP_REPORT
    /// KEY - INVOICE_GRP_ID,REPORT_TYPE_ID
    /// </summary>
    public class InvoiceGrpReport : ObjectBase<InvoiceGrpReport>
    {
        public InvoiceGrpReport()
        { }
        public InvoiceGrpReport(InvoiceGrpReport copyMe)
        {
            this.INVOICE_GRP_ID = copyMe.INVOICE_GRP_ID;
            this.FILE_TYPE = copyMe.FILE_TYPE;
            this.REPORT_TYPE_ID = copyMe.REPORT_TYPE_ID;
            this.TAB_ORDER = copyMe.TAB_ORDER;
        }

        private int _INVOICE_GRP_ID = 0;
        public int INVOICE_GRP_ID
        {
            get
            {
                return _INVOICE_GRP_ID;
            }
            set
            {
                _INVOICE_GRP_ID = value;
                OnPropertyChanged("INVOICE_GRP_ID");
            }
        }


        private int _REPORT_TYPE_ID = 0;
        public int REPORT_TYPE_ID
        {
            get
            {
                return _REPORT_TYPE_ID;
            }
            set
            {
                _REPORT_TYPE_ID = value;
                OnPropertyChanged("REPORT_TYPE_ID");
            }
        }


        private int _TAB_ORDER = 2;
        public int TAB_ORDER
        {
            get
            {
                return _TAB_ORDER;
            }
            set
            {
                _TAB_ORDER = value;
                OnPropertyChanged("TAB_ORDER");
            }
        }


        private string _FILE_TYPE = "XLS3";
        public string FILE_TYPE
        {
            get
            {
                return _FILE_TYPE;
            }
            set
            {
                _FILE_TYPE = value;
                OnPropertyChanged("FILE_TYPE");
            }
        }

        /// <summary>
        /// Used by the ComboWithCheckBoxes for selection only
        /// </summary>
        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }

        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_GRP_ID)
                .CombineHashCode(REPORT_TYPE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as InvoiceGrpReport;
            if (t == null)
                return false;
            if (INVOICE_GRP_ID == t.INVOICE_GRP_ID && REPORT_TYPE_ID == t.REPORT_TYPE_ID)
                return true;
            return false;
        }
        #endregion

    }
}
