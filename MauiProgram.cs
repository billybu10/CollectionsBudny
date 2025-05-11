using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CollectionsBudny;

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

			
#if DEBUG
		builder.Logging.AddDebug();
		Debug.WriteLine("┌────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────");
		Debug.WriteLine("|Application data path: " + FileSystem.AppDataDirectory);
		Debug.WriteLine("└────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────");
		
#endif

        return builder.Build();
	}
}
