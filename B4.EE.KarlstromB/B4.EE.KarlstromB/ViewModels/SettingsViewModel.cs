﻿using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Services.Local;
using B4.EE.KarlstromB.Domain.Validators;
using FluentValidation;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        private readonly IAppSettingsService settingsService;
        private readonly IUsersService usersService;
        private IValidator userValidator;

        public SettingsViewModel(IAppSettingsService settingsService,
            IUsersService usersService)
        {
            this.settingsService = settingsService;
            this.usersService = usersService;
            userValidator = new UserValidator();
        }

        #region Properties

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(nameof(FirstName)); }
        }

        private string errorFirstName;
        public string ErrorFirstName
        {
            get { return errorFirstName; }
            set { errorFirstName = value; }
        }


        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(nameof(LastName)); }
        }


        private string errorName;
        public string ErrorName
        {
            get { return errorName; }
            set { errorName = value; RaisePropertyChanged(nameof(ErrorName)); }
        }

        #endregion

        public async override void Init(object initData)
        {
            base.Init(initData);

            var settings = await settingsService.GetSettings();

            var currentUser = await usersService.GetUser(settings.CurrentUserId);

            FirstName = currentUser.FirstName;
            LastName = currentUser.LastName;
        }

        public ICommand SaveSettingsCommand => new Command(
            async () =>
            {
                var currentSettings = await settingsService.GetSettings();
                await settingsService.SaveSettings(currentSettings);

                var user = await usersService.GetUser(currentSettings.CurrentUserId);
                user.FirstName = FirstName?.Trim();
                user.LastName = LastName?.Trim();

                if (Validate(user))
                {
                    await usersService.UpdateUser(user);

                    await CoreMethods.PopPageModel(false, true);
                }
            }
        );

        private bool Validate(User user)
        {
            ErrorName = "";

            var validationResult = userValidator.Validate(user);
            foreach (var error in validationResult.Errors)
            {
                ErrorName = error.ErrorMessage;
            }
            return validationResult.IsValid;
        }
    }
}
