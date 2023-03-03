using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using HOI4Launcher.Models;
using HOI4Launcher.Services;

namespace HOI4Launcher.ViewModels;

public partial class DLCViewModel : ViewModelBase
{
    public DLCViewModel(string gameDirectory, DLCFile dlcFile)
    {
        var dlcsService = new DLCService(gameDirectory, dlcFile);
        _DLCs = dlcsService.LoadDLCs().ToList();
    }

    [ObservableProperty] private IEnumerable<DLC> _DLCs;

    public IEnumerable<string> GetDisabledDLCs()
    {
        return _DLCs.Where(dlc => !dlc.Enabled).Select(dlc => dlc.FileName);
    }
}