using Microsoft.AspNetCore.Identity;
using SmartTaskManager.Models;

namespace SmartTaskManager.Helpers
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<UserMaster> _hasher = new();

        public static string Hash(UserMaster user, string plainPassword)
        {
            return _hasher.HashPassword(user, plainPassword);
        }

        public static bool Verify(UserMaster user, string hashedPassword, string plainPassword)
        {
            try
            {
                var result = _hasher.VerifyHashedPassword(user, hashedPassword, plainPassword);
                return result == PasswordVerificationResult.Success
                    || result == PasswordVerificationResult.SuccessRehashNeeded;
            }
            catch (FormatException)
            {
                // Stored value isn't a valid PasswordHasher hash — almost certainly
                // a legacy plaintext password from before hashing was added.
                return false;
            }
        }

        // Used once, as a fallback, to detect and migrate pre-hashing accounts.
        public static bool IsLegacyPlainTextMatch(string storedValue, string plainPassword)
        {
            return storedValue == plainPassword;
        }
    }
}