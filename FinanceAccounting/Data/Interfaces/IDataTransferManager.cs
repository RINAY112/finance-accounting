namespace FinanceAccounting.Data.Interfaces;

public interface IDataTransferManager
{
    List<T> Import<T>(string filePath);
    
    void Export<T>(string filePath, IReadOnlyCollection<T> data);
}