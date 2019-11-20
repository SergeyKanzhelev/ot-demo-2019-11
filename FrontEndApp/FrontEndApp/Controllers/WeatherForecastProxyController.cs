using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEndApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastProxyController : ControllerBase
    {
        private readonly ILogger<WeatherForecastProxyController> _logger;
        private readonly HttpClient _httpClient;

        public WeatherForecastProxyController(
            ILogger<WeatherForecastProxyController> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var jsonStream = await
                      _httpClient.GetStreamAsync("http://localhost:5001/weatherforecast");

            var weatherForecast = await
                  JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(jsonStream);

            return weatherForecast;
        }
    }
}
