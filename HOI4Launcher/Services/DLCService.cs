using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using HOI4Launcher.Models;
using HOI4Launcher.Utility;

namespace HOI4Launcher.Services;

public partial class DLCService
{
    private readonly string _gameDirectory;
    private readonly DLCFile _file;

    public DLCService(string gameDirectory, DLCFile file)
    {
        _gameDirectory = gameDirectory;
        _file = file;
    }

    public IEnumerable<DLC> LoadDLCs()
    {
        var dlcs = new List<DLC>();
        var directory = new DirectoryInfo(Path.Join(_gameDirectory,"/dlc/"));
        foreach (FileInfo file in directory.GetFiles("*.dlc", SearchOption.AllDirectories))
        {
            var text = File.ReadAllText(file.FullName);

            string name = text.ParseValue("name");
            string category = text.ParseValue("category").FormatCategory();
            string fileName = GetRelativePath(file.FullName);

            var dlc = new DLC(name, category, fileName);
            dlc.Enabled = !_file.DisabledDLCs.Contains(fileName);

            dlc.Picture = PictureUtillity.LoadPicture(file.Directory!.FullName) ?? dlc.Picture;
            dlcs.Add(dlc);
        }
        return dlcs;
    }

    private static string GetRelativePath(string name)
    {
        var index = name.IndexOf("dlc");
        return name[index..].Replace('\\', '/');
    }
}