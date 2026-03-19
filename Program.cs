using static MessageManager;

public class Program
{
    public static void Main(string[] args)
    {
        Warn(
            $"WAŻNA INFORMACJA!\n" +
            $"\tZanim przejdziesz dalej, musisz mieć chociaż\n" +
            $"\tjeden utworzony waypoint na docelowej mapie!\n");
        Console.Write("Kliknij dowolny przycisk... ");
        Console.ReadKey();
        Console.Clear();

        GUSPath.LoadPath();

        while (true)
        {
            Console.Write(
            $"============ MENU ============\n" +
            $"[1] generator waypointów\n" +
            $"[2] przywróć backup pliku\n" +
            $"[3] ścieżka do biblioteki steam\n" +
            $"[4] instrukcja\n" +
            $"[5] wyjdź\n" +
            $"\n Wybierz tryb: ");

            string choice = Console.ReadLine() ?? "";
            Console.Clear();

            if (choice == "1") { WaypointGenerator.Run(); continue; }
            if (choice == "2") { BackupManager.RestoreFile(); continue; }
            if (choice == "3") { PathMenu(); continue; }
            if (choice == "4") { Info(); continue; }
            if (choice == "5") { Environment.Exit(0); continue; }
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

    public static void Info()
    {
        Console.WriteLine(
            "=== Jak to działa? ===\n" +
            "\n1) Włącz grę\n" +
            " - uruchom mapę, na której chcesz więcej waypointów\n" +
            " - upewnij się, że jest tam minimum 1 waypoint\n" +
            "\n2) Generator waypointów\n" +
            " - mapa powinna już się wyświetlać na liście\n" +
            " - waypointy utworzone za jednym razem będą takie same\n" +
            "\n3) Ustawienia podczas gry\n" +
            " - waypointy po wygenerowaniu powinny być w lewym górnym rogu mapy\n" +
            " - pamiętaj by ręcznie dostosować kordy z menu waypointów\n" +
            " - nadpisz wysokość na delikatnie wyższą niż pozycja gracza, by punkt\n" +
            "   wskazywał prawidłowe miejsce, a nie np. kilometr pod ziemią\n");

        Console.Write("Kliknij dowolny przycisk... ");
        Console.ReadKey();
        Console.Clear();
    }
}





