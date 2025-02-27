using System.ComponentModel.DataAnnotations.Schema;

namespace exercise_app.Models;

[Table("ExerciseTypes")]
public class ExerciseType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}