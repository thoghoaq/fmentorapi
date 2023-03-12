using System.Security.Cryptography;
using System.Text;

namespace FMentorAPI.BusinessLogic.Utils;

public static class PasswordHashUtil
{
    public static string HashPassword(string password, string salt = "default")
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = $"{password}{salt}";
        var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
        var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string hashedPassword, string salt = "default")
    {
        var computedHash = HashPassword(password, salt);
        return hashedPassword == computedHash;
    }
    
    public static string GenerateSalt()
    {
        var bytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        return Convert.ToBase64String(bytes);
    }
}