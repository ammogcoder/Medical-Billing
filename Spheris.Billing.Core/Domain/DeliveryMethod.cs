using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Spheris.Billing.Core.Domain
{
    /// <summary>
    /// DELIVERY_METHOD
    /// KEY - DELIVERY_METHOD
    /// </summary>
    public class DeliveryMethod : ObjectBase<DeliveryMethod>
    {

        public DeliveryMethod()
        {
        }

        public DeliveryMethod(string method, string descr)
        {
            TheDeliveryMethod = method;
            Descr = descr;
        }

        /// <summary>
        /// DELIVERY_METHOD
        /// KEY
        /// </summary>
        private string deliveryMethod;
        public string TheDeliveryMethod
        {
            get
            {
                return deliveryMethod;
            }
            set
            {
                deliveryMethod = value;
                OnPropertyChanged("TheDeliveryMethod");
            }
        }

        private string _descr;
        public string Descr
        {
            get
            {
                return _descr;
            }
            set
            {
                _descr = value;
                OnPropertyChanged("Descr");
            }
        }


        public override string GetValidationError(string propertyName)
        {
            return null;
        }


        #region Equals
        public override int GetHashCode()
        {
            return 0.CombineHashCode (TheDeliveryMethod);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as DeliveryMethod;
            if (t == null)
                return false;
            if (TheDeliveryMethod == t.TheDeliveryMethod)
                return true;
            return false;
        }
        #endregion

    }
}
