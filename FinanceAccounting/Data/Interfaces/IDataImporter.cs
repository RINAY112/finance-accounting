namespace FinanceAccounting.Data.Interfaces;

public interface IDataImporter
{
    string Format { get; }
    
    List<T> Import<T>(string filePath);
}