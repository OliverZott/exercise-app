using System.ComponentModel.DataAnnotations.Schema;

namespace exercise_app.Models;

[Table("Exercises")]
public class Exercise : BaseEntity
{
    public DateTime DateTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Notes { get; set; }
    public ExerciseType ExerciseType { get; set; }
}
