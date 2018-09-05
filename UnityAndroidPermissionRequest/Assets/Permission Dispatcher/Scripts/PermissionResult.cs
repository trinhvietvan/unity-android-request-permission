public class PermissionResult
{
    public string permissionName;
    public bool permissionGranted;
    public bool neverAskAgain;

    public override string ToString()
    {
        return string.Format("{0} - Granted: {1} - Never Ask Again: {2}", permissionName, permissionGranted, neverAskAgain);
    }
}
