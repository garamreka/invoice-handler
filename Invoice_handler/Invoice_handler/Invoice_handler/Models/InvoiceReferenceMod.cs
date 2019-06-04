using System;

namespace Invoice_handler.Models
{
    /// <summary>
    /// InvoiceReferenceMod
    /// </summary>
    [Serializable]
    public sealed class InvoiceReferenceMod : InvoiceReferenceBase
    {
        /// <summary>
        /// OriginalInvoiceNumber
        /// </summary>
        public string OriginalInvoiceNumber { get; set; }

        /// <summary>
        /// ModificationIssueDate
        /// </summary>
        public string ModificationIssueDate { get; set; }

        /// <summary>
        /// ModificationTimeStamp
        /// </summary>
        public DateTime ModificationTimeStamp { get; set; }

        
    }
}
