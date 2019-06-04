using System;

namespace Invoice_handler.Models
{
    /// <summary>
    /// LineMod
    /// </summary>
    [Serializable]
    public sealed class LineMod: LineBase
    {
        /// <summary>
        /// LineModificationReference
        /// </summary>
        public LineModificationReference LineModificationReference { get; set; }
    }
}
