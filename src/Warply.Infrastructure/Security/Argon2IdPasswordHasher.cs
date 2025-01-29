using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using Warply.Domain.Services.Security;

namespace Warply.Infrastructure.Security;

internal class Argon2IdPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltLength);

        var passwordBytes = Encoding.UTF8.GetBytes(password);

        using var argon2 = new Argon2id(passwordBytes)
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize
        };

        var hash = argon2.GetBytes(HashLength);

        var combinedBytes = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, combinedBytes, salt.Length, hash.Length);

        return Convert.ToBase64String(combinedBytes);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        try
        {
            var combinedBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltLength];
            var hash = new byte[HashLength];
            Buffer.BlockCopy(combinedBytes, 0, salt, 0, SaltLength);
            Buffer.BlockCopy(combinedBytes, SaltLength, hash, 0, HashLength);

            var passwordBytes = Encoding.UTF8.GetBytes(providedPassword);

            using var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                Iterations = Iterations,
                MemorySize = MemorySize
            };

            var newHash = argon2.GetBytes(HashLength);

            return CryptographicOperations.FixedTimeEquals(newHash, hash);
        }
        catch
        {
            return false;
        }
    }

    #region Variables

    private const int DegreeOfParallelism = 8; // Threads Numbers
    private const int Iterations = 4; // Iterations Numbers
    private const int MemorySize = 256; // 1Gb in KiB
    private const int HashLength = 32; // Hash size in bytes
    private const int SaltLength = 16; // Salt size in bytes

    #endregion
}