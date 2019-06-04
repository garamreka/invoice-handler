namespace Invoice_handler.Models
{
    /// <summary>
    /// InvoiceHead
    /// </summary>
    public sealed class InvoiceHead
    {
        /// <summary>
        /// SupplierInfo
        /// </summary>
        public SupplierInfo SupplierInfo { get; set; }

        /// <summary>
        /// CustomerInfo
        /// </summary>
        public CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        /// InvoiceData
        /// </summary>
        public InvoiceData InvoiceData { get; set; }
    }
}
