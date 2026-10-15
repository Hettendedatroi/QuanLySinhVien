using System.Threading;
using System.Threading.Tasks;
using webdemo.Models;

namespace webdemo.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string username, string password, CancellationToken cancellationToken);
        Task<UserAccount> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
    }
}
