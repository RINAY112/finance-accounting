namespace FinanceAccounting.Data.Interfaces;

public interface IDataExporter
{
    string Format { get; }

    void Export<T>(string filePath, IReadOnlyCollection<T> data);
}