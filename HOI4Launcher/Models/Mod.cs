using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HOI4Launcher.Utility;

namespace HOI4Launcher.Models;

public class Mod
{
    public Mod(string name, string fileName)
    {
        Name = name;
        FileName = fileName;
    }

    public string Name { get; set; }

    public string FileName { get; set; }
    public bool Enabled { get; set; }
    public Bitmap Picture { get; set; } = PictureUtillity.LoadStandardPicture();
    public IEnumerable<string>? Tags { get; set; }

}