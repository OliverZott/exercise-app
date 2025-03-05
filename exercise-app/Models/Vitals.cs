using System.ComponentModel.DataAnnotations.Schema;

namespace exercise_app.Models;

[Table("Vitals")]
public class Vitals : BaseEntity
{
    public DateTime DateTime { get; set; }
    public int? Diastolic { get; set; }
    public int? Systolic { get; set; }
    public int? HeartRate { get; set; }
    public double? Weight { get; set; }
    public Quality? WellBeing { get; set; }
    public Quality? SleepQuality { get; set; }
}
