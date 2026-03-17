using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ShopMasterExtreme
{
    public static class Localization
    {
        internal static string ManualLanguage = "Auto";
        internal static bool LocalizationLoaded = false;
        internal static Dictionary<string, string> Lang = new Dictionary<string, string>();

        internal static void TryLoadLocalization()
        {
            if (LocalizationLoaded)
                return;

            if (ManualLanguage != "Auto")
            {
                LoadSpecificLanguage(ManualLanguage);
                LocalizationLoaded = true;
                return;
            }

            try
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.ChineseSimplified:
                    case SystemLanguage.ChineseTraditional:
                    case SystemLanguage.Chinese:
                        LoadChinese();
                        break;

                    case SystemLanguage.Japanese:
                        LoadJapanese();
                        break;

                    case SystemLanguage.Korean:
                        LoadKorean();
                        break;

                    case SystemLanguage.German:
                        LoadGerman();
                        break;

                    case SystemLanguage.French:
                        LoadFrench();
                        break;

                    case SystemLanguage.Russian:
                        LoadRussian();
                        break;

                    case SystemLanguage.Spanish:
                        LoadSpanish();
                        break;

                    case SystemLanguage.Portuguese:
                        LoadPortuguese();
                        break;

                    default:
                        LoadEnglish();
                        break;
                }
                LocalizationLoaded = true;
            }
            catch (Exception ex)
            {
                Loger.LogWarning($"[ShopMasterExtreme] Language init error: {ex.Message}");
                LocalizationLoaded = true;
            }
        }
        private static void LoadEnglish()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Bulk purchase module mounted successfully";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Failed to mount bulk purchase module: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] UI assembly or event binding failed: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Out of stock, purchase failed";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Not enough money, purchase failed";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] Purchased {0} x {1}";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Bulk purchase error: {0}";
            Lang["BulkPurchaseNotification1"] = "Out of Stock";
            Lang["BulkPurchaseNotification2"] = "Not Enough Money";
            Lang["BulkPurchaseNotification3"] = "Successfully purchased {0} x {1}";

            Lang["Mod_ControlPanelTitle"] = "[ShopMasterExtreme Control Panel]";
            Lang["Mod_EnableLog"] = "Enable Log Output";
            Lang["Mod_ShowAllItems"] = "Show All Items (Only works for some shops)";
            Lang["Mod_Reboot"] = "Restart";
            Lang["Mod_EnablePatch"] = "Enable Patch";
            Lang["Mod_ForceRefresh"] = "Force Refresh All Shops";
            Lang["Mod_CurrentKey"] = "Current Hotkey: {0}";
            Lang["Mod_ChangeKey"] = "Change Hotkey";
            Lang["Mod_WaitingKey"] = "Press any key to bind...";
            Lang["Mod_SaveConfig"] = "Save Config";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Control Panel {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] Removed ModConfig event delegate";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Config saved: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Error reading Config.ini: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Current control panel hotkey: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini saved: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Error writing Config.ini: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Error while trying load ModConfig: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig detected, registering items...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Restock amount set to {0}";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Language changing to: {0}... Rebooting Module";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig not found, using default value 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony not found, please subscribe to Harmony!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] Target method StockShop.DoRefreshStock not found";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Startup complete";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Patch failed: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Harmony patch uninstalled";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Failed to uninstall Harmony: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Restock amount updated to {0}";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Forced refresh for all shops ({0} total)";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Failed to force refresh shops: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Please press the new key to bind...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] New hotkey bound to: {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Shop {0} restocked to {1} units (ShowAllItems: {2})";
        }

        private static void LoadChinese()
        {
            Lang["BulkPurchase1"] = "[商店终极功能拓展] 批量购买模块已成功挂载";
            Lang["BulkPurchase2"] = "[商店终极功能拓展] 批量购买模块挂载失败: {0}";
            Lang["BulkPurchase3"] = "[商店终极功能拓展] UI组装或事件绑定失败: {0}";
            Lang["BulkPurchase4"] = "[商店终极功能拓展] 库存不足，购买失败";
            Lang["BulkPurchase5"] = "[商店终极功能拓展] 金钱不足，购买失败";
            Lang["BulkPurchase6"] = "[商店终极功能拓展] 购买 {0} 个 {1} ";
            Lang["BulkPurchase7"] = "[商店终极功能拓展] 批量购买出错: {0}";
            Lang["BulkPurchaseNotification1"] = "库存不足";
            Lang["BulkPurchaseNotification2"] = "金钱不足";
            Lang["BulkPurchaseNotification3"] = "成功购买了 {0} 个 {1}";

            Lang["Mod_ControlPanelTitle"] = "[商店终极功能拓展 控制面板]";
            Lang["Mod_EnableLog"] = "启用日志输出";
            Lang["Mod_ShowAllItems"] = "显示所有商店物品（仅对部分商店有效）";
            Lang["Mod_Reboot"] = "重新启动";
            Lang["Mod_EnablePatch"] = "启用补丁";
            Lang["Mod_ForceRefresh"] = "强制刷新所有商店";
            Lang["Mod_CurrentKey"] = "当前热键: {0}";
            Lang["Mod_ChangeKey"] = "修改热键";
            Lang["Mod_WaitingKey"] = "请按任意键以绑定...";
            Lang["Mod_SaveConfig"] = "保存配置";

            Lang["Log_PanelStatus"] = "[商店终极功能拓展] 控制面板 {0}";
            Lang["Log_ConfigRemoved"] = "[商店终极功能拓展] 已移除 ModConfig 事件委托";
            Lang["Log_ConfigSaved"] = "[商店终极功能拓展] 已保存配置：RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[商店终极功能拓展] 读取 Config.ini 出错: {0}";
            Lang["Log_CurrentKeyInfo"] = "[商店终极功能拓展] 当前控制面板热键为: {0}";
            Lang["Log_ConfigWriteInfo"] = "[商店终极功能拓展] 已保存 Config.ini：{0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[商店终极功能拓展] 写入 Config.ini 出错: {0}";
            Lang["Log_ModConfigLoadError"] = "[商店终极功能拓展] 在尝试读取ModConfig时出错: {0}";
            Lang["Log_RegisterConfig"] = "[商店终极功能拓展] 检测到 ModConfig，正在注册配置项...";
            Lang["Log_RestockAmount"] = "[商店终极功能拓展] 当前补货数量设定为 {0}";
            Lang["Log_ChangingLanguage"] = "[商店终极功能拓展] 语言切换至: {0}... 正在重启。";
            Lang["Log_NoModConfig"] = "[商店终极功能拓展] 未检测到 ModConfig，将使用默认值 99";
            Lang["Log_HarmonyError"] = "[商店终极功能拓展] 未找到 Harmony 类型或 HarmonyMethod 类型，请订阅Harmony！";
            Lang["Log_TargetNotFound"] = "[商店终极功能拓展] 找不到 StockShop.DoRefreshStock";
            Lang["Log_StartComplete"] = "[商店终极功能拓展] 启动完成";
            Lang["Log_PatchError"] = "[商店终极功能拓展] Patch 失败: {0}";
            Lang["Log_UnpatchSuccess"] = "[商店终极功能拓展] Harmony patch 已卸载";
            Lang["Log_UnpatchError"] = "[商店终极功能拓展] 卸载 Harmony 失败: {0}";
            Lang["Log_RestockUpdated"] = "[商店终极功能拓展] 补货数量更新为 {0}";
            Lang["Log_ForceRefreshSuccess"] = "[商店终极功能拓展] 已强制刷新所有商店（共 {0} 个）";
            Lang["Log_ForceRefreshError"] = "[商店终极功能拓展] 强制刷新商店失败: {0}";
            Lang["Log_WaitKeyPrompt"] = "[商店终极功能拓展] 请按下要绑定的新键...";
            Lang["Log_NewKeyBind"] = "[商店终极功能拓展] 新热键绑定为: {0}";
            Lang["Log_ShopRestocked"] = "[商店终极功能拓展] 商店 {0} 已补货至 {1} 件 (显示所有物品: {2})";
        }

        private static void LoadJapanese()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] 一括購入モジュールが正常に読み込まれました";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] 一括購入モジュールの読み込みに失敗しました: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] UIアセンブリまたはイベントバインドに失敗しました: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] 在庫不足のため、購入に失敗しました";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] 所持金不足のため、購入に失敗しました";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] {1} を {0} 個購入しました";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] 一括購入エラー: {0}";
            Lang["BulkPurchaseNotification1"] = "在庫不足";
            Lang["BulkPurchaseNotification2"] = "所持金不足";
            Lang["BulkPurchaseNotification3"] = "{1} を {0} 個購入しました";

            Lang["Mod_ControlPanelTitle"] = "[ShopMasterExtreme コントロールパネル]";
            Lang["Mod_EnableLog"] = "ログ出力を有効にする";
            Lang["Mod_ShowAllItems"] = "全商品を表示（一部のショップのみ有効）";
            Lang["Mod_Reboot"] = "再起動";
            Lang["Mod_EnablePatch"] = "パッチを有効にする";
            Lang["Mod_ForceRefresh"] = "全ショップを強制更新";
            Lang["Mod_CurrentKey"] = "現在のホットキー: {0}";
            Lang["Mod_ChangeKey"] = "ホットキーを変更";
            Lang["Mod_WaitingKey"] = "任意のキーを押してバインドしてください...";
            Lang["Mod_SaveConfig"] = "設定を保存";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] コントロールパネル {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] ModConfig イベントデリゲートを削除しました";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] 設定を保存しました: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Config.ini の読み込みエラー: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] 現在のコントロールパネルのホットキー: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini を保存しました: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Config.ini の書き込みエラー: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] ModConfigの読み込み中にエラーが発生しました: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig を検出しました。設定項目を登録中...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] 補充数を {0} に設定しました";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] 言語を {0} に変更中... モジュールを再起動しています";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig が見つかりません。デフォルト値 99 を使用します";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony が見つかりません。Harmony をサブスクライブしてください！";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] ターゲットメソッド StockShop.DoRefreshStock が見つかりません";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] 起動完了";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] パッチ適用失敗: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Harmony パッチをアンインストールしました";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Harmony のアンインストールに失敗しました: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] 補充数が {0} に更新されました";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] 全ショップ（計 {0} 箇所）を強制更新しました";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] ショップの強制更新に失敗しました: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] バインドする新しいキーを押してください...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] 新しいホットキーを {0} に設定しました";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] ショップ {0} の在庫を {1} 個に補充しました (全商品表示: {2})";
        }

        private static void LoadKorean()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] 일괄 구매 모듈이 성공적으로 로드되었습니다";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] 일괄 구매 모듈 로드 실패: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] UI 어셈블리 또는 이벤트 바인딩 실패: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] 재고 부족으로 구매에 실패했습니다";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] 소지금 부족으로 구매에 실패했습니다";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] {1}을(를) {0}개 구매했습니다";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] 일괄 구매 오류: {0}";
            Lang["BulkPurchaseNotification1"] = "재고 부족";
            Lang["BulkPurchaseNotification2"] = "소지금 부족";
            Lang["BulkPurchaseNotification3"] = "{1} {0}개를 성공적으로 구매했습니다";

            Lang["Mod_ControlPanelTitle"] = "[ShopMasterExtreme 제어 패널]";
            Lang["Mod_EnableLog"] = "로그 출력 활성화";
            Lang["Mod_ShowAllItems"] = "모든 품목 표시 (일부 상점에만 해당)";
            Lang["Mod_Reboot"] = "재시작";
            Lang["Mod_EnablePatch"] = "패치 활성화";
            Lang["Mod_ForceRefresh"] = "모든 상점 강제 새로고침";
            Lang["Mod_CurrentKey"] = "현재 단축키: {0}";
            Lang["Mod_ChangeKey"] = "단축키 변경";
            Lang["Mod_WaitingKey"] = "바인딩할 키를 누르세요...";
            Lang["Mod_SaveConfig"] = "설정 저장";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] 제어 패널 {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] ModConfig 이벤트 대리자를 제거했습니다";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] 설정이 저장되었습니다: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Config.ini 읽기 오류: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] 현재 제어 패널 단축키: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini 저장됨: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Config.ini 쓰기 오류: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] ModConfig를 불러오는 중 오류가 발생했습니다: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig 감지됨, 설정 항목 등록 중...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] 보충 수량이 {0}으로 설정되었습니다";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] 언어를 {0}(으)로 변경 중... 모듈을 재부팅하는 중";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig를 찾을 수 없습니다. 기본값 99를 사용합니다";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony를 찾을 수 없습니다. Harmony를 구독해 주세요!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] 대상 메서드 StockShop.DoRefreshStock을 찾을 수 없습니다";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] 시작 완료";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] 패치 실패: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Harmony 패치가 제거되었습니다";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Harmony 제거 실패: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] 보충 수량이 {0}으로 업데이트되었습니다";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] 모든 상점(총 {0}개)을 강제 새로고침했습니다";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] 상점 강제 새로고침 실패: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] 바인딩할 새 키를 눌러주세요...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] 새 단축키가 {0}(으)로 바인딩되었습니다";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] 상점 {0}의 재고가 {1}개로 보충되었습니다 (모든 품목 표시: {2})";
        }

        private static void LoadGerman()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Sammelkauf-Modul erfolgreich geladen";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Sammelkauf-Modul konnte nicht geladen werden: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] UI-Assembly oder Event-Binding fehlgeschlagen: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Nicht vorrätig, Kauf fehlgeschlagen";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Nicht genug Geld, Kauf fehlgeschlagen";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] {0} x {1} gekauft";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Fehler beim Sammelkauf: {0}";
            Lang["BulkPurchaseNotification1"] = "Nicht vorrätig";
            Lang["BulkPurchaseNotification2"] = "Nicht genug Geld";
            Lang["BulkPurchaseNotification3"] = "{0} x {1} erfolgreich gekauft";

            Lang["Mod_ControlPanelTitle"] = "[ShopMasterExtreme Kontrollpanel]";
            Lang["Mod_EnableLog"] = "Log-Ausgabe aktivieren";
            Lang["Mod_ShowAllItems"] = "Alle Artikel anzeigen (nur für einige Shops)";
            Lang["Mod_Reboot"] = "Neustart";
            Lang["Mod_EnablePatch"] = "Patch aktivieren";
            Lang["Mod_ForceRefresh"] = "Alle Shops zwangsweise aktualisieren";
            Lang["Mod_CurrentKey"] = "Aktueller Hotkey: {0}";
            Lang["Mod_ChangeKey"] = "Hotkey ändern";
            Lang["Mod_WaitingKey"] = "Drücken Sie eine beliebige Taste zum Binden...";
            Lang["Mod_SaveConfig"] = "Konfiguration speichern";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Kontrollpanel {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] ModConfig-Event-Delegat entfernt";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Konfiguration gespeichert: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Fehler beim Lesen der Config.ini: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Aktueller Hotkey für Kontrollpanel: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini gespeichert: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Fehler beim Schreiben der Config.ini: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Fehler beim Laden der ModConfig: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig erkannt, registriere Elemente...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Nachfüllmenge auf {0} gesetzt";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Sprache wird auf {0} geändert... Modul wird neu gestartet";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig nicht gefunden, verwende Standardwert 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony nicht gefunden, bitte abonniere Harmony!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] Zielmethode StockShop.DoRefreshStock nicht gefunden";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Start abgeschlossen";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Patch fehlgeschlagen: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Harmony-Patch deinstalliert";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Harmony-Deinstallation fehlgeschlagen: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Nachfüllmenge auf {0} aktualisiert";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Aktualisierung für alle Shops erzwungen ({0} insgesamt)";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Erzwungene Shop-Aktualisierung fehlgeschlagen: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Bitte drücken Sie die neue Taste zum Binden...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] Neuer Hotkey gebunden an: {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Shop {0} auf {1} Einheiten nachgefüllt (Alle Artikel anzeigen: {2})";
        }

        private static void LoadFrench()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Module d'achat en gros chargé avec succès";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Échec du chargement du module d'achat en gros : {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] Échec de l'assemblage de l'UI ou de la liaison d'événement : {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Rupture de stock, achat échoué";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Pas assez d'argent, achat échoué";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] Acheté {0} x {1}";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Erreur d'achat en gros : {0}";
            Lang["BulkPurchaseNotification1"] = "Rupture de stock";
            Lang["BulkPurchaseNotification2"] = "Pas assez d'argent";
            Lang["BulkPurchaseNotification3"] = "Achat réussi de {0} x {1}";

            Lang["Mod_ControlPanelTitle"] = "[Panneau de Contrôle ShopMasterExtreme]";
            Lang["Mod_EnableLog"] = "Activer la sortie du journal";
            Lang["Mod_ShowAllItems"] = "Afficher tous les articles (uniquement pour certains magasins)";
            Lang["Mod_Reboot"] = "Redémarrer";
            Lang["Mod_EnablePatch"] = "Activer le correctif";
            Lang["Mod_ForceRefresh"] = "Forcer l'actualisation de tous les magasins";
            Lang["Mod_CurrentKey"] = "Raccourci actuel : {0}";
            Lang["Mod_ChangeKey"] = "Modifier le raccourci";
            Lang["Mod_WaitingKey"] = "Appuyez sur n'importe quelle touche pour lier...";
            Lang["Mod_SaveConfig"] = "Enregistrer la config";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Panneau de contrôle {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] Délégué d'événement ModConfig supprimé";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Configuration enregistrée : RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Erreur de lecture de Config.ini : {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Raccourci actuel du panneau de contrôle : {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini enregistré : {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Erreur d'écriture de Config.ini : {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Erreur lors du chargement de ModConfig : {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig détecté, enregistrement des éléments...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Quantité de réapprovisionnement fixée à {0}";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Changement de langue vers : {0}... Redémarrage du module";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig non trouvé, utilisation de la valeur par défaut 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony non trouvé, veuillez vous abonner à Harmony !";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] Méthode cible StockShop.DoRefreshStock introuvable";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Démarrage terminé";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Échec du correctif : {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Correctif Harmony désinstallé";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Échec de la désinstallation de Harmony : {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Quantité de réapprovisionnement mise à jour à {0}";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Actualisation forcée de tous les magasins ({0} au total)";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Échec de l'actualisation forcée des magasins : {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Veuillez appuyer sur la nouvelle touche pour lier...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] Nouveau raccourci lié à : {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Magasin {0} réapprovisionné à {1} unités (AfficherTousArticles : {2})";
        }

        private static void LoadRussian()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Модуль оптовых закупок успешно загружен";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Не удалось загрузить модуль оптовых закупок: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] Ошибка сборки UI или привязки событий: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Нет в наличии, покупка не удалась";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Недостаточно денег, покупка не удалась";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] Куплено {1} (x{0})";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Ошибка оптовой закупки: {0}";
            Lang["BulkPurchaseNotification1"] = "Нет в наличии";
            Lang["BulkPurchaseNotification2"] = "Недостаточно денег";
            Lang["BulkPurchaseNotification3"] = "Успешно куплено {1} (x{0})";

            Lang["Mod_ControlPanelTitle"] = "[Панель управления ShopMasterExtreme]";
            Lang["Mod_EnableLog"] = "Включить вывод логов";
            Lang["Mod_ShowAllItems"] = "Показать все товары (только для некоторых магазинов)";
            Lang["Mod_Reboot"] = "Перезагрузка";
            Lang["Mod_EnablePatch"] = "Включить патч";
            Lang["Mod_ForceRefresh"] = "Принудительно обновить все магазины";
            Lang["Mod_CurrentKey"] = "Текущая клавиша: {0}";
            Lang["Mod_ChangeKey"] = "Изменить клавишу";
            Lang["Mod_WaitingKey"] = "Нажмите любую клавишу для привязки...";
            Lang["Mod_SaveConfig"] = "Сохранить конфиг";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Панель управления {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] Удален делегат события ModConfig";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Конфигурация сохранена: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Ошибка чтения Config.ini: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Текущая клавиша панели управления: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini сохранен: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Ошибка записи Config.ini: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Ошибка при загрузке ModConfig: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] Обнаружен ModConfig, регистрация элементов...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Количество пополнения установлено на {0}";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Смена языка на: {0}... Перезагрузка модуля";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig не найден, используется значение по умолчанию 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony не найден, пожалуйста, подпишитесь на Harmony!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] Целевой метод StockShop.DoRefreshStock не найден";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Запуск завершен";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Ошибка патча: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Патч Harmony удален";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Не удалось удалить Harmony: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Количество пополнения обновлено до {0}";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Принудительное обновление всех магазинов (всего: {0})";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Не удалось принудительно обновить магазины: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Пожалуйста, нажмите новую клавишу для привязки...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] Новая клавиша привязана к: {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Магазин {0} пополнен до {1} ед. (ПоказатьВсеТовары: {2})";
        }

        private static void LoadSpanish()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Módulo de compra a granel montado con éxito";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Error al montar el módulo de compra a granel: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] Error en el ensamblaje de la IU o en la vinculación de eventos: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Sin existencias, error en la compra";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Dinero insuficiente, error en la compra";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] Comprado {0} x {1}";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Error en la compra a granel: {0}";
            Lang["BulkPurchaseNotification1"] = "Sin Existencias";
            Lang["BulkPurchaseNotification2"] = "Dinero Insuficiente";
            Lang["BulkPurchaseNotification3"] = "Se ha comprado con éxito {0} x {1}";

            Lang["Mod_ControlPanelTitle"] = "[Panel de Control de ShopMasterExtreme]";
            Lang["Mod_EnableLog"] = "Habilitar Salida de Registro";
            Lang["Mod_ShowAllItems"] = "Mostrar Todos los Artículos (Solo funciona en algunas tiendas)";
            Lang["Mod_Reboot"] = "Reiniciar";
            Lang["Mod_EnablePatch"] = "Habilitar Parche";
            Lang["Mod_ForceRefresh"] = "Forzar Actualización de Todas las Tiendas";
            Lang["Mod_CurrentKey"] = "Tecla de Acceso Rápido Actual: {0}";
            Lang["Mod_ChangeKey"] = "Cambiar Tecla de Acceso Rápido";
            Lang["Mod_WaitingKey"] = "Presione cualquier tecla para vincular...";
            Lang["Mod_SaveConfig"] = "Guardar Configuración";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Panel de Control {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] Se eliminó el delegado de eventos ModConfig";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Configuración guardada: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Error al leer Config.ini: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Tecla de acceso rápido actual: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini guardado: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Error al escribir Config.ini: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Error al intentar cargar ModConfig: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig detectado, registrando elementos...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Cantidad de reabastecimiento establecida en {0}";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Cambiando el idioma a: {0}... Reiniciando el módulo";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig no encontrado, usando valor predeterminado 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] ¡Harmony no encontrado, por favor suscríbete a Harmony!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] No se encontró el método objetivo StockShop.DoRefreshStock";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Inicio completado";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Error al aplicar el parche: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Parche de Harmony desinstalado";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Error al desinstalar Harmony: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Cantidad de reabastecimiento actualizada a {0}";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Actualización forzada para todas las tiendas ({0} en total)";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Error al forzar la actualización de las tiendas: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Por favor, presione la nueva tecla para vincular...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] Nueva tecla vinculada a: {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Tienda {0} reabastecida a {1} unidades (MostrarTodosArtículos: {2})";
        }

        private static void LoadPortuguese()
        {
            Lang["BulkPurchase1"] = "[ShopMasterExtreme] Módulo de compra em massa montado com sucesso";
            Lang["BulkPurchase2"] = "[ShopMasterExtreme] Falha ao montar módulo de compra em massa: {0}";
            Lang["BulkPurchase3"] = "[ShopMasterExtreme] Falha na montagem da UI ou vinculação de eventos: {0}";
            Lang["BulkPurchase4"] = "[ShopMasterExtreme] Fora de estoque, falha na compra";
            Lang["BulkPurchase5"] = "[ShopMasterExtreme] Dinheiro insuficiente, falha na compra";
            Lang["BulkPurchase6"] = "[ShopMasterExtreme] Comprou {0} x {1}";
            Lang["BulkPurchase7"] = "[ShopMasterExtreme] Erro na compra em massa: {0}";
            Lang["BulkPurchaseNotification1"] = "Fora de Estoque";
            Lang["BulkPurchaseNotification2"] = "Dinheiro Insuficiente";
            Lang["BulkPurchaseNotification3"] = "Comprou {0} x {1} com sucesso";

            Lang["Mod_ControlPanelTitle"] = "[Painel de Controle ShopMasterExtreme]";
            Lang["Mod_EnableLog"] = "Ativar Saída de Log";
            Lang["Mod_ShowAllItems"] = "Mostrar Todos os Itens (Apenas para algumas lojas)";
            Lang["Mod_Reboot"] = "Reiniciar";
            Lang["Mod_EnablePatch"] = "Ativar Patch";
            Lang["Mod_ForceRefresh"] = "Forçar Atualização de Todas as Lojas";
            Lang["Mod_CurrentKey"] = "Tecla de Atalho Atual: {0}";
            Lang["Mod_ChangeKey"] = "Alterar Tecla de Atalho";
            Lang["Mod_WaitingKey"] = "Pressione qualquer tecla para vincular...";
            Lang["Mod_SaveConfig"] = "Salvar Configuração";

            Lang["Log_PanelStatus"] = "[ShopMasterExtreme] Painel de Controle {0}";
            Lang["Log_ConfigRemoved"] = "[ShopMasterExtreme] Delegado de evento ModConfig removido";
            Lang["Log_ConfigSaved"] = "[ShopMasterExtreme] Configuração salva: RestockAmount = {0}";
            Lang["Log_ConfigReadError"] = "[ShopMasterExtreme] Erro ao ler Config.ini: {0}";
            Lang["Log_CurrentKeyInfo"] = "[ShopMasterExtreme] Tecla de atalho atual do painel: {0}";
            Lang["Log_ConfigWriteInfo"] = "[ShopMasterExtreme] Config.ini salvo: {0}, ShowLog={1}, ShowAllItems={2}";
            Lang["Log_ConfigWriteError"] = "[ShopMasterExtreme] Erro ao gravar Config.ini: {0}";
            Lang["Log_ModConfigLoadError"] = "[ShopMasterExtreme] Erro ao tentar carregar ModConfig: {0}";
            Lang["Log_RegisterConfig"] = "[ShopMasterExtreme] ModConfig detectado, registrando itens...";
            Lang["Log_RestockAmount"] = "[ShopMasterExtreme] Quantidade de reestoque definida para {0}";
            Lang["Log_ChangingLanguage"] = "[ShopMasterExtreme] Alterando o idioma para: {0}... Reiniciando o módulo";
            Lang["Log_NoModConfig"] = "[ShopMasterExtreme] ModConfig não encontrado, usando valor padrão 99";
            Lang["Log_HarmonyError"] = "[ShopMasterExtreme] Harmony não encontrado, por favor assine o Harmony!";
            Lang["Log_TargetNotFound"] = "[ShopMasterExtreme] Método alvo StockShop.DoRefreshStock não encontrado";
            Lang["Log_StartComplete"] = "[ShopMasterExtreme] Inicialização concluída";
            Lang["Log_PatchError"] = "[ShopMasterExtreme] Falha no Patch: {0}";
            Lang["Log_UnpatchSuccess"] = "[ShopMasterExtreme] Patch do Harmony desinstalado";
            Lang["Log_UnpatchError"] = "[ShopMasterExtreme] Falha ao desinstalar o Harmony: {0}";
            Lang["Log_RestockUpdated"] = "[ShopMasterExtreme] Quantidade de reestoque atualizada para {0}";
            Lang["Log_ForceRefreshSuccess"] = "[ShopMasterExtreme] Atualização forçada para todas as lojas ({0} no total)";
            Lang["Log_ForceRefreshError"] = "[ShopMasterExtreme] Falha ao forçar a atualização das lojas: {0}";
            Lang["Log_WaitKeyPrompt"] = "[ShopMasterExtreme] Por favor, pressione a nova tecla para vincular...";
            Lang["Log_NewKeyBind"] = "[ShopMasterExtreme] Nova tecla de atalho vinculada a: {0}";
            Lang["Log_ShopRestocked"] = "[ShopMasterExtreme] Loja {0} reabastecida para {1} unidades (MostrarTodosItens: {2})";
        }

        internal static void LoadSpecificLanguage(string langName)
        {
            Lang.Clear();
            switch (langName)
            {
                case "Chinese": LoadChinese(); break;
                case "English": LoadEnglish(); break;
                case "Japanese": LoadJapanese(); break;
                case "Korean": LoadKorean(); break;
                case "German": LoadGerman(); break;
                case "French": LoadFrench(); break;
                case "Russian": LoadRussian(); break;
                case "Spanish": LoadSpanish(); break;
                case "Portuguese": LoadPortuguese(); break;
                default: LoadEnglish(); break;
            }
        }

        internal static void TryUnloadLocallization()
        {
            try
            {
                LocalizationLoaded = false;
                Lang.Clear();
            }
            catch (Exception ex)
            {
                Loger.LogWarning($"[ShopMasterExtreme] Language unload error: {ex.Message}");
            }
        }
    }
}
