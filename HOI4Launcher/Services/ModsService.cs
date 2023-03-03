using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using HOI4Launcher.Models;
using HOI4Launcher.Utility;

namespace HOI4Launcher.Services;

public class ModsService
{
    public readonly static string GameDataPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/Paradox Interactive/Hearts of Iron IV/";
    public readonly static string ModsPath = Path.Join(GameDataPath,"mod/");
    private readonly DLCFile _file;

    public ModsService(DLCFile file)
    {
        _file = file;
    }

    public IEnumerable<Mod> LoadMods()
    {
        var mods = new List<Mod>();
        var directory = new DirectoryInfo(ModsPath);
        foreach (var file in directory.GetFiles("*.mod"))
        {
            var text = File.ReadAllText(file.FullName);
            var name = text.ParseValue("name");
            var fileName = "mod/" + file.Name;
            var mod = new Mod(name, fileName);
            mod.Enabled = _file.EnabledMods.Contains(mod.FileName);
            mods.Add(mod);

            if(text.Contains("tags"))
                mod.Tags = GetTags(text);

            if(text.Contains("path"))
            {
                var path = text.ParseValue("path");
                if (!Directory.Exists(path))
                    path = Path.Join(GameDataPath, path);

                mod.Picture = PictureUtillity.LoadPicture(path) ?? mod.Picture;
            }
        }
        return mods;
    }

    private static IEnumerable<string> GetTags(string text)
    {
        List<string> tags = new();
        int startIndex = text.IndexOf("\ntags");
        if(startIndex == -1)
            startIndex = text.IndexOf("tags");
        int openBracket = text.IndexOf('{', startIndex);
        int closeBracket = text.IndexOf('}', openBracket);

        int openQuote = text.IndexOf('"', openBracket)+1;
        int closeQuote = text.IndexOf('"', openQuote); 
        while(openQuote < closeBracket)
        {
            tags.Add(text[openQuote..closeQuote]);
            openQuote = text.IndexOf('"', closeQuote+1)+1;
            closeQuote = text.IndexOf('"', openQuote);
        }
        return tags;
    }


    
}