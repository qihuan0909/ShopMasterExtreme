using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Duckov.Economy;
using Duckov.Economy.UI;
using Cysharp.Threading.Tasks;
using ItemStatsSystem;
using Duckov.UI;
using Duckov.Utilities;
using Duckov;

namespace ShopMasterExtreme
{
    public static class BulkPurchase
    {
        internal static TMP_InputField myInputField;
        private static TextMeshProUGUI priceText;
        private static Button buyButton;

        private static StockShop activeShop;
        private static StockShop.Entry currentSelectionEntry;

        private static int currentMaxStock = 1;
        private static int currentInputValue = 1;
        private static int currentUnitPrice = 0;

        public static void ApplyPatches(object harmonyInstance, Type harmonyType, Type harmonyMethodType)
        {
            try
            {
                var targetShow = typeof(StockShopView).GetMethod("SetupAndShow", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var prefixShow = typeof(BulkPurchase).GetMethod(nameof(BeforeSetupAndShow), BindingFlags.Public | BindingFlags.Static);
                var prefixShowHM = Activator.CreateInstance(harmonyMethodType, prefixShow);

                var targetSelection = typeof(StockShopView).GetMethod("OnSelectionChanged", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var postfixSelection = typeof(BulkPurchase).GetMethod(nameof(AfterSelectionChanged), BindingFlags.Public | BindingFlags.Static);
                var postfixSelectionHM = Activator.CreateInstance(harmonyMethodType, postfixSelection);

                var patchMethod = harmonyType.GetMethod("Patch", new Type[] { typeof(MethodBase), harmonyMethodType, harmonyMethodType, harmonyMethodType, harmonyMethodType });

                patchMethod.Invoke(harmonyInstance, new object[] { targetShow, prefixShowHM, null, null, null });
                patchMethod.Invoke(harmonyInstance, new object[] { targetSelection, null, postfixSelectionHM, null, null });

                Loger.Log("[ShopMasterExtreme] 批量购买模块已成功挂载！");
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] 批量购买模块挂载失败: {ex}");
            }
        }
        public static void BeforeSetupAndShow(StockShop stockShop)
        {
            activeShop = stockShop;
        }

        public static void AfterSelectionChanged(StockShopView __instance)
        {
            StockShopItemEntry selection = __instance.GetSelection();
            if (selection == null || activeShop == null) return;

            currentSelectionEntry = selection.Target as StockShop.Entry;
            if (currentSelectionEntry == null) return;

            currentMaxStock = currentSelectionEntry.CurrentStock;

            Item itemInstance = activeShop.GetItemInstanceDirect(currentSelectionEntry.ItemTypeID);
            currentUnitPrice = activeShop.ConvertPrice(itemInstance, false);

            InjectAndSetupUI();

            if (myInputField != null)
            {
                myInputField.text = "1";
                currentInputValue = 1;
                UpdatePriceDisplay();
            }
        }

        private static void InjectAndSetupUI()
        {
            try
            {
                GameObject buttonObj = GameObject.Find("LevelConfig/LevelManager(Clone)/GameplayUICanvas/StockShopView/ItemDetails/Panel/ButtonContainer/Button");
                if (buttonObj == null) return;

                if (buyButton == null) buyButton = buttonObj.GetComponent<Button>();

                Transform priceTextTransform = buttonObj.transform.Find("BG/Line2/Price/Text (TMP) (1)");
                if (priceTextTransform != null && priceText == null)
                {
                    priceText = priceTextTransform.GetComponent<TextMeshProUGUI>();
                }

                if (buyButton != null)
                {
                    buyButton.onClick.RemoveAllListeners();
                    buyButton.onClick.AddListener(OnCustomBuyButtonClicked);
                }

                if (myInputField == null)
                {
                    Transform bgTransform = buttonObj.transform.Find("BG");
                    TMP_InputField template = UnityEngine.Object.FindAnyObjectByType<TMP_InputField>();
                    if (template == null) return;

                    GameObject myInputObj = UnityEngine.Object.Instantiate(template.gameObject, bgTransform);
                    myInputObj.name = "BatchBuyInputField";
                    myInputField = myInputObj.GetComponent<TMP_InputField>();

                    myInputField.onValueChanged.RemoveAllListeners();
                    myInputField.onEndEdit.RemoveAllListeners();

                    RectTransform rect = myInputField.GetComponent<RectTransform>();
                    rect.anchorMin = new Vector2(0, 0.5f);
                    rect.anchorMax = new Vector2(0, 0.5f);
                    rect.pivot = new Vector2(0, 0.5f);
                    rect.anchoredPosition = new Vector2(40, 0);
                    rect.sizeDelta = new Vector2(60, 40);

                    myInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
                }

                myInputField.onValueChanged.RemoveAllListeners();
                myInputField.onValueChanged.AddListener((val) => {
                    if (int.TryParse(val, out int num))
                    {
                        if (num < 1) num = 1;
                        if (num > currentMaxStock) num = currentMaxStock;

                        currentInputValue = num;
                        if (num.ToString() != val) myInputField.text = num.ToString();

                        UpdatePriceDisplay();
                    }
                    else
                    {
                        currentInputValue = 1;
                        UpdatePriceDisplay();
                    }
                });
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] UI组装或事件绑定失败: {ex}");
            }
        }

        private static void UpdatePriceDisplay()
        {
            if (priceText != null)
            {
                long totalPrice = (long)currentUnitPrice * currentInputValue;
                priceText.text = totalPrice.ToString();
            }
        }

        private static void OnCustomBuyButtonClicked()
        {
            if (activeShop == null || currentSelectionEntry == null || currentInputValue <= 0) return;
            ExecuteBulkBuyAsync().Forget();
        }

        private static async UniTaskVoid ExecuteBulkBuyAsync()
        {
            buyButton.interactable = false;

            try
            {
                long totalPrice = (long)currentUnitPrice * currentInputValue;
                Cost totalCost = new Cost(totalPrice);

                if (currentSelectionEntry.CurrentStock < currentInputValue)
                {
                    NotificationText.Push("库存不足");
                    Loger.Log($"库存不足，购买失败");
                    return;
                }

                if (!EconomyManager.IsEnough(totalCost, activeShop.AccountAvaliable, true))
                {
                    NotificationText.Push("金钱不足");
                    Loger.Log($"金钱不足，购买失败");
                    return;
                }

                EconomyManager.Pay(totalCost, activeShop.AccountAvaliable, true);

                currentSelectionEntry.CurrentStock -= currentInputValue;

                Item lastItem = null;
                for (int i = 0; i < currentInputValue; i++)
                {
                    Item item = await ItemAssetsCollection.InstantiateAsync(currentSelectionEntry.ItemTypeID);
                    lastItem = item;

                    if (!ItemUtilities.SendToPlayerCharacterInventory(item, false))
                    {
                        ItemUtilities.SendToPlayerStorage(item, false);
                    }
                }


                if (lastItem != null)
                {
                    NotificationText.Push($"成功购买了 {currentInputValue} 个 {lastItem.DisplayName} ");
                    Loger.Log($"购买 {currentInputValue} 个 {lastItem.DisplayName} ");
                    AudioManager.Post("UI/buy");
                }

                if (StockShopView.Instance != null)
                {
                    var currentSelection = StockShopView.Instance.GetSelection();
                    StockShopView.Instance.SetSelection(currentSelection);
                }

                myInputField.text = "1";
                currentInputValue = 1;
                UpdatePriceDisplay();
            }
            catch (Exception ex)
            {
                Loger.LogError($"[ShopMasterExtreme] 批量购买出错: {ex}");
            }
            finally
            {
                buyButton.interactable = true;
            }
        }
    }
}