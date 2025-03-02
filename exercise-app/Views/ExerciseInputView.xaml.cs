using exercise_app.ViewModels;

namespace exercise_app.Views;

public partial class ExerciseInputView : ContentPage
{
    public ExerciseInputView(ExerciseInputViewModel exerciseInputViewModel)
    {
        InitializeComponent();
        BindingContext = exerciseInputViewModel;
    }
}