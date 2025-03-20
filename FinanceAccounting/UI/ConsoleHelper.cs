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

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(message);
        Console.ForegroundColor = DefaultForegroundColor;
    }
}