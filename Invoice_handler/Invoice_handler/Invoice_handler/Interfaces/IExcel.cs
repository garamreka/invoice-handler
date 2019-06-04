namespace Invoice_handler.Interfaces
{
    /// <summary>
    /// IExcel interface
    /// </summary>
    public interface IExcel
    {
        /// <summary>
        /// Creates new excel workbook
        /// </summary>
        void CreateNewWorkbook();

        /// <summary>
        /// Writes into a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="text"></param>
        void WriteToCell(int row, int column, string text);

        /// <summary>
        /// Saves the excel file
        /// </summary>
        void Save();

        /// <summary>
        /// Saves the excel file into a specific folder in specific name
        /// </summary>
        /// <param name="path"></param>
        void SaveAs(string path);

        /// <summary>
        /// Opens the file
        /// </summary>
        /// <param name="path"></param>
        void Open(string path);

        /// <summary>
        /// Closes the file
        /// </summary>
        void Close();
    }
}
