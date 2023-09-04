namespace Domain.Models.UserRoles;

[Flags]
public enum UserRole
{
    None = 0,
    Admin = 1,
    FreeUser = 2,
    Trainee = 4,
    Trainer = 8,
}