using System.IO;
using Newtonsoft.Json;

namespace Core
{
    public class Settings
    {
        private static Settings _instance;
        private static readonly object syncLock = new object();

        private string _settingsPath => Path.Combine(Directory.GetCurrentDirectory(), "settings.json");
        public static Settings Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (_instance == null) _instance = new Settings(); return _instance;
                }
            }
        }

        public Settings() { }
        private Settings(bool load)
        {
            if (load) Load();
            Directory.CreateDirectory(BackupPath);
        }

        public void Load() => _instance = File.Exists(_settingsPath) ? JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_settingsPath)) : new Settings();

        public void Save()
        {
            Directory.CreateDirectory(BackupPath);
            File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(_instance, Formatting.Indented));
        }


        public string BackupPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "backup");
        public string WallpaperPath { get; set ; } 
        public string Text { get ; set ;  }
    }
}
