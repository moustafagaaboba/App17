﻿using System.Windows.Input;

using App17.Contracts.Services;
using App17.Helpers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using Windows.ApplicationModel;

namespace App17.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;

    private ElementTheme _elementTheme;
    public ElementTheme ElementTheme
    {
        get
        {
            return _elementTheme;
        }

        set
        {
            SetProperty(ref _elementTheme, value);
        }
    }

    private string _versionDescription;
    public string VersionDescription
    {
        get
        {
            return _versionDescription;
        }

        set
        {
            SetProperty(ref _versionDescription, value);
        }
    }

    private ICommand _switchThemeCommand;

    public ICommand SwitchThemeCommand
    {
        get
        {
            if (_switchThemeCommand == null)
            {
                _switchThemeCommand = new RelayCommand<ElementTheme>(
                    async (param) =>
                    {
                        if (ElementTheme != param)
                        {
                            ElementTheme = param;
                            await _themeSelectorService.SetThemeAsync(param);
                        }
                    });
            }
            return _switchThemeCommand;
        }
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        _elementTheme = _themeSelectorService.Theme;
        VersionDescription = GetVersionDescription();
    }

    private static string GetVersionDescription()
    {
        var appName = "AppDisplayName".GetLocalized();
        var version = Package.Current.Id.Version;

        return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
