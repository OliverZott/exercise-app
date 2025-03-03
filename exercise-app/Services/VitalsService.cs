using exercise_app.Models;
using SQLite;

namespace exercise_app.Services;

public class VitalsService
{
    private SQLiteConnection connection;
    private string dbPath;

    public string StatusMessage { get; set; }

    public VitalsService(string dbPath)
    {
        this.dbPath = dbPath;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        connection = new SQLiteConnection(dbPath);
        connection.CreateTable<Vitals>();
    }

    public List<Vitals> GetVitals()
    {
        try
        {
            return connection.Table<Vitals>().OrderByDescending(v => v.DateTime).ToList();
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to retrieve vitals list";
            throw;
        }
    }

    public Vitals GetVitals(int id)
    {
        try
        {
            return connection.Table<Vitals>().FirstOrDefault(v => v.Id == id);
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to retrieve vitals";
            throw;
        }
    }

    public void AddVitals(Vitals vitals)
    {
        try
        {
            connection.Insert(vitals);
            StatusMessage = "Vitals added";
        }
        catch (Exception e)
        {
            StatusMessage = $"Failed to add vitals on {vitals.DateTime}";
            throw;
        }
    }

    public void UpdateVitals(Vitals vitals)
    {
        try
        {
            connection.Update(vitals);
            StatusMessage = "Vitals updated";
        }
        catch (Exception e)
        {
            StatusMessage = $"Failed to update vitals on {vitals.DateTime}";
            throw;
        }
    }

    public void DeleteVitals(int id)
    {
        try
        {
            var vitals = GetVitals(id);
            if (vitals != null)
            {
                connection.Delete(vitals);
                StatusMessage = "Vitals deleted";
            }
        }
        catch (Exception e)
        {
            StatusMessage = "Failed to delete vitals";
            throw;
        }
    }
}