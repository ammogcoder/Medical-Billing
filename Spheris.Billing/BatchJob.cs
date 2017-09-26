using Spheris.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if SEE_DOMAIN_SPACE
namespace Spheris.Billing
{
    public class BatchJob
    {
        #region Private Fields

        private int _id;
        private DateTime _submittedOn;
        private DateTime _completedOn;
        private BatchJobStatus _batchStatus;
        private string _submissionType;
        private string _submittedBy;
        private string _errorMessage;
        private string _comments;
        private BatchJobType _batchJobType;
        private DateRange _jobRange;
        private BatchJobResults _results;
        private string _externalSystem;
        private int _locationId;
        
        #endregion

        #region Public Properties

        /// <summary>
        /// The ID number of the batch job.
        /// </summary>
        public int Id { 
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// The date & time the batch job was submitted (started).
        /// </summary>
        public DateTime SubmittedOn 
        {
            get { return _submittedOn; }
            set { _submittedOn = value; }
        }

        /// <summary>
        /// The date & time that the batch job completed.
        /// </summary>
        public DateTime CompletedOn
        {
            get { return _completedOn; }
            set { _completedOn = value; }
        }

        /// <summary>
        /// The status of the batch job.
        /// </summary>
        public BatchJobStatus BatchStatus
        {
            get { return _batchStatus; }
            set { _batchStatus = value; }
        }

        /// <summary>
        /// The submission type.
        /// </summary>
        public string SubmissionType
        {
            get { return _submissionType; }
            set { _submissionType = value; }
        }

        /// <summary>
        /// The user name of the person who submitted the job.
        /// </summary>
        public string SubmittedBy
        {
            get { return _submittedBy; }
            set { _submittedBy = value; }
        }

        /// <summary>
        /// Any error messages that might have been created during while the job was executing.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        /// <summary>
        /// Any comments relating to the job.
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        /// <summary>
        /// The type of job that was executed.
        /// </summary>
        public BatchJobType Type
        {
            get { return _batchJobType; }
            set { _batchJobType = value; }
        }

        /// <summary>
        /// The date range of work unit records effected by the job.
        /// </summary>
        public DateRange JobRange
        {
            get { return _jobRange; }
            set { _jobRange = value; }
        }

        /// <summary>
        /// Result statistics from the job.
        /// </summary>
        public BatchJobResults Results
        {
            get { return _results; }
            set { _results = value; }
        }

        /// <summary>
        /// The external transcription system that this batch job relates to.
        /// </summary>
        public string ExternalSystem
        {
            get { return _externalSystem; }
            set { _externalSystem = value; }
        }

        /// <summary>
        /// The ID of a location that was effected if the job only effected a single location.
        /// </summary>
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }

        #endregion

    }
}
#endif