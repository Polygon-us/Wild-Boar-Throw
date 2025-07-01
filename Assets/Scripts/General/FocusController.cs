using UnityEngine;

namespace General
{
    public class FocusController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                EnableImmersiveMode();
        }

        private void EnableImmersiveMode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject window = activity.Call<AndroidJavaObject>("getWindow");
        AndroidJavaObject decorView = window.Call<AndroidJavaObject>("getDecorView");

        int flags =
            0x00000400 | // View.SYSTEM_UI_FLAG_LAYOUT_STABLE
            0x00000200 | // View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
            0x00000100 | // View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
            0x00000002 | // View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
            0x00000004 | // View.SYSTEM_UI_FLAG_FULLSCREEN
            0x00001000;  // View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY

        decorView.Call("setSystemUiVisibility", flags);
#endif
        }
    }
}