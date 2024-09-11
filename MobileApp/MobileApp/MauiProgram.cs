using Microsoft.Extensions.Logging;

namespace MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Configuración de HttpClient
            builder.Services.AddHttpClient("ApiGateway", client =>
            {
                // https://0bbb-190-95-204-185.ngrok-free.app
                client.BaseAddress = new Uri("http://localhost:5000/ ");
            });

            // Registrar MainPage con IHttpClientFactory
            builder.Services.AddTransient<MainPage>(sp => new MainPage(sp.GetRequiredService<IHttpClientFactory>()));

            return builder.Build();
        }
    }
}
