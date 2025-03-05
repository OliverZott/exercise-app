using exercise_app.Models;
using System.Globalization;

namespace exercise_app.Utils;

public class ExerciseTypeDisplayConverter : IValueConverter
{
    private static readonly Dictionary<ExerciseType, string> ExerciseTypeDisplayNames = new()
    {
        { ExerciseType.Mountainbike, "Mountainbike" },
        { ExerciseType.Downhill, "Downhill" },
        { ExerciseType.WeightLifting, "Weight Lifting" },
        { ExerciseType.TrailRunning, "Trail Running" },
        { ExerciseType.NordicSkiing, "Nordic Skiing" },
        { ExerciseType.AlpineSkiing, "Alpine Skiing" },
        { ExerciseType.SkiTouring, "Ski Touring" },
        { ExerciseType.Snowboarding, "Snowboarding" },
        { ExerciseType.Hiking, "Hiking" }
    };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<ExerciseType> exerciseTypes)
        {
            return exerciseTypes
                .Select(e => ExerciseTypeDisplayNames.TryGetValue(e, out var displayName) ? displayName : e.ToString())
                .ToList();
        }

        if (value is ExerciseType exerciseType && ExerciseTypeDisplayNames.TryGetValue(exerciseType, out var singleDisplayName))
        {
            return singleDisplayName;
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
