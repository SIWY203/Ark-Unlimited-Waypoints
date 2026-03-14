using static MessageManager;

public class Program
{
    public static void Main(string[] args)
    {
        int num = 99; // 100
        int waypointCount = 1;
        string map = "Ragnarok";

        FindLastCreatedWaypoint(ref num);
        Creator.CreateWaypoints(map, num, waypointCount);
    }

    public static void FindLastCreatedWaypoint(ref int num)
    {
        string path = GamePath.Path;
        string content = File.ReadAllText(path);

        bool exists = true;
        while (exists)
        {
            num++;
            exists = content.Contains($"Waypoint{num}");
        }
        Console.WriteLine(num);
        
    }

}





