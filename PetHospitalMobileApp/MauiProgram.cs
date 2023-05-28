using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PetHospitalMobileApp.Data;
using PetHospitalMobileApp.Infrastructure;
using PetHospitalMobileApp.Services;
using RestSharp;

namespace PetHospitalMobileApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSentry(options =>
            {
                options.Dsn = "https://8c30ab4402b246f1a917bcd2fe527933@o4505261862158336.ingest.sentry.io/4505261864189952";

                options.Debug = true;

                options.TracesSampleRate = 1.0;
            })

            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});



		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
        var connectionOptions = new ConnectionOptions("https://192.168.1.10:7182");
        builder.Services.AddSingleton(connectionOptions);

        var options = new RestClientOptions()
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
        var restClient = new RestClient(options);
        builder.Services.AddSingleton(sp => restClient);
        builder.Services.AddSingleton<LocalStorage>();
        builder.Services.AddSingleton<WeatherForecastService>();
        var translateService = new TranslateService();
        translateService.LoadFromResourcesAsync().GetAwaiter().GetResult();
        builder.Services.AddSingleton(translateService);
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<AnimalService>();

        return builder.Build();
	}
}
