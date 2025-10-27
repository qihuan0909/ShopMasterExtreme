using System;
using System.Reflection;
using Duckov.Economy;
using UnityEngine;
using ShopStack99sModConfig;

namespace ShopStack99
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private static readonly bool ShowLog = true;
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
                Log($"[99ShopStack] 控制面板 {(showUI ? "打开" : "关闭")}");
            }
        }

        private void OnDisable()
        {
            if (ModConfigAPI.IsAvailable())
            {
                ModConfigAPI.SafeRemoveOnOptionsChangedDelegate(OnOptionChanged);
                Log("[99ShopStack] 已移除 ModConfig 事件委托");

                ModConfigAPI.SafeSave("ShopStack99", "RestockAmount", restockAmount);
                Log($"[99ShopStack] 已保存配置：RestockAmount = {restockAmount}");
            }

            TryUnpatch();

            updateReady = false;
            ModConfigReday = false;
            patched = false;
            showUI = false;
        }

        private static void Log(string word)
        {
            if (ShowLog)
            {
                Debug.Log(word);
            }
        }

        private static void LogWarning(string word)
        {
            if (ShowLog)
            {
                Debug.LogWarning(word);
            }
        }

        private static void LogError(string word)
        {
            if (ShowLog)
            {
                Debug.LogError(word);
            }
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
                    }
                }
                catch (Exception ex)
                {
                    LogError($"[99ShopStack] 读取 Config.ini 出错: {ex}");
                    keyCode = KeyCode.Home;
                }
            }
            else
            {
                keyCode = KeyCode.Home;
                SaveConfig();
            }

            Log($"[99ShopStack] 当前控制面板热键为: {keyCode}");
        }

        private static void SaveConfig()
        {
            try
            {
                string content = $"# ShopStack99 Configuration\nToggleKey={keyCode}\n";
                System.IO.File.WriteAllText(configPath, content);
                Log($"[99ShopStack] 已保存 Config.ini：{keyCode}");
            }
            catch (Exception ex)
            {
                LogError($"[99ShopStack] 写入 Config.ini 出错: {ex}");
            }
        }

        private void TrySetConfig()
        {
            if (ModConfigAPI.IsAvailable())
            {
                if (ModConfigReday)
                    return;

                ModConfigAPI.Initialize();
                Log("[99ShopStack] 检测到 ModConfig，正在注册配置项...");

                ModConfigAPI.SafeAddInputWithSlider(
                    "ShopStack99",
                    "RestockAmount",
                    "每次补货数量",
                    typeof(int),
                    restockAmount,
                    new Vector2(1, 999)
                );

                ModConfigAPI.SafeAddOnOptionsChangedDelegate(OnOptionChanged);

                restockAmount = ModConfigAPI.SafeLoad("ShopStack99", "RestockAmount", 99);
                Log($"[99ShopStack] 当前补货数量设定为 {restockAmount}");
                ModConfigReday = true;
            }
            else
            {
                LogWarning("[99ShopStack] 未检测到 ModConfig，将使用默认值 99");
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
                    LogError("[99ShopStack] 未找到 Harmony 类型或 HarmonyMethod 类型！");
                    return;
                }

                harmonyInstance = Activator.CreateInstance(harmonyType, "com.hgxy.99ShopStack");

                var target = typeof(StockShop).GetMethod(
                    "DoRefreshStock",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                if (target == null)
                {
                    LogError("[99ShopStack] 找不到 StockShop.DoRefreshStock");
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

                Log("[99ShopStack] 启动完成");
                patched = true;
                updateReady = true;
            }
            catch (Exception ex)
            {
                LogError($"[99ShopStack] Patch 失败: {ex}");
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
                        .Invoke(harmonyInstance, new object[] { "com.hgxy.99ShopStack" });
                    Log("[99ShopStack] Harmony patch 已卸载");
                }
                patched = false;
                updateReady = false;
            }
            catch (Exception ex)
            {
                LogError($"[99ShopStack] 卸载 Harmony 失败: {ex}");
            }
        }

        private void OnOptionChanged(string key)
        {
            if (key == $"{"ShopStack99"}_RestockAmount")
            {
                restockAmount = ModConfigAPI.SafeLoad("ShopStack99", "RestockAmount", 99);
                Log($"[99ShopStack] 补货数量更新为 {restockAmount}");

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
                Log($"[99ShopStack] 已强制刷新所有商店（共 {count} 个）");
            }
            catch (Exception ex)
            {
                LogError($"[99ShopStack] 强制刷新商店失败: {ex}");
            }
        }

        private void OnGUI()
        {
            if (!showUI) return;

            GUI.BeginGroup(new Rect(20, 20, 260, 300), "[99ShopStack 控制面板]", GUI.skin.window);

            if (GUI.Button(new Rect(10, 25, 230, 30), patched ? "重新启动" : "启用补丁"))
            {
                if (patched)
                    TryUnpatch();
                else
                    TryPatch();
            }

            if (GUI.Button(new Rect(10, 70, 230, 30), "强制刷新所有商店"))
            {
                ForceRefreshAllShops();
            }

            GUI.Label(new Rect(10, 120, 230, 25), $"当前热键: {keyCode}");
            if (GUI.Button(new Rect(10, 150, 230, 30), "修改热键"))
            {
                waitingForKey = true;
                Log("[99ShopStack] 请按下要绑定的新键...");
            }

            if (waitingForKey)
            {
                GUI.Label(new Rect(10, 190, 230, 25), "请按任意键以绑定...");
                Event e = Event.current;
                if (e.isKey)
                {
                    keyCode = e.keyCode;
                    waitingForKey = false;
                    Log($"[99ShopStack] 新热键绑定为: {keyCode}");

                    SaveConfig(); 
                }
            }

            GUI.EndGroup();
        }

        public static void AfterRefresh(StockShop __instance)
        {
            int amount = ModConfigAPI.IsAvailable()
            ? ModConfigAPI.SafeLoad("ShopStack99", "RestockAmount", restockAmount)
            : restockAmount;

            foreach (var e in __instance.entries)
            {
                e.CurrentStock = amount;
            }

            Log($"[99ShopStack] 商店 {__instance.MerchantID} 已补货至 99 件");
        }
    }
}
