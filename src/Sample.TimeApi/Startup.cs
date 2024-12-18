using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using Sample.Common;
using Sample.TimeApi.Data;
using Sample.TimeApi.IRepositories;
using StackExchange.Redis;
using System;
namespace Sample.TimeApi
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

            // Configure elastic search URI and default index
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("products");

            // Add elstic cliend dependency
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            // Register it for DI
            services.AddSingleton<IElasticClient>(client);

            // Add product service dependency
            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<IDeviceRepository, SqlDeviceRepository>();

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
            services.AddHttpClient();

            // Add Redis service dependency

            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var redisConfig = $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}";
                return ConnectionMultiplexer.Connect(redisConfig);
            });



            services.AddSampleAppOptions(Configuration);
            services.AddWebSampleTelemetry(Configuration);

            var sampleAppOptions = Configuration.GetSampleAppOptions();

            if (sampleAppOptions.UseOpenTelemetry)
            {
                services.AddSingleton<SqlDeviceRepository>();
                services.AddSingleton<IDeviceRepository, OpenTelemetryCollectingDeviceRepository<SqlDeviceRepository>>();
            }
            else
            {
                services.AddSingleton<IDeviceRepository, SqlDeviceRepository>();
            }
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
