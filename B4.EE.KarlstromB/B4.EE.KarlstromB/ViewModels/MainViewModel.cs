using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Services.Local;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private readonly IUsersService usersService;
        private readonly IAppSettingsService settingsService;

        public MainViewModel(IUsersService usersService,
            IAppSettingsService settingsService)
        {
            this.usersService = usersService;
            this.settingsService = settingsService;
        }
        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await EnsureUserAndSettings();
        }


        private string nameUser;
        public string NameUser
        {
            get { return nameUser; }
            set { nameUser = value; RaisePropertyChanged(nameof(NameUser)); }
        }

        public ICommand OpenOverviewPageCommand => new Command(
           async () =>
           {
               await CoreMethods.PushPageModel<OverviewViewModel>(true);
           }
        );

        public ICommand OpenSettingsPageCommand => new Command(
           async () =>
           {
               await CoreMethods.PushPageModel<SettingsViewModel>(true);
           }
        );

        private async Task EnsureUserAndSettings()
        {
            var settings = await settingsService.GetSettings();
            if (settings == null)
            {
                //create new user
                var newUser = new User
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    FirstName = "",
                    LastName = ""
                };
                await usersService.CreateUser(newUser);

                //create new settings
                var newSettings = new AppSettings
                {
                    CurrentUserId = newUser.Id
                };
                await settingsService.SaveSettings(newSettings);
            }
            else
            {
                var currentUser = await usersService.GetUser(settings.CurrentUserId);
                if (currentUser.FirstName != null)
                {
                    NameUser = currentUser.FirstName;
                }
            }
        }
    }
}
