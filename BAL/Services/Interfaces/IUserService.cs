using DAL.Models;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserCredentialsValidAsync(string username, string password);
        User GetUserByUsername(string username);
    }
}
