using System.ComponentModel.DataAnnotations.Schema;

namespace exercise_app.Models;

[Table("Exercises")]
public class Exercise : BaseEntity
{
    public DateTime DateTime { get; set; }

    [NotMapped]
    public TimeSpan Duration
    {
        get => TimeSpan.FromSeconds(DurationSeconds);
        set => DurationSeconds = (int)value.TotalSeconds;
    }

    public int DurationSeconds { get; set; } // Store duration as total seconds

    public ExerciseType ExerciseType { get; set; }

    public string? Notes { get; set; }
    public int Distance { get; set; }
    public int Elevation { get; set; }
    public int AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
}
