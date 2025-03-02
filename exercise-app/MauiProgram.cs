using CommunityToolkit.Maui;
using exercise_app.Services;
using exercise_app.ViewModels;
using exercise_app.ViewModels.Components;
using exercise_app.Views;
using exercise_app.Views.Components;
using Microsoft.Extensions.Logging;
using ListView = Microsoft.Maui.Controls.ListView;

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

        builder.Services.AddSingletonWithShellRoute<ListView, ListViewModel>(nameof(ListView));
        builder.Services.AddSingletonWithShellRoute<ExerciseTile, ExerciseTileViewModel>(nameof(ExerciseTile));
        builder.Services.AddSingletonWithShellRoute<ExerciseInputView, ExerciseInputViewModel>(nameof(ExerciseInputView));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
