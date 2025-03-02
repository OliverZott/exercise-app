using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;
using exercise_app.Services;
using exercise_app.Views;
using System.Collections.ObjectModel;

namespace exercise_app.ViewModels;

public partial class ListViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Exercise>? ExerciseList { get; private set; } = [];

    [ObservableProperty] public bool isRefreshing;

    public ListViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [RelayCommand]
    public async Task GetExerciseList()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            if (ExerciseList?.Count != 0) ExerciseList.Clear();
            var exerciseList = _databaseService.GetExercises();
            foreach (var bloodPressure in exerciseList)
            {
                ExerciseList.Add(bloodPressure);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert($"Error", "Failed to retrieve list of exercises values", "Ok");  // could abstract that away and user via DI
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task NavigateToInputView()
    {
        await Shell.Current.GoToAsync(nameof(ExerciseInputView));
    }

    [RelayCommand]
    public async Task NavigateToExerciseDetailView()
    {
        throw new NotImplementedException();
    }

}
