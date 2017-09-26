using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data
{

    public interface IAccessSettings
    {
        object GetValue(string propertyName);
        void setValue(string propertyName, string value);

    }
}
