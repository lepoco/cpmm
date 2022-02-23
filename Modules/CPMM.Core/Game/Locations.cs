// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Game
{
    /// <summary>
    /// Default locations for game directory.
    /// </summary>
    public static class Locations
    {
        /// <summary>
        /// Suffix for saved games in user root directory.
        /// </summary>
        ///  <value><see langword="Saved Games\CD Projekt Red\Cyberpunk 2077"></see></value>
        public static string SavesSuffix => @"Saved Games\CD Projekt Red\Cyberpunk 2077";

        /// <summary>
        /// Suffix for game settings in user root directory.
        /// </summary>
        /// <value><see langword="AppData\Local\CD Projekt Red\Cyberpunk 2077"></see></value>
        public static string SettingsSuffix => @"AppData\Local\CD Projekt Red\Cyberpunk 2077";

        /// <summary>
        /// Default GOG Cyberpunk location.
        /// </summary>
        /// <value><see langword="C:\Program Files (x86)\GOG Galaxy\Games\Cyberpunk 2077"></see></value>
        public static string GogDefault => @"C:\Program Files (x86)\GOG Galaxy\Games\Cyberpunk 2077";

        /// <summary>
        /// Alternative GOG Cyberpunk location.
        /// </summary>
        /// <value><see langword="C:\Program Files\GOG Galaxy\Games\Cyberpunk 2077"></see></value>
        public static string GogAlternative => @"C:\Program Files\GOG Galaxy\Games\Cyberpunk 2077";

        /// <summary>
        /// Default Steam Cyberpunk location.
        /// </summary>
        /// <value><see langword="C:\Program Files (x86)\Steam\steamapps\common\Cyberpunk 2077"></see></value>
        public static string SteamDefault => @"C:\Program Files (x86)\Steam\steamapps\common\Cyberpunk 2077";

        /// <summary>
        /// Alternative Steam Cyberpunk location.
        /// </summary>
        /// <value><see langword="C:\Program Files (x86)\Steam\steamapps\common\Cyberpunk 2077"></see></value>
        public static string SteamAlternative => @"C:\Program Files (x86)\Steam\steamapps\common\Cyberpunk 2077";

        /// <summary>
        /// Common alternate user location.
        /// </summary>
        /// <value><see langword="C:\Games\Cyberpunk 2077"></see></value>
        public static string UserPrimary => @"C:\Games\Cyberpunk 2077";

        /// <summary>
        /// Common alternate user location.
        /// </summary>
        /// <value><see langword="D:\Games\Cyberpunk 2077"></see></value>
        public static string UserSecondary => @"D:\Games\Cyberpunk 2077";

    }
}
