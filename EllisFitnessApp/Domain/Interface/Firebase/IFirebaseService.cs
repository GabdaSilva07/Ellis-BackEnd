namespace Domain.Interface.Firebase;

public interface IFirebaseService
{
    Task<string> GetAccessTokenAsync();
    Task<string> CreateCustomTokenAsync(string userId);
}