using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        TokenResponse tokenResponse = new TokenResponse();
        using (var client = new HttpClient())
        {
            // discover the end point.
            var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:7295/");
            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return Enumerable.Empty<WeatherForecast>();
            }
            // request for a token
            tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discovery.TokenEndpoint,
                // Get all of these from secret manager.
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "ClaimsAPI"
            });

            if (tokenResponse.IsError)
            {
                await Console.Out.WriteLineAsync(tokenResponse.Error);
                return Enumerable.Empty<WeatherForecast>();
            }
        }

        using (var client = new HttpClient())
        {
            // use token to access resources
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("https://localhost:7047/WeatherForecast");

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync(response.StatusCode.ToString());
            }
            else
            {
                var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;

                Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
