using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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

        internal static string settingsFileName = "settings.json";

        // Static lock object to synchronize access
        private static readonly object _fileLock = new();

        internal static void UpdateSettings(Action<Settings> updateAction)
        {
            lock (_fileLock) // Ensures only one thread can execute this block at a time
            {

                // Define the settings file path
                string settingsFilePath = Path.Combine(GetSettingsFilePath(), settingsFileName);
                // Read and deserialize the settings.json file
                var json = File.ReadAllText(settingsFilePath);
                var settings = JsonSerializer.Deserialize<Settings>(json);

                // Apply the updates via the passed action
                updateAction(settings);

                // Serialize and write back to the file
                var updatedJson = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(settingsFilePath, updatedJson);
            }

            InitAppSettings();
        }
       
        internal static void InitAppSettings()
        {
            CheckAndCreateIfNotExists();

            //reload the configuration so that we have access to the latest settings
            configuration = new ConfigurationBuilder()
                                   .SetBasePath(GetSettingsFilePath())
                                   .AddJsonFile(settingsFileName, optional: false, reloadOnChange: true)
                                   .Build();
        }


        private static string GetSettingsFilePath()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;

            // Get the AppData folder path
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName);

            return appDataPath;
        }

        private static void CheckAndCreateIfNotExists()
        {
            // Get the AppData folder path
            string appDataPath = GetSettingsFilePath();

            // Ensure the directory exists
            Directory.CreateDirectory(appDataPath);

            // Define the settings file path
            string settingsFilePath = Path.Combine(appDataPath, "settings.json");

            if (!File.Exists(settingsFilePath)) 
            {
                // Write to the settings file
                string content = "{\r\n  \"AppSettings\": {\r\n    \"VSPEExecutablePath\": \"C:\\\\Program Files\\\\Eterlogic Software\\\\Virtual Serial Ports Emulator (x64)\\\\VSPEmulator.exe\",\r\n    \"IsDisplayFormOnStartup\": true,\r\n    \"IsDisplayInTaskBar\": true,\r\n    \"IsServiceMode\": false,\r\n    \"IsShowSplashScreen\": false,\r\n    \"EOFString\": \"\\n\\r\\n\"\r\n  }\r\n}";
                File.WriteAllText(settingsFilePath, content);

                Console.WriteLine($"Settings saved to: {settingsFilePath}");
            }
            
        }
    }
}
