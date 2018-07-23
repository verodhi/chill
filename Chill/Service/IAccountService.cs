using System.Threading.Tasks;
using Chill.Model;

namespace Chill.Service
{
    public interface IAccountService
    {
        Task<AccountResultVM> Deposit(AccountDepositWithdrawDTO accountDepositWithdrawDTO);
        AccountResultVM Get(int accountNumber);
        Task<AccountResultVM> Withdraw(AccountDepositWithdrawDTO accountDepositWithdrawDTO);
    }
}