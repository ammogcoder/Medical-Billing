using System;

namespace Spheris.Common
{
    public class DateRange
    {
        private DateTime _beginsOn;
        private DateTime _endsBefore;

        #region Constructors

        public DateRange()
        { }

        public DateRange(DateTime beginsOn, DateTime endsBefore)
        {
            _beginsOn = beginsOn;
            _endsBefore = endsBefore;
        }

        #endregion

        /// <summary>
        /// The date that the range begins on.
        /// </summary>
        public DateTime BeginsOn
        {
            get { return _beginsOn; }
            set { _beginsOn = value; }
        }

        /// <summary>
        /// The end of the range.  Dates & times prior to this value are included.  The actual value is not included.
        /// </summary>
        public DateTime EndsBefore
        {
            get { return _endsBefore; }
            set { _endsBefore = value; }
        }
    }
}
