using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// EXT_WORK_TYPE
    /// KEY - EXT_SYS, EXT_CLIENT_KEY, EXT_WORK_TYPE
    /// </summary>
    public class Invoice : ObjectBase<Brand>
    {
        public bool IsAdding { get; set; }

        public Invoice Clone()
        {
            return BaseClone<Invoice>();
        }


        private decimal _INVOICE_ID;
        public decimal INVOICE_ID
        {
            get
            {
                return _INVOICE_ID;
            }
            set
            {
                _INVOICE_ID = value;
                OnPropertyChanged("INVOICE_ID");
            }
        }
        private decimal _INVOICE_GRP_ID;
        public decimal INVOICE_GRP_ID
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
        private string _TARGET_INVOICE_NBR;
        public string TARGET_INVOICE_NBR
        {
            get
            {
                return _TARGET_INVOICE_NBR;
            }
            set
            {
                _TARGET_INVOICE_NBR = value;
                OnPropertyChanged("TARGET_INVOICE_NBR");
            }
        }
        private decimal _BATCH_JOB_ID;
        public decimal BATCH_JOB_ID
        {
            get
            {
                return _BATCH_JOB_ID;
            }
            set
            {
                _BATCH_JOB_ID = value;
                OnPropertyChanged("BATCH_JOB_ID");
            }
        }
        private decimal? _REVERSED_BATCH_JOB_ID;
        public decimal? REVERSED_BATCH_JOB_ID
        {
            get
            {
                return _REVERSED_BATCH_JOB_ID;
            }
            set
            {
                _REVERSED_BATCH_JOB_ID = value;
                OnPropertyChanged("REVERSED_BATCH_JOB_ID");
            }
        }
        private DateTime? _PAYMENT_DUE;
        public DateTime? PAYMENT_DUE
        {
            get
            {
                return _PAYMENT_DUE;
            }
            set
            {
                _PAYMENT_DUE = value;
                OnPropertyChanged("PAYMENT_DUE");
            }
        }
        private DateTime? _PAYMENT_RECVD;
        public DateTime? PAYMENT_RECVD
        {
            get
            {
                return _PAYMENT_RECVD;
            }
            set
            {
                _PAYMENT_RECVD = value;
                OnPropertyChanged("PAYMENT_RECVD");
            }
        }
        private decimal _LINE_ITEM_TOTAL;
        public decimal LINE_ITEM_TOTAL
        {
            get
            {
                return _LINE_ITEM_TOTAL;
            }
            set
            {
                _LINE_ITEM_TOTAL = value;
                OnPropertyChanged("LINE_ITEM_TOTAL");
            }
        }
        private decimal _TAT_REDUCTION;
        public decimal TAT_REDUCTION
        {
            get
            {
                return _TAT_REDUCTION;
            }
            set
            {
                _TAT_REDUCTION = value;
                OnPropertyChanged("TAT_REDUCTION");
            }
        }
        private decimal? _NET_AMT_BILLED;
        public decimal? NET_AMT_BILLED
        {
            get
            {
                return _NET_AMT_BILLED;
            }
            set
            {
                _NET_AMT_BILLED = value;
                OnPropertyChanged("NET_AMT_BILLED");
            }
        }
        private string _INVOICE_STATUS;
        public string INVOICE_STATUS
        {
            get
            {
                return _INVOICE_STATUS;
            }
            set
            {
                _INVOICE_STATUS = value;
                OnPropertyChanged("INVOICE_STATUS");
            }
        }
        private DateTime? _BILL_PERIOD_START;
        public DateTime? BILL_PERIOD_START
        {
            get
            {
                return _BILL_PERIOD_START;
            }
            set
            {
                _BILL_PERIOD_START = value;
                OnPropertyChanged("BILL_PERIOD_START");
            }
        }
        private DateTime? _BILL_PERIOD_END_BEFORE;
        public DateTime? BILL_PERIOD_END_BEFORE
        {
            get
            {
                return _BILL_PERIOD_END_BEFORE;
            }
            set
            {
                _BILL_PERIOD_END_BEFORE = value;
                OnPropertyChanged("BILL_PERIOD_END_BEFORE");
            }
        }
        private DateTime _CREATED_ON;
        public DateTime CREATED_ON
        {
            get
            {
                return _CREATED_ON;
            }
            set
            {
                _CREATED_ON = value;
                OnPropertyChanged("CREATED_ON");
            }
        }
        private DateTime? _EXPORTED_TO_GL_ON;
        public DateTime? EXPORTED_TO_GL_ON
        {
            get
            {
                return _EXPORTED_TO_GL_ON;
            }
            set
            {
                _EXPORTED_TO_GL_ON = value;
                OnPropertyChanged("EXPORTED_TO_GL_ON");
            }
        }
        private decimal _UNITS_BILLED;
        public decimal UNITS_BILLED
        {
            get
            {
                return _UNITS_BILLED;
            }
            set
            {
                _UNITS_BILLED = value;
                OnPropertyChanged("UNITS_BILLED");
            }
        }
        private string _CHAR_COMP_METHOD;
        public string CHAR_COMP_METHOD
        {
            get
            {
                return _CHAR_COMP_METHOD;
            }
            set
            {
                _CHAR_COMP_METHOD = value;
                OnPropertyChanged("CHAR_COMP_METHOD");
            }
        }
        private DateTime? _PAYMENT_TERMS_START;
        public DateTime? PAYMENT_TERMS_START
        {
            get
            {
                return _PAYMENT_TERMS_START;
            }
            set
            {
                _PAYMENT_TERMS_START = value;
                OnPropertyChanged("PAYMENT_TERMS_START");
            }
        }
        private string _ERR_MSG;
        public string ERR_MSG
        {
            get
            {
                return _ERR_MSG;
            }
            set
            {
                _ERR_MSG = value;
                OnPropertyChanged("ERR_MSG");
            }
        }
        private DateTime? _JOB_COMPLETED_ON;
        public DateTime? JOB_COMPLETED_ON
        {
            get
            {
                return _JOB_COMPLETED_ON;
            }
            set
            {
                _JOB_COMPLETED_ON = value;
                OnPropertyChanged("JOB_COMPLETED_ON");
            }
        }
        private DateTime? _RELEASED_TO_PORTAL_ON;
        public DateTime? RELEASED_TO_PORTAL_ON
        {
            get
            {
                return _RELEASED_TO_PORTAL_ON;
            }
            set
            {
                _RELEASED_TO_PORTAL_ON = value;
                OnPropertyChanged("RELEASED_TO_PORTAL_ON");
            }
        }
        private string _DESCR;
        public string  DESCR
        {
            get
            {
                return _DESCR;
            }
            set
            {
                _DESCR = value;
                OnPropertyChanged("DESCR");
            }
        }




        public override string GetValidationError(string propertyName)
        {
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode(INVOICE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Invoice;
            if (t == null)
                return false;
            if (INVOICE_ID == t.INVOICE_ID)
                return true;
            return false;
        }
        #endregion

    }
}
