using CommunityToolkit.Maui;
using exercise_app.Services;
using exercise_app.ViewModels;
using exercise_app.ViewModels.Components;
using exercise_app.Views;
using exercise_app.Views.Components;
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
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ExerciseService>(s, dbPath));
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<VitalsService>(s, dbPath));


        builder.Services.AddSingletonWithShellRoute<DataListView, DataListViewModel>(nameof(DataListView));
        builder.Services.AddSingletonWithShellRoute<DataDetailView, DataDetailViewModel>(nameof(DataDetailView));
        builder.Services.AddSingletonWithShellRoute<DataObjectTile, ExerciseTileViewModel>(nameof(DataObjectTile));
        builder.Services.AddSingletonWithShellRoute<ExerciseInputView, ExerciseInputViewModel>(nameof(ExerciseInputView));
        builder.Services.AddSingletonWithShellRoute<VitalsInputView, VitalsInputViewModel>(nameof(VitalsInputView));
        builder.Services.AddSingletonWithShellRoute<ExerciseTile, ExerciseTileViewModel>(nameof(ExerciseTile));
        builder.Services.AddSingletonWithShellRoute<VitalsTile, VitalsTileViewModel>(nameof(VitalsTile));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
