using System;
using System.Collections;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HOI4Launcher.Utility;

namespace HOI4Launcher.Models;

public class DLC
{
    public DLC(string name, string category, string fileName)
    {
        Name = name;
        Category = category;
        FileName = fileName;
    }

    public string Name { get; }

    public string Category { get; }

    public string FileName { get; }
    
    public bool Enabled { get; set; }

    public Bitmap Picture { get; set; } = PictureUtillity.LoadStandardPicture();

}