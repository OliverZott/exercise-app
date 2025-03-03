using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using exercise_app.Models;
using exercise_app.Services;
using exercise_app.Views;
using System.Collections.ObjectModel;

namespace exercise_app.ViewModels;

public partial class DataListViewModel : BaseViewModel
{
    private readonly ExerciseService _exerciseService;
    private readonly VitalsService _vitalsService;

    [ObservableProperty] public bool isRefreshing;

    public ObservableCollection<Exercise> ExerciseList { get; private set; } = [];

    public ObservableCollection<Vitals> VitalsList { get; private set; } = [];

    public IList<DataObject> DataObjects { get; private set; } = [];  // Observable for view????


    public DataListViewModel(ExerciseService exerciseService, VitalsService vitalsService)
    {
        _exerciseService = exerciseService;
        _vitalsService = vitalsService;
    }


    [RelayCommand]
    public async Task GetDataList()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            GetExercisesAndVitals();
            GenerateDataObjects();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert($"Error", "Failed to retrieve data", "Ok");  // could abstract that away and user via DI
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    private void GetExercisesAndVitals()
    {
        if (ExerciseList.Count != 0) ExerciseList.Clear();
        if (VitalsList.Count != 0) VitalsList.Clear();
        var exerciseList = _exerciseService.GetExercises();
        var vitalList = _vitalsService.GetVitals();
        foreach (var exercise in exerciseList)
        {
            ExerciseList.Add(exercise);
        }
        foreach (var vital in vitalList)
        {
            VitalsList.Add(vital);
        }
    }

    private void GenerateDataObjects()
    {
        var dataObjectDictionary = new Dictionary<DateTime, DataObject>();

        foreach (var exercise in ExerciseList)
        {
            var date = exercise.DateTime.Date;
            if (!dataObjectDictionary.ContainsKey(date))
            {
                dataObjectDictionary[date] = new DataObject { DateTime = date };
            }
            dataObjectDictionary[date].Exercises.Add(exercise);
        }
        foreach (var vitals in VitalsList)
        {
            var date = vitals.DateTime.Date;
            if (!dataObjectDictionary.ContainsKey(date))
            {
                dataObjectDictionary[date] = new DataObject { DateTime = date };
            }
            dataObjectDictionary[date].Vitals.Add(vitals);
        }
        DataObjects = [.. dataObjectDictionary.Values];
    }

    [RelayCommand]
    public async Task NavigateToExerciseInputView()
    {
        await Shell.Current.GoToAsync(nameof(ExerciseInputView));
    }

    [RelayCommand]
    public async Task NavigateToVitalsInputView()
    {
        await Shell.Current.GoToAsync(nameof(VitalsInputView));
    }

    [RelayCommand]
    public async Task NavigateToDataDetailView()
    {
        await Shell.Current.GoToAsync(nameof(DataDetailView));
    }

}
