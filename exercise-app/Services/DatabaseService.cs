using exercise_app.Models;
using SQLite;

namespace exercise_app.Services;

public class DatabaseService
{

    private SQLiteConnection connection;
    private string dbPath;

    public string StatusMessage { get; set; }

    public DatabaseService(string dbPath)
    {
        this.dbPath = dbPath;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        connection = new SQLiteConnection(dbPath);
        // Create the tables if they do not exist
        connection.CreateTable<Exercise>();
        connection.CreateTable<ExerciseType>();
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

    public void UpdateBloodPressure(Exercise selectedExercise)
    {
        throw new NotImplementedException();
    }
}