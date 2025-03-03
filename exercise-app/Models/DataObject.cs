namespace exercise_app.Models;

public class DataObject
{
    public DateTime DateTime { get; set; }
    public IList<Exercise> Exercises { get; set; } = [];
    public IList<Vitals> Vitals { get; set; } = [];
}
