using SQLite;

namespace exercise_app.Services;

public class DatabaseService
{

    private SQLiteConnection connection;
    private string dbPath;

    public DatabaseService(string dbPath)
    {
        this.dbPath = dbPath;
        connection = new SQLiteConnection(dbPath);
    }
}