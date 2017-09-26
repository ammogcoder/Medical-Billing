using Spheris.Billing.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Billing.Data.RepositoryBases
{
    /// <summary>
    /// Structure used to hold the properties of an add-on charge to be included in an invoice.
    /// </summary>
    public class InvoiceReportAddOnCharge
    {
        private string _comments;

        /// <summary>
        /// The Id of the add-on charge type.
        /// </summary>
        public string AddOnChargeTypeId { get; set; }

        /// <summary>
        /// Comments about the add-on charge.  If the quantity is greater than one, then the quantity and amount each
        /// will be prefixed to the comment.
        /// </summary>
        public string Comments
        {
            get
            {
                if (Quantity > 1)
                {
                    return String.Format(@"[{0} @ {1}] {2}", Quantity, AmountEach, _comments);
                }
                else
                {
                    return _comments;
                }
            }
            set { _comments = value; }
        }

        /// <summary>
        /// The quantity of the add-on charge to be charged.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// The description of the add-on charge.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The cost amount for each unit of the add-on.
        /// </summary>
        public double AmountEach { get; set; }

        /// <summary>
        /// The total cost of the add-on as defined by [quantity]*[amount each].  Read-only. 
        /// </summary>
        public double Total
        {
            get { return AmountEach * Quantity; }
        }
    }

    public abstract class InvoiceReportAddOnChargesQuery:RepositoryBase<InvoiceReportAddOnCharge>, IInvoiceReportAddOnChargesQuery
    {
        public abstract List<InvoiceReportAddOnCharge> Get(int invoiceID);
    }
}
