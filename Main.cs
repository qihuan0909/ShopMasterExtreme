using System;
using System.Reflection;
using Duckov.Economy;
using UnityEngine;
using ShopMasterExtremesModConfig;
using TMPro;
using UnityEngine.UI;
using Duckov.Economy.UI;

namespace ShopMasterExtreme
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private static bool showAllItems = false;
        private bool updateReady = false;
        private bool ModConfigReday = false;
        private bool waitingForKey = false;
        private object harmonyInstance;
        private Type harmonyType;
        private Type harmonyMethodType;
        private static string configPath;
        private static KeyCode keyCode;

        private bool patched = false;
        private static bool showUI = false;
        private static int restockAmount = 99;

        private void Update()
        {
            TryPatch();
            TrySetConfig();

            if (Input.GetKeyDown(keyCode))
            {
                showUI = !showUI;
                Loger.Log($"[ShopMasterExtreme] 控制面板 {(showUI ? "打开" : "关闭")}");
            }
        }

        private void OnDisable()
        {
            if (ModConfigAPI.IsAvailable())
            {
                ModConfigAPI.SafeRemoveOnOptionsChangedDelegate(OnOptionChanged);
                Loger.Log("[ShopMasterExtreme] 已移除 ModConfig 事件委托");

                ModConfigAPI.SafeSave("ShopMasterExtreme", "RestockAmount", restockAmount);
                Loger.Log($"[ShopMasterExtreme] 已保存配置：RestockAmount = {restockAmount}");
            }

            TryUnpatch();

            updateReady = false;
            ModConfigReday = false;
            patched = false;
            showUI = false;
        }

        private void Start()
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
                        else if (line.StartsWith("ShowAllItems=", StringComparison.OrdinalIgnoreCase))
                        {
                            bool.TryParse(line.Substring("ShowAllItems=".Length).Trim(), out showAllItems);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loger.LogError($"[ShopMasterExtreme] 读取 Config.ini 出错: {ex}");
                    keyCode = KeyCode.Home;
                }
            }
            else
            {
                keyCode = KeyCode.Home;
                SaveConfig();
            }

            Loger.Log($"[ShopMasterExtreme] 当前控制面板热键为: {keyCode}");
        }

        private static void SaveConfig()
        {
            try
            {
                string content = $"# ShopMasterExtreme Configuration\n" +
                                 $"ToggleKey={keyCode}\n" +
                                 $"Loger.ShowLog={Loger.ShowLog}\n" +
                                 $"ShowAllItems={showAllItems}\n";
                System.IO.File.WriteAllText(configPath, content);
                Loger.Log($"[ShopMasterExtreme] 已保存 Config.ini：{keyCode}, ShowLog={Loger.ShowLog}, ShowAllItems={showAllItems}");
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] 写入 Config.ini 出错: {ex}");
            }
        }

        private void TrySetConfig()
        {
            if (ModConfigAPI.IsAvailable())
            {
                if (ModConfigReday)
                    return;

                ModConfigAPI.Initialize();
                Loger.Log("[ShopMasterExtreme] 检测到 ModConfig，正在注册配置项...");

                ModConfigAPI.SafeAddInputWithSlider(
                    "ShopMasterExtreme",
                    "RestockAmount",
                    "每次补货数量",
                    typeof(int),
                    restockAmount,
                    new Vector2(1, 999)
                );

                ModConfigAPI.SafeAddOnOptionsChangedDelegate(OnOptionChanged);

                restockAmount = ModConfigAPI.SafeLoad("ShopMasterExtreme", "RestockAmount", 99);
                Loger.Log($"[ShopMasterExtreme] 当前补货数量设定为 {restockAmount}");
                ModConfigReday = true;
            }
            else
            {
                Loger.LogWarning("[ShopMasterExtreme] 未检测到 ModConfig，将使用默认值 99");
                ModConfigReday = true;
            }
        }

        private void TryPatch()
        {
            if (updateReady)
                return;

            try
            {
                harmonyType = Type.GetType("HarmonyLib.Harmony, 0Harmony");
                harmonyMethodType = Type.GetType("HarmonyLib.HarmonyMethod, 0Harmony");
                if (harmonyType == null || harmonyMethodType == null)
                {
                    Loger.LogError("[ShopMasterExtreme] 未找到 Harmony 类型或 HarmonyMethod 类型！");
                    return;
                }

                harmonyInstance = Activator.CreateInstance(harmonyType, "com.hgxy.ShopMasterExtreme");

                var target = typeof(StockShop).GetMethod(
                    "DoRefreshStock",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                if (target == null)
                {
                    Loger.LogError("[ShopMasterExtreme] 找不到 StockShop.DoRefreshStock");
                    return;
                }

                var postfix = typeof(ModBehaviour).GetMethod(nameof(AfterRefresh),
                    BindingFlags.Public | BindingFlags.Static);

                var postfixHM = Activator.CreateInstance(harmonyMethodType, postfix);

                var patch = harmonyType.GetMethod("Patch", new Type[]
                {
                    typeof(MethodBase),
                    harmonyMethodType,
                    harmonyMethodType,
                    harmonyMethodType,
                    harmonyMethodType
                });

                patch.Invoke(harmonyInstance, new object[] { target, null, postfixHM, null, null });

                BulkPurchase.ApplyPatches(harmonyInstance, harmonyType, harmonyMethodType);

                Loger.Log("[ShopMasterExtreme] 启动完成");
                patched = true;
                updateReady = true;
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] Patch 失败: {ex}");
                patched = false;
            }
        }

        private void TryUnpatch()
        {
            try
            {
                if (harmonyInstance != null)
                {
                    harmonyType.GetMethod("UnpatchAll", new[] { typeof(string) })
                        .Invoke(harmonyInstance, new object[] { "com.hgxy.ShopMasterExtreme" });
                    Loger.Log("[ShopMasterExtreme] Harmony patch 已卸载");
                }
                GameObject.Destroy(BulkPurchase.myInputField.gameObject);
                patched = false;
                updateReady = false;
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] 卸载 Harmony 失败: {ex}");
            }
        }

        private void OnOptionChanged(string key)
        {
            if (key == $"{"ShopMasterExtreme"}_RestockAmount")
            {
                restockAmount = ModConfigAPI.SafeLoad("ShopMasterExtreme", "RestockAmount", 99);
                Loger.Log($"[ShopMasterExtreme] 补货数量更新为 {restockAmount}");
                ForceRefreshAllShops();
            }
        }

        private void ForceRefreshAllShops()
        {
            try
            {
                var allShops = GameObject.FindObjectsOfType<StockShop>();
                int count = 0;
                foreach (var shop in allShops)
                {
                    var method = typeof(StockShop).GetMethod("DoRefreshStock",
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    method?.Invoke(shop, null);
                    count++;
                }
                Loger.Log($"[ShopMasterExtreme] 已强制刷新所有商店（共 {count} 个）");
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] 强制刷新商店失败: {ex}");
            }
        }

        private void OnGUI()
        {
            if (!showUI) return;

            GUI.BeginGroup(new Rect(20, 20, 280, 380), "[ShopMasterExtreme 控制面板]", GUI.skin.window);

            Loger.ShowLog = GUI.Toggle(new Rect(10, 25, 230, 25), Loger.ShowLog, "启用日志输出");
            showAllItems = GUI.Toggle(new Rect(10, 55, 230, 25), showAllItems, "显示所有商店物品（仅对部分商店有效）");

            if (GUI.Button(new Rect(10, 95, 230, 30), patched ? "重新启动" : "启用补丁"))
            {
                if (patched)
                    TryUnpatch();
                else
                    TryPatch();
            }

            if (GUI.Button(new Rect(10, 135, 230, 30), "强制刷新所有商店"))
            {
                ForceRefreshAllShops();
            }

            GUI.Label(new Rect(10, 175, 230, 25), $"当前热键: {keyCode}");
            if (GUI.Button(new Rect(10, 205, 230, 30), "修改热键"))
            {
                waitingForKey = true;
                Loger.Log("[ShopMasterExtreme] 请按下要绑定的新键...");
            }

            if (waitingForKey)
            {
                GUI.Label(new Rect(10, 245, 230, 25), "请按任意键以绑定...");
                Event e = Event.current;
                if (e.isKey)
                {
                    keyCode = e.keyCode;
                    waitingForKey = false;
                    Loger.Log($"[ShopMasterExtreme] 新热键绑定为: {keyCode}");
                    SaveConfig();
                }
            }

            if (GUI.Button(new Rect(10, 285, 230, 30), "保存配置"))
            {
                SaveConfig();
            }

            GUI.EndGroup();
        }

        public static void AfterRefresh(StockShop __instance)
        {
            int amount = ModConfigAPI.IsAvailable()
            ? ModConfigAPI.SafeLoad("ShopMasterExtreme", "RestockAmount", restockAmount)
            : restockAmount;

            foreach (var e in __instance.entries)
            {
                if (showAllItems)
                {
                    e.Show = showAllItems;
                }
                e.CurrentStock = amount;
            }

            Loger.Log($"[ShopMasterExtreme] 商店 {__instance.MerchantID} 已补货至 {amount} 件 (显示所有物品: {showAllItems})");
        }
    }
}
