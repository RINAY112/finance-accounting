using FinanceAccounting.Models;

namespace FinanceAccounting.UI;

public static class ConsoleHelper
{
    private static readonly ConsoleColor DefaultForegroundColor;

    private static readonly ConsoleColor DefaultBackgroundColor;

    static ConsoleHelper()
    {
        DefaultForegroundColor = Console.ForegroundColor;
        DefaultBackgroundColor = Console.BackgroundColor;
    }

    public static void PrintInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(message);
        Console.ForegroundColor = DefaultForegroundColor;
    }

    public static void PrintError(string message = "Incorrect value\n")
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ForegroundColor = DefaultForegroundColor;
    }

    public static void PrintCategories(IReadOnlyCollection<Category> categories)
    {
        Console.Clear();
        if (categories.Count == 0)
            Console.WriteLine("Category list is empty.");
        else
        {
            foreach (Category category in categories)
            {
                Console.WriteLine(category);
            }
        }
        InputManager.WaitForExit();
    }

    public static void PrintBankAccounts(IReadOnlyCollection<BankAccount> bankAccounts)
    {
        Console.Clear();
        if (bankAccounts.Count == 0)
            Console.WriteLine("Bank account list is empty.");
        else
        {
            foreach (BankAccount bankAccount in bankAccounts)
            {
                Console.WriteLine(bankAccount);
            }
        }
        InputManager.WaitForExit();
    }

    public static void PrintOperations(IReadOnlyCollection<Operation> operations)
    {
        Console.Clear();
        if (operations.Count == 0)
            Console.WriteLine("Operations list is empty.");
        else
        {
            foreach (Operation operation in operations)
            {
                Console.WriteLine(operation);
            }
        }
        InputManager.WaitForExit();
    }

    public static void PrintDecimal(string prefix, decimal value)
    {
        Console.Clear();
        Console.Write(prefix);
        Console.WriteLine(value);
    }
}