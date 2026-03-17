using System;
using System.Reflection;
using Duckov.Economy;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Duckov.Economy.UI;
using ShopMasterExtremesModConfig;
using ShopMasterExtreme.Functions;
using ShopMasterExtreme.Configs;

namespace ShopMasterExtreme
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {

        private void Update()
        {
            Miscellaneous.GUI.MonitorGUIToggle();
        }

        private void OnDisable()
        {
            ModConfig.SaveModConfig();
            Miscellaneous.Harmony.TryUnpatch();
            Localization.TryUnloadLocallization();

            ModConfig.ModConfigReday = false;
            Miscellaneous.Harmony.patched = false;
            Miscellaneous.GUI.showUI = false;
        }

        private void Start()
        {
            Localization.TryLoadLocalization();
            Miscellaneous.Harmony.TryPatch();
            ModConfig.TrySetConfig();
            Config.TryLoadConfig();
        }

        private void OnGUI()
        {
            Miscellaneous.GUI.DrawGUI();
        }
    }
}
