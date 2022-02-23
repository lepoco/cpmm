// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CPMM.Code
{
    /// <summary>
    /// Contains information about supported languages.
    /// </summary>
    internal static class Languages
    {
        public static readonly IDictionary<string, string> Map = new Dictionary<string, string>
        {
            // Keep EN first
            {"en_US", "English"},

            // Keep the rest alphabetically by codes
            {"cs_CZ",  "Čeština"}, // Czech
            {"de_DE",  "Deutsch"}, // German
            {"es_ES",  "Español"}, // Spanish
            {"fr_FR",  "Français"}, // French
            {"hu_HU",  "Magyar"}, // Hungarian
            {"it_IT",  "Italiana" }, // Italian
            {"ja_JP",  "日本"}, // Japanese
            {"ko_KR",  "한국어"}, // Korean
            {"pl_PL",  "Polski"}, // Polish
            {"pt_BR",  "Português" }, // Portuguese
            {"ru_RU",  "Pусский" }, // Russian
            {"tr_TR",  "Türk" }, // Turkish
            {"zh_CN",  "中国人" }, // Chinese
        };

        public static IEnumerable<string> GetNames()
        {
            var namesCollection = new List<string>();

            foreach (var singleLanguage in Map)
                namesCollection.Add(singleLanguage.Value);

            return namesCollection.ToArray();
        }

        public static IEnumerable<string> GetCodes()
        {
            var codesCollection = new List<string>();

            foreach (var singleLanguage in Map)
                codesCollection.Add(singleLanguage.Key);

            return codesCollection.ToArray();
        }

        public static string GetCodeByName(string name)
        {
            name = name.ToLower().Trim();

            return Map.Where(language => language.Value.ToLower().Trim() == name).FirstOrDefault(new KeyValuePair<string, string>(String.Empty, String.Empty)).Value;
        }

        public static string GetNameByCode(string code)
        {
            if (!Map.ContainsKey(code))
                return String.Empty;

            return Map[code];
        }

        public static int GetIndexByName(string name)
        {
            name = name.ToLower().Trim();

            for (int i = 0; i < Map.Count; i++)
                if (Map.ElementAt(i).Value.ToLower().Trim() == name)
                    return i;

            return 0;
        }

        public static int GetIndexByCode(string code)
        {
            code = code.ToLower().Trim();

            for (int i = 0; i < Map.Count; i++)
                if (Map.ElementAt(i).Key.ToLower().Trim() == code)
                    return i;

            return 0;
        }
    }
}