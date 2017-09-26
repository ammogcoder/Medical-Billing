using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// REPORT_TYPE
    /// KEY - DESCR,REPORT_TYPE_ID
    /// </summary>
    public class ReportType : ObjectBase<ReportType>
    {

        public ReportType()
        { }

        public ReportType(ReportType other, bool empty)
        {
            this.ALLOW_DYNAMIC_MODS = other.ALLOW_DYNAMIC_MODS;
            this.DATA_DUMP_ONLY = other.DATA_DUMP_ONLY;
            this.DESCR = other.DESCR;
            this.DisplayName = other.DisplayName;
            if (empty == false)
                this.IsSelected = other.IsSelected;
            this.MUST_ENCRYPT = other.MUST_ENCRYPT;
            this.REPORT_TYPE_ID = other.REPORT_TYPE_ID;
            this.SHORT_NAME = other.SHORT_NAME;
            this.SQL = other.SQL;
            this.Title = other.Title;
        }

        /// <summary>
        /// DESCR VARCHAR 75 
        /// Allownull = false
        /// </summary>
        private string _DESCR;
        public string DESCR
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


        /// <summary>
        /// REPORT_TYPE_ID int
        /// Allownull = false
        /// </summary>
        private int _REPORT_TYPE_ID;
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


        /// <summary>
        /// SHORT_NAME VARCHAR 9
        /// Allownull = true
        /// </summary>
        private string _SHORT_NAME;
        public string SHORT_NAME
        {
            get
            {
                return _SHORT_NAME;
            }
            set
            {
                _SHORT_NAME = value;
                OnPropertyChanged("SHORT_NAME");
            }
        }


        /// <summary>
        /// SQL VARCHAR 4000
        /// Allownull = true
        /// </summary>
        private string _SQL;
        public string SQL
        {
            get
            {
                return _SQL;
            }
            set
            {
                _SQL = value;
                OnPropertyChanged("SQL");
            }
        }


        /// <summary>
        /// ALLOW_DYNAMIC_MODS bool
        /// Allownull = true
        /// </summary>
        private bool _ALLOW_DYNAMIC_MODS;
        public bool ALLOW_DYNAMIC_MODS
        {
            get
            {
                return _ALLOW_DYNAMIC_MODS;
            }
            set
            {
                _ALLOW_DYNAMIC_MODS = value;
                OnPropertyChanged("ALLOW_DYNAMIC_MODS");
            }
        }


        /// <summary>
        /// DATA_DUMP_ONLY bool
        /// Allownull = true
        /// </summary>
        private bool _DATA_DUMP_ONLY;
        public bool DATA_DUMP_ONLY
        {
            get
            {
                return _DATA_DUMP_ONLY;
            }
            set
            {
                _DATA_DUMP_ONLY = value;
                OnPropertyChanged("DATA_DUMP_ONLY");
            }
        }


        /// <summary>
        /// MUST_ENCRYPT bool
        /// Allownull = false
        /// </summary>
        private bool _MUST_ENCRYPT;
        public bool MUST_ENCRYPT
        {
            get
            {
                return _MUST_ENCRYPT;
            }
            set
            {
                _MUST_ENCRYPT = value;
                OnPropertyChanged("MUST_ENCRYPT");
            }
        }


        /// <summary>
        /// Used by the ComboWithCheckBoxes for selection only
        /// </summary>
        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }


        /// <summary>
        /// Used by the ComboWithCheckBoxes for selection only
        /// </summary>
        public string title;
        public string Title
        {
            get
            {
                return String.Format("{0} <{1}>", DESCR, SHORT_NAME);
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///   
        /// </summary>
        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {

            return 0.CombineHashCode(REPORT_TYPE_ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as ReportType;
            if (t == null)
                return false;
            if (REPORT_TYPE_ID == t.REPORT_TYPE_ID)
                return true;
            return false;
        }
        #endregion

    }

}
