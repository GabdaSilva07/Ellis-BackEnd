namespace Domain.Models.AuthResponse;

public class AuthResponse
{
    public string IdToken { get; set; }  // JWT token
    public string Email { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }  // Token expiry time in seconds
    public string LocalId { get; set; }  // User's unique Firebase ID
}