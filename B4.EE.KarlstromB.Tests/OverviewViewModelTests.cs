using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.ViewModels;
using FreshMvvm;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace B4.EE.KarlstromB.Tests
{
    public class OverviewViewModelTests
    {
        private Mock<IAppSettingsService> appSettingsMock;
        private Mock<ICocktailsService> cocktailsServiceMock;
        private Mock<ISeederService> seederServiceMock;
        private Mock<IPageModelCoreMethods> coreMethodsMock;

        public OverviewViewModelTests()
        {
            appSettingsMock = new Mock<IAppSettingsService>();
            cocktailsServiceMock = new Mock<ICocktailsService>();
            seederServiceMock = new Mock<ISeederService>();
            coreMethodsMock = new Mock<IPageModelCoreMethods>();
        }

        private OverviewViewModel GetOverviewViewModel(List<Cocktail> cocktails)
        {
            if (cocktails != null)
            {
                cocktailsServiceMock.Setup(e => e.GetAllCocktails())
                    .Returns(Task.FromResult(cocktails.AsQueryable()));
            }

            var vm = new OverviewViewModel(appSettingsMock.Object, cocktailsServiceMock.Object, seederServiceMock.Object)
            {
                CoreMethods = coreMethodsMock.Object
            };

            return vm;
        }

        [Fact]
        public void OpenCocktailPageCommand_Opens_CocktailPage_InEditMode_WhenCocktailIsNotNull()
        {
            //arrange
            var cocktail = new Cocktail { Id = Guid.Parse("00000000-1111-0000-0000-000000000001"), Name = "Moscow Mule" };
            var vm = GetOverviewViewModel(null);

            //act
            vm.OpenCocktailPageCommand.Execute(cocktail);

            //assert
            coreMethodsMock.Verify(e => e.PushPageModel<CocktailViewModel>(cocktail, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        }

        [Fact]
        public void OpenCocktailPageCommand_Opens_CocktailPage_WhenCocktailIsNull()
        {
            //arrange
            var cocktail = new Cocktail { Id = Guid.Empty, Name = null };
            var vm = GetOverviewViewModel(null);

            //act
            vm.OpenCocktailPageCommand.Execute(cocktail);

            //assert
            coreMethodsMock.Verify(e => e.PushPageModel<CocktailViewModel>(cocktail, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        }

        [Fact]
        public void DeleteCocktailCommand_GetsCancelled_ByUser_WithAlert()
        {
            //arrange
            var cocktail1 = new Cocktail {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000001"),
                Name = "Cosmopolitan",
                Ingredients = null
            };
            var cocktail2 = new Cocktail
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000002"),
                Name = "Manhattan",
                Ingredients = null
            };

            var cocktails = new List<Cocktail>
            {
                cocktail1,
                cocktail2
            };

            coreMethodsMock.Setup(c => c.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var vm = GetOverviewViewModel(cocktails);

            //act
            vm.DeleteCocktailCommand.Execute(cocktail1);

            //assert
            cocktailsServiceMock.Verify(e => e.DeleteCocktail(Guid.Parse("00000000-1111-0000-0000-000000000001")), Times.Never());
            Assert.Contains(cocktail1, vm.Cocktails);
        }
    }
}
