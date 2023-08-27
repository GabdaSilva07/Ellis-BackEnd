namespace MongoDb;

public class MongoDbConnection
{
    private readonly string _connectionString;

    public MongoDbConnection()
    {
    }

    public MongoDbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
}