using DOU.GestionOT.App.MVVM.Models.Login;
using System.Net.Http.Json;
using System.Text.Json;

namespace DOU.GestionOT.App.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public LoginService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RegisterResponse> Register(RegisterModel model)
        {
            // model.Email = "maui@gmail.com"; model.Password = "Maui@123";

            var httpClient = httpClientFactory.CreateClient("custom-httpclient");

            var result = await httpClient.PostAsJsonAsync("/register", model);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<RegisterResponse>();

                //await Shell.Current.DisplayAlert("Alert", "sucessfully Register", "Ok");

                if (response is not null)
                {
                    return response;
                }
            }
            //await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
            return null;
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            //model.Email = "maui@gmail.com"; model.Password = "Maui@123";

            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);

            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
            if (response is not null)
            {
                return response;
            }
            return null;
        }

        public async Task<WeatherForecast[]> GetWeatherForeCastData()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null!;

            string token = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.AccessToken!;
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");
            return result!;
        }
    }
}
