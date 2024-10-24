using Microsoft.Extensions.DependencyInjection;
using RealTimeWeatherMonitoringApp.Presentation.Controller;
using RealTimeWeatherMonitoringApp.Presentation.Utility;

// Inject Dependencies
var provider = DependencyInjector.CreateServiceProvider();

// Start
provider.GetRequiredService<ServerInterfaceController>().Start();
await provider.GetRequiredService<UserInterfaceController>().Start();