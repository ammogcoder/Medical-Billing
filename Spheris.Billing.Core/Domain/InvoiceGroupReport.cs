using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    public class InvoiceGroupReport:INotifyPropertyChanged
    {
        #region Fields

        private int _invoiceGrpID;
        private int _reportTypeID;
        private int _tabOrder;

        #endregion

        #region Properties

        public int InvoiceGroupID
        {
            get { return _invoiceGrpID; }
            set { _invoiceGrpID = value; OnPropertyChanged("InvoiceGroupID"); }
        }

        public int ReportTypeID
        {
            get { return _reportTypeID; }
            set { _reportTypeID = value; OnPropertyChanged("ReportTypeID"); }
        }

        public int TabOrder
        {
            get { return _tabOrder; }
            set { _tabOrder = value; OnPropertyChanged("TabOrder"); }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
