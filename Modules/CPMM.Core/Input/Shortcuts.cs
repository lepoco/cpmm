// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

#nullable enable

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CPMM.Core.Input
{
    public class Shortcuts
    {
        /// <summary>
        /// Event triggered when shortcut is matched.
        /// </summary>
        public delegate void ShortcutEvent(object sender);

        public struct Shortcut
        {
            public IEnumerable<Key> Sequence { get; set; }

            public ShortcutEvent OnShortcutEvent { get; set; }
        }

        private readonly List<Key> _recentKeys = new();

        public IEnumerable<Shortcut> ShortcutsCollection = new List<Shortcut>();

        public void Initialize(Window? mainWindow)
        {
            if (mainWindow == null)
                return;

            mainWindow.KeyUp += MainWindow_KeyUp;
            _recentKeys.Clear();
        }

        private void MainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var focusedElement = FocusManager.GetFocusedElement(Application.Current.MainWindow!);

            if (focusedElement != null)
            {
                if (focusedElement.GetType() == typeof(TextBlock) || focusedElement.GetType() == typeof(ComboBox))

                    return;
            }

            _recentKeys.Add(e.Key);

            if (_recentKeys.Count > 15)
                _recentKeys.RemoveAt(0);

            InvokeEvents();
        }

        private void InvokeEvents()
        {
            foreach (var singleShortcut in ShortcutsCollection)
            {
                if (_recentKeys.Count > singleShortcut.Sequence.Count() - 1 && Enumerable.SequenceEqual(
                        _recentKeys.Skip(Math.Max(0, _recentKeys.Count() - singleShortcut.Sequence.Count())).ToArray(),
                        singleShortcut.Sequence))
                {
                    singleShortcut.OnShortcutEvent(this);
                }
            }
        }
    }
}