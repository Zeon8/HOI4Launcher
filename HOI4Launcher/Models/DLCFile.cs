using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HOI4Launcher.Services;

namespace HOI4Launcher.Models;

public class DLCFile
{
    public DLCFile(IEnumerable<string> enabledMods, IEnumerable<string> disabledDLCs)
    {
        EnabledMods = enabledMods;
        DisabledDLCs = disabledDLCs;
    }

    [JsonPropertyName("enabled_mods")]
    public IEnumerable<string> EnabledMods { get; set; }

    [JsonPropertyName("disabled_dlcs")]
    public IEnumerable<string> DisabledDLCs { get; set; }

    private static readonly string DlcLoadFilePath = ModsService.GameDataPath + "dlc_load.json";

    public static DLCFile? Read()
    {
        if (File.Exists(DlcLoadFilePath))
        {
            var json = File.ReadAllText(DlcLoadFilePath);
            var file = JsonSerializer.Deserialize<DLCFile>(json)!;
            return file;
        }
        return null;
    }

    public Task Save()
    {
        var json = JsonSerializer.Serialize(this);
        return File.WriteAllTextAsync(Path.Join(ModsService.GameDataPath + "dlc_load.json"), json);
    }
}