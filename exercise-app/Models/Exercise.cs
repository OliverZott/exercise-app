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
    public string? Notes { get; set; }
    public string? ExerciseType { get; set; }

    // Foreign key for ExerciseType
    //public int ExerciseTypeId { get; set; }

    // NOT WORKING in SQLite !!!
    //[ForeignKey("ExerciseTypeId")]
    //public ExerciseType ExerciseType { get; set; }
}
