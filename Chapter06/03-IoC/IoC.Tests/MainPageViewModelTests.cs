using IoC.Services;
using IoC.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IoC.Tests
{
    [TestClass]
    public class MainPageViewModelTests
    {
        [TestMethod]
        public void SaveCommand_CannotExecute_WithInvalidProperties()
        {
            var dataServiceMock = new Mock<IDatabaseService>();
            var vm = new MainViewModel(dataServiceMock.Object);
            //Act
            vm.Name = string.Empty;
            vm.Surname = string.Empty;
            // Assert.
            Assert.IsFalse(vm.SaveCommand.CanExecute(null));
        }
    }
}