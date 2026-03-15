using static MessageManager;

public static class WaypointGenerator
{
    public static void Run()
    {
        int num = 99;
        while (true)
        {
            string fullPath = GUSPath.GetFullPath();

            if (!File.Exists(fullPath))
            {
                Error($"Nie znaleziono pliku pod adresem:\n{fullPath}\n" +
                    $"to znaczy, że ścieżka jest nieprawidłowa, lub plik został usunięty.\n" +
                    $"Uruchom na chwilę grę (plik zostanie utworzony) i spróbuj ponownie...");

                Console.Write(
                    "\n[1] Zmień ścieżkę\n" +
                    "[dowolny klawisz] Powrót do menu\n\n" +
                    "Wybierz: ");

                if (Console.ReadLine() == "1") GUSPath.SetPath();
                else return;

                continue;
            }


            Console.WriteLine("======== generator waypointów ========");
            int waypointCount = 0;
            bool success = false;

            while (!success)
            {
                Console.Write("Ile waypointów utworzyć?: ");
                string input = Console.ReadLine() ?? "";

                if (!int.TryParse(input, out int result) || result <= 0)
                {
                    Error("Nieprawidłowa liczba! Spróbuj ponownie.");
                    continue;
                }
                if (result > 20)
                {
                    Warn("Limit jednoczesnego generowania to 20!");
                    waypointCount = 20;
                }
                else waypointCount = result;
                success = true;
            }

            string map = "";
            success = false;
            while (!success)
            {
                Console.Write("\nNa jakiej mapie mają być waypointy? Wpisz jedną...");
                Console.WriteLine(
                    "Wpisz nazwę w takim formacie, np:\n" +
                    "TheIsland, ScorchedEarth, Aberration,\n" +
                    "TheCenter, Extinction, Ragnarok, itd\n");
                Console.WriteLine(
                    $"Można też sprawdzić jaką nazwę mapy wpisać, jeśli masz\n" +
                    $"chociaż 1 waypoint, wchodząc do plików gry w bibliotece:\n" +
                    $"...ShooterGame\\Saved\\Config\\Windows\\GameUserSettings.ini\n");
                Console.Write("Mapa: ");
                map = Console.ReadLine() ?? "";
                if (map == "") { Error("Nie wpisano mapy!"); continue; }
                success = true;
            }

            FindLastCreatedWaypoint(ref num);
            if (!BackupManager.BackupFile()) return;            
            Creator.CreateWaypoints(map, num, waypointCount);

            Success($"Wygenerowano nowe waypointy!");
            Console.ReadLine();
            Console.Clear();
            break;

        }
    }

    public static void FindLastCreatedWaypoint(ref int num)
    {
        string fullPath = GUSPath.GetFullPath();
        string content = File.ReadAllText(fullPath);

        bool exists = true;
        while (exists)
        {
            num++;
            exists = content.Contains($"Waypoint{num}");
        }
        Log($"Wykryto {num - 100} wygenerowanych wcześniej waypointów.");

    }
}







