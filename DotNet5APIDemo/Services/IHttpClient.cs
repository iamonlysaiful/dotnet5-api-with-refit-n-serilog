using Refit;
using System.Threading.Tasks;

namespace DotNet5APIDemo.Services
{
    public interface IHttpClient
    {
        [Get("/{path}?page={page}")]
        Task<string> GetUserList(string path,int page);

        [Get("/{path}/{id}")]
        Task<string> GetSingleUser(string path, int id);
    }
}
