using Microsoft.Win32;

namespace WinUISample.Services
{
    public class RegistryService
    {
        public bool IsFirstTimeLaunch()
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Contoso\ContosoExpenses", true);
            if (regKey == null)
            {
                regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Contoso\ContosoExpenses", RegistryKeyPermissionCheck.ReadWriteSubTree);
                regKey.SetValue("FirstRun", "false");
                return true;
            }

            else
            {
                string isFirstRun = regKey.GetValue("FirstRun").ToString();
                if (isFirstRun == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ResetApp()
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Contoso\ContosoExpenses", true);
            if (regKey is not null)
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\Contoso\ContosoExpenses");
            }
        }
    }
}
