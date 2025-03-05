using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;
using exercise_app.Services;
using System.ComponentModel.DataAnnotations;

namespace exercise_app.ViewModels;

public partial class VitalsInputViewModel : BaseViewModel
{
    private readonly VitalsService _vitalsService;

    [ObservableProperty]
    [Range(50, 250, ErrorMessage = "Systolic value must be between 50 and 250")]
    public int? systolic;

    [ObservableProperty]
    [Range(50, 150, ErrorMessage = "Diastolic value must be between 50 and 150")]
    public int? diastolic;

    [ObservableProperty]
    [Range(30, 230, ErrorMessage = "Heart rate must be between 30 and 230")]
    public int? heartRate;

    [ObservableProperty]
    [Range(50, 150, ErrorMessage = "Weight rate must be between 50 and 150")]
    public double? weight;

    [ObservableProperty] public DateTime selectedDate;
    [ObservableProperty] public TimeSpan selectedTime;
    [ObservableProperty] public bool isRefreshing;

    [ObservableProperty] Vitals? selectedVitals;

    private bool inEditMode = false;


    public VitalsInputViewModel(VitalsService vitalsService)
    {
        _vitalsService = vitalsService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var value) || value == null) return;
        var selectedVitalsId = Convert.ToInt32(value);
        SelectedVitals = _vitalsService.GetVitals(selectedVitalsId);
        InitializeForm();
    }

    private void InitializeForm()
    {
        if (SelectedVitals == null) return;

        SelectedDate = SelectedVitals.DateTime.Date;
        SelectedTime = SelectedVitals.DateTime.TimeOfDay;
        Diastolic = SelectedVitals.Diastolic;
        Systolic = SelectedVitals.Systolic;
        HeartRate = SelectedVitals.HeartRate;
        Weight = SelectedVitals.Weight;
        inEditMode = true;
    }

    [RelayCommand]
    public async Task SaveOrEditVitals()
    {
        if (inEditMode)
        {
            await UpdateVitals();
        }
        else
        {
            await SaveVitals();
        }
    }


    [RelayCommand]
    public async Task UpdateVitals()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await Shell.Current.DisplayAlert("Invalid Data", string.Join("\n", GetErrors().Select(e => e.ErrorMessage)), "Ok"); return;
        }

        var newDateTime = new DateTime(
            SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
            SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);

        SelectedVitals!.DateTime = newDateTime;

        _vitalsService.UpdateVitals(SelectedVitals);

        ClearForm();
        await NavigateBack();
    }



    [RelayCommand]
    public async Task SaveVitals()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await Shell.Current.DisplayAlert("Invalid Data", string.Join("\n", GetErrors().Select(e => e.ErrorMessage)), "Ok"); return;
        }

        var currentDateTime = new DateTime(
            SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
            SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);

        var vitals = new Vitals()
        {
            DateTime = currentDateTime,
            Systolic = Systolic,
            Diastolic = Diastolic,
            HeartRate = HeartRate,
            Weight = Weight
        };

        _vitalsService.AddVitals(vitals);
        await Shell.Current.DisplayAlert("Info", _vitalsService.StatusMessage, "Ok");

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
                {nameof(Vitals), SelectedVitals! }
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