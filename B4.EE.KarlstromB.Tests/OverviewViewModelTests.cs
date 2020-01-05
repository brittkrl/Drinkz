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
        public void OpenCocktailPageCommand_Opens_CocktailPage()
        {
            //arrange
            var cocktail = new Cocktail { Id = Guid.Parse("00000000-1111-0000-0000-000000000001"), Name = "Moscow Mule" };
            var vm = GetOverviewViewModel(null);

            //act
            vm.OpenCocktailPageCommand.Execute(cocktail);

            //assert
            coreMethodsMock.Verify(e => e.PushPageModel<CocktailViewModel>(cocktail, It.IsAny<bool>(), It.IsAny<bool>()), Times.Once());
        }
    }
}
