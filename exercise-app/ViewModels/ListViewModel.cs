using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;

namespace exercise_app.ViewModels;

public partial class ListViewModel : BaseViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ExerciseList))]
    public List<Exercise>? exerciseList;

    [ObservableProperty] public bool isRefreshing;


    [RelayCommand]
    public async Task GetExerciseList()
    {
        throw new NotImplementedException();
    }

}
