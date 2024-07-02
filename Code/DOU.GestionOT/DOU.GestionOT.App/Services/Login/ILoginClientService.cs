using DOU.GestionOT.App.MVVM.Models.Login;

namespace DOU.GestionOT.App.Services.Login
{
    public interface ILoginClientService
    {
        Task<WeatherForecast[]> GetWeatherForeCastData();
        Task Login(LoginModel model);
        Task Register(RegisterModel model);
    }
}