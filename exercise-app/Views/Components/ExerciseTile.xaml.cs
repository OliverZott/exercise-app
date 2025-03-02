using exercise_app.Models;
using exercise_app.ViewModels.Components;

namespace exercise_app.Views.Components;

public partial class ExerciseTile : ContentView
{
    public ExerciseTile()
    {
    }

    public ExerciseTile(ExerciseTileViewModel exerciseTileViewModel, Exercise exercise)
    {
        InitializeComponent();
        BindingContext = exercise;
        //BindingContext = exerciseTileViewModel;
    }
}