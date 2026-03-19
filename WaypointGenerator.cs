using System.Text.RegularExpressions;
using static MessageManager;

public static class WaypointGenerator
{
    public static void Run()
    {
        int num = 100;

        while (true)
        {
            string fullPath = GUSPath.GetFullPath();

            if (!File.Exists(fullPath))
            {
                Error($"Nie znaleziono pliku pod adresem:\n{fullPath}\n" +
                    $"to znaczy, że ścieżka jest nieprawidłowa, lub plik został usunięty.\n" +
                    $"Ustal ścieżkę i spróbuj ponownie...");

                Console.Write(
                    "\n[1] Zmień ścieżkę\n" +
                    "[Enter] wróć do menu\n" +
                    "\nWybierz: ");

                if (Console.ReadLine() == "1") GUSPath.SetPath();
                else return;

                continue;
            }


            Console.WriteLine("======== generator waypointów ========");

            bool success;
            string map = "";
            int waypointCount = 0;
            

            // MAP
            HashSet<string> discoveredMaps = ScanForExistingMaps(fullPath);
            if (discoveredMaps.Count == 0)
            {
                Error(
                    "Nie znaleziono żadnej mapy w pliku gry!\n" +
                    "Musisz najpierw zagrać, by kontynuować!\n");
                Console.Write("\nKliknij dowolny przycisk... ");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            var mapList = discoveredMaps.ToList();

            success = false;
            while (!success)
            {
                Warn(
                    "Jeśli nie widzisz mapy na liście, włącz grę\n" +
                    "i dodaj na tej mapie conajmniej 1 waypoint.\n");

                Console.Write("Na jakiej mapie mają być waypointy?\n");
                for (int i = 0; i < mapList.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {mapList[i]}");
                }
                Console.Write("\nWybierz numer: ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= mapList.Count)
                {
                    map = mapList[choice - 1];
                    success = true;
                }
                else
                {
                    Console.Clear();
                    Error($"Nieprawidłowy numer!");
                    Console.Write("Kliknij dowolny przycisk... ");
                    Console.ReadLine();
                    Console.Clear();
                }
            }


            // COUNT
            success = false;
            while (!success)
            {
                Console.Clear();
                Console.WriteLine($"Wybrana mapa: {map}\n");
                Console.Write("Ile waypointów utworzyć?: ");
                string input = Console.ReadLine() ?? "";

                if (!int.TryParse(input, out int result) || result <= 0)
                {
                    Console.Clear();
                    Error("Nieprawidłowa liczba!");
                    Console.Write("Kliknij dowolny przycisk... ");
                    Console.ReadLine();
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


            

            FindLastCreatedWaypoint(ref num);
            if (!BackupManager.BackupFile()) return;
            CreateWaypoints(map, num, waypointCount);

            Success($"Dodano {waypointCount} waypointów na mapie {map}!");
            Console.ReadLine();
            Console.Clear();
            break;

        }
    }



    private static HashSet<string> ScanForExistingMaps(string path)
    {
        HashSet<string> maps = new HashSet<string>();
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            var match = Regex.Match(line, @"MapName=""([^""]+)""");
            if (match.Success)
            {
                maps.Add(match.Groups[1].Value);
            }
        }
        return maps;
    }


    private static void FindLastCreatedWaypoint(ref int num)
    {
        string fullPath = GUSPath.GetFullPath();
        string content = File.ReadAllText(fullPath);

        bool exists = true;
        while (exists)
        {
            num++;
            exists = content.Contains($"CustomTag=\"Waypoint{num}");
        }
        Log($"Wykryto {num - 101} wygenerowanych wcześniej waypointów.\n");
    }

    
    private static void CreateWaypoints(string map, int num, int waypointCount)
    {
        string name = "WAYPOINT";
        (float X, float Y, float Z) cords = (-600000.000000f, -600000.000000f, 0.000000f);
        (float R, float G, float B, float A) color = (1.000000f, 1.000000f, 1.000000f, 1.000000f);
        string markIcon = $"MarkIcon=\"/Script/Engine.Texture2D'/Game/PrimalEarth/UI/Textures/T_UI_HUDPointOfInterest_Location.T_UI_HUDPointOfInterest_Location'\"";

        string header = $"[/Script/ShooterGame.ShooterGameUserSettings]\r\n";
        List<string> content = new() { header };

        for (int i = 0; i < waypointCount; i++)
        {
            string waypoint = $"SavedMinimapMarks=(Name=\"{name}\",CustomTag=\"Waypoint{num}\"," +
            $"Location=(X={cords.X},Y={cords.Y},Z={cords.Z})," +
            $"Color=(R={color.R},G={color.G},B={color.B},A={color.A})," +
            $"{markIcon},MapName=\"{map}\",bIsShowing=True,bIsShowingText=True)";

            content.Add(waypoint);
            num ++;
        }

        File.AppendAllLines(GUSPath.GetFullPath(), content);
    }
}







