using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace SM.ClubManager.AccessControl.UI.Infrastructure
{
    public class AppSettings
    {
        public string? VSPEExecutablePath { get; set; }

        public bool IsDisplayFormOnStartup { get; set; }
        
        public bool IsDisplayInTaskBar { get; set; }
        
        public bool IsServiceMode { get; set; }
        
        public bool IsShowSplashScreen { get; set; }

        public string? EOFString { get; set; }

        //"IsDisplayFormOnStartup": true,
        //"IsDisplayInTaskBar": true,
        //"IsServiceMode": false,
        //"IsShowSplashScreen": false,
        //"EOFString": "\n\r\n"       
    }

    public class Settings
    {
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettingsManager
    {
        internal static IConfiguration configuration { get; private set; }

        internal static string settingsFile = "appsettings.json";

        // Static lock object to synchronize access
        private static readonly object _fileLock = new();

        internal static void UpdateSettings(string settingsFile, Action<Settings> updateAction)
        {
            lock (_fileLock) // Ensures only one thread can execute this block at a time
            {
                // Read and deserialize the settings.json file
                var json = File.ReadAllText(settingsFile);
                var settings = JsonSerializer.Deserialize<Settings>(json);

                // Apply the updates via the passed action
                updateAction(settings);

                // Serialize and write back to the file
                var updatedJson = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsFile, updatedJson);
            }

            InitAppSettings();
        }
       
        internal static void InitAppSettings()
        {
            //reload the configuration so that we have access to the latest settings
            configuration = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile(settingsFile, optional: false, reloadOnChange: true)
                                   .Build();
        }

    }
}
