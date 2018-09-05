using System;
using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR
public class AndroidPermissionCallback : AndroidJavaProxy
{
    private event Action<PermissionResult[]> OnPermissionRequestResultCallback;

    public AndroidPermissionCallback(Action<PermissionResult[]> onPermissionRequestcallback)
        : base("van.unity.permission.dispatcher.IPermissionRequestResult")
    {
        if (onPermissionRequestcallback != null)
        {
            OnPermissionRequestResultCallback += onPermissionRequestcallback;
        }
    }

    public virtual void OnRequestPermissionsResult(AndroidJavaObject resultWrapper)
    {
        if (OnPermissionRequestResultCallback != null)
        {
            AndroidJavaObject[] rawResults = resultWrapper.Get<AndroidJavaObject[]>("results");
            PermissionResult[] results = new PermissionResult[rawResults.Length];

            for (int i = 0; i < rawResults.Length; i++)
            {
                results[i] = new PermissionResult();
                results[i].permissionName = rawResults[i].Get<string>("permissionName");
                results[i].permissionGranted = rawResults[i].Get<bool>("permissionGranted");
                results[i].neverAskAgain = rawResults[i].Get<bool>("neverAskAgain");
            }

            OnPermissionRequestResultCallback(results);
        }
    }
}
#else

/// <summary>
/// This is fake class for Run, Build on platform that is not Android
/// </summary>
public class AndroidPermissionCallback
{
    public AndroidPermissionCallback(Action<PermissionResult[]> onPermissionRequestcallback)
    {
    }
}

#endif