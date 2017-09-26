using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public class BatchJobStatus
    {
        private string _status;
        private string _description;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
