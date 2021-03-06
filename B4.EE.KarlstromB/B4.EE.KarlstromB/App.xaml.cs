﻿using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Services.Local;
using B4.EE.KarlstromB.ViewModels;
using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.EE.KarlstromB
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConfigureFreshIoc();

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainViewModel>());
        }

        private void ConfigureFreshIoc()
        {
            FreshIOC.Container.Register<IAppSettingsService>(new JsonAppSettingsService());
            FreshIOC.Container.Register<IUsersService>(new JsonUsersService());
            FreshIOC.Container.Register<ICocktailsService>(new JsonCocktailsService());
            FreshIOC.Container.Register<ISeederService, DataSeederService>();
            FreshIOC.Container.Register(DependencyService.Get<IPhotoPickerService>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
