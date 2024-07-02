﻿using DOU.GestionOT.App.MVVM.Models.Login;
using System.Net.Http.Json;
using System.Text.Json;

namespace DOU.GestionOT.App.Services.Login
{
    public class LoginClientService : ILoginClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public LoginClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task Register(RegisterModel model)
        {
            // model.Email = "maui@gmail.com"; model.Password = "Maui@123";
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");

            var result = await httpClient.PostAsJsonAsync("/register", model);

            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "sucessfully Register", "Ok");
            }
            await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok"); ;
        }

        public async Task Login(LoginModel model)
        {
            //model.Email = "maui@gmail.com"; model.Password = "Maui@123";
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);
            //var result = await _client.PostAsJsonAsync($"http://192.168.0.60:5057/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
            if (response is not null)
            {
                var serializeResponse = JsonSerializer.Serialize(
                    new LoginResponse() { AccessToken = response.AccessToken, RefreshToken = response.RefreshToken, UserName = model.Email });
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
            }
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
