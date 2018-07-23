using System.Threading.Tasks;
using Chill.Model;

namespace Chill.Repository
{
    public interface IAccountRepository
    {
        Account Get(int accountNumber);
        Task<bool> SaveAllAsync();
    }
}