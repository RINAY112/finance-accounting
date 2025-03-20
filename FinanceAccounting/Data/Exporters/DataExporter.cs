using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.UI;

namespace FinanceAccounting.Data.Exporters;

public abstract class DataExporter : IDataExporter
{
    public abstract string Format { get; }
    
    public void Export<T>(string filePath, IReadOnlyCollection<T> data)
    {
        using (var writer = new StreamWriter(filePath))
        {
            Write(writer, data);
        }
    }

    protected abstract void Write<T>(StreamWriter writer, IReadOnlyCollection<T> data);
}