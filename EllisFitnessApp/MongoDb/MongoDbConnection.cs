using Domain.Models.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDb;

public abstract class MongoDbConnection
{
    private readonly string _connectionString;
    protected readonly MongoClient _mongoClient;
    protected readonly IMongoDatabase _database;

    protected MongoDbConnection()
    {
        _connectionString = "defaultConnectionString";
    }


    protected MongoDbConnection(IOptions<MongoConfig> config)
    {
        _mongoClient = new MongoClient(config.Value.ConnectionString);
        _database = _mongoClient.GetDatabase(config.Value.DatabaseName);
    }

}