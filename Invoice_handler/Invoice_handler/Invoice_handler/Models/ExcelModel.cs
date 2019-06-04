namespace Invoice_handler.Models
{
    /// <summary>
    /// ExcelModel
    /// </summary>
    public sealed class ExcelModel
    {
        /// <summary>
        /// VatNumber
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// InvoiceNumber
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// FinalNetPrice
        /// </summary>
        public string FinalNetPrice { get; set; }

        /// <summary>
        /// FinalVat
        /// </summary>
        public string FinalVat { get; set; }
    }
}
