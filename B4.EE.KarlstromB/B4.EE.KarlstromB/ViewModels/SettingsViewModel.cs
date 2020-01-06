using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Services.Local;
using B4.EE.KarlstromB.Domain.Validators;
using FluentValidation;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly ICocktailsService cocktailsService;
        private IValidator userValidator;

        public SettingsViewModel(IAppSettingsService settingsService,
            IUsersService usersService,
            ICocktailsService cocktailsService)
        {
            this.settingsService = settingsService;
            this.usersService = usersService;
            this.cocktailsService = cocktailsService;
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
            set { errorName = value; 
                RaisePropertyChanged(nameof(ErrorName)); 
                RaisePropertyChanged(nameof(ErrorNameVisible));
            }
        }

        public bool ErrorNameVisible
        {
            get { return !string.IsNullOrWhiteSpace(ErrorName); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; RaisePropertyChanged(nameof(Age)); }
        }

        private string errorAge;
        public string ErrorAge
        {
            get { return errorAge; }
            set { errorAge = value; 
                RaisePropertyChanged(nameof(ErrorAge));
                RaisePropertyChanged(nameof(ErrorAgeVisible));
            }
        }

        public bool ErrorAgeVisible
        {
            get { return !string.IsNullOrWhiteSpace(ErrorAge); }
        }

        private ObservableCollection<Cocktail> cocktails;
        public ObservableCollection<Cocktail> Cocktails
        {
            get { return cocktails; }
            set { cocktails = value; RaisePropertyChanged(nameof(Cocktails)); }
        }

        #endregion

        public async override void Init(object initData)
        {
            base.Init(initData);

            var settings = await settingsService.GetSettings();

            var currentUser = await usersService.GetUser(settings.CurrentUserId);

            FirstName = currentUser.FirstName;
            LastName = currentUser.LastName;
            Age = currentUser.Age;

            await RefreshCocktails();
        }

        public ICommand SaveSettingsCommand => new Command(
            async () =>
            {
                var currentSettings = await settingsService.GetSettings();
                await settingsService.SaveSettings(currentSettings);

                var user = await usersService.GetUser(currentSettings.CurrentUserId);
                user.FirstName = FirstName?.Trim();
                user.LastName = LastName?.Trim();
                user.Age = Age;

                if (Validate(user))
                {
                    await usersService.UpdateUser(user);

                    await CoreMethods.PopPageModel(false, true);
                }
            }
        );

        public ICommand OpenCocktailPageCommand => new Command<Cocktail>(
            async (Cocktail cocktail) =>
            {
                await CoreMethods.PushPageModel<CocktailViewModel>(cocktail, false, true);
            }
        );

        private bool Validate(User user)
        {
            ErrorName = "";
            ErrorAge = "";

            var validationResult = userValidator.Validate(user);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(user.FirstName) || error.PropertyName == nameof(user.LastName))
                {
                    ErrorName = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(user.Age))
                {
                    ErrorAge = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }

        private async Task RefreshCocktails()
        {
            var cocktails = await cocktailsService.GetAllCocktails();
            Cocktails = new ObservableCollection<Cocktail>(cocktails.Where(e => e.Rating > 8).OrderBy(e => e.Name));
        }
    }
}
