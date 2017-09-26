using System;
using System.Runtime.Serialization;
using System.Text;

namespace Spheris.Billing.Core.Exceptions
{
    [Serializable]
    public class MissingContractException:Exception
    {
        private int _invoiceGroupId;
        private DateTime _contractDate;

        public int InvoiceGroupId
        {
            get { return _invoiceGroupId; }
        }

        public DateTime ContractDate
        {
            get { return _contractDate; }
        }

        public MissingContractException(int invoiceGroupId, DateTime contractDate)
            : base(String.Format("Invoice group #{0} did not have a contract or TAT schedule established on {1}.", invoiceGroupId, contractDate.ToString("MM/dd/yyyy")))
        {
            _invoiceGroupId = invoiceGroupId;
            _contractDate = contractDate;
        }

        public MissingContractException(int invoiceGroupId, DateTime contractDate, System.Exception innerException)
            : base(String.Format("Invoice group #{0} did not have a contract or TAT schedule established on {1}.", invoiceGroupId, contractDate.ToString("MM/dd/yyyy"))
                   , innerException)
        {
            _invoiceGroupId = invoiceGroupId;
            _contractDate = contractDate;
        }

        protected MissingContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
		{
			if (info != null)
			{
				_invoiceGroupId = int.Parse(info.GetString("InvoiceGroupId"));
                _contractDate = DateTime.Parse(info.GetString("ContractDate"));
			}
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("InvoiceGroupId", _invoiceGroupId);
                info.AddValue("ContractDate", _contractDate);
            }
        }
    }
}
