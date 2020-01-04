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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.ViewModels
{
    public class CocktailViewModel : FreshBasePageModel
    {
        private readonly ICocktailsService cocktailsService;
        private readonly IAppSettingsService settingsService;

        private IValidator cocktailValidator;
        private AppSettings settings;
        private Cocktail currentCocktail;
        private bool isNew = true;

        public CocktailViewModel(
            ICocktailsService cocktailsService,
            IAppSettingsService settingsService)
        {
            this.cocktailsService = cocktailsService;
            this.settingsService = settingsService;
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

        #endregion

        public async override void Init(object initData)
        {
            base.Init(initData);
            currentCocktail = initData as Cocktail;
            settings = await settingsService.GetSettings();
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

        public ICommand SaveCocktailCommand => new Command(
            async () =>
            {
                SaveCocktail();
                if (Validate(currentCocktail))
                {
                    if (isNew)
                    {
                        await cocktailsService.AddCocktail(currentCocktail);
                    }
                    else
                    {
                        await cocktailsService.UpdateCocktail(currentCocktail);
                    }

                    MessagingCenter.Send(this, "Cocktail saved", currentCocktail);

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

        private async Task RefreshCocktail()
        {
            if (currentCocktail != null)
            {
                isNew = false;
                PageTitle = "Edit cocktail";
                
                currentCocktail = await cocktailsService.GetCocktail(currentCocktail.Id);
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
        }

        private void SaveCocktail()
        {
            currentCocktail.Name = CocktailName;
            currentCocktail.Preparation = CocktailPreparation;
            currentCocktail.Rating = Rating;
            currentCocktail.UserId = settings.CurrentUserId;
            currentCocktail.ImageUrl = ImageUrl;
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
    }
}
