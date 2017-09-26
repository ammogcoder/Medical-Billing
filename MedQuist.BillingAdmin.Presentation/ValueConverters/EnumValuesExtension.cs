using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace MedQuist.BillingAdmin.Presentation.ValueConverters
{
    /// <summary>
    /// <ComboBox ItemsSource="{Binding Source={c:EnumValues {x:Type src:DevicePointFormat}}} />
    /// </summary>
    public class EnumValuesExtension : MarkupExtension
    {
        private readonly Type _enumType;
        public EnumValuesExtension(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            if (!enumType.IsEnum)
                throw new ArgumentException("Argument enumType must derive from type Enum.");
            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            if (_enumType == null)
                throw new ArgumentNullException("enum NOT set");
            return Enum.GetValues(_enumType);
        }
    }
}

