using Domain.Logger.Interface.CQRS;

namespace MongoDb.Commands;

public class InsertCommand<T> : MongoDbConnection, ICommand<T, T> where T : class
{

    public async Task<T> ExecuteAsync(T data, string collectionName)
    {
        var collection = _database.GetCollection<T>(collectionName);
        await collection.InsertOneAsync(data);
        return await Task.FromResult(data);
    }
    
}