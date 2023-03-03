using System.IO;
using System.Text.Json;

namespace HOI4Launcher.Models;

public class Settings
{
    public string GameDirectoryPath { get; set; }
    public string Language { get; set; } = "Eng";

    public Settings(string gameDirectoryPath)
    {
        GameDirectoryPath = gameDirectoryPath;
    }

    public static Settings? Read()
    {
        if (File.Exists("settings.json"))
        {
            var settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("settings.json"));
            if(settings is not null)
                return settings;
        }
        return null;
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText("settings.json", json);
    }
}