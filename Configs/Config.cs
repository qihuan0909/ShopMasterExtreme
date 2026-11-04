using ShopMasterExtreme.Functions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ShopMasterExtreme.Configs
{
    internal static class Config
    {
        internal static KeyCode keyCode;
        private static string configPath;

        internal static void TryLoadConfig()
        {
            string dllDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            configPath = System.IO.Path.Combine(dllDir, "Config.ini");

            if (System.IO.File.Exists(configPath))
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(configPath);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("ToggleKey=", StringComparison.OrdinalIgnoreCase))
                        {
                            string keyName = line.Substring("ToggleKey=".Length).Trim();
                            if (Enum.TryParse(keyName, out KeyCode parsed))
                                keyCode = parsed;
                            else
                                keyCode = KeyCode.Home;
                        }
                        else if (line.StartsWith("Loger.ShowLog=", StringComparison.OrdinalIgnoreCase))
                        {
                            bool.TryParse(line.Substring("Loger.ShowLog=".Length).Trim(), out Loger.ShowLog);
                        }
                        else if (line.StartsWith("Loger.ShowWarn=", StringComparison.OrdinalIgnoreCase))
                        {
                            bool.TryParse(line.Substring("Loger.ShowWarn=".Length).Trim(), out Loger.ShowWarn);
                        }
                        else if (line.StartsWith("Loger.ShowError=", StringComparison.OrdinalIgnoreCase))
                        {
                            bool.TryParse(line.Substring("Loger.ShowError=".Length).Trim(), out Loger.ShowError);
                        }
                        else if (line.StartsWith("ShowAllItems=", StringComparison.OrdinalIgnoreCase))
                        {
                            bool.TryParse(line.Substring("ShowAllItems=".Length).Trim(), out ShopStackManager.showAllItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loger.LogError(string.Format(Localization.Lang["Log_ConfigReadError"], ex));
                    keyCode = KeyCode.Home;
                }
            }
            else
            {
                keyCode = KeyCode.Home;
                SaveConfig();
            }

            Loger.Log(string.Format(Localization.Lang["Log_CurrentKeyInfo"], keyCode));
        }

        internal static void SaveConfig()
        {
            try
            {
                string content = $"# ShopMasterExtreme Configuration\n" +
                                 $"ToggleKey={keyCode}\n" +
                                 $"Loger.ShowLog={Loger.ShowLog}\n" +
                                 $"Loger.ShowWarn={Loger.ShowWarn}\n" +
                                 $"Loger.ShowError={Loger.ShowError}\n" +
                                 $"ShowAllItems={ShopStackManager.showAllItems}\n";
                System.IO.File.WriteAllText(configPath, content);
                Loger.Log(string.Format(Localization.Lang["Log_ConfigWriteInfo"], keyCode, Loger.ShowLog, ShopStackManager.showAllItems));
            }
            catch (Exception ex)
            {
                Loger.LogError(string.Format(Localization.Lang["Log_ConfigWriteError"], ex));
            }
        }
    }
}
