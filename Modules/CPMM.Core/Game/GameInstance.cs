// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Game
{
    public class GameInstance : IGame
    {
        public string Name { get; internal set; } = String.Empty;

        public string Version { get; internal set; } = String.Empty;

        GameInstance()
        {

        }

        public static async Task<IGame> FetchAsync()
        {
            throw new NotImplementedException();
        }
    }
}
