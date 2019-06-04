namespace Invoice_handler.Models
{
    /// <summary>
    /// TaxNumberBase
    /// </summary>
    public abstract class TaxNumberBase
    {
        /// <summary>
        /// TaxpayerId
        /// </summary>
        public string TaxpayerId { get; set; }

        /// <summary>
        /// VatCode
        /// </summary>
        public string VatCode { get; set; }

        /// <summary>
        /// CountyCode
        /// </summary>
        public string CountyCode { get; set; }
    }
}
