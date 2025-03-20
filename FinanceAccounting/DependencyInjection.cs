using FinanceAccounting.Data.Exporters;
using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Data.Importers;
using FinanceAccounting.Data.Managers;
using FinanceAccounting.Facades;
using FinanceAccounting.Factories;
using FinanceAccounting.Factories.Interfaces;
using FinanceAccounting.Services.Interfaces;
using FinanceAccounting.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceAccounting;

public static class DependencyInjection
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<IDataImporter, CsvImporter>();
        services.AddSingleton<IDataImporter, JsonImporter>();
        services.AddSingleton<IDataImporter, YamlImporter>();
        services.AddSingleton<IDataExporter, CsvExporter>();
        services.AddSingleton<IDataExporter, JsonExporter>();
        services.AddSingleton<IDataExporter, YamlExporter>();

        services.AddSingleton<IDataTransferManager>(provider =>
        {
            var manager = new DataTransferManager();

            var importers = provider.GetServices<IDataImporter>();
            foreach (var importer in importers)
            {
                manager.RegisterImporter(importer);
            }

            var exporters = provider.GetServices<IDataExporter>();
            foreach (var exporter in exporters)
            {
                manager.RegisterExporter(exporter);
            }
            return manager;
        });
        
        services.AddScoped<IBankAccountFactory, BankAccountFactory>();
        services.AddScoped<ICategoryFactory, CategoryFactory>();
        services.AddScoped<IOperationFactory, OperationFactory>();

        services.AddScoped<IBankAccountService, BankAccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOperationService, OperationService>();

        services.AddScoped<BankAccountFacade>();
        services.AddScoped<CategoryFacade>();
        services.AddScoped<OperationFacade>();
        services.AddScoped<AnalyticsFacade>();
        
        return services.BuildServiceProvider();
    }
}