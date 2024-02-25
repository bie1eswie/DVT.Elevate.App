// See https://aka.ms/new-console-template for more information
using DVT.Elevate.Domian.Abstract.App;
using DVT.Elevate.Domian.Configuration;
using DVT.Elevate.Service;
using DVT.Elevate.Service.Elevator;
using DVT.Elevator.Abstract.Elevator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();
var builderHost = Host.CreateDefaultBuilder();
builderHost.ConfigureLogging(logger =>
{
    //logger.AddConsole();
    logger.ClearProviders();
});

// Service Injection 
var _host = builderHost.ConfigureServices(services =>
{
    services.AddTransient<IElevatorFactoryService, PassengerElevatorFactoryService>();
    services.AddSingleton<IElevatorControlCenter, PassengerElevatorControlCenter>();
    services.AddSingleton<IElevatorApp, ElevatorApp>();
    services.Configure<ConfigurationOptions>(config, x => x.BindNonPublicProperties = true);
    services.AddHostedService<ElevatorControlEngine>();
}).Build();

var app = _host.Services.GetRequiredService<IElevatorApp>();
Parallel.Invoke(_host.Run, app.Execute);


