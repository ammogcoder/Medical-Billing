using System;
using Spheris.Billing.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillingAdmin.Properties;
using System.Configuration;
using System.ComponentModel.Composition;

namespace BillingAdmin
{
    [Export(typeof(IAccessSettings))]
    [Export]
    public class AccessSettings : IAccessSettings
    {
        [ImportingConstructor]
        public AccessSettings()
        { }

        public object GetValue(string propertyName)
        {
            Settings mysetttings = new Settings();
            SettingsProperty prop = mysetttings.Properties[propertyName];
            return prop.DefaultValue;
        }


        public void setValue(string propertyName,string value)
        {
            Settings mysetttings = new Settings();
            SettingsProperty prop = mysetttings.Properties[propertyName];
            prop.DefaultValue = value;
        }
    }
}
