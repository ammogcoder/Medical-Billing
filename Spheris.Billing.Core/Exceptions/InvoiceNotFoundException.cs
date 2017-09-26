using System;
using System.Runtime.Serialization;
using System.Text;

namespace Spheris.Billing.Core.Exceptions
{
    /// <summary>
    /// Exception that is thrown when an operation is performed on an invoice before it has been created.
    /// </summary>
    [Serializable]
    public class InvoiceNotFoundException : Exception
    {
        private const string MESSAGE = @"Invoice #{0} does not exist.";

        public InvoiceNotFoundException(int invoiceNumber, Exception ex)
            : base(String.Format(MESSAGE, invoiceNumber), ex)
        {
        }

        public InvoiceNotFoundException(int invoiceNumber)
            : base(string.Format(MESSAGE, invoiceNumber))
        {
        }

        public InvoiceNotFoundException(int invoiceNumber, string message, Exception ex)
            : base(String.Format(MESSAGE, invoiceNumber) + " " + message, ex)
        {
        }

        protected InvoiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
