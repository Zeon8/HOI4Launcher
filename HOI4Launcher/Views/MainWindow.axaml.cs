using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HOI4Launcher.Models;
using HOI4Launcher.ViewModels;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace HOI4Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
    }

    public async void OnOpened(object? sender, EventArgs args)
    {
        if (await LoadSettings() is Settings settings)
            DataContext = new MainWindowViewModel(settings);
        else
        {
            await MessageBoxManager.GetMessageBoxStandardWindow("Error loading",
                    "Cannot start launcher without specifying the path to the game directry",
                    ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error)
                    .ShowDialog(this);
            Close();
        }
    }

    private async Task<Settings?> LoadSettings()
    {
        if (Settings.Read() is not Settings settings)
        {
            string? path = await GetGameDirectory();
            if (string.IsNullOrWhiteSpace(path))
                return null;
            
            settings = new Settings(path);
            settings.Save();
        }
        return settings;
    }

    private async Task<string?> GetGameDirectory()
    {
        if (File.Exists("hoi4.exe") || File.Exists("hoi.app"))
            return Directory.GetCurrentDirectory();

        if (await AskAboutSpecifying() == ButtonResult.Cancel)
            return null;

        return await OpenDialog();
    }

    private async Task<ButtonResult> AskAboutSpecifying()
    {
        return await MessageBoxManager.GetMessageBoxStandardWindow("Notice",
            "As you started launcher for first time, please specify path to the game directory",
            ButtonEnum.OkCancel, MessageBox.Avalonia.Enums.Icon.Folder)
            .ShowDialog(this);
    }

    private Task<string?> OpenDialog()
    {
        var dialog = new OpenFolderDialog()
        {
            Title = "Select HOI4 folder"
        };
        return dialog.ShowAsync(this);
    }

    public async void OnSelectDirectoryClicked(object? sender, RoutedEventArgs args)
    {
        string? path = await OpenDialog();
        if (!string.IsNullOrWhiteSpace(path))
        {
            var vm = (MainWindowViewModel)DataContext!;
            vm.SettingsViewModel.GameDirectoryPath = path;
        }
    }
}