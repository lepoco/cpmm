// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using Lepo.i18n;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CPMM.Views.Pages
{
    internal class SettingsData : ViewData
    {
        private bool _useMica = true;
        public bool UseMica
        {
            get => _useMica;
            set => UpdateProperty(ref _useMica, value, nameof(UseMica));
        }

        private bool _useTray = true;
        public bool UseTray
        {
            get => _useTray;
            set => UpdateProperty(ref _useTray, value, nameof(UseTray));
        }

        private string _gameRootDirectory = Translator.String("global.directoryNotSelected");
        public string GameRootDirectory
        {
            get => _gameRootDirectory;
            set => UpdateProperty(ref _gameRootDirectory, value, nameof(GameRootDirectory));
        }

        private string _gameSettingsDirectory = Translator.String("global.directoryNotSelected");
        public string GameSettingsDirectory
        {
            get => _gameSettingsDirectory;
            set => UpdateProperty(ref _gameSettingsDirectory, value, nameof(GameSettingsDirectory));
        }

        private string _gameSavesDirectory = Translator.String("global.directoryNotSelected");
        public string GameSavesDirectory
        {
            get => _gameSavesDirectory;
            set => UpdateProperty(ref _gameSavesDirectory, value, nameof(GameSavesDirectory));
        }

        private string _backupsDirectory = Translator.String("global.directoryNotSelected");
        public string BackupsDirectory
        {
            get => _backupsDirectory;
            set => UpdateProperty(ref _backupsDirectory, value, nameof(BackupsDirectory));
        }

        private int _languageIndex = 0;
        public int LanguageIndex
        {
            get => _languageIndex;
            set => UpdateProperty(ref _languageIndex, value, nameof(LanguageIndex));
        }

        private IEnumerable<string> _languageOptions = new string[] { };
        public IEnumerable<string> LanguageOptions
        {
            get => _languageOptions;
            set => UpdateProperty(ref _languageOptions, value, nameof(LanguageOptions));
        }

        private IEnumerable<string> _homePages = new string[] { };
        public IEnumerable<string> HomePages
        {
            get => _homePages;
            set => UpdateProperty(ref _homePages, value, nameof(HomePages));
        }
    }

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        internal SettingsData SettingsDataStack { get; } = new();

        public Settings()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            if (!String.IsNullOrEmpty(GH.Settings.Options.GameRootDirectory))
                SettingsDataStack.GameRootDirectory = GH.Settings.Options.GameRootDirectory;

            if (!String.IsNullOrEmpty(GH.Settings.Options.GameSettingsDirectory))
                SettingsDataStack.GameSettingsDirectory = GH.Settings.Options.GameSettingsDirectory;

            if (!String.IsNullOrEmpty(GH.Settings.Options.GameSavesDirectory))
                SettingsDataStack.GameSavesDirectory = GH.Settings.Options.GameSavesDirectory;

            SettingsDataStack.HomePages = new[]
            {
                Translator.String("container.nav.dashboard"),
                Translator.String("container.nav.list"),
                Translator.String("container.nav.install"),
                Translator.String("container.nav.settings")
            };

            SettingsDataStack.LanguageOptions = Languages.GetNames();
            SettingsDataStack.LanguageIndex = Languages.GetIndexByCode(GH.Settings.Options.Language);

            DataContext = SettingsDataStack;
        }
    }
}
