using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.UI;

namespace FinanceAccounting.Data.Importers;

public abstract class DataImporter : IDataImporter
{
    public abstract string Format { get; }
    
    public List<T> Import<T>(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            return Read<T>(reader);
        }
    }

    protected abstract List<T> Read<T>(StreamReader reader);
}