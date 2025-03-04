using CommunityToolkit.Mvvm.ComponentModel;
using exercise_app.Models;
using System.Collections.ObjectModel;

namespace exercise_app.ViewModels;

[QueryProperty(nameof(DataObject), "DataObject")]
public partial class DataDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    DataObject dataObject;

    public ObservableCollection<Exercise> ExerciseList => [.. DataObject?.Exercises ?? new List<Exercise>()];
    public ObservableCollection<Vitals> VitalsList => [.. DataObject?.Vitals ?? new List<Vitals>()];

    public DataDetailViewModel()
    {
    }
}