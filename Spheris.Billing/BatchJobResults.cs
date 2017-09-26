using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing
{
    public class BatchJobResults
    {
        private int _selected;
        private int _inserted;
        private int _updated;
        private int _deleted;
        private int _rejected;

        public int Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public int Inserted
        {
            get { return _inserted; }
            set { _inserted = value; }
        }

        public int Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        public int Rejected
        {
            get { return _rejected; }
            set { _rejected = value; }
        }
    }
}
