using static MessageManager;
public static class GUSPath
{
    private static string ConfigFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SteamLibPath.txt");
    public static string FilePath { get; private set; } = string.Empty;
    public static string FileName => "GameUserSettings.ini";


    public static void LoadPath()
    {
        if (File.Exists(ConfigFile))
        {
            FilePath = File.ReadAllText(ConfigFile).Trim();
            if (string.IsNullOrEmpty(FilePath) || !Directory.Exists(FilePath))
            {
                SetPath();
            }
        }
        else SetPath();
    }

    public static void SetPath()
    {
        Console.Clear();
        Console.WriteLine("=== Konfiguracja ścieżki ARK ===");
        Console.WriteLine("Podaj literę dysku (np. C) lub pełną ścieżkę do SteamLibrary");
        Console.Write("\nWpisz: ");

        string input = Console.ReadLine()?.ToUpper() ?? string.Empty;

        // fix dla samej litery dysku
        if (input.Length == 1 || (input.Length == 2 && input.EndsWith(":")))
        {
            input = input.Substring(0, 1) + @":\";
        }

        // pełna ścieżka
        string detectedPath = Path.Combine(input, "SteamLibrary", "steamapps", "common", "ARK Survival Ascended", "ShooterGame", "Saved", "Config", "Windows");

        if (Directory.Exists(detectedPath))
        {
            FilePath = detectedPath;

            File.WriteAllText(ConfigFile, FilePath);
            Success($"Ścieżka została zapisana: {FilePath}");
        }
        else
        {
            Error($"Nie znaleziono folderu:\n{detectedPath}");
            Console.WriteLine("Naciśnij dowolny klawisz, aby spróbować ponownie...");
            Console.ReadKey();
            SetPath(); // rekurencja (dopuszczalna przy konfiguracji)
        }

    }

    public static string GetFullPath()
    {
        // jeśli FilePath puste, załaduj
        if (string.IsNullOrEmpty(FilePath)) LoadPath();

        return Path.Combine(FilePath, FileName);
    }

}





