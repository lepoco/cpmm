// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Code;
using Lepo.i18n;
using System;
using System.Windows;

namespace CPMM.Views
{
    internal class ContainerData : ViewData
    {
        private string _loadingText = Translator.String("container.loading.default");
        public string LoadingText
        {
            get => _loadingText;
            set => UpdateProperty(ref _loadingText, value, nameof(LoadingText));
        }

        private Visibility _loadingVisibility = Visibility.Collapsed;
        public Visibility LoadingVisibility
        {
            get => _loadingVisibility;
            set => UpdateProperty(ref _loadingVisibility, value, nameof(LoadingVisibility));
        }

        private Visibility _rootGridVisibility = Visibility.Visible;
        public Visibility RootGridVisibility
        {
            get => _rootGridVisibility;
            set => UpdateProperty(ref _rootGridVisibility, value, nameof(RootGridVisibility));
        }
    }
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        internal ContainerData ContainerDataStack { get; } = new();

        public Container()
        {
            if (GH.Settings.Options.UseMica)
                WPFUI.Background.Manager.Apply(this);

            InitializeComponent();

            if (!GH.Settings.Options.Initialized)
                Initializer.Run();

            InitializeData();
        }

        public void ChangeDisplayedLanguage(string language, string page = "")
        {
            // TODO: Here would have to refresh all the views instances and texts in the Container.

            Translator.Flush();
            RootNavigation.FlushPages();

            GH.SetLanguage(language);

            if (!String.IsNullOrEmpty(page))
                RootNavigation.Navigate(page, true);
        }

        private void InitializeData()
        {
            DataContext = ContainerDataStack;
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard");
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            // TRAY
        }
    }
}
