using MongoDB.Driver;

namespace MongoDb;

public abstract class MongoDbConnection
{
    private readonly string _connectionString;
    protected readonly MongoClient _mongoClient;
    protected readonly IMongoDatabase _database;

    public MongoDbConnection()
    {
        _connectionString = "defaultConnectionString";
    }

    public MongoDbConnection(string connectionString, string databaseName)
    {
        _mongoClient = new MongoClient(connectionString);
        _database = _mongoClient.GetDatabase(databaseName);

    }
}