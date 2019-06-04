namespace Invoice_handler.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SummaryNormal
    {
        /// <summary>
        /// InvoiceNetAmount
        /// </summary>
        public string InvoiceNetAmount { get; set; }

        /// <summary>
        /// InvoiceVatAmount
        /// </summary>
        public string InvoiceVatAmount { get; set; }

        /// <summary>
        /// InvoiceVatAmountHUF
        /// </summary>
        public string InvoiceVatAmountHUF => InvoiceVatAmount;
    }
}
