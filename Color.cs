
public static class MessageManager
{
    public static void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[LOG]: ");
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[OK]: ");
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[WARN]: ");
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[ERROR]: ");
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

}






