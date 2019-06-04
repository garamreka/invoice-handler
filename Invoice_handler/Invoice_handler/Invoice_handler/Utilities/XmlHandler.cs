using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Invoice_handler.Interfaces;
using Invoice_handler.Models;

namespace Invoice_handler.Utilities
{
    /// <summary>
    /// XmlHandler
    /// </summary>
    public sealed class XmlHandler : IXmlHandler
    {
        #region Fields
        
        private XElement _xDocument;
        private string _nameSpace = "{http://schemas.nav.gov.hu/2013/szamla}";
        private string[] _appearance = { "PAPER", "ELECTRONIC", "EDI", "UNKNOWN" };
        private Random _random = new Random();
        private const string _szamlaNode = "szamla";
        private const string _afaertekosszNode = "afaertekossz";
        private const string _vevoNode = "vevo";
        private const string _adoszamNode = "adoszam";
        private const string _nettoarosszNode = "nettoarossz";
        private const string _fejlecNode = "fejlec";
        private const string _szlasorszamNode = "szlasorszam";
        private const string _osszesitesNode = "osszesites";
        private const string _vegosszegNode = "vegosszeg";
        private const string _cimNode = "cim";
        private const string _szamlakibocsatoNode = "szamlakibocsato";
        private const string _telepulesNode = "telepules";
        private const string _termek_szolgaltatas_tetelekNode = "termek_szolgaltatas_tetelek";
        private const string _modosito_szlaNode = "modosito_szla";
        private const string _nevNode = "nev";
        private const string _eredeti_sorszamNode = "eredeti_sorszam";
        private const string _szladatumNode = "szladatum";
        private const string _iranyitoszamNode = "iranyitoszam";
        private const string _kozterulet_neveNode = "kozterulet_neve";
        private const string _teljdatumNode = "teljdatum";
        private const string _nem_kotelezoNode = "nem_kotelezo";
        private const string _penznemNode = "penznem";
        private const string _termeknevNode = "termeknev";
        private const string _mennyNode = "menny";
        private const string _mertekegysNode = "mertekegys";
        private const string _nettoegysarNode = "nettoegysar";
        private const string _nettoarNode = "nettoar";
        private const string _adokulcsNode = "adokulcs";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlHandler()
        {
            _xDocument = XElement.Load(Path.Combine(Environment.CurrentDirectory, "source.xml"));
        }

        #endregion

        #region IXmlHandler members

        /// <summary>
        /// Collects all invoices where final VAT is equals ot greater than 100000 HUF
        /// </summary>
        /// <returns>The invoices as XElements</returns>
        public IEnumerable<XElement> GetInvoicesWithVat100()
        {
            IEnumerable<XElement> finalVat100 = _xDocument.Descendants(_nameSpace + _szamlaNode)
                .Where(element =>
                {
                    int vat;
                    string vatValue = element.Descendants(_nameSpace + _afaertekosszNode).First().Value;
                    Int32.TryParse(vatValue, out vat);

                    return vat >= 100000;
                });

            return finalVat100;
        }

        /// <summary>
        /// Collects all invoice's final net price where it is originally smaller than 100000 HUF and groups it by customer's VAT number 
        /// </summary>
        /// <returns>Key-value pairs where key is customer's VAT number and values are final net prices</returns>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetNetPriceGroupedByVatNumber()
        {
            var result = new List<KeyValuePair<string, IEnumerable<string>>>();

            var query = _xDocument.Descendants(_nameSpace + _szamlaNode)
                .GroupBy(invoice => (string) invoice.Element(_nameSpace + _vevoNode).Element(_nameSpace + _adoszamNode))
                .Select(g => new
                {
                    adoszam = g.Key,
                    values = from netPrice in g.AncestorsAndSelf().Descendants(_nameSpace + _nettoarosszNode)
                        select netPrice.Value
                });

            foreach (var element in query)
            {
                var item = new KeyValuePair<string, IEnumerable<string>>(element.adoszam, element.values);
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Creates ExcelModel from Xml
        /// </summary>
        /// <param name="xElements"></param>
        /// <returns>The ExcelModels</returns>
        public List<ExcelModel> CreateExcelModelsFromXml(IEnumerable<XElement> xElements)
        {
            if (xElements.Any() == false)
            {
                throw new ArgumentException("xElements parameter should not be empty");
            }

            var excelModels = new List<ExcelModel>();

            foreach (var xElement in xElements)
            {
                var excelModel = new ExcelModel
                {
                    VatNumber = 
                        xElement.Element(_nameSpace + _vevoNode)?.Element(_nameSpace + _adoszamNode)?.Value,
                    InvoiceNumber = 
                        xElement.Element(_nameSpace + _fejlecNode)?.Element(_nameSpace + _szlasorszamNode)?.Value,
                    FinalNetPrice = 
                        xElement.Element(_nameSpace + _osszesitesNode)?.Element(_nameSpace + _vegosszegNode)?
                            .Element(_nameSpace + _nettoarosszNode)?.Value,
                    FinalVat = 
                        xElement.Element(_nameSpace + _osszesitesNode)?.Element(_nameSpace + _vegosszegNode)?
                            .Element(_nameSpace + _afaertekosszNode)?.Value
                };
                
                excelModels.Add(excelModel);
            }

            return excelModels;
        }

        /// <summary>
        /// Generates xml files from InvoiceModel
        /// </summary>
        public void GenerateInvoiceXmls()
        {
            IEnumerable<XElement> invoices = _xDocument.Descendants(_nameSpace + _szamlaNode);

            int count = 1;

            foreach (var xElement in invoices)
            {
                var invoice = CreateInvoiceModelFromXml(xElement, count);
                Serializer.Serialize(invoice, 
                    Path.Combine(Environment.CurrentDirectory, 
                        $"{invoice.InvoiceExchange.InvoiceHead.InvoiceData.InvoiceNumber}.xml"));
                count++;
            }
        }

        #endregion

        #region Private members
        
        /// <summary>
        /// Creates InvoiceModel from Xml
        /// </summary>
        /// <param name="element"></param>
        /// <param name="count"></param>
        /// <returns>The InvoiceModel</returns>
        private Invoice CreateInvoiceModelFromXml(XElement element, int count)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Element parameter should not be null");
            }

            if (count < 1)
            {
                throw new ArgumentOutOfRangeException("Count parameter is out of range");
            }

            var invoiceData = element.Element(_nameSpace + _fejlecNode);
            var supplier = element.Element(_nameSpace + _szamlakibocsatoNode);
            var customer = element.Element(_nameSpace + _vevoNode);
            var lines = element.Elements(_nameSpace + _termek_szolgaltatas_tetelekNode);
            var invoiceSummary = element.Element(_nameSpace + _osszesitesNode)?.Element(_nameSpace + _vegosszegNode);

            if (invoiceData == null ||
                supplier == null ||
                customer == null ||
                lines.Any() == false ||
                invoiceSummary == null)
            {
                throw new NullReferenceException("Unable to get node");
            }

            string supplierTaxNumber = supplier.Element(_nameSpace + _adoszamNode)?.Value;
            string supplierAddress = supplier.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _telepulesNode)?.Value;
            string customerTaxNumber = customer.Element(_nameSpace + _adoszamNode)?.Value;
            string customerAddress = customer.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _telepulesNode)?.Value;
            

            var modifiedInvoice = element.Element(_nameSpace + _modosito_szlaNode);

            InvoiceReferenceBase invoiceReference;

            if (modifiedInvoice == null)
            {
                invoiceReference = new InvoiceReference()
                {
                    ModifyWithoutMaster = false
                };
            }
            else
            {
                invoiceReference = new InvoiceReferenceMod
                {
                    OriginalInvoiceNumber = modifiedInvoice.Element(_nameSpace + _eredeti_sorszamNode)?.Value,
                    ModificationIssueDate = invoiceData.Element(_nameSpace + _szladatumNode)?.Value,
                    ModificationTimeStamp = GetModificationTimeStamp(invoiceData.Element(_nameSpace + _szladatumNode)?.Value),
                    ModifyWithoutMaster = false
                };
            }

            var invoice = new Invoice
            {
                InvoiceExchange = new InvoiceExchange
                {
                    InvoiceReference = invoiceReference,
                    InvoiceHead = new InvoiceHead
                    {
                        SupplierInfo = new SupplierInfo
                        {
                            SupplierTaxNumber = new SupplierTaxNumber
                            {
                                TaxpayerId = supplierTaxNumber.Split('-')[0],
                                VatCode = supplierTaxNumber.Split('-')[1],
                                CountyCode = supplierTaxNumber.Split('-')[2]
                            },
                            SupplierName = supplier.Element(_nameSpace + _nevNode)?.Value,
                            SupplierAddress = new SupplierAddress
                            {
                                SimpleAddress = new SimpleAddress
                                {
                                    CountryCode = supplierAddress.Split('|')[1],
                                    PostalCode = supplier.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _iranyitoszamNode)?.Value,
                                    City = supplierAddress.Split('|')[0],
                                    AdditionalAddressDetail = supplier.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _kozterulet_neveNode)?.Value
                                }
                            }
                        },
                        CustomerInfo = new CustomerInfo
                        {
                            CustomerTaxNumber = new CustomerTaxNumber
                            {
                                TaxpayerId = customerTaxNumber.Split('-')[0],
                                VatCode = customerTaxNumber.Split('-')[1],
                                CountyCode = customerTaxNumber.Split('-')[2]
                            },
                            CustomerName = customer.Element(_nameSpace + _nevNode)?.Value,
                            CustomerAddress = new CustomerAddress
                            {
                                SimpleAddress = new SimpleAddress
                                {
                                    CountryCode = customerAddress.Split('|')[1],
                                    PostalCode = customer.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _iranyitoszamNode)?.Value,
                                    City = customerAddress.Split('|')[0],
                                    AdditionalAddressDetail = customer.Element(_nameSpace + _cimNode)?.Element(_nameSpace + _kozterulet_neveNode)?.Value
                                }
                            }
                        },
                        InvoiceData = new InvoiceData
                        {
                            InvoiceNumber = invoiceData.Element(_nameSpace + _szlasorszamNode)?.Value,
                            InvoiceCategory = "NORMAL",
                            InvoiceIssueDate = invoiceData.Element(_nameSpace + _szladatumNode)?.Value,
                            InvoiceDeliveryDate = invoiceData.Element(_nameSpace + _teljdatumNode)?.Value,
                            CurrencyCode = element.Element(_nameSpace + _nem_kotelezoNode)?.Element(_nameSpace + _penznemNode)?.Value,
                            InvoiceAppearance = _appearance[_random.Next(0, 3)]
                        }
                    },
                    InvoiceLines = new List<LineBase>(),
                    InvoiceSummary = new InvoiceSummary
                    {
                        SummaryNormal = new SummaryNormal
                        {
                            InvoiceNetAmount = invoiceSummary.Element(_nameSpace + _nettoarosszNode)?.Value,
                            InvoiceVatAmount = invoiceSummary.Element(_nameSpace + _afaertekosszNode)?.Value
                        }
                    }
                }
            };

            foreach (var xElement in lines)
            {
                if (modifiedInvoice == null)
                {
                    invoice.InvoiceExchange.InvoiceLines.Add(new Line
                    {
                        LineNumber = count.ToString(),
                        LineDescription = xElement.Element(_nameSpace + _termeknevNode).Value,
                        Quantity = xElement.Element(_nameSpace + _mennyNode).Value,
                        UnitOfMeasure = xElement.Element(_nameSpace + _mertekegysNode).Value,
                        UnitPrice = xElement.Element(_nameSpace + _nettoegysarNode).Value,
                        LineAmountsNormal = new LineAmountsNormal
                        {
                            LineNetAmount = xElement.Element(_nameSpace + _nettoarNode).Value,
                            LineVatRate = new LineVatRate
                            {
                                VatPercentage = xElement.Element(_nameSpace + _adokulcsNode).Value
                            }
                        }
                    });
                }
                else
                {
                    invoice.InvoiceExchange.InvoiceLines.Add(new LineMod()
                    {
                        LineNumber = count.ToString(),
                        LineModificationReference = new LineModificationReference()
                        {
                            LineOperation = "CREATE"
                        },
                        LineDescription = xElement.Element(_nameSpace + _termeknevNode).Value,
                        Quantity = xElement.Element(_nameSpace + _mennyNode).Value,
                        UnitOfMeasure = xElement.Element(_nameSpace + _mertekegysNode).Value,
                        UnitPrice = xElement.Element(_nameSpace + _nettoegysarNode).Value,
                        LineAmountsNormal = new LineAmountsNormal
                        {
                            LineNetAmount = xElement.Element(_nameSpace + _nettoarNode).Value,
                            LineVatRate = new LineVatRate
                            {
                                VatPercentage = xElement.Element(_nameSpace + _adokulcsNode).Value
                            }
                        }
                    });
                }
                
            }

            return invoice;
        }

        private DateTime GetModificationTimeStamp(string modificationIssueDate)
        {
            DateTime dateTime;

            DateTime.TryParse(modificationIssueDate, out dateTime);
            TimeSpan time = DateTime.Now.TimeOfDay;

            return dateTime.Date + time;
        }

        #endregion
    }
}
