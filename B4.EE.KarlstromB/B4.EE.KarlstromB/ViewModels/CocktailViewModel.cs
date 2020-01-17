using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Validators;
using FluentValidation;
using FreshMvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.ViewModels
{
    public class CocktailViewModel : FreshBasePageModel
    {
        private readonly ICocktailsService _cocktailsService;
        private readonly IAppSettingsService _settingsService;

        private IValidator cocktailValidator;
        private AppSettings settings;
        private Cocktail currentCocktail;
        private bool isNew = true;

        public CocktailViewModel(
            ICocktailsService cocktailsService,
            IAppSettingsService settingsService)
        {
            _cocktailsService = cocktailsService;
            _settingsService = settingsService;
            cocktailValidator = new CocktailValidator();
        }

        #region Properties

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; RaisePropertyChanged(nameof(PageTitle)); }
        }

        private string cocktailName;
        public string CocktailName
        {
            get { return cocktailName; }
            set { cocktailName = value; RaisePropertyChanged(nameof(CocktailName)); }
        }

        private string cocktailNameError;
        public string CocktailNameError
        {
            get { return cocktailNameError; }
            set
            {
                cocktailNameError = value;
                RaisePropertyChanged(nameof(CocktailNameError));
                RaisePropertyChanged(nameof(CocktailNameErrorVisible));
            }
        }

        public bool CocktailNameErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(CocktailNameError); }
        }

        private string cocktailPreparation;
        public string CocktailPreparation
        {
            get { return cocktailPreparation; }
            set { cocktailPreparation = value; RaisePropertyChanged(nameof(CocktailPreparation)); }
        }

        private int? rating;
        public int? Rating
        {
            get { return rating; }
            set { rating = value; RaisePropertyChanged(nameof(Rating)); }
        }

        private ObservableCollection<Ingredient> ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; RaisePropertyChanged(nameof(Ingredients)); }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; RaisePropertyChanged(nameof(ImageUrl)); }
        }

        public string ImageName { get; set; }

        private MediaFile selectedImage;
        public MediaFile SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; RaisePropertyChanged(nameof(SelectedImage)); }
        }

        private bool hasIce;
        public bool HasIce
        {
            get { return hasIce; }
            set { hasIce = value; RaisePropertyChanged(nameof(HasIce)); }
        }

        #endregion

        public async override void Init(object initData)
        {
            base.Init(initData);
            currentCocktail = initData as Cocktail;
            settings = await _settingsService.GetSettings();
            await RefreshCocktail();
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is Ingredient)
            {
                LoadCocktail();
            }
        }

        #region Commands

        public ICommand SaveCocktailCommand => new Command(
            async () =>
            {
                SaveCocktail();
                if (Validate(currentCocktail))
                {
                    if (isNew)
                    {
                        await _cocktailsService.AddCocktail(currentCocktail);
                    }
                    else
                    {
                        await _cocktailsService.UpdateCocktail(currentCocktail);
                    }

                    await CoreMethods.PopPageModel(false, true);
                }
            });

        public ICommand OpenIngredientPageCommand => new Command<Ingredient>(
            async (Ingredient ingredient) =>
            {
                SaveCocktail();
                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        CocktailId = currentCocktail.Id,
                        Cocktail = currentCocktail
                    };
                }
                await CoreMethods.PushPageModel<IngredientViewModel>(ingredient, false, true);
            });

        public ICommand DeleteIngredientCommand => new Command<Ingredient>(
            (Ingredient ingredient) =>
            {
                currentCocktail.Ingredients.Remove(ingredient);
                LoadCocktail();
            });

        public ICommand UploadPhotoCommand => new Command(
            async () =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await CoreMethods.DisplayAlert("Error", "This is not supported on your device", "Continue");
                    return;
                }
                await CrossMedia.Current.Initialize();

                var mediaOptions = new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                };
                selectedImage = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                if (selectedImage == null)
                {
                    await CoreMethods.DisplayAlert("Something went wrong", "You might have cancelled the request to upload a photo. Please try again.", "Continue");
                    return;
                }

                ImageUrl = selectedImage.Path;
            });

        public ICommand TakePhotoCommand => new Command(
            async () =>
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await CoreMethods.DisplayAlert("Error", "This is not supported on your device", "Continue");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = $"{ImageName}.jpg"
                });

                if (file == null)
                {
                    await CoreMethods.DisplayAlert("Something went wrong", "You might have cancelled the request to take a photo. Please try again.", "Continue");
                    return;
                }

                ImageUrl = file.Path;
            });

        #endregion

        #region Methods

        private async Task RefreshCocktail()
        {
            if (currentCocktail != null)
            {
                isNew = false;
                PageTitle = "Edit cocktail";
                
                currentCocktail = await _cocktailsService.GetCocktail(currentCocktail.Id);
            }
            else
            {
                isNew = true;
                PageTitle = "New cocktail";
                currentCocktail = new Cocktail();
                currentCocktail.Id = Guid.NewGuid();
                currentCocktail.UserId = settings.CurrentUserId;
                currentCocktail.Ingredients = new ObservableCollection<Ingredient>();
                currentCocktail.ImageUrl = "noimage.png";
                currentCocktail.HasIce = false;
            }
            LoadCocktail();
        }

        private void LoadCocktail()
        {
            CocktailName = currentCocktail.Name;
            CocktailPreparation = currentCocktail.Preparation;
            Rating = currentCocktail.Rating;
            ImageUrl = currentCocktail.ImageUrl;
            Ingredients = new ObservableCollection<Ingredient>(currentCocktail.Ingredients.OrderBy(e => e.Name));
            HasIce = currentCocktail.HasIce;
        }

        private void SaveCocktail()
        {
            currentCocktail.Name = CocktailName;
            currentCocktail.Preparation = CocktailPreparation;
            currentCocktail.Rating = Rating;
            currentCocktail.UserId = settings.CurrentUserId;
            currentCocktail.ImageUrl = ImageUrl;
            currentCocktail.HasIce = HasIce;
        }

        private bool Validate(Cocktail cocktail)
        {
            CocktailNameError = "";

            var validationResult = cocktailValidator.Validate(cocktail);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(cocktail.Name))
                {
                    CocktailNameError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }

        #endregion
    }
}
