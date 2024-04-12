using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using TeamTracker.Views;

namespace TeamTracker;

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
                fonts.AddFont("Montserrat-Medium.ttf", "FT-M");
                fonts.AddFont("Montserrat-Regular.ttf", "FT-R");
                fonts.AddFont("Montserrat-Light.ttf", "FT-L");
                fonts.AddFont("Montserrat-Bold.ttf", "FT-B");
                fonts.AddFont("fa-regular.otf", "FA-R");
                fonts.AddFont("fa-solid.otf", "FA-B");
                fonts.AddFont("fa-regular-brands.otf", "FA-BR");
            });
        RegisterRoutes();
        InitializeCustomRenders();
        return builder.Build();
	}

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("DashboradPage", typeof(DashboardPage));
        Routing.RegisterRoute("UsersDetailPage", typeof(UsersDetailPage));
    }

    private static void InitializeCustomRenders()
    {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                if (view is Entry)
                {
                    #if __ANDROID__
                    handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
                    #elif __IOS__
                    #endif
                }
            });
    }
}

