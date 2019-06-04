using System;
using Invoice_handler.Interfaces;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Invoice_handler.Utilities
{
    /// <summary>
    /// Excel
    /// </summary>
    public sealed class Excel :IExcel
    {
        #region Fields
        
        _Application _excel = new _Excel.Application();
        private Workbook _workbook;
        private Worksheet _worksheet;

        #endregion

        #region IExcel members

        /// <summary>
        /// Opens the file
        /// </summary>
        /// <param name="path"></param>
        public void Open(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid input parameter");
            }
            _workbook = _excel.Workbooks.Open(path);
            _worksheet = _workbook.Worksheets[1];
        }

        /// <summary>
        /// Writes into a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="text"></param>
        public void WriteToCell(int row, int column, string text)
        {
            if (row < 0 || column < 0 || string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Invalid input parameter");
            }

            row++;
            column++;
            _worksheet.Cells[row, column].Value2 = text;

        }

        /// <summary>
        /// Creates new excel workbook
        /// </summary>
        public void CreateNewWorkbook()
        {
            _workbook = _excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        }

        /// <summary>
        /// Closes the file
        /// </summary>
        public void Close()
        {
            _workbook.Close();
        }

        /// <summary>
        /// Saves the excel file
        /// </summary>
        public void Save()
        {
            _workbook.Save();
        }

        /// <summary>
        /// Saves the excel file into a specific folder in specific name
        /// </summary>
        /// <param name="path"></param>
        public void SaveAs(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid input parameter");
            }
            _workbook.SaveAs(path);
        }

        #endregion
    }
}
