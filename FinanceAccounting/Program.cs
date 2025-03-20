using FinanceAccounting;
using FinanceAccounting.Models;
using FinanceAccounting.Data.Interfaces;
using FinanceAccounting.Data.Exporters;
using FinanceAccounting.DTO;
using FinanceAccounting.Data.Importers;
using FinanceAccounting.Facades;
using FinanceAccounting.UI;
using FinanceAccounting.UI.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceAccounting;

public static class Program
{
    public static void Main()
    {
        var provider = DependencyInjection.ConfigureServices();
        using (var scope = provider.CreateScope())
        {
            MainMenuOption? mainMenuOption;
            while ((mainMenuOption = InputManager.GetMainMenuOption()) is not null)
            {
                if (mainMenuOption == MainMenuOption.BankAccountSection)
                    BankAccountActionHandler(scope.ServiceProvider);
                else if (mainMenuOption == MainMenuOption.CategorySection)
                    CategoryActionHandler(scope.ServiceProvider);
                else if (mainMenuOption == MainMenuOption.OperationSection)
                    OperationActionHandler(scope.ServiceProvider);
                else if (mainMenuOption == MainMenuOption.FinanceSection)
                    AnalyticsActionHandler(scope.ServiceProvider);
            }
        }
    }

    public static void BankAccountActionHandler(IServiceProvider provider)
    {
        var bankAccountFacade = provider.GetRequiredService<BankAccountFacade>()!;
        
        AccountAction? accountAction;
        while ((accountAction = InputManager.GetAccountAction()) is not null)
        {
            try
            {
                if (accountAction == AccountAction.PrintAll)
                    ConsoleHelper.PrintBankAccounts(bankAccountFacade.BankAccounts);
                else if (accountAction == AccountAction.Create)
                {
                    Console.Clear();
                    bankAccountFacade.CreateAccount(
                        InputManager.GetInt("ID: "),
                        InputManager.GetString("Name: ", notEmpty: true),
                        InputManager.GetDecimal("Amount: ")
                    );
                }
                else if (accountAction == AccountAction.Edit)
                {
                    Console.Clear();
                    bankAccountFacade.UpdateAccountName(
                        InputManager.GetInt("ID: "),
                        InputManager.GetString("New name: ", notEmpty: true)
                    );
                }
                else if (accountAction == AccountAction.Delete)
                {
                    Console.Clear();
                    bankAccountFacade.DeleteAccount(InputManager.GetInt("ID: "));
                }
                else if (accountAction == AccountAction.Deposit)
                {
                    Console.Clear();
                    bankAccountFacade.DepositToAccount(
                        InputManager.GetInt("ID: "),
                        InputManager.GetDecimal("Amount: ")
                    );
                }
                else if (accountAction == AccountAction.Withdraw)
                {
                    Console.Clear();
                    bankAccountFacade.WithdrawFromAccount(
                        InputManager.GetInt("ID: "),
                        InputManager.GetDecimal("Amount: ")
                    );
                }
                else if (accountAction == AccountAction.ImportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    var importedAccounts = bankAccountFacade.Import(path);
                    ConsoleHelper.PrintInfo($"{importedAccounts.Count} bank accounts successfully imported from {path}");
                    InputManager.WaitForExit();
                }
                else if (accountAction == AccountAction.ExportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    bankAccountFacade.Export(path);
                    ConsoleHelper.PrintInfo($"Bank accounts successfully exported to {path}");
                    InputManager.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                ConsoleHelper.PrintError(ex.Message);
                InputManager.WaitForExit();
            }
        }    
    }

    public static void CategoryActionHandler(IServiceProvider provider)
    {
        var categoryFacade = provider.GetRequiredService<CategoryFacade>()!;
        
        CategoryAction? categoryAction;
        while ((categoryAction = InputManager.GetCategoryAction()) is not null)
        {
            try
            {
                if (categoryAction == CategoryAction.PrintAll)
                    ConsoleHelper.PrintCategories(categoryFacade.Categories);
                else if (categoryAction == CategoryAction.Create)
                {
                    Console.Clear();
                    var categoryType = InputManager.GetCategoryType();
                    if (categoryType is not null)
                        categoryFacade.CreateCategory(
                            InputManager.GetInt("ID: "),
                            InputManager.GetString("Name: ", notEmpty: true),
                            categoryType.Value
                        );
                }
                else if (categoryAction == CategoryAction.Edit)
                {
                    Console.Clear();
                    categoryFacade.UpdateCategoryName(
                        InputManager.GetInt("ID: "),
                        InputManager.GetString("New name: ", notEmpty: true)
                    );
                }
                else if (categoryAction == CategoryAction.Delete)
                {
                    Console.Clear();
                    categoryFacade.DeleteCategory(InputManager.GetInt("ID: "));
                }
                else if (categoryAction == CategoryAction.ImportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    var importedCategories = categoryFacade.Import(path);
                    ConsoleHelper.PrintInfo($"{importedCategories.Count} categories successfully imported from {path}");
                    InputManager.WaitForExit();
                }
                else if (categoryAction == CategoryAction.ExportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    categoryFacade.Export(path);
                    ConsoleHelper.PrintInfo($"Categories successfully exported to {path}");
                    InputManager.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                ConsoleHelper.PrintError(ex.Message);
                InputManager.WaitForExit();
            }
        }
    }

    public static void OperationActionHandler(IServiceProvider provider)
    {
        var operationFacade = provider.GetRequiredService<OperationFacade>()!;
        
        OperationAction? operationAction;
        while ((operationAction = InputManager.GetOperationAction()) is not null)
        {
            try
            {
                if (operationAction == OperationAction.PrintAll)
                    ConsoleHelper.PrintOperations(operationFacade.Operations);
                else if (operationAction == OperationAction.Create)
                {
                    var timeInputMethod = InputManager.GetDateTimeInputMethod();
                    if (timeInputMethod is not null)
                    {
                        Console.Clear();
                        operationFacade.CreateOperation(
                            InputManager.GetInt("ID: "),
                            InputManager.GetDecimal("Amount: "),
                            timeInputMethod == DateTimeInputMethod.Auto
                                ? DateTime.Now
                                : InputManager.GetDateTime("Creation time: "),
                            InputManager.GetInt("Bank Account ID: "),
                            InputManager.GetInt("Category ID: "),
                            InputManager.GetString("Description: ")
                        );
                    }
                }
                else if (operationAction == OperationAction.Edit)
                {
                    Console.Clear();
                    operationFacade.UpdateOperationDescription(
                        InputManager.GetInt("ID: "),
                        InputManager.GetString("New description: ")
                    );
                }
                else if (operationAction == OperationAction.Delete)
                {
                    Console.Clear();
                    operationFacade.DeleteOperation(InputManager.GetInt("ID: "));
                }
                else if (operationAction == OperationAction.ImportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    var importedOperations = operationFacade.Import(path);
                    ConsoleHelper.PrintInfo($"{importedOperations.Count} operations successfully imported from {path}");
                    InputManager.WaitForExit();
                }
                else if (operationAction == OperationAction.ExportAll)
                {
                    Console.Clear();
                    var path = Path.GetFullPath(InputManager.GetString("Enter file path: "));
                    operationFacade.Export(path);
                    ConsoleHelper.PrintInfo($"Operations successfully exported to {path}");
                    InputManager.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                ConsoleHelper.PrintError(ex.Message);
                InputManager.WaitForExit();
            }
        }
    }

    public static void AnalyticsActionHandler(IServiceProvider provider)
    {
        var analyticsFacade = provider.GetRequiredService<AnalyticsFacade>()!;
        
        AnalyticsAction? analyticsAction;
        try
        {
            while ((analyticsAction = InputManager.GetAnalyticsAction()) is not null)
            {
                if (analyticsAction == AnalyticsAction.GetOperationsByType)
                {
                    var categoryType = InputManager.GetCategoryType();
                    if (categoryType is not null)
                        ConsoleHelper.PrintOperations(analyticsFacade.GetOperationsByType(categoryType.Value));
                }
                else if (analyticsAction == AnalyticsAction.CalculatePeriodNetBalance)
                {
                    var dateTimeInputMethod = InputManager.GetDateTimeInputMethod();
                    if (dateTimeInputMethod is not null)
                    {
                        Console.Clear();
                        var startDateTime = InputManager.GetDateTime("Start date: ");
                        var endDateTime = dateTimeInputMethod == DateTimeInputMethod.Auto
                            ? DateTime.Now
                            : InputManager.GetDateTime("End Date: ");
                        ConsoleHelper.PrintDecimal($"Net balance for {startDateTime} to {endDateTime} - ",
                            analyticsFacade.CalculatePeriodNetBalance(InputManager.GetInt("Account ID: "),
                                startDateTime, endDateTime));
                        InputManager.WaitForExit();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
            InputManager.WaitForExit();
        }
    }
}