using Domain.Logger.Interface.CQRS;

namespace MongoDb.Commands;

public class InsertCommand<T> : MongoDbConnection, ICommand<T, T> where T : class
{

    public async Task<T> ExecuteAsync(T data, string collectionName)
    {
        var collection = GetCollection<T>(collectionName);
        collection.InsertOneAsync(data);
        return await Task.FromResult(data);
    }
    
}