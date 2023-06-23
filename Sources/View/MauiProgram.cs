using Microsoft.Extensions.Logging;
using Model;
using StubLib;
using View.Page;
using ViewModels;
using CommunityToolkit.Maui;
using View.ModelViewPage;
namespace View;

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
		
			 builder.Services.AddSingleton<IDataManager, StubData>().AddSingleton<ChampionManagerVM>();
			 builder.Services.AddTransient<ChampionsView>();
		builder.Services.AddTransient<ChampionsViewM>();
        builder.Services.AddTransient<ChampionDetailViewM>();
#if DEBUG
        builder.Logging.AddDebug();

#endif

		return builder.Build();
	}
}

