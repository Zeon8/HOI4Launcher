using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HOI4Launcher.Models;
using HOI4Launcher.Services;

namespace HOI4Launcher.ViewModels;

public partial class ModsViewModel : ViewModelBase
{
    private readonly ModsService _modsService;

    public ModsViewModel(DLCFile dlcFile)
    {
        _modsService = new ModsService(dlcFile);
        _mods = _modsService.LoadMods();
    }

    public IEnumerable<Mod> ShowedMods
    {
        get
        {
            if(string.IsNullOrWhiteSpace(_searchText))
                return _mods;
            else
                return _mods.Where(mod => mod.Name.StartsWith(_searchText, true, null));
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowedMods))]
    private string? _searchText;

    private IEnumerable<Mod> _mods;

    public IEnumerable<string> GetEnabledMods()
    {
        return _mods.Where(mod => mod.Enabled).Select(mod => mod.FileName);
    }

    public void RefreshMods()
    {
        _mods = _modsService.LoadMods();
        OnPropertyChanged(nameof(ShowedMods));
    }
}
