using static MessageManager;

public class Program
{
    public static void Main(string[] args)
    {
        int num = 99; //100

        Console.Write(
            $"[1] generator waypointów" +
            $"[2] przywróć backup pliku" +
            $"\n Wybierz tryb: ");
        string choice = Console.ReadLine() ?? "";
        if (choice == "2") BackupManager.RestoreFile();

        while (true)
        {
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
                    $"chociaż 1 waypoint, wchodząc do plików gry w bibliotece:" +
                    $"...ShooterGame\\Saved\\Config\\Windows\\GameUserSettings.ini");
                Console.Write("Mapa: ");
                map = Console.ReadLine() ?? "";
                if (map == "") { Error("Nie wpisano mapy!"); continue; }
                success = true;
            }

            if (!BackupManager.BackupFile()) continue;
            FindLastCreatedWaypoint(ref num);
            Creator.CreateWaypoints(map, num, waypointCount);
        }
        
    }

    public static void FindLastCreatedWaypoint(ref int num)
    {
        string path = GUSPath.GetPath();
        string content = File.ReadAllText(path);

        bool exists = true;
        while (exists)
        {
            num++;
            exists = content.Contains($"Waypoint{num}");
        }
        Log($"Wykryto {num-100} wygenerowanych waypointów.");
        
    }


}





