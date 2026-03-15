using static MessageManager;

public class Program
{
    public static void Main(string[] args)
    {
        GUSPath.LoadPath();

        while (true)
        {
            Console.Write(
            $"============ MENU ============\n" +
            $"[1] generator waypointów\n" +
            $"[2] przywróć backup pliku\n" +
            $"[3] ścieżka do biblioteki steam\n" +
            $"[4] wyjdź\n" +
            $"\n Wybierz tryb: ");

            string choice = Console.ReadLine() ?? "";
            Console.Clear();

            if (choice == "1") { WaypointGenerator.Run(); continue; }
            if (choice == "2") { BackupManager.RestoreFile(); continue; }
            if (choice == "3") { PathMenu(); continue; }
            if (choice == "4") { Environment.Exit(0); continue; }
            else Error("Spróbuj ponownie.");
        }


    }

    public static void PathMenu()
    {
        Console.WriteLine("=== Ścieżka do GameUserSettings.ini ===\n");
        Log(GUSPath.FilePath);
        if (!Directory.Exists(GUSPath.FilePath)) Warn("<brak>");
        Console.Write("\n[1] Zmień \n[2] Anuluj \n\nWybierz: ");
        string input = Console.ReadLine()??"";
        Console.Clear();
        if (input == "1") GUSPath.SetPath();
    }
}





