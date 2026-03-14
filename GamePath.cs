using static MessageManager;
public static class GamePath
{
    public static string Path { get; private set; } = string.Empty;

    public static void SetPath()
    {
        Console.WriteLine("Ustaw ścieżkę do biblioteki steam, \nw której znajduje się Ark Ascended.");
        Console.WriteLine("Dla ścieżki \"C:/SteamLibrary\", \nprawidłowy spis to po prostu \"C:\".");
        Console.Write("\nWpisz: ");
        string steamLibPath = Console.ReadLine() ?? string.Empty;
        if (Directory.Exists(steamLibPath))
        {
            Success("Ścieżka została zapisana!");
        }
        else
        {
            Error("Coś poszło nie tak!");
        }
            

        Path = $@"{steamLibPath}\SteamLibrary\steamapps\common\ARK Survival Ascended\ShooterGame\Saved\Config\Windows\GameUserSettings.ini";

    }

    public static string GetPath()
    {
        if (string.IsNullOrEmpty(Path))
        {
            Warn("Ścieżka do gry nie jest ustawiona!");
            SetPath();
        }
        return Path;
    }

}





