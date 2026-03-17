using ShopMasterExtreme.Functions;
using ShopMasterExtremesModConfig;
using System;
using UnityEngine;

namespace ShopMasterExtreme.Configs
{
    internal static class ModConfig
    {
        internal static bool ModConfigReday = false;
        private static readonly System.Collections.Generic.SortedDictionary<string, object> langMenuOptions =
        new System.Collections.Generic.SortedDictionary<string, object>
        {
            { "0. Automatic / 自动判断", "Auto" },
            { "1. Simplified Chinese / 简体中文", "Chinese" },
            { "2. English", "English" },
            { "3. Japanese / 日本語", "Japanese" },
            { "4. Korean / 한국어", "Korean" },
            { "5. German / Deutsch", "German" },
            { "6. French / Français", "French" },
            { "7. Russian / Русский", "Russian" },
            { "8. Spanish / Español", "Spanish" },
            { "9. Portuguese / Português", "Portuguese" }
        };

        internal static void TrySetConfig()
        {
            if (ModConfigAPI.IsAvailable())
            {
                while (ModConfigReday == false)
                {
                    try
                    {
                        ModConfigAPI.Initialize();
                        Loger.Log(string.Format(Localization.Lang["Log_RegisterConfig"]));

                        ModConfigAPI.SafeAddInputWithSlider(
                            "ShopMasterExtreme",
                            "RestockAmount",
                            "Shop Stack Amount | 商店库存数量",
                            typeof(int),
                            ShopStackManager.restockAmount,
                            new Vector2(1, 999)
                        );

                        ModConfigAPI.SafeAddDropdownList(
                            "ShopMasterExtreme",
                            "Language",
                            "Mod Language | 插件语言设置",
                            langMenuOptions,
                            typeof(string),
                            "Auto"
                        );

                        ModConfigAPI.SafeAddOnOptionsChangedDelegate(OnOptionChanged);

                        ShopStackManager.restockAmount = ModConfigAPI.SafeLoad("ShopMasterExtreme", "RestockAmount", 99);
                        Localization.ManualLanguage = ModConfigAPI.SafeLoad("ShopMasterExtreme", "Language", "Auto");
                        Loger.Log(string.Format(Localization.Lang["Log_RestockAmount"], ShopStackManager.restockAmount));
                        ModConfigReday = true;
                    }
                    catch (Exception ex)
                    {
                        Loger.LogError(string.Format(Localization.Lang["Log_ModConfigLoadError"], ex));
                        break;
                    }
                }
            }
            else
            {
                Loger.LogWarning(string.Format(Localization.Lang["Log_NoModConfig"]));
                ModConfigReday = true;
            }
        }

        private static void OnOptionChanged(string key)
        {
            if (key == $"{"ShopMasterExtreme"}_RestockAmount")
            {
                ShopStackManager.restockAmount = ModConfigAPI.SafeLoad("ShopMasterExtreme", "RestockAmount", 99);
                Loger.Log(string.Format(Localization.Lang["Log_RestockUpdated"], ShopStackManager.restockAmount));
                ShopStackManager.ForceRefreshAllShops();
            }
            else if (key == "ShopMasterExtreme_Language")
            {
                string newLang = ModConfigAPI.SafeLoad("ShopMasterExtreme", "Language", "Auto");

                if (newLang != Localization.ManualLanguage)
                {
                    Localization.ManualLanguage = newLang;
                    Loger.Log(string.Format(Localization.Lang["Log_ChangingLanguage"], newLang));

                    Miscellaneous.Harmony.TryUnpatch();
                    Localization.TryUnloadLocallization();

                    Localization.TryLoadLocalization();
                    Miscellaneous.Harmony.TryPatch();

                    Loger.Log(Localization.Lang["Log_StartComplete"]);
                }
            }
        }

        internal static void SaveModConfig()
        {
            if (ModConfigAPI.IsAvailable())
            {
                ModConfigAPI.SafeRemoveOnOptionsChangedDelegate(OnOptionChanged);
                Loger.Log(Localization.Lang["Log_ConfigRemoved"]);

                ModConfigAPI.SafeSave("ShopMasterExtreme", "RestockAmount", ShopStackManager.restockAmount);
                Loger.Log(string.Format(Localization.Lang["Log_ConfigSaved"], ShopStackManager.restockAmount));
            }
        }
    }
}
