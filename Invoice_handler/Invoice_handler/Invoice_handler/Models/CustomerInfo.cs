namespace Invoice_handler.Models
{
    /// <summary>
    /// CustomerInfo
    /// </summary>
    public sealed class CustomerInfo
    {
        /// <summary>
        /// CustomerTaxNumber
        /// </summary>
        public CustomerTaxNumber CustomerTaxNumber { get; set; }

        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// CustomerAddress
        /// </summary>
        public CustomerAddress CustomerAddress { get; set; }
    }
}
