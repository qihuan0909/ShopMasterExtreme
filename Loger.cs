using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ShopMasterExtreme
{
    public static class Loger
    {
        internal static bool ShowLog = false;
        internal static void Log(string word)
        {
            if (ShowLog)
            {
                Debug.Log(word);
            }
        }

        internal static void LogWarning(string word)
        {
            if (ShowLog)
            {
                Debug.LogWarning(word);
            }
        }

        internal static void LogError(string word)
        {
            if (ShowLog)
            {
                Debug.LogError(word);
            }
        }
    }
}
