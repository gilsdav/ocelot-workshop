using System.Threading.Tasks;

namespace PizzaGraphQL.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(string user, string password);
        Task<string> Logout();
    }
}