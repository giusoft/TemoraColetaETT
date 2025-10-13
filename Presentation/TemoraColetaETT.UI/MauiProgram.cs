using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.LifecycleEvents;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Infrastructure.Configuration;
using TemoraColetaETT.Infrastructure.Persistence;
using TemoraColetaETT.Infrastructure.Services;
using TemoraColetaETT.UI.ViewModels;
using TemoraColetaETT.UI.Views;
using TemoraColetaETT.UI.Views.Components;

#if WINDOWS
    using Microsoft.UI.Windowing;
    using WinRT.Interop;
#endif

namespace TemoraColetaETT.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Env.Load();
            LogService.Initialize();
            ConfigureServices(builder.Services);

            #if WINDOWS
                    builder.ConfigureLifecycleEvents(events =>
                    {
                        events.AddWindows(windows =>
                        {
                            windows.OnWindowCreated(window =>
                            {
                                var hwnd = WindowNative.GetWindowHandle(window);
                                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                                var appWindow = AppWindow.GetFromWindowId(windowId);
                                appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                            });
                        });
                    });
            #endif

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(Env.ApiBaseUrl);
            });

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

            services.AddSingleton<IFacialRecognitionService, FacialRecognitionService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginView>();

            services.AddTransient<DashboardViewModel>();
            services.AddTransient<DashboardView>();

            services.AddTransient<RegisterPersonViewModel>();
            services.AddTransient<RegisterPersonView>();

            services.AddTransient<FacialBiometricsViewModel>();
            services.AddTransient<FacialBiometricsView>();

            services.AddSingleton<AppShellViewModel>();
            services.AddTransient<AppShell>();
        }
    }
}