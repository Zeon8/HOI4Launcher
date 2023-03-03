using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using HOI4Launcher.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace HOI4Launcher.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public DLCViewModel DLCViewModel { get; }
    public ModsViewModel ModsViewModel { get; }
    public SettingsViewModel SettingsViewModel { get; }
    private readonly Settings _settings;
    private readonly DLCFile _dlcFile;

    public MainWindowViewModel(Settings settings)
    {
        _settings = settings;
        _dlcFile = DLCFile.Read()!;
        if(_dlcFile is null)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Error loading",
                "The mods folder could not be found, try reinstalling the game and try again",
                ButtonEnum.Ok, Icon.Error);
            throw new FileNotFoundException("File dlc_load.json not found");
        }
        
        DLCViewModel = new DLCViewModel(settings.GameDirectoryPath, _dlcFile);
        ModsViewModel = new ModsViewModel(_dlcFile);
        SettingsViewModel = new SettingsViewModel(settings);
    }

    public async void Run()
    {
        await SaveChanges();
        string extension = OperatingSystem.IsWindows() ? ".exe" : ".app";
        Process.Start(Path.Join(_settings.GameDirectoryPath, "/hoi4" + extension));
    }

    private Task SaveChanges()
    {
        _dlcFile.DisabledDLCs = DLCViewModel.GetDisabledDLCs();
        _dlcFile.EnabledMods = ModsViewModel.GetEnabledMods();
        return _dlcFile.Save();
    }
}
