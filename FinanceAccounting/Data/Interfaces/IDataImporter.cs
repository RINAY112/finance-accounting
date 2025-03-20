namespace FinanceAccounting.Data.Interfaces;

public interface IDataImporter
{
    List<T> Import<T>(string filePath);
}