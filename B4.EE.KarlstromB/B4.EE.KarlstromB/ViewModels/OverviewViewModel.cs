using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
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
    public class OverviewViewModel : FreshBasePageModel
    {
        private readonly IAppSettingsService settingsService;
        private readonly ICocktailsService cocktailsService;
        private ISeederService seederService;

        public OverviewViewModel(
            IAppSettingsService settingsService,
            ICocktailsService cocktailsService,
            ISeederService seederService)
        {
            this.settingsService = settingsService;
            this.cocktailsService = cocktailsService;
            this.seederService = seederService;

            this.seederService.EnsureSeeded();
            Cocktails = new ObservableCollection<Cocktail>(cocktailsService.GetAllCocktails().Result);
        }

        private ObservableCollection<Cocktail> cocktails;
        public ObservableCollection<Cocktail> Cocktails
        {
            get { return cocktails; }
            set { cocktails = value; RaisePropertyChanged(nameof(Cocktails)); }
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RefreshCocktails();
        }

        public ICommand OpenCocktailPageCommand => new Command<Cocktail>(
            async (Cocktail cocktail) =>
            {
                await CoreMethods.PushPageModel<CocktailViewModel>(cocktail, false, true);
            }
        );

        public ICommand DeleteCocktailCommand => new Command<Cocktail>(
            async (Cocktail cocktail) =>
            {
                bool answer = await CoreMethods.DisplayAlert($"Delete {cocktail.Name}", $"Are you sure you want to delete {cocktail.Name}?", "Ok", "Cancel");

                if (answer)
                {
                    await cocktailsService.DeleteCocktail(cocktail.Id);
                    Cocktails.Remove(cocktail);
                    await RefreshCocktails();
                }
                else
                {
                    return;
                }
            }
        );

        private async Task RefreshCocktails()
        {
            var cocktails = await cocktailsService.GetAllCocktails();
            Cocktails = new ObservableCollection<Cocktail>(cocktails.OrderBy(e => e.Name));
        }
    }
}
