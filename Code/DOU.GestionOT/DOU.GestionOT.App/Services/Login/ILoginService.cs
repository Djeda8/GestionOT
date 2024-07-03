using DOU.GestionOT.App.MVVM.Models.Login;

namespace DOU.GestionOT.App.Services.Login
{
    public interface ILoginService
    {
        Task<WeatherForecast[]> GetWeatherForeCastData();
        Task<LoginResponse> Login(LoginModel model);
        Task<RegisterResponse> Register(RegisterModel model);
    }
}