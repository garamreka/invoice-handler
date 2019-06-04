using System.Collections.Generic;
using System.Xml.Linq;
using Invoice_handler.Models;

namespace Invoice_handler.Interfaces
{
    /// <summary>
    /// IXmlHandler interface
    /// </summary>
    public interface IXmlHandler
    {
        /// <summary>
        /// Collects all invoices where final VAT is equals ot greater than 100000 HUF
        /// </summary>
        /// <returns>The invoices as XElements</returns>
        IEnumerable<XElement> GetInvoicesWithVat100();

        /// <summary>
        /// Collects all invoice's final net price where it is originally smaller than 100000 HUF and groups it by customer's VAT number 
        /// </summary>
        /// <returns>Key-value pairs where key is customer's VAT number and values are final net prices</returns>
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetNetPriceGroupedByVatNumber();
        
        /// <summary>
        /// Creates ExcelModel from Xml
        /// </summary>
        /// <param name="xElements"></param>
        /// <returns>The ExcelModels</returns>
        List<ExcelModel> CreateExcelModelsFromXml(IEnumerable<XElement> xElements);
        
        /// <summary>
        /// Generates xml files from InvoiceModel
        /// </summary>
        void GenerateInvoiceXmls();
    }
}
