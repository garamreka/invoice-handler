namespace Invoice_handler.Models
{
    /// <summary>
    /// SimpleAddress
    /// </summary>
    public sealed class SimpleAddress
    {
        /// <summary>
        /// CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// PostalCode
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// AdditionalAddressDetail
        /// </summary>
        public string AdditionalAddressDetail { get; set; }
    }
}
