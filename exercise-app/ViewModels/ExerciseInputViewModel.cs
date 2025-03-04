using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;
using exercise_app.Services;

namespace exercise_app.ViewModels;

public partial class ExerciseInputViewModel : BaseViewModel
{
    private readonly ExerciseService _exerciseService;

    [ObservableProperty]
    public TimeSpan duration;

    [ObservableProperty]
    public string? notes;

    [ObservableProperty]
    public ExerciseType selectedExerciseType;

    public List<ExerciseType> ExerciseTypes { get; } = Enum.GetValues<ExerciseType>().Cast<ExerciseType>().ToList();

    [ObservableProperty] Exercise selectedExercise;

    [ObservableProperty] public DateTime selectedDate;
    [ObservableProperty] public TimeSpan selectedTime;
    [ObservableProperty] public bool isRefreshing;

    private bool inEditMode = false;

    public ExerciseInputViewModel(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
        SelectedDate = DateTime.Today;
        SelectedTime = DateTime.Now.TimeOfDay;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var value) || value == null) return;
        var selectedExerciseId = Convert.ToInt32(value);
        SelectedExercise = _exerciseService.GetExercise(selectedExerciseId);
        InitializeForm();
    }

    private void InitializeForm()
    {
        if (SelectedExercise == null) return;

        SelectedDate = SelectedExercise.DateTime.Date;
        SelectedTime = SelectedExercise.DateTime.TimeOfDay;
        Duration = TimeSpan.FromSeconds(SelectedExercise.DurationSeconds);
        Notes = SelectedExercise.Notes;
        SelectedExerciseType = SelectedExercise.ExerciseType;
        inEditMode = true;
    }

    [RelayCommand]
    public async Task SaveOrEditExercise()
    {
        if (inEditMode)
        {
            await UpdateExercise();
        }
        else
        {
            await SaveExercise();
        }
    }

    [RelayCommand]
    public async Task UpdateExercise()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await Shell.Current.DisplayAlert("Invalid Data", string.Join("\n", GetErrors().Select(e => e.ErrorMessage)), "Ok"); return;
        }

        var newDateTime = new DateTime(
            SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
            SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);

        SelectedExercise!.DateTime = newDateTime;
        SelectedExercise.DurationSeconds = (int)Duration.TotalSeconds;
        SelectedExercise.Notes = Notes;
        SelectedExercise.ExerciseType = SelectedExerciseType;

        _exerciseService.UpdateExercise(SelectedExercise);

        ClearForm();
        await NavigateBack();
    }

    [RelayCommand]
    public async Task SaveExercise()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await Shell.Current.DisplayAlert("Invalid Data", string.Join("\n", GetErrors().Select(e => e.ErrorMessage)), "Ok"); return;
        }

        var currentDateTime = new DateTime(
            SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
            SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);

        var exercise = new Exercise()
        {
            DateTime = currentDateTime,
            DurationSeconds = (int)Duration.TotalSeconds,
            Notes = Notes,
            ExerciseType = SelectedExerciseType,
        };

        _exerciseService.AddExercise(exercise);
        await Shell.Current.DisplayAlert("Info", _exerciseService.StatusMessage, "Ok");

        ClearForm();
        await NavigateBack();
    }

    [RelayCommand]
    public async Task NavigateBack()
    {
        if (inEditMode)
        {
            inEditMode = false;
            await Shell.Current.GoToAsync("..", new Dictionary<string, object>
            {
                {nameof(Exercise), SelectedExercise! }
            });
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    private void ClearForm()
    {
        SelectedDate = DateTime.Today;
        SelectedTime = DateTime.Now.TimeOfDay;
        SelectedExercise = null;
        Notes = null;
        Duration = TimeSpan.Zero;
        SelectedExerciseType = ExerciseType.Mountainbike; // Default value
    }
}