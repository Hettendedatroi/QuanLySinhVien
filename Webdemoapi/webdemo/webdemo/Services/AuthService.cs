using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webdemo.DBcontext;
using webdemo.Models;

namespace webdemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;

        public AuthService(AppDbContext dbContext, IPasswordHasher<UserAccount> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(string username, string password, CancellationToken cancellationToken)
        {
            username = username.Trim();
            var normalizedUsername = username.ToUpperInvariant();
            var exists = await _dbContext.UserAccounts
                .AnyAsync(user => user.NormalizedUsername == normalizedUsername, cancellationToken);

            if (exists)
            {
                return false;
            }

            var account = new UserAccount
            {
                Username = username,
                NormalizedUsername = normalizedUsername
            };
            account.PasswordHash = _passwordHasher.HashPassword(account, password);

            _dbContext.UserAccounts.Add(account);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<UserAccount> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
        {
            var normalizedUsername = username.Trim().ToUpperInvariant();
            var account = await _dbContext.UserAccounts
                .SingleOrDefaultAsync(user => user.NormalizedUsername == normalizedUsername, cancellationToken);

            if (account == null)
            {
                return null;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                account.PasswordHash = _passwordHasher.HashPassword(account, password);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return account;
        }
    }
}
