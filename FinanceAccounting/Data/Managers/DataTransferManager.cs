using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.UI;

namespace FinanceAccounting.Data.Managers;

public class DataTransferManager : IDataTransferManager
{
    private readonly Dictionary<string, IDataImporter> _importers = new();
    private readonly Dictionary<string, IDataExporter> _exporters = new();
    
    public void RegisterImporter(IDataImporter importer) => _importers[importer.Format] = importer;
    
    public void RegisterExporter(IDataExporter exporter) => _exporters[exporter.Format] = exporter;

    public List<T> Import<T>(string filePath)
    {
        string extension = Path.GetExtension(filePath);
        if (_importers.TryGetValue(extension, out var importer))
            return importer.Import<T>(filePath);
        else
            throw new NotSupportedException($"Format {extension} is not supported for import");
    }

    public void Export<T>(string filePath, IReadOnlyCollection<T> data)
    {
        string extension = Path.GetExtension(filePath);
        if (_exporters.TryGetValue(extension, out var exporter))
            exporter.Export<T>(filePath, data);
        else
            throw new NotSupportedException($"Format {extension} is not supported for export");
    }
}