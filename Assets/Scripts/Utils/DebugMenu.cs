#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class DebugMenu
    {
        [MenuItem("WBT/Clear save")]
        public static void ClearSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif