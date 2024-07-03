using Moq;
using DOU.GestionOT.App.Services.Login;
using Moq.Protected;
using System.Net.Http.Json;

namespace DOU.GestionOT.App.Test2.Services.Login
{
    public class LoginServiceFixture
    {
        public LoginService LoginService { get; }

        public LoginServiceFixture()
        {
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            HttpResponseMessage httpResponseMessageLogin = new()
            {
                Content = JsonContent.Create(new
                {
                    AccessToken = "12345",
                    RefreshToken = "67890",
                    UserName = "Daniel"
                })
            };
            
            HttpResponseMessage httpResponseMessageRegister = new()
            {
                Content = JsonContent.Create(new
                {
                    Type = "Type",
                    Title = "Title",
                    Status = 5,
                    Detail = "Detail",
                    Instance = "Instance",
                    Errors = new { AdditionalProp1 = new List<string>(), AdditionalProp2 = new List<string>(), AdditionalProp3 = new List<string>() },
                    AdditionalProp1 = "",
                    AdditionalProp2 = "",
                    AdditionalProp3 = ""
                })
            };

            //httpMessageHandlerMock
            //     .Protected() // <= this is most important part that it need to setup.
            //     .Setup<Task<HttpResponseMessage>>(
            //         "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            //     .ReturnsAsync(httpResponseMessage);   

            httpMessageHandlerMock
                 .Protected() // <= this is most important part that it need to setup.
                 .Setup<Task<HttpResponseMessage>>(
                     "SendAsync",
                     ItExpr.Is<HttpRequestMessage>(match => match.Method == HttpMethod.Post && String.Equals(match.RequestUri, "http://localhost/login")),
                     ItExpr.IsAny<CancellationToken>())
                 .ReturnsAsync(httpResponseMessageLogin);

            httpMessageHandlerMock
                 .Protected() // <= this is most important part that it need to setup.
                 .Setup<Task<HttpResponseMessage>>(
                     "SendAsync",
                     ItExpr.Is<HttpRequestMessage>(match => match.Method == HttpMethod.Post && String.Equals(match.RequestUri, "http://localhost/register")),
                     ItExpr.IsAny<CancellationToken>())
                 .ReturnsAsync(httpResponseMessageRegister);

            // create the HttpClient
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new System.Uri("http://localhost") // It should be in valid uri format.
            };

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            LoginService = new(mockHttpClientFactory.Object);
        }
    }
}
