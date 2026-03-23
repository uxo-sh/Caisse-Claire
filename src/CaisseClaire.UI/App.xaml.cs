using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using CaisseClaire.Core.Interfaces;
using CaisseClaire.Core.Services;
using CaisseClaire.Data.Repositories;

namespace CaisseClaire.UI;

/// <summary>
/// Application entry point with dependency injection and Serilog configuration.
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;
    public IConfiguration Configuration { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Build configuration
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .WriteTo.Console()
            .WriteTo.File("logs/caisseclaire-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("CaisseClaire application starting...");

        // Configure dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();

        // Set DataContext for MainWindow
        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Configuration);

        // Register services
        services.AddSingleton<ICashRegisterService, CashRegisterService>();

        // Register repositories
        services.AddSingleton<IProductRepository, ProductRepository>();

        // Register windows
        services.AddTransient<MainWindow>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.Information("CaisseClaire application shutting down.");
        Log.CloseAndFlush();
        base.OnExit(e);
    }
}
