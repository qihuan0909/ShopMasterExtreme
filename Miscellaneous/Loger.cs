using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ShopMasterExtreme
{
    public static class Loger
    {
        internal static bool ShowLog = true;
        internal static bool ShowWarn = true;
        internal static bool ShowError = true;
        internal static void Log(string word)
        {
            if (ShowLog)
            {
                Debug.Log(word);
            }
        }

        internal static void LogWarning(string word)
        {
            if (ShowWarn)
            {
                Debug.LogWarning(word);
            }
        }

        internal static void LogError(string word)
        {
            if (ShowError)
            {
                Debug.LogError(word);
            }
        }
    }
}
