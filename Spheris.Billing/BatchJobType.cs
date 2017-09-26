using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if SEE_DOMAIN_SPACE
namespace Spheris.Billing
{
    public class BatchJobType
    {
        private string _type;
        private string _description;
        private string _platform;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Description 
        { 
            get { return _description; }
            set { _description = value; }
        }

        public string Platform
        {
            get { return _platform; }
            set { _platform = value; }
        }
    }
}
#endif