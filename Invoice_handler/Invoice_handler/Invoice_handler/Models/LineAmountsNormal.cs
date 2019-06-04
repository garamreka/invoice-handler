namespace Invoice_handler.Models
{
    /// <summary>
    /// LineAmountsNormal
    /// </summary>
    public sealed class LineAmountsNormal
    {
        /// <summary>
        /// LineNetAmount
        /// </summary>
        public string LineNetAmount { get; set; }

        /// <summary>
        /// LineVatRate
        /// </summary>
        public LineVatRate LineVatRate { get; set; }
    }
}
