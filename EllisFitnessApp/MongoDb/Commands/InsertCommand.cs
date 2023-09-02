using Domain.Logger.Interface.CQRS;
using Domain.Models.Config;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;

namespace MongoDb.Commands;

public class InsertCommand<T> : MongoDbConnection, ICommand<T, T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public InsertCommand(IOptions<MongoConfig> config, string collectionName) : base(config)
    {
        _collection = _database.GetCollection<T>(collectionName);
    }
    
    public async Task<T> ExecuteAsync(T data)
    {
        await _collection.InsertOneAsync(data);
        return await Task.FromResult(data);
    }
    
}