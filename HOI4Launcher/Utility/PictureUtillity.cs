using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace HOI4Launcher.Utility;

public static class PictureUtillity
{
    public static Bitmap LoadStandardPicture()
    {
        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        return new Bitmap(assets?.Open(new Uri("avares://HOI4Launcher/Assets/noimage.png")));
    }

    public static Bitmap? LoadPicture(string path)
    {
        var files = Directory.GetFiles(path)
            .Where(f => f.EndsWith(".png") || f.EndsWith(".jpg") || f.EndsWith(".bmp"));
        if(files.Any())
            return new Bitmap(files.First());
        return null;
    }


}