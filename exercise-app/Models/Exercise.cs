using System.ComponentModel.DataAnnotations.Schema;

namespace exercise_app.Models;

[Table("Exercises")]
public class Exercise : BaseEntity
{
    public DateTime DateTime { get; set; }
    public int? Diastolic { get; set; }
    public int? Systolic { get; set; }
    public int? HeartRate { get; set; }
    public int? Weight { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public string? Notes { get; set; }
}
