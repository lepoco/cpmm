// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Carlos Muñoz, Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

namespace CPMM.Core.Common
{
    internal static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => String.Empty,
                "" => String.Empty,
                _ => input[0].ToString().ToUpper() + input.Substring(1)
            };
    }
}