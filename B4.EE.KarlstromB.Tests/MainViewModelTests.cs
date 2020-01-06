using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.ViewModels;
using FreshMvvm;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace B4.EE.KarlstromB.Tests
{
    public class MainViewModelTests
    {
        private Mock<IAppSettingsService> appSettingsMock;
        private Mock<IUsersService> usersServiceMock;
        private Mock<IPageModelCoreMethods> coreMethodsMock;

        public MainViewModelTests()
        {
            appSettingsMock = new Mock<IAppSettingsService>();
            usersServiceMock = new Mock<IUsersService>();
            coreMethodsMock = new Mock<IPageModelCoreMethods>();
        }

        private MainViewModel GetMainViewModel()
        {
            var vm = new MainViewModel(usersServiceMock.Object, appSettingsMock.Object)
            {
                CoreMethods = coreMethodsMock.Object
            };
            return vm;
        }

        [Fact]
        public void OpenOverviewPageCommand_Opens_OverviewPage()
        {
            //arrange
            var vm = GetMainViewModel();

            //act
            vm.OpenOverviewPageCommand.Execute(null);

            //assert
            coreMethodsMock.Verify(e => e.PushPageModel<OverviewViewModel>(It.IsAny<bool>()), Times.Once());
        }

        [Fact]
        public void OpenSettingsPageCommand_Opens_SettingsPage()
        {
            //arrange
            var vm = GetMainViewModel();

            //act
            vm.OpenSettingsPageCommand.Execute(null);

            //assert
            coreMethodsMock.Verify(e => e.PushPageModel<SettingsViewModel>(It.IsAny<bool>()), Times.Once());
        }
    }
}
