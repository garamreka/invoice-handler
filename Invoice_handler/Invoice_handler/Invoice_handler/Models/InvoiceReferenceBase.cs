using System;
using System.Xml.Serialization;

namespace Invoice_handler.Models
{
    /// <summary>
    /// InvoiceReferenceBase
    /// </summary>
    [XmlInclude(typeof(InvoiceReference))]
    [XmlInclude(typeof(InvoiceReferenceMod))]
    [Serializable]
    public abstract class InvoiceReferenceBase
    {
        /// <summary>
        /// ModifyWithoutMaster
        /// </summary>
        public bool ModifyWithoutMaster { get; set; }
    }
}
