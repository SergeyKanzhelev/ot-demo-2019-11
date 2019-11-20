using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace.Configuration;
using OpenTelemetry.Trace.Sampler;
using System;
using System.Collections.Generic;

namespace BackEndApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           
            services.AddOpenTelemetry(b => {
                b.AddRequestCollector()
                .UseZipkin(o =>
                {
                    //o.ServiceName = "BackEndApp";
                    o.Endpoint = new Uri("http://zipkin.azurewebsites.net/api/v2/spans");
                });





                // sets sampler
                //b.SetSampler(new HealthRequestsSampler(Samplers.AlwaysSample));

                // sets resource
                //b.SetResource(new Resource(new Dictionary<string, string>() {
                //    { "service.name", "BackEndApp" },
                //    { "deploymentTenantId", "kubecon-demo-surface" } }));

                // set the FlightID from the distributed context
                //b.AddProcessorPipeline(pipelineBuilder => pipelineBuilder.AddProcessor(_ => new FlightIDProperties()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
