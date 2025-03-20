using FinanceAccounting.Models;

namespace FinanceAccounting.UI;

using Enums;

public static class InputManager
{
    private static readonly ConsoleColor DefaultBackgroundColor;
    private static readonly ConsoleColor DefaultForegroundColor;
    
    static InputManager()
    {
        DefaultBackgroundColor = Console.BackgroundColor;
        DefaultForegroundColor = Console.ForegroundColor;
    }
    
    private static int? GetOption(in List<string> options, string req = "Select an option")
    {
        int currentOption = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{req} (ecs to exit)");
            for (int i = 0; i < options.Count(); ++i)
            {
                if (i == currentOption)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);

                if (i == currentOption)
                {
                    Console.BackgroundColor = DefaultBackgroundColor;
                    Console.ForegroundColor = DefaultBackgroundColor;
                }
            }

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(intercept: true).Key;
            } while (key != ConsoleKey.Escape && key != ConsoleKey.Enter &&
                     key != ConsoleKey.UpArrow && key != ConsoleKey.DownArrow);
            
            if (key == ConsoleKey.Enter)
                return currentOption;
            else if (key == ConsoleKey.DownArrow && currentOption != options.Count() - 1)
                ++currentOption;
            else if (key == ConsoleKey.UpArrow && currentOption != 0)
                --currentOption;
            else if (key == ConsoleKey.Escape)
                return null;
        }

    }
    
    public static int GetInt(string req)
    {
        int res;
        Console.Write(req);
        while (!int.TryParse(Console.ReadLine(), out res))
        {
            ConsoleHelper.PrintError();
            Console.Write(req);
        }

        return res;
    }
    
    public static void WaitForExit()
    {
        Console.WriteLine("\nEsc to exit");
        while (Console.ReadKey(intercept: true).Key != ConsoleKey.Escape) ;
    }

    public static bool GetBool(string req)
    {
        Console.Write(req);
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(intercept: true).Key;
        } while (key != ConsoleKey.Y && key != ConsoleKey.N);

        if (key == ConsoleKey.Y)
            return true;
        else
            return false;
    }

    public static decimal GetDecimal(string req)
    {
        decimal res;
        Console.Write(req);
        while (!decimal.TryParse(Console.ReadLine(), out res))
        {
            ConsoleHelper.PrintError();
            Console.Write(req);
        }

        return res;
    }

    public static string GetString(string req, bool notEmpty = false)
    {
        Console.Write(req);
        string res;
        while ((res = Console.ReadLine()) == "" && notEmpty)
        {
            ConsoleHelper.PrintError();
            Console.Write(req);
        }

        return res;
    }

    public static MainMenuOption? GetMainMenuOption()
    {
        return (MainMenuOption?)GetOption(["Bank accounts", "Categories", "Operations", "Analytics"]);
    }

    public static AccountAction? GetAccountAction()
    {
        return (AccountAction?)GetOption(["Print all", "Create", "Edit", "Delete", "Deposit", "Withdraw", "Import all", "Export all"]);
    }

    public static CategoryAction? GetCategoryAction()
    {
        return (CategoryAction?)GetOption(["Print all", "Create", "Edit", "Delete", "Import all", "Export all"]);
    }

    public static OperationAction? GetOperationAction()
    {
        return (OperationAction?)GetOption(["Print all", "Create", "Edit", "Delete", "Import all", "Export all"]);
    }

    public static CategoryType? GetCategoryType()
    {
        return (CategoryType?)GetOption(["Income", "Expense"], "Select category type");
    }

    public static DateTimeInputMethod? GetDateTimeInputMethod()
    {
        return (DateTimeInputMethod?)GetOption(["Auto (now)", "Manual"], "Select time input method");
    }

    public static DateTime GetDateTime(string req)
    {
        Console.Write(req);
        DateTime res;
        while (!DateTime.TryParse(Console.ReadLine(), out res))
        {
            ConsoleHelper.PrintError();
            Console.Write(req);
        }

        return res;
    }

    public static AnalyticsAction? GetAnalyticsAction()
    {
        return (AnalyticsAction?)GetOption(["Get operations by type", "Calculate account period net balance"]);
    }
}