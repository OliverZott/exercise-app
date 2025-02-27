using CommunityToolkit.Mvvm.ComponentModel;

namespace exercise_app.ViewModels;

public partial class BaseViewModel : ObservableValidator
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;
}
