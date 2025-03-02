using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;
using exercise_app.Services;
using System.ComponentModel.DataAnnotations;

namespace exercise_app.ViewModels;

public partial class ExerciseInputViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    [Required(ErrorMessage = "Systolic value is required")]
    [Range(50, 250, ErrorMessage = "Systolic value must be between 50 and 250")]
    public int? systolic;

    [ObservableProperty]
    [Required(ErrorMessage = "Diastolic value is required")]
    [Range(50, 150, ErrorMessage = "Diastolic value must be between 50 and 150")]
    public int? diastolic;

    [ObservableProperty]
    [Required(ErrorMessage = "Heart rate value is required")]
    [Range(30, 230, ErrorMessage = "Heart rate must be between 30 and 230")]
    public int? heartRate;

    [ObservableProperty]
    [Required(ErrorMessage = "Weight rate value is required")]
    [Range(50, 150, ErrorMessage = "Weight rate must be between 50 and 150")]
    public int? weight;

    [ObservableProperty] public DateTime selectedDate;
    [ObservableProperty] public TimeSpan selectedTime;
    [ObservableProperty] public bool isRefreshing;

    [ObservableProperty] Exercise? selectedExercise = null;

    private bool inEditMode = false;


    public ExerciseInputViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        SelectedDate = DateTime.Today;
        SelectedTime = DateTime.Now.TimeOfDay;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var value) || value == null) return;
        var selectedExerciseId = Convert.ToInt32(value);
        SelectedExercise = _databaseService.GetExercise(selectedExerciseId);
        InitializeForm();
    }

    private void InitializeForm()
    {
        if (SelectedExercise == null) return;

        Systolic = SelectedExercise.Systolic;
        Diastolic = SelectedExercise.Diastolic;
        HeartRate = SelectedExercise.HeartRate;
        SelectedDate = SelectedExercise.DateTime.Date;
        SelectedTime = SelectedExercise.DateTime.TimeOfDay;
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
        SelectedExercise.Systolic = Systolic;
        SelectedExercise.Diastolic = Diastolic;
        SelectedExercise.HeartRate = HeartRate;

        _databaseService.UpdateBloodPressure(SelectedExercise);

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
            Diastolic = Diastolic,
            Systolic = Systolic,
            HeartRate = HeartRate,
            Weight = Weight

        };

        _databaseService.AddExercise(exercise);
        await Shell.Current.DisplayAlert("Info", _databaseService.StatusMessage, "Ok");

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
        Systolic = null;
        Diastolic = null;
        HeartRate = null;
        Weight = null;
    }

}