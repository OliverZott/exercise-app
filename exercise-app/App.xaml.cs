using Microsoft.Extensions.Logging;

namespace exercise_app;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override void OnStart()
    {
        base.OnStart();
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogUnhandledException(ex);
            }
        };
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            LogUnhandledException(e.Exception);
            e.SetObserved();
        };
    }

    private void LogUnhandledException(Exception ex)
    {
        var mauiContext = Handler?.MauiContext;
        if (mauiContext != null)
        {
            var logger = mauiContext.Services.GetService<ILogger<App>>();
            logger?.LogError(ex, "Unhandled exception occurred.");
        }
    }
}