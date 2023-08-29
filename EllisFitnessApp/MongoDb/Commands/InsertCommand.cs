using Domain.Logger.Interface.CQRS;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace MongoDb.Commands;

public class InsertCommand<T> : MongoDbConnection, ICommand<T, T> where T : class
{
    IMongoCollection<T> _collection;

    public InsertCommand(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName)
    {
        _collection = _database.GetCollection<T>(collectionName);
    }
    

    public async Task<T> ExecuteAsync(T data)
    {
        await _collection.InsertOneAsync(data);
        return await Task.FromResult(data);
    }
    
}