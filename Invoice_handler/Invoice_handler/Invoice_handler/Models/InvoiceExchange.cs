using System.Collections.Generic;

namespace Invoice_handler.Models
{
    /// <summary>
    /// InvoiceExchange
    /// </summary>
    public sealed class InvoiceExchange
    {
        /// <summary>
        /// InvoiceReference
        /// </summary>
        public InvoiceReferenceBase InvoiceReference { get; set; }

        /// <summary>
        /// InvoiceHead
        /// </summary>
        public InvoiceHead InvoiceHead { get; set; }

        /// <summary>
        /// InvoiceLines
        /// </summary>
        public List<LineBase> InvoiceLines { get; set; }

        /// <summary>
        /// InvoiceSummary
        /// </summary>
        public InvoiceSummary InvoiceSummary { get; set; }
    }
}
