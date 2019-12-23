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


        public OverviewViewModel(
            IAppSettingsService settingsService, 
            ICocktailsService cocktailsService)
        {
            this.settingsService = settingsService;
            this.cocktailsService = cocktailsService;
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
            async (Cocktail cocktail) => {
                await CoreMethods.PushPageModel<CocktailViewModel>(cocktail, false, true);
            }
        );

        public ICommand DeleteCocktailCommand => new Command<Cocktail>(
            async (Cocktail cocktail) => {
                await cocktailsService.DeleteCocktail(cocktail.Id);
                await RefreshCocktails();
            }
        );

        private async Task RefreshCocktails()
        {
            var settings = await settingsService.GetSettings();
            var cocktails = await cocktailsService.GetCocktailsForUser(settings.CurrentUserId);
            Cocktails = null;
            Cocktails = new ObservableCollection<Cocktail>(cocktails.OrderBy(e => e.Name));
        }
        //public IList<Cocktail> CocktailExamples { get { return BasicCocktails.CocktailsExamples; } }
    }
}
