using DOU.GestionOT.API2.Code.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var isDevelopment = app.Environment.IsDevelopment();
        if (exceptionHandler!.Error is DomainException exception)
        {
            await Results.Problem(
                title: exception!.Message,
                detail: isDevelopment ? exception!.StackTrace : null,
                statusCode: 400
                )
            .ExecuteAsync(context);
        }
        else if (exceptionHandler!.Error.InnerException is JsonException jsonException)
        {
            await Results.Problem(
                title: jsonException.Message,
                detail: isDevelopment ? exceptionHandler!.Error.StackTrace : null,
                statusCode: 400
                )
            .ExecuteAsync(context);
        }
        else
        {
            await Results.Problem(
            title: exceptionHandler!.Error.InnerException != null ? exceptionHandler!.Error.InnerException.Message : exceptionHandler!.Error.Message,
            detail: isDevelopment ? exceptionHandler!.Error.StackTrace : null,
            statusCode: 500
            )
            .ExecuteAsync(context);
        }
    });
});

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseReDoc(c =>
{
    c.HideDownloadButton();
    c.DocumentTitle = "Nord Pirineus - PreOrders API Documentation";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
