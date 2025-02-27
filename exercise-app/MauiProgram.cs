using CommunityToolkit.Maui;
using exercise_app.Services;
using Microsoft.Extensions.Logging;

namespace exercise_app;

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
            })
            .UseMauiCommunityToolkit();

        // Database configuration
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "exercise.db3");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<DatabaseService>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
