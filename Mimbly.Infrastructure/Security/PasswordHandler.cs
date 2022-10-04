namespace Mimbly.Infrastructure.Security;

using BC = BCrypt.Net.BCrypt;

public static class PasswordHandler
{
    public static string HashPassword(string password)
    {
        var salt = BC.GenerateSalt(10);
        return BC.HashPassword(password, salt);
    }

    public static bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        return BC.Verify(providedPassword, hashedPassword);
    }
}