// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using Lepo.i18n;
using Microsoft.Win32;
using System.Windows;
using WPFUI.Controls.Interfaces;

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

        private bool _dialogMultiSelect = false;

        public bool DialogMultiSelect
        {
            get => _dialogMultiSelect;
            set => UpdateProperty(ref _dialogMultiSelect, value, nameof(DialogMultiSelect));
        }
    }

    /// <summary>
    /// Interaction logic for Install.xaml
    /// </summary>
    public partial class Install : INavigable
    {
        internal InstallData InstallDataStack { get; } = new();

        internal OpenFileDialog DialogSelector;

        public Install()
        {
            InitializeComponent();
            DialogSelector = InitializeDialog();

            DataContext = InstallDataStack;
        }

        public void OnNavigationRequest(INavigation sender, object current)
        {
            // Navigated
        }

        private OpenFileDialog InitializeDialog()
        {
            var dialogFilter = Translator.String("global.dialog.cyberMods") + " (*.7z;*.zip;*.rar)|*.7z;*.zip;*.rar";
            dialogFilter += "|All we had to do, was follow the damn train, CJ!(*.hotcoffee)|*.exe";
            dialogFilter += "|" + Translator.String("global.dialog.allFiles") + " (*.*)|*.*";

            return new OpenFileDialog()
            {
                Title = Translator.String("global.dialog.selectMods"),
                Filter = dialogFilter,
                CheckPathExists = true,
                Multiselect = InstallDataStack.DialogMultiSelect
            };
        }

        private void ButtonSelect_OnClick(object sender, RoutedEventArgs e)
        {
            if (!DialogSelector.ShowDialog() ?? false)
                return;

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"INFO | {DialogSelector.FileName} selected, Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}", "CPMM");
#endif

            // File_modSelector.FileNames
        }
    }
}