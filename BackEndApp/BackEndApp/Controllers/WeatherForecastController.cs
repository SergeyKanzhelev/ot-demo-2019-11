using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Context;
using OpenTelemetry.Trace;

namespace BackEndApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private class Message
        {
            public IDictionary<string, string> Metadata = new Dictionary<string, string>();
        }

        private readonly ILogger<WeatherForecastController> _logger;
        private ITracer _tracer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TracerFactoryBase tracerFactory)
        {
            _logger = logger;
            _tracer = tracerFactory.GetTracer("custom");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            // this is how you can set a custom attribute
            //_tracer.CurrentSpan.SetAttribute("forecastSource", "random");

            // injecting context into the custom message
            //var message = new Message();
            //_tracer.TextFormat.Inject<Message>(_tracer.CurrentSpan.Context, message, (m, k, v) => m.Metadata.Add(k, v));

            // this will set a context for all telemetry in using block
            //using (DistributedContext.SetCurrent(new DistributedContext(new DistributedContextEntry[] {
            //    new DistributedContextEntry("FlightID", "randomSource") })))
            //{
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            //}
        }
    }
}
