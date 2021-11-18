using DotNet5APIDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNet5APIDemo.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers(int page);
        Task<User> GetUserById(int id);
    }
}
