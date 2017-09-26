using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    public class InvoiceStyleColumn:INotifyPropertyChanged
    {
        #region Fields

        private string _invoiceStyle;
        private int _id;
        private int _index;
        private string _name;
        private string _sqlColumn;
        private int _groupingIndex;
        private int _sortIndex;
        private string _width;

        #endregion

        #region Properties

        public string InvoiceStyle
        {
            get { return _invoiceStyle; }
            set { _invoiceStyle = value; OnPropertyChanged("InvoiceStyle");}
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("InvoiceGrpId"); }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged("Index"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string SqlColumn
        {
            get { return _sqlColumn; }
            set { _sqlColumn = value; OnPropertyChanged("SqlColumn"); }
        }

        public int GroupingIndex
        {
            get { return _groupingIndex; }
            set { _groupingIndex = value; OnPropertyChanged("GroupingIndex"); }
        }

        public int SortIndex
        {
            get { return _sortIndex; }
            set { _sortIndex = value; OnPropertyChanged("SortIndex"); }
        }

        public string Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
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
