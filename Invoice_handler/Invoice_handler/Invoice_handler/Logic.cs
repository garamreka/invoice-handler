using System;
using System.Collections.Generic;
using System.IO;
using Invoice_handler.Interfaces;
using Invoice_handler.Utilities;

namespace Invoice_handler
{
    /// <summary>
    /// Logic
    /// </summary>
    public sealed class Logic
    {
        #region Fields

        private IXmlHandler _xmlHandler;
        private readonly List<string> _excelHeader1 = new List<string>
        {
            "Customer VAT number", "Invoice number", "Final net price", "Final VAT"
        };
        private readonly List<string> _excelHeader2 = new List<string>
        {
            "VAT number", " Aggregated net price"
        };

        #endregion

        #region Constructor
        
        public Logic(IXmlHandler xmlHandler)
        {
            _xmlHandler = xmlHandler;
        }

        #endregion

        #region Public members
        
        /// <summary>
        /// Creates an excel file based on xml document
        /// </summary>
        public void CreateExcelFromXml()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "InvoicesWithVat100OrMore.xlsx");
            var elements = _xmlHandler.GetInvoicesWithVat100();
            var excelModels = _xmlHandler.CreateExcelModelsFromXml(elements);

            var excel = new Excel();

            try
            {
                excel.CreateNewWorkbook();
                excel.SaveAs(path);
            }
            finally
            {
                excel.Close();
            }

            try
            {
                excel.Open(path);

                for (int i = 0; i < _excelHeader1.Count; i++)
                {
                    excel.WriteToCell(0, i, _excelHeader1[i]);
                }

                for (int i = 0; i < excelModels.Count; i++)
                {
                    excel.WriteToCell(i + 1, 0, excelModels[i].VatNumber);
                    excel.WriteToCell(i + 1, 1, excelModels[i].InvoiceNumber);
                    excel.WriteToCell(i + 1, 2, excelModels[i].FinalNetPrice);
                    excel.WriteToCell(i + 1, 3, excelModels[i].FinalVat);
                }

                excel.Save();
            }
            finally
            {
                excel.Close();
            }
        }

        /// <summary>
        /// Creates excel file based on a key-value pair
        /// </summary>
        public void CreateExcelFromKeyValuePair()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "NetPricesByVatNumber.xlsx");
            var elements = _xmlHandler.GetNetPriceGroupedByVatNumber();

            var excel = new Excel();


            try
            {
                excel.CreateNewWorkbook();
                excel.SaveAs(path);
            }
            finally
            {
                excel.Close();
            }

            try
            {
                excel.Open(path);

                for (int i = 0; i < _excelHeader2.Count; i++)
                {
                    excel.WriteToCell(0, i, _excelHeader2[i]);
                }

                double aggregateNetPrice = 0;
                int count = 1;

                foreach (var item in elements)
                {
                    excel.WriteToCell(count, 0, item.Key);

                    foreach (var priceString in item.Value)
                    {
                        double netPrice;

                        double.TryParse(priceString, out netPrice);

                        if (netPrice < 100000 && netPrice > -100000)
                        {
                            aggregateNetPrice += netPrice;
                        }

                    }

                    excel.WriteToCell(count, 1, aggregateNetPrice.ToString());
                    aggregateNetPrice = 0;
                    count++;
                }

                excel.Save();
            }
            finally
            {
                excel.Close();
            }
        }

        /// <summary>
        /// Creates separate xml documents
        /// </summary>
        public void GenerateSeparateXmls()
        {
            _xmlHandler.GenerateInvoiceXmls();
        }

        #endregion
    }
}
