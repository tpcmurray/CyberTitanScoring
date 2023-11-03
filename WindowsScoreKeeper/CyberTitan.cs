using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace WindowsScoreKeeper
{
    public partial class CyberTitan : Form
    {
        public CyberTitan()
        {
            InitializeComponent();
        }

        private void CyberTitan_Load(object sender, EventArgs e)
        {
            bool werEyxists = WindowsApis.Users.IsUserExisting("malacath");
            WindowsApis.Users.CreateUser("malacath2", "Test12345%");

            bool adminExists = WindowsApis.Users.IsUserExisting("Administrator");
            bool werExists = WindowsApis.Users.IsUserExisting("wer");
            bool guestDisabled = WindowsApis.Users.IsUserDisabled("Guest");
            bool amIDisabled = WindowsApis.Users.IsUserDisabled("tpcmu");
            bool amIAdmin = WindowsApis.Users.IsUserAdmin("tpcmu");
            bool nicoleIsAdmin = WindowsApis.Users.IsUserAdmin("nicole");

            bool cShareExists = WindowsApis.Shares.IsShareExisting("C$");
            bool xShareExists = WindowsApis.Shares.IsShareExisting("X$");

            bool file1Exists = WindowsApis.Files.IsFileInExistence(@"C:\inetpub\wwwroot\iisstart.htm");
            bool file2Exists = WindowsApis.Files.IsFileInExistence(@"C:\inetpub\wwwroot\doesNotExist.htm");

            bool chromeIsInstalled = WindowsApis.Installs.IsAppInstalled("Chrome");
            string chromeVersion = WindowsApis.Registry.GetRegistryKey(@"HKEY_CURRENT_USER\SOFTWARE\Google\Chrome\BLBeacon\", "version");

            int i = 111;
        }

        private void doStateCheck_Tick(object sender, EventArgs e)
        {

        }
    }
}
