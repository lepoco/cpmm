// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using CPMM.Core.Mods;
using System;
using System.Linq;
using System.Windows;

namespace CPMM.Controls
{
    /// <summary>
    /// Card representing mod.
    /// </summary>
    public class ModCard : System.Windows.Controls.Control
    {
        /// <summary>
        /// Property for <see cref="Mod"/>.
        /// </summary>
        public static readonly DependencyProperty ModProperty = DependencyProperty.Register(nameof(Mod),
            typeof(IMod), typeof(ModCard), new PropertyMetadata(new Mod(), ModPropertyChangedCallback));

        /// <summary>
        /// Property for <see cref="Title"/>.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
            typeof(string), typeof(ModCard), new PropertyMetadata(String.Empty));

        /// <summary>
        /// Property for <see cref="SubTitle"/>.
        /// </summary>
        public static readonly DependencyProperty SubTitleProperty = DependencyProperty.Register(nameof(SubTitle),
            typeof(string), typeof(ModCard), new PropertyMetadata(String.Empty));

        public IMod Mod
        {
            get => (IMod)GetValue(ModProperty);
            set => SetValue(ModProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string SubTitle
        {
            get => (string)GetValue(SubTitleProperty);
            set => SetValue(SubTitleProperty, value);
        }

        private static void ModPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ModCard control) return;

            if (control.Mod is not IMod)
                return;

            control.Title = control.Mod.Name;
            control.SubTitle = control.Mod.Files.Count() + " files in archive '" + control.Mod.ArchiveName + "'";

        }
    }
}
