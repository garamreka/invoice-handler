using System;
using System.Xml.Serialization;

namespace Invoice_handler.Models
{
    [XmlInclude(typeof(Line))]
    [XmlInclude(typeof(LineMod))]
    [Serializable]
    public abstract class LineBase
    {
        /// <summary>
        /// LineNumber
        /// </summary>
        public string LineNumber { get; set; }

        /// <summary>
        /// LineDescription
        /// </summary>
        public string LineDescription { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// UnitOfMeasure
        /// </summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// UnitPrice
        /// </summary>
        public string UnitPrice { get; set; }

        /// <summary>
        /// LineAmountsNormal
        /// </summary>
        public LineAmountsNormal LineAmountsNormal { get; set; }
    }
}
