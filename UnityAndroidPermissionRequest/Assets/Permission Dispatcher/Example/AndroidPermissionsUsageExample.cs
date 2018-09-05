using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AndroidPermissionsUsageExample : MonoBehaviour
{
    private const string READ_PHONE_STATE = "android.permission.READ_PHONE_STATE";
    private const string READ_EXTERNAL_STORAGE = "android.permission.READ_EXTERNAL_STORAGE";
    private const string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";

    void Start()
    {
        Debug.Log("Android API Level: " + AndroidPermissionsManager.GetApiLevel());
    }

    private bool CheckPermissions()
    {
        return AndroidPermissionsManager.IsPermissionGranted(READ_PHONE_STATE);
    }

    public void OnGrantButtonPress()
    {
        AndroidPermissionsManager.RequestPermission(new[] { READ_EXTERNAL_STORAGE, WRITE_EXTERNAL_STORAGE }, new AndroidPermissionCallback(
            results =>
            {
                bool isNeedOpenAppSetting = false;

                for (int i = 0; i < results.Length; i++)
                {
                    Debug.Log(results[i].ToString());

                    if (results[i].neverAskAgain == true && results[i].permissionGranted == false)
                    {
                        isNeedOpenAppSetting = true;
                    }
                }

                if (isNeedOpenAppSetting)
                {
                    AndroidPermissionsManager.OpenAppSetting();
                }
            }));
    }
}
