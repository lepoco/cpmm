// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using Lepo.i18n;
using System.Windows;
using System.Windows.Controls;

namespace CPMM.Views.Pages
{
    internal class DashboardData : ViewData
    {
        private int _installedMods = 0;
        public int InstalledMods
        {
            get => _installedMods;
            set => UpdateProperty(ref _installedMods, value, nameof(InstalledMods));
        }

        private string _gameVersion = Translator.String("global.unknown");
        public string GameVersion
        {
            get => _gameVersion;
            set => UpdateProperty(ref _gameVersion, value, nameof(GameVersion));
        }

        private string _managerVersion = Translator.String("global.unknown");
        public string ManagerVersion
        {
            get => _managerVersion;
            set => UpdateProperty(ref _managerVersion, value, nameof(ManagerVersion));
        }

    }

    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        internal DashboardData DashboardDataStack { get; } = new();

        public Dashboard()
        {
            InitializeComponent();

            DataContext = DashboardDataStack;
        }

        private void ButtonAction_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not WPFUI.Controls.CardAction control) return;

            switch (control.Tag.ToString())
            {
                case "list":
                    GH.Navigate("list");
                    break;

                case "add":
                    GH.Navigate("install");
                    break;

                case "help":
                    GH.Navigate("help");
                    break;
            }
        }
    }
}
