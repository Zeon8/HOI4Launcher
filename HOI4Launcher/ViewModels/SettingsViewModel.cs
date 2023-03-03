using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using HOI4Launcher.Models;

namespace HOI4Launcher.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    public string GameDirectoryPath { get; set; }
    private readonly Settings _settings;

    public SettingsViewModel(Settings settings)
    {
        _settings = settings;
        GameDirectoryPath = settings.GameDirectoryPath;
    }

    public void Save()
    {
        _settings.GameDirectoryPath = GameDirectoryPath;
        _settings.Save();
    }
}