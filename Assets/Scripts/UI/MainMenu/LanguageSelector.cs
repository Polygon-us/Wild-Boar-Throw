using UnityEngine.Localization.Settings;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace UI.MainMenu
{
    public class LanguageSelector : MonoBehaviour
    {
        public TMP_Dropdown dropdown;

        private async UniTaskVoid Start()
        {
            // Wait for the localization system to initialize
            await LocalizationSettings.InitializationOperation;

            // Generate list of available Locales
            var options = new List<TMP_Dropdown.OptionData>();
            int selected = 0;
            for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
            {
                var locale = LocalizationSettings.AvailableLocales.Locales[i];
                if (LocalizationSettings.SelectedLocale == locale)
                    selected = i;
                options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
            }

            dropdown.options = options;

            dropdown.value = selected;
            dropdown.onValueChanged.AddListener(LocaleSelected);
        }

        private static void LocaleSelected(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}