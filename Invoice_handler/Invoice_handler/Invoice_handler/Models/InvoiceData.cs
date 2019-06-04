namespace Invoice_handler.Models
{
    /// <summary>
    /// InvoiceData
    /// </summary>
    public sealed class InvoiceData
    {
        /// <summary>
        /// InvoiceNumber
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// InvoiceCategory
        /// </summary>
        public string InvoiceCategory { get; set; }

        /// <summary>
        /// InvoiceIssueDate
        /// </summary>
        public string InvoiceIssueDate { get; set; }

        /// <summary>
        /// InvoiceDeliveryDate
        /// </summary>
        public string InvoiceDeliveryDate { get; set; }

        /// <summary>
        /// CurrencyCode
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// InvoiceAppearance
        /// </summary>
        public string InvoiceAppearance { get; set; }
    }
}
