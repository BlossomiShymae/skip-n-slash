using Avalonia;
using BlossomiShymae.SkipNSlash.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlossomiShymae.SkipNSlash.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure(() => new App(BuildServices()))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    public static ServiceProvider BuildServices()
    {
        var builder = new ServiceCollection();

        builder.AddHttpClient();

        builder.AddSingleton<MainWindowViewModel>();

        var services = builder.BuildServiceProvider();

        return services;
    }
}
