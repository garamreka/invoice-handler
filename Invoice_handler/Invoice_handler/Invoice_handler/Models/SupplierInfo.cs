namespace Invoice_handler.Models
{
    /// <summary>
    /// SupplierInfo
    /// </summary>
    public sealed class SupplierInfo
    {
        /// <summary>
        /// SupplierTaxNumber
        /// </summary>
        public SupplierTaxNumber SupplierTaxNumber { get; set; }

        /// <summary>
        /// SupplierName
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// SupplierAddress
        /// </summary>
        public SupplierAddress SupplierAddress { get; set; }
    }
}
