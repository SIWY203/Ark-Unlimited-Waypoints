using static MessageManager;

public static class BackupManager
{
    public static bool BackupFile()
    {
        string source = GUSPath.GetPath();
        string tempBackup = GUSPath.GetPath() + ".tmp";
        try
        {
            if (File.Exists(GUSPath.GetPath()))
            {
                // Kopia do pliku tymczasowego
                File.Copy(source, "backup-" + source, true);
                // Skoro nie wyrzuciło exception, to kopiowanie się udało.
                File.Move(tempBackup, source, true); // podmiana plików
                Log("Utworzono backup ustawień (backup-GameUserSettings.ini)");
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
        string source = GUSPath.GetPath();
        string backupFile = GUSPath.GetPath() + ".tmp";
        string tempRestore = source + ".tmp"; // Plik tymczasowy

        try
        {
            // czy w ogóle jest z czego przywracać
            if (!File.Exists(backupFile))
            {
                Warn("Nie znaleziono pliku backupu (GameUserSettingsBackup.txt).");
                return;
            }

            // backup do pliku tymczasowego
            File.Copy(backupFile, tempRestore, true);
            // Skoro kopia się udała - szybki "swap"
            File.Move(tempRestore, source, true);
            Success("Ustawienia zostały przywrócone!");
        }
        catch (Exception ex)
        {
            Error($"Błąd podczas przywracania!");
            Error($"Szczegóły: {ex.Message}");
            if (File.Exists(tempRestore)) File.Delete(tempRestore); // Sprzątanie
        }
    }

}


