using System.Threading.Tasks;

namespace TechVocabulary.API.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string username, string password);
    }
}
