using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class ExerciseInputView : ContentPage
{
    private readonly ExerciseInputViewModel exerciseInputViewModel;

    public ExerciseInputView(ExerciseInputViewModel exerciseInputViewModel)
    {
        InitializeComponent();
        BindingContext = exerciseInputViewModel;
        this.exerciseInputViewModel = exerciseInputViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        exerciseInputViewModel.SelectedDate = DateTime.Today;
        exerciseInputViewModel.SelectedTime = DateTime.Now.TimeOfDay;
    }
}
