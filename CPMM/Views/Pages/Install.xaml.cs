// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using Lepo.i18n;
using System.Windows;
using System.Windows.Controls;

namespace CPMM.Views.Pages
{
    internal class InstallData : ViewData
    {
        private string _modificationPath = Translator.String("global.unknown");

        public string ModificationPath
        {
            get => _modificationPath;
            set => UpdateProperty(ref _modificationPath, value, nameof(ModificationPath));
        }
    }

    /// <summary>
    /// Interaction logic for Install.xaml
    /// </summary>
    public partial class Install : Page
    {
        internal InstallData InstallDataStack { get; } = new();

        public Install()
        {
            InitializeComponent();

            DataContext = InstallDataStack;
        }

        private void ButtonSelect_OnClick(object sender, RoutedEventArgs e)
        {
            // Select
        }
    }
}