using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// BatchJobType
    /// KEY BATCH_JOB_TYPE
    /// </summary>
    public class BatchJobType : ObjectBase<BatchJobType>
    {
        /// <summary>
        /// BatchJobType.AllowDBNull = false;
        /// BatchJobType.Unique = true;
        /// BatchJobType.MaxLength = 5;
        /// KEY
        /// </summary>
        private string _BATCH_JOB_TYPE;
        public string BATCH_JOB_TYPE
        {
            get
            {
                return _BATCH_JOB_TYPE;
            }
            set
            {
                _BATCH_JOB_TYPE = value;
                OnPropertyChanged("BATCH_JOB_TYPE");
            }
        }


        /// <summary>
        /// DESCR.MaxLength = 75;
        /// NOT NULL
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
        /// PLATFORM.MaxLength = 5;
        /// NULLABLE
        /// </summary>
        private string _PLATFORM;
        public string PLATFORM
        {
            get
            {
                return _PLATFORM;
            }
            set
            {
                _PLATFORM = value;
                OnPropertyChanged("PLATFORM");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            // TODO - Is edited?
            return null;
        }


        #region Equals

        public override int GetHashCode()
        {
            return 0.CombineHashCode(BATCH_JOB_TYPE);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as BatchJobType;
            if (t == null)
                return false;
            if (BATCH_JOB_TYPE == t.BATCH_JOB_TYPE)
                return true;
            return false;
        }
        #endregion

    }
}
