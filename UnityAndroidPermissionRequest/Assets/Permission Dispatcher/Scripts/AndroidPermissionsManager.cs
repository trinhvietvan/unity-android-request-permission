using UnityEngine;

public class AndroidPermissionsManager
{
    private static AndroidJavaObject m_Activity;
    private static AndroidJavaObject m_PermissionService;

    private static AndroidJavaObject GetActivity()
    {
        if (m_Activity == null)
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
        return m_Activity;
    }

    private static AndroidJavaObject GetPermissionsService()
    {
        return m_PermissionService ??
            (m_PermissionService = new AndroidJavaObject("van.unity.permission.dispatcher.UnityAndroidPermissions"));
    }

    public static bool IsPermissionGranted(string permissionName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return GetPermissionsService().Call<bool>("IsPermissionGranted", GetActivity(), permissionName);
#else
        return true;
#endif
    }

    public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        GetPermissionsService().Call("RequestPermissionAsync", GetActivity(), permissionNames, callback);
#endif
    }

    public static void OpenAppSetting()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        GetPermissionsService().Call("OpenAppSetting", GetActivity());
#endif
    }

    public static int GetApiLevel()
    {
        int apiLevel = 0;

#if UNITY_ANDROID && !UNITY_EDITOR
        var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
        var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
        apiLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
#endif

        return apiLevel;
    }
}
