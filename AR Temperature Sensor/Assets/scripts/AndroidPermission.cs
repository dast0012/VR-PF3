using UnityEngine;

public class AndroidPermission : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer =
               new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject wifiManager =
                activity.Call<AndroidJavaObject>("getSystemService", "wifi");

            AndroidJavaObject multicastLock =
                wifiManager.Call<AndroidJavaObject>("createMulticastLock", "udp_lock");

            multicastLock.Call("acquire");
        }
#endif
    }
}
