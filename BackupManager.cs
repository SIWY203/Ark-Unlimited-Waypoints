using static MessageManager;

public static class BackupManager
{
    public static bool BackupFile()
    {
        string sourceFile = GUSPath.GetFullPath();
        string backupFile = sourceFile + "-backup";
        string tempBackup = sourceFile + ".tmp";
        try
        {
            if (!Confirm(
                "Czy na pewno chcesz dodać waypointy?\n" +
                "To nadpisze poprzedni backup ustawień!\n\n" +
                "Kontynuować? (T/N): ")) return false;
            

            if (File.Exists(sourceFile))
            {
                // Kopia do pliku tymczasowego
                File.Copy(sourceFile, tempBackup, true);
                // Skoro nie wyrzuciło exception, to kopiowanie się udało.
                File.Move(tempBackup, backupFile, true); // podmiana plików
                Log("Utworzono backup ustawień (GameUserSettings.ini-backup)");
                return true;
            }
            else
            {
                Error("Nie znaleziono pliku źródłowego GameUserSettings.ini!");
                return false;
            }
        }
        catch (Exception ex)
        {
            Error($"Błąd podczas tworzenia kopii pliku: {ex.Message}");
            if (File.Exists(tempBackup)) File.Delete(tempBackup); // sprzątanie
            return false;
        }
    }

    public static void RestoreFile()
    {
        string sourceFile = GUSPath.GetFullPath();
        string backupFile = sourceFile + "-backup";
        string tmpRestore = backupFile + ".tmp";

        try
        {
            if (!Confirm(
                "Czy na pewno chcesz przywrócić poprzednie ustawienia?\n" +
                "Uwaga! To usunie twoje ostatnio dodane waypointy!\n\n" +
                "Na pewno przywrócić? (T/N): ")) return;

            Console.Clear();
            if (!File.Exists(backupFile))
            {
                Warn("Jeszcze nie utworzono żadnego backupa!");
                return;
            }
            if (File.Exists(sourceFile))
            {
                // bezinwazyjnie: backup do pliku tmp, potem szybki "swap"
                File.Copy(backupFile, tmpRestore, true);
                File.Move(tmpRestore, sourceFile, true);
                Success("Ustawienia zostały przywrócone!");
                Console.ReadLine(); Console.Clear();
            }
            else Error("Podczas backupu nie znaleziono pliku źródłowego");
        }

        catch (Exception ex)
        {
            Error($"Błąd podczas przywracania!");
            Error($"Szczegóły: {ex.Message}");
            if (File.Exists(tmpRestore)) File.Delete(tmpRestore); // Sprzątanie
            Console.ReadLine(); Console.Clear();
        }
    }


    public static bool Confirm(string question)
    {
        Console.Write(question);
        string choice = Console.ReadLine() ?? "N";
        if (choice.ToUpper() != "T")
        {
            Warn("Anulowano.");
            Console.ReadLine(); Console.Clear();
            return false;
        }
        return true;
    }

}


