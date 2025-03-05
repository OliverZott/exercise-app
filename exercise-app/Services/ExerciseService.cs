using exercise_app.Models;
using SQLite;

namespace exercise_app.Services;

public class ExerciseService
{
    private SQLiteConnection connection;
    private readonly string dbPath;

    public string StatusMessage { get; set; }

    public ExerciseService(string dbPath)
    {
        this.dbPath = dbPath;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        connection = new SQLiteConnection(dbPath);
        connection.CreateTable<Exercise>();
    }

    public List<Exercise> GetExercises()
    {
        try
        {
            return connection.Table<Exercise>().OrderByDescending(e => e.DateTime).ToList();
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to retrieve exercise list";
            throw;
        }
    }

    public Exercise GetExercise(int id)
    {
        try
        {
            return connection.Table<Exercise>().FirstOrDefault(e => e.Id == id);
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to retrieve exercise";
            throw;
        }
    }

    public void AddExercise(Exercise exercise)
    {
        try
        {
            connection.Insert(exercise);
            StatusMessage = "Exercise added";
        }
        catch (Exception e)
        {
            StatusMessage = $"Failed to add exercise on {exercise.DateTime}";
            throw;
        }
    }

    public void UpdateExercise(Exercise exercise)
    {
        try
        {
            connection.Update(exercise);
            StatusMessage = "Exercise updated";
        }
        catch (Exception e)
        {
            StatusMessage = $"Failed to update exercise on {exercise.DateTime}";
            throw;
        }
    }

    public void DeleteExercise(int id)
    {
        try
        {
            var exercise = GetExercise(id);
            if (exercise != null)
            {
                connection.Delete(exercise);
                StatusMessage = "Exercise deleted";
            }
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to delete exercise";
            throw;
        }
    }
}