using B4.EE.KarlstromB.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace B4.EE.KarlstromB.Domain.Services.Local
{
    public class JsonAppSettingsService : IAppSettingsService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonAppSettingsService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "appsettings.json");

            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<AppSettings> GetSettings()
        {
            try
            {
                string settingsJson = File.ReadAllText(_filePath);
                AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(settingsJson);
                return await Task.FromResult(settings);
            }
            catch 
            {
                return null;
            }
        }

        public async Task<bool> SaveSettings(AppSettings settings)
        {
            try
            {
                string settingsJson = JsonConvert.SerializeObject(settings, Formatting.Indented, _serializerSettings);
                File.WriteAllText(_filePath, settingsJson);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }
}
