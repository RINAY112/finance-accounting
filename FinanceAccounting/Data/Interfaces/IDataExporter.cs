namespace FinanceAccounting.Data.Interfaces;

public interface IDataExporter
{
    void Export<T>(string filePath, IReadOnlyCollection<T> data);
}