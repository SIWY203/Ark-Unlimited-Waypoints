public static class Creator
{
    public static void CreateWaypoints()
    {
        int waypointCount = 10;
        int num = 100;
        string name = "WAYPOINT";
        string map = "Ragnarok";
        (float X, float Y, float Z) cords = (-589500.000000f, -589500.000000f, -23514.923828f);
        (float R, float G, float B, float A) color = (1.000000f, 1.000000f, 1.000000f, 1.000000f);


        string header = $"[/Script/ShooterGame.ShooterGameUserSettings]\r\n";
        List<string> content = new() { header };

        for (int i = 0; i < waypointCount; i++)
        {
            string waypoint = $"SavedMinimapMarks=(Name=\"{name}\",CustomTag=\"Waypoint{num}\"," +
            $"Location=(X={cords.X},Y={cords.Y},Z={cords.Z})," +
            $"Color=(R={color.R},G={color.G},B={color.B},A={color.A})," +
            $"ID=232,MarkIcon=\"/Script/Engine.Texture2D'/Game/PrimalEarth/UI/Textures/T_UI_HUDPointOfInterest_Location.T_UI_HUDPointOfInterest_Location'\"," +
            $"MapName=\"{map}_WP\",bIsShowing=True,IconColor=(R=1.000000,G=1.000000,B=1.000000,A=1.000000),bIsShowingText=True,CharacterID=-1,CharacterIsPlayer=False)";

            content.Add(waypoint);
            num += 1;
        }

        File.AppendAllLines("test.txt", content);


        // --- LOG ---
        //Console.WriteLine($"Dopisano zawartość:\n");
        //foreach (string c in content)
        //{
        //    Console.WriteLine(c + "\n");
        //}
    }
    
}


