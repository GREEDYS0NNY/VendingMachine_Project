namespace VendingMachine
{
    internal interface IDataAccess
    {
        void ImportData(string fileName); // import from csv, xls
        void ExportData(string fileName); // export to csv, xls
    }
}
