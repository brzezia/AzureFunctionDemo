using System;
using AzureFunctionDemo.AzureGrid;
using AzureFunctionDemo.Mappers;
using AzureFunctionDemo.Model;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace AzureFunctionDemo
{
    public class Startup : FunctionsStartup
    {
        private readonly IConfiguration _configuration;
        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("d:\\afLogs\\logs.log")
                .MinimumLevel.Debug()
                .CreateLogger();

        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<EventGridConfig>((x) =>
            {
                x.TopicKey = Environment.GetEnvironmentVariable("topicKey");
                x.TopicHostName = Environment.GetEnvironmentVariable("topichostname");
                x.Subject = Environment.GetEnvironmentVariable("subject");
            });
            
          
            builder.Services.AddScoped(typeof(IEventGridSender<>), typeof(EventGridSender<>));
            builder.Services.AddScoped<IConverter, Converter>();
            builder.Services.AddLogging(b => b.AddSerilog(dispose: true));

        }
    }
}
