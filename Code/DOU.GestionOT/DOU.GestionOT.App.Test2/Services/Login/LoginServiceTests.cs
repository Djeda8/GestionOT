using DOU.GestionOT.App.MVVM.Models.Login;
using DOU.GestionOT.App.Services.Login;

namespace DOU.GestionOT.App.Test2.Services.Login
{
    public class LoginServiceTests : IClassFixture<LoginServiceFixture>
    {
        private readonly LoginService LoginService;

        public LoginServiceTests(LoginServiceFixture loginServiceFixture)
        {
            LoginService = loginServiceFixture.LoginService;
        }

        [Fact]
        public async Task Login_Return_LoginResponse()
        {
            // Arrange
            LoginModel model = new() { Email = "122", Password = "221" };

            //Act
            var response = await LoginService.Login(model);

            //Assert
            Assert.Equal("12345", response.AccessToken);
        }

        [Fact]
        public async Task Register_Return_RegisterResponse()
        {
            // Arrange
            RegisterModel model = new() { Email = "122", Password = "221" };

            //Act
            var response = await LoginService.Register(model);

            //Assert
            Assert.Equal(5, response.Status);
        }
    }
}
