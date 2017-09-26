using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Spheris.Billing.Core.Domain
{
    public class InvoiceGroupTable : ObservableCollection<InvoiceGroup>, IDataErrorInfo, INotifyPropertyChanged
    {
        #region Fields
        #endregion
        public InvoiceGroupTable()
        {
        }
        public InvoiceGroupTable(List<InvoiceGroup> list)
            : base(list)
        {

        }
        public InvoiceGroupTable(IEnumerable<InvoiceGroup> collection)
            : base(collection)
        {

        }


        private string _billingSpecialist;
        public string BillingSpecialist
        {
            get
            {
                return _billingSpecialist;
            }
            set
            {
                _billingSpecialist = value;
                OnPropertyChanged("BillingSpecialist");
            }
        }
        static string ValidateBillingSpecialist()
        {
            //TODO
            return null;
        }


        private ObservableCollection<DeliveryMethod> _deliveryMethods;
        public ObservableCollection<DeliveryMethod> DeliveryMethods
        {
            get
            {
                return _deliveryMethods;
            }
            set
            {

                _deliveryMethods = value;
                OnPropertyChanged("DeliveryMethods");
            }
        }

        string ValidateDeliveryMethods()
        {
            //TODO
            return null;
        }


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        string ValidateName()
        {
            //TODO
            return null;
        }


        private string _contract;
        public string Contract
        {
            get
            {
                return _contract;
            }
            set
            {
                _contract = value;
                OnPropertyChanged("Contract");
            }
        }
        string ValidateContract()
        {
            //TODO
            return null;
        }


        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }
        #endregion


        #region Validator
        static readonly string[] ValidatedProperties = 
		        { 
		            "BillingSpecialist",
		            "DeliveryMethod",
		            "Name",
		            "Contract"
		        };

        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {

                case "BillingSpecialist":
                    error = ValidateBillingSpecialist();
                    break;
                case "DeliveryMethod":
                    error = ValidateDeliveryMethods();
                    break;
                case "Name":
                    error = ValidateName();
                    break;
                case "Contract":
                    error = ValidateContract();
                    break;
                default:
                    break;
            }
            return error;
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

    }
}
