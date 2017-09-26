using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Core.Domain
{
    public class BatchJob : ObjectBase<BatchJob>
    {
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
        private decimal _BATCH_JOB_ID;

        public DateTime SUBMITTED_ON
        {
            get
            {
                return _SUBMITTED_ON;
            }
            set
            {
                _SUBMITTED_ON = value;
                OnPropertyChanged("SUBMITTED_ON");
            }
        }
        private DateTime _SUBMITTED_ON;

        public DateTime? COMPLETED_ON
        {
            get
            {
                return _COMPLETED_ON;
            }
            set
            {
                _COMPLETED_ON = value;
                OnPropertyChanged("COMPLETED_ON");
            }
        }
        private DateTime? _COMPLETED_ON;

        /// <summary>
        /// // Use BatchJobStatus struct constants
        /// </summary>
        public string BATCH_STATUS
        {
            get
            {
                return _BATCH_STATUS;
            }
            set
            {
                _BATCH_STATUS = value;
                OnPropertyChanged("BATCH_STATUS");
            }
        }
        private string _BATCH_STATUS;

        public string SUBMISSION_TYPE
        {
            get
            {
                return _SUBMISSION_TYPE;
            }
            set
            {
                _SUBMISSION_TYPE = value;
                OnPropertyChanged("SUBMISSION_TYPE");
            }
        }
        private string _SUBMISSION_TYPE;

        public string SUBMITTED_BY
        {
            get
            {
                return _SUBMITTED_BY;
            }
            set
            {
                _SUBMITTED_BY = value;
                OnPropertyChanged("SUBMITTED_BY");
            }
        }
        private string _SUBMITTED_BY;

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
        private string _ERR_MSG;

        public string COMMENTS
        {
            get
            {
                return _COMMENTS;
            }
            set
            {
                _COMMENTS = value;
                OnPropertyChanged("COMMENTS");
            }
        }
        private string _COMMENTS;

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
        private string _BATCH_JOB_TYPE;

        /// <summary>
        /// From time
        /// </summary>
        public DateTime? TIMESPAN_GE
        {
            get
            {
                return _TIMESPAN_GE;
            }
            set
            {
                _TIMESPAN_GE = value;
                OnPropertyChanged("TIMESPAN_GE");
            }
        }
        private DateTime? _TIMESPAN_GE;

        public DateTime? TIMESPAN_LT
        {
            get
            {
                return _TIMESPAN_LT;
            }
            set
            {
                _TIMESPAN_LT = value;
                OnPropertyChanged("TIMESPAN_LT");
            }
        }
        private DateTime? _TIMESPAN_LT;

        public decimal? NBR_SEL
        {
            get
            {
                return _NBR_SEL;
            }
            set
            {
                _NBR_SEL = value;
                OnPropertyChanged("NBR_SEL");
            }
        }
        private decimal? _NBR_SEL;

        public decimal? NBR_INS
        {
            get
            {
                return _NBR_INS;
            }
            set
            {
                _NBR_INS = value;
                OnPropertyChanged("NBR_INS");
            }
        }
        private decimal? _NBR_INS;

        public decimal? NBR_UPD
        {
            get
            {
                return _NBR_UPD;
            }
            set
            {
                _NBR_UPD = value;
                OnPropertyChanged("NBR_UPD");
            }
        }
        private decimal? _NBR_UPD;

        public decimal? NBR_DEL
        {
            get
            {
                return _NBR_DEL;
            }
            set
            {
                _NBR_DEL = value;
                OnPropertyChanged("NBR_DEL");
            }
        }
        private decimal? _NBR_DEL;

        public decimal? NBR_REJECT
        {
            get
            {
                return _NBR_REJECT;
            }
            set
            {
                _NBR_REJECT = value;
                OnPropertyChanged("NBR_REJECT");
            }
        }
        private decimal? _NBR_REJECT;

        public string EXT_SYS
        {
            get
            {
                return _EXT_SYS;
            }
            set
            {
                _EXT_SYS = value;
                OnPropertyChanged("EXT_SYS");
            }
        }
        private string _EXT_SYS;

        /// <summary>
        /// Not part of the database - used in the Grid only
        /// Populated from the BatchJobType's Descr table to 
        /// BatchJobType's Descr - tied to BATCH_JOB_TYPE
        /// </summary>
        public string BatchJobTypeDescription
        {
            get
            {
                return _BatchJobTypeDescription;
            }
            set
            {
                _BatchJobTypeDescription = value;
                OnPropertyChanged("BatchJobTypeDescription");
            }
        }
        private string _BatchJobTypeDescription;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as BatchJob;
            if (t == null)
                return false;
            if (BATCH_JOB_ID == t.BATCH_JOB_ID)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return 0.CombineHashCode(BATCH_JOB_ID);
        }

        public override string GetValidationError(string propertyName)
        {
            //TODO
            return null;
        }
    }

    public struct BatchJobStatus
    {
        public const string Completed = "COMP";
        public const string FailedNoWork = "FERR";
        public const string InProcess = "INPR";
        public const string FailedPartial = "PART";
    }
}
