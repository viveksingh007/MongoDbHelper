using Microsoft.Win32;
using MongoDB.Driver;
using System;

namespace MongoDBHelper
{
    public class DBHelper
    {
        public bool IsApplictionInstalled(string appName)
        {
            string displayName;
            RegistryKey key;

            if (string.IsNullOrWhiteSpace(appName)) return false;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // NOT FOUND
            return false;
        }

        public bool IsServerRunning(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) return false;

            var mongoClient = new MongoClient(connectionString);
            var databases = mongoClient.ListDatabases();
            if (string.Equals(mongoClient.Cluster.Description?.State.ToString(), "CONNECTED", StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }
    }
}
