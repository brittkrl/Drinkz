using B4.EE.KarlstromB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace B4.EE.KarlstromB.Domain.Services
{
    public interface IAppSettingsService
    {
        Task<AppSettings> GetSettings();
        Task<bool> SaveSettings(AppSettings settings);
    }
}
